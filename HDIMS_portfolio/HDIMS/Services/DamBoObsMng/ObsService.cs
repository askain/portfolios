using System;
using System.Collections;
using System.Collections.Generic;
using HDIMS.Models;
using HDIMS.Models.Domain.DamBoObsMng;

namespace HDIMS.Services.DamBoObsMng
{
    public class ObsService : IObsService
    {
        #region == GetObsList() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08.21
        /// 3. 설  명 : 관측소 관리 조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<ObsModel> Select(Hashtable param)
        {
            IList<ObsModel> list = new List<ObsModel>();
                
            try
            {
                list = DaoFactory.Instance.QueryForList<ObsModel>("DamBoObsMng.GetObsList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == SelectObsType() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08.20
        /// 3. 설  명 : 관측소 관리 조회, 구분 콤보 조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<ObsModel> SelectObsType()
        {
            IList<ObsModel> list = new List<ObsModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<ObsModel>("DamBoObsMng.GetObsType", null);
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