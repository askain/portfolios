using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;

namespace HDIMS.Utils.Login
{
    public class EmpData
    {
        #region == GetEmpData() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.07.27
        /// 3. 설  명 : 사용자 정보 추출
        /// 4. 이  력 :
        /// </summary>
        /// <param name="iIdx">0: 사번 , 1: 이름 , 2: 비번, 3: 권한코드, 4: 권한명, 5: 로그인일시, 6: 관리댐 코드, 7: LONGITUDE, 8:LATITUDE, 9:RANGE, 10:관리단코드, 11:댐구분, 12:부서, 13:기본페이지, 14:이메일, 15:연락처HPTEL</param>
        /// <return></return>
        public string GetEmpData(int iIdx)
        {
            string sData = string.Empty;
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                FormsAuthenticationTicket ticket = id.Ticket;

                string userData = ticket.UserData;
                string[] arrUserData = userData.Split(':');
                if (iIdx < arrUserData.Length)
                    sData = arrUserData[iIdx].ToString();
            }

            return sData;
        }
        #endregion


        public bool IsThisUserAbleToChangeData(string damcd) {
            string[] authenticatedAuthCd = new string[2];
            authenticatedAuthCd[0] = "01";  //마스터
            authenticatedAuthCd[1] = "03";  //관리자

            EmpData emp = new EmpData();
            if (emp.GetEmpData(3).Equals(authenticatedAuthCd[0]))   //allmighty
            {
                return true;
            } else if (emp.GetEmpData(3).Equals(authenticatedAuthCd[1]) && emp.GetEmpData(6).IndexOf(damcd) != -1)     //관리자이며, 담당댐을 수정중인경우
            {
                    return true;
            }
            return false;
        }

        //public Hashtable GetCurrentUsers()
        //{
        //    return (Hashtable)HttpContext.Current.Application["current_users"];
        //}

        public IList<Hashtable> GetCurrentUsersWithDamNm()
        {
            return  SessionUtil.GetSessionUsers();
        }
    }
}