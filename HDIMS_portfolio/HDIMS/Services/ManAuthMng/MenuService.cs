using System;
using System.Collections;
using System.Collections.Generic;
using Common.Logging;
using HDIMS.Models;
using HDIMS.Models.Domain.ManAuthMng;

namespace HDIMS.Services.ManAuthMng
{
    public class MenuService : IMenuService
    {
        #region == Global Variable ==
        private static readonly ILog log = LogManager.GetLogger(typeof(MenuService));
        #endregion

        #region == Update() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08.10
        /// 3. 설  명 : 메뉴관리 메뉴 수정
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int Update(Hashtable param)
        {
            int iRetVal = 0;
            try
            {
                iRetVal += DaoFactory.Instance.Update("ManAuthMng.UpdateMenu", param);
            }
            catch (Exception e)
            {
                throw e;
            }

            return iRetVal;
        }
        #endregion

        #region == Insert() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08.15
        /// 3. 설  명 : 메뉴관리 메뉴 추가
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int Insert(Hashtable param)
        {
            int iRetVal = 0;
            try
            {
                DaoFactory.Instance.Insert("ManAuthMng.InsertMenu", param);
                iRetVal++;
            }
            catch (Exception e)
            {
                throw e;
            }

            return iRetVal;
        }
        #endregion

        #region == SelectNodeCount() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08.15
        /// 3. 설  명 : 메뉴 트리 선택시 자식 노드 Count 가져오기(순서 항목 바인딩)
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<MenuNodeCountModel> SelectNodeCount(Hashtable param)
        {
            IList<MenuNodeCountModel> list = new List<MenuNodeCountModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<MenuNodeCountModel>("ManAuthMng.SelectNodeCount", param);
            }
            catch (Exception e)
            {
                throw e;
            }

            return list;
        }
        #endregion

    }
}