using System;
using System.Collections;
using System.Collections.Generic;
using Common.Logging;
using HDIMS.Models;
using HDIMS.Models.Domain.ManAuthMng;

namespace HDIMS.Services.ManAuthMng
{
    public class AuthService : IAuthService
    {
        #region == Global Variable ==
        private static readonly ILog log = LogManager.GetLogger(typeof(AuthService));
        #endregion

        #region == Select() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08.10
        /// 3. 설  명 : 권한관리 조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<AuthModel> Select()
        {
            IList<AuthModel> list = new List<AuthModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<AuthModel>("ManAuthMng.GetAuthList", null);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == InsertUpdate() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08.10
        /// 3. 설  명 : 권한관리 Row 추가 / 수정
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int InsertUpdate(IList<AuthModel> AuthList)
        {
            IList<AuthModel> list = new List<AuthModel>();
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (AuthModel item in AuthList)
                {
                    AuthModel auth = DaoFactory.Instance.QueryForObject<AuthModel>("ManAuthMng.GetAuthList", item);

                    if (auth != null)
                    {
                        DaoFactory.Instance.Update("ManAuthMng.UpdateAuth", item);
                    }
                    else
                    {
                        DaoFactory.Instance.Insert("ManAuthMng.InsertAuth", item);
                    }
                    iRetVal++;
                }
                DaoFactory.Instance.CommitTransaction();
            }
            catch (Exception e)
            {
                DaoFactory.Instance.RollBackTransaction();
                iRetVal = 0;
                throw e;
            }

            return iRetVal;
        }
        #endregion

        #region == Delete() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08.10
        /// 3. 설  명 : 권한관리 Row 삭제
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int Delete(IList<AuthModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (AuthModel item in param)
                {
                    iRetVal += DaoFactory.Instance.Delete("ManAuthMng.DeleteAuth", item);
                }
                DaoFactory.Instance.CommitTransaction();
            }
            catch (Exception e)
            {
                DaoFactory.Instance.RollBackTransaction();
                iRetVal = 0;
                throw e;
            }

            return iRetVal;
        }
        #endregion


        #region == SelectMenuMng() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08.16
        /// 3. 설  명 : 권한관리 메뉴 리스트 조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<MenuMngModel> SelectMenuMng(Hashtable param)
        {
            IList<MenuMngModel> list = new List<MenuMngModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<MenuMngModel>("ManAuthMng.GetMenuMngList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == SelectRegMenuMng() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08.16
        /// 3. 설  명 : 권한관리 등록된 메뉴 리스트 조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<MenuMngModel> SelectRegMenuMng(Hashtable param)
        {
            IList<MenuMngModel> list = new List<MenuMngModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<MenuMngModel>("ManAuthMng.GetRegMenuMngList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == InsertAuthMenu() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08.17
        /// 3. 설  명 : 권한관리 등록된 메뉴리스트(권한에 따른 등록메뉴) Row 추가
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int InsertAuthMenu(IList<MenuMngModel> AuthMenuList)
        {
            IList<MenuMngModel> list = new List<MenuMngModel>();
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (MenuMngModel item in AuthMenuList)
                {

                    MenuMngModel regMenu = DaoFactory.Instance.QueryForObject<MenuMngModel>("ManAuthMng.GetRegMenuMngList", item);

                    if (regMenu == null)
                    {
                        DaoFactory.Instance.Insert("ManAuthMng.InsertAuthMenu", item);
                    }
                    iRetVal++;
                }

                DaoFactory.Instance.CommitTransaction();
            }
            catch (Exception e)
            {
                DaoFactory.Instance.RollBackTransaction();
                iRetVal = 0;
                throw e;
            }

            return iRetVal;
        }
        #endregion

        #region == DeleteAuthMenu() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08.17
        /// 3. 설  명 : 권한관리 등록된 메뉴리스트(권한에 따른 등록메뉴) Row 삭제
        /// 4. 이  력 :+
        /// </summary>
        /// <returns></returns>
        public int DeleteAuthMenu(IList<MenuMngModel> AuthMenuList)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (MenuMngModel item in AuthMenuList)
                {
                    iRetVal += DaoFactory.Instance.Delete("ManAuthMng.DeleteAuthMenu", item);
                }
                DaoFactory.Instance.CommitTransaction();
            }
            catch (Exception e)
            {
                DaoFactory.Instance.RollBackTransaction();
                iRetVal = 0;
                throw e;
            }

            return iRetVal;
        }
        #endregion
    }
}