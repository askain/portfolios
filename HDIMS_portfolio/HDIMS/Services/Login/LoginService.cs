using System;
using System.Collections;
using System.Collections.Generic;
using Common.Logging;
using HDIMS.Models;
using HDIMS.Models.Domain.Login;

namespace HDIMS.Services.Login
{
    public class LoginService : ILoginService
    {
        #region == Global Variable ==
        private static readonly ILog log = LogManager.GetLogger(typeof(LoginService));
        #endregion

        #region == GetEmployee() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.07.07
        /// 3. 설  명 : 로그인시 사용자 확인
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<LoginModel> GetEmployee(Hashtable param)
        {
            IList<LoginModel> list = new List<LoginModel>();
            LoginModel retModel = new LoginModel();

            string sEmpNo = param["EmpNo"].ToString();      // 사번
            string sPassWD = param["PassWD"].ToString(); ;  // 비밀번호

            try
            {
                IList<LoginModel> empNoChkList = new List<LoginModel>();

                empNoChkList = DaoFactory.Instance.QueryForList<LoginModel>("Login.ChkEmpNo", param);

                if (empNoChkList == null || empNoChkList.Count == 0)
                {
                    retModel.RETVAL = -1;           // 존재하지 않는 사원
                }
                else
                {
                    retModel = empNoChkList[0];
                    if (sEmpNo == retModel.EMPNO)    // 사번 체크
                    {
                        list = DaoFactory.Instance.QueryForList<LoginModel>("Login.ChkPassWD", param);

                        if (list == null || list.Count == 0)
                        {
                            retModel.RETVAL = 0;        // 암호가 틀림
                        }
                        else
                        {
                            retModel = list[0];
                            if (retModel.PASSWD == sPassWD)
                            {
                                retModel.RETVAL = 1;    // 유효한 사번
                            }
                        }
                    }
                }
                list.Add(retModel);
            }
            catch (Exception e)
            {
                log.Error(e);
            }

            return list;
        }
        #endregion

        #region == SetLoginInfo ==
        ///<summary>
        ///1. 작성자 : 임장식
        ///2. 작성일 : 2011.08.22
        ///3. 설  명 : 로그인 정보 입력
        ///4. 이  력
        ///</summary>
        ///<returns></return>
        public void InsertLoginInfo(Hashtable param)
        {
            try
            {
                DaoFactory.Instance.BeginTransaction();
                DaoFactory.Instance.Insert("Login.InsertLoginInfo", param);
                DaoFactory.Instance.CommitTransaction();
            }
            catch(Exception e)
            {
                DaoFactory.Instance.RollBackTransaction();
                throw e;
            }
        }
        #endregion

        #region == UpdateLoginInfo ==
        ///<summary>
        ///1. 작성자 : 임장식
        ///2. 작성일 : 2011.08.22
        ///3. 설  명 : 로그인 정보 수정(로그아웃)
        ///4. 이  력
        ///</summary>
        ///<returns></return>
        public void UpdateLoginInfo(Hashtable param)
        {
            try
            {
                DaoFactory.Instance.BeginTransaction();
                DaoFactory.Instance.Update("Login.UpdateLoginInfo", param);
                DaoFactory.Instance.CommitTransaction();
            }
            catch (Exception e)
            {
                DaoFactory.Instance.RollBackTransaction();
                throw e;
            }
        }
        #endregion
    }
}