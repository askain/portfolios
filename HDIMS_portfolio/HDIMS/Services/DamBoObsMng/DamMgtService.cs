using System;
using System.Collections;
using System.Collections.Generic;
using Common.Logging;
using HDIMS.Models;
using HDIMS.Models.Domain.DamBoObsMng;

namespace HDIMS.Services.DamBoObsMng
{
    public class DamMgtService : IDamMgtService
    {
        #region == Global Variable ==
        private static readonly ILog log = LogManager.GetLogger(typeof(DamMgtService));
        #endregion

        #region == Select() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2012.01.31
        /// 3. 설  명 : 담당자관리-관리단 조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<DamMgtModel> Select(Hashtable param)
        {
            IList<DamMgtModel> list = new List<DamMgtModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<DamMgtModel>("DamBoObsMng.GetDamMgtList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == Select() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.07.26
        /// 3. 설  명 : 댐관리단코드  트리조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<DamMgtModel> GetDamMgtTreeList(Hashtable param)
        {
            IList<DamMgtModel> list = new List<DamMgtModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<DamMgtModel>("DamBoObsMng.GetDamMgtTreeList", param);
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
        /// 2. 작성일 : 2011.07.26
        /// 3. 설  명 : 댐관리단코드  Row 추가
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int Insert(IList<DamMgtModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (DamMgtModel item in param)
                {
                   DaoFactory.Instance.Insert("DamBoObsMng.InsertDamMgt", item);
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

        #region == Update() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.07.26
        /// 3. 설  명 : 댐관리단코드  Row 수정
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int Update(IList<DamMgtModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (DamMgtModel item in param)
                {
                    iRetVal += DaoFactory.Instance.Update("DamBoObsMng.UpdateDamMgt", item);
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
        /// 2. 작성일 : 2012.01.31
        /// 3. 설  명 : 관리단관리  Row 삭제
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int Delete(IList<DamMgtModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (DamMgtModel item in param)
                {
                    iRetVal += DaoFactory.Instance.Delete("DamBoObsMng.DeleteDamMgt", item);
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

        public int GetDamMgtNodeCount(Hashtable param)
        {
            int ret = 0;

            try
            {
                ret = DaoFactory.Instance.QueryForObject<int>("DamBoObsMng.GetDamMgtNodeCount", param);
            }
            catch (Exception e)
            {
                throw e;
            }

            return ret;
        }
    }
}