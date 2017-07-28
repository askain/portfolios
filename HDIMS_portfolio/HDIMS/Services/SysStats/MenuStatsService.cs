using System;
using System.Collections;
using System.Collections.Generic;
using HDIMS.Models;
using HDIMS.Models.Domain.SysStats;

namespace HDIMS.Services.SysStats
{
    public class MenuStatsService : IMenuStatsService
    {
        #region == Select() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08.29
        /// 3. 설  명 : 메뉴 통계 리스트 조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<MenuStatsModel> Select(Hashtable param)
        {
            IList<MenuStatsModel> list = new List<MenuStatsModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<MenuStatsModel>("SysStats.GetMenuStatsList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == SelectTotCnt() ==
        ///<summary>
        ///1. 작성자 : 임장식
        ///2. 작성일 : 2011.08.29
        ///3. 설  명 : 메뉴 통계, Total Count 조회
        ///4. 이  력 :
        ///</summary>
        ///<returns></returns>
        public IList<MenuStatsModel> SelectTotCnt(Hashtable param)
        {
            IList<MenuStatsModel> list = new List<MenuStatsModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<MenuStatsModel>("SysStats.GetMenuStatsTotCnt", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == Insert() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08.29
        /// 3. 설  명 : 메뉴 통계 추가
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int Insert(Hashtable param)
        {
            int iRetVal = 0;
            try
            {
                DaoFactory.Instance.BeginTransaction();
                DaoFactory.Instance.Insert("SysStats.InsertMenuStats", param);
                DaoFactory.Instance.CommitTransaction();
                iRetVal++;
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