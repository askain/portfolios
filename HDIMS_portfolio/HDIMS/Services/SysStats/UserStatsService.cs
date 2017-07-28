using System;
using System.Collections;
using System.Collections.Generic;
using HDIMS.Models;
using HDIMS.Models.Domain.SysStats;

namespace HDIMS.Services.SysStats
{
    public class UserStatsService : IUserStatsService
    {
        #region == Select() ==
        ///<summary>
        ///1. 작성자 : 임장식
        ///2. 작성일 : 2011.08.22
        ///3. 설  명 : 사용자별 통계 조회
        ///4. 이  력 :
        ///</summary>
        ///<returns></returns>
        public IList<UserStatsModel> Select(Hashtable param)
        {
            IList<UserStatsModel> list = new List<UserStatsModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<UserStatsModel>("SysStats.GetUserStatsList", param);
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
        ///2. 작성일 : 2011.08.22
        ///3. 설  명 : 사용자별 통계, Total Count 조회
        ///4. 이  력 :
        ///</summary>
        ///<returns></returns>
        public IList<UserStatsModel> SelectTotCnt(Hashtable param)
        {
            IList<UserStatsModel> list = new List<UserStatsModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<UserStatsModel>("SysStats.GetUserStatsTotCnt", param);
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