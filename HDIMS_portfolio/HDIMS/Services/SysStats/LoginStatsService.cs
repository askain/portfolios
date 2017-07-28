using System;
using System.Collections;
using System.Collections.Generic;
using HDIMS.Models;
using HDIMS.Models.Domain.SysStats;

namespace HDIMS.Services.SysStats
{
    public class LoginStatsService : ILoginStatsService
    {
        #region == SelectMonth() ==
        ///<summary>
        ///1. 작성자 : 임장식
        ///2. 작성일 : 2011.08.24
        ///3. 설  명 : 로그인 통계, 월별 통계 리스트 조회
        ///4. 이  력 :
        ///</summary>
        ///<returns></returns>
        public IList<LoginStatsModel> SelectMonth(Hashtable param)
        {
            IList<LoginStatsModel> list = new List<LoginStatsModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<LoginStatsModel>("SysStats.GetLoginStatsMonth", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == SelectDay() ==
        ///<summary>
        ///1. 작성자 : 임장식
        ///2. 작성일 : 2011.08.24
        ///3. 설  명 : 로그인 통계, 일별 통계 리스트 조회
        ///4. 이  력 :
        ///</summary>
        ///<returns></returns>
        public IList<LoginStatsModel> SelectDay(Hashtable param)
        {
            IList<LoginStatsModel> list = new List<LoginStatsModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<LoginStatsModel>("SysStats.GetLoginStatsDay", param);
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
        ///3. 설  명 : 로그인 통계, Total Count 조회
        ///4. 이  력 :
        ///</summary>
        ///<returns></returns>
        public IList<LoginStatsModel> SelectTotCnt(Hashtable param)
        {
            IList<LoginStatsModel> list = new List<LoginStatsModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<LoginStatsModel>("SysStats.GetLoginStatsTotCnt", param);
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