using System;
using System.Collections;
using System.Collections.Generic;
using HDIMS.Models;
using HDIMS.Models.Domain.DamBoObsMng;
using log4net;

namespace HDIMS.Services.DamBoObsMng
{
    public class DamBoService : IDamBoService
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DamBoService));

        public IList<HDIMS.Models.Domain.Common.Code> GetDamCodeList(Hashtable param)
        {
            IList<HDIMS.Models.Domain.Common.Code> list = new List<HDIMS.Models.Domain.Common.Code>();
            try
            {
                list = DaoFactory.Instance.QueryForList<HDIMS.Models.Domain.Common.Code>("DamBoObsMng.GetDamCodeList", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }

        #region == Select() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08.20
        /// 3. 설  명 : 댐보관리 조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<DamBoModel> Select(Hashtable param)
        {
            IList<DamBoModel> list = new List<DamBoModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<DamBoModel>("DamBoObsMng.GetDamBoList", param);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
            return list;
        }
        #endregion

        #region == SeletWk() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08.20
        /// 3. 설  명 : 댐/보 관리 리스트의 수계 콤보 조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<DamBoModel> SelectWk()
        {
            IList<DamBoModel> list = new List<DamBoModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<DamBoModel>("DamBoObsMng.GetWk", null);
            }
            catch (Exception e)
            {
                log.Error(e); 
                throw e;
            }
            return list;
        }
        #endregion

        #region == SelectDamType() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08.20
        /// 3. 설  명 : 댐/보 관리 리스트의 수계 콤보 조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<DamBoModel> SelectDamType(Hashtable param)
        {
            IList<DamBoModel> list = new List<DamBoModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<DamBoModel>("DamBoObsMng.GetDamType", param);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
            return list;
        }
        #endregion

        #region == SeletWkCombo() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08.20
        /// 3. 설  명 : 댐/보 관리 리스트 조회 조건, 수계 콤보 조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<DamBoModel> SelectWkCombo()
        {
            IList<DamBoModel> list = new List<DamBoModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<DamBoModel>("DamBoObsMng.GetWkCombo", null);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
            return list;
        }
        #endregion

        #region == SeletDamTypeCombo() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08.20
        /// 3. 설  명 : 댐/보 관리 리스트 조회 조건, 수계 콤보 조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<DamBoModel> SelectDamTypeCombo()
        {
            IList<DamBoModel> list = new List<DamBoModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<DamBoModel>("DamBoObsMng.GetDamTypeCombo", null);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
            return list;
        }
        #endregion

        #region == InsertUpdateDamBo() ==ㄴ
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08. 21
        /// 3. 설  명 : 댐/보 관리 저장 수정
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int InsertUpdateDamBo(IList<DamBoModel> param)
        {
            IList<DamBoModel> list = new List<DamBoModel>();
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (DamBoModel item in param)
                {
                    DamBoModel damBo = DaoFactory.Instance.QueryForObject< DamBoModel>("DamBoObsMng.GetDamBoList2", item);

                    if (damBo == null)
                    {
                        DaoFactory.Instance.Insert("DamBoObsMng.InsertDamBo", item);
                    }
                    else
                    {
                        DaoFactory.Instance.Update("DamBoObsMng.UpdateDamBo", item);
                    }

                    iRetVal++;
                }
                DaoFactory.Instance.CommitTransaction();
            }
            catch (Exception e)
            {
                DaoFactory.Instance.RollBackTransaction();
                iRetVal = 0;
                log.Error(e);
                throw e;
            }
            return iRetVal;
        }
        #endregion

        #region == DeleteDamBo() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.08. 21
        /// 3. 설  명 : 댐/보 관리 삭제
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int  DeleteDamBo(IList<DamBoModel> param)
        {
            IList<DamBoModel> list = new List<DamBoModel>();
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (DamBoModel item in param)
                {
                    iRetVal += DaoFactory.Instance.Delete("DamBoObsMng.DeleteDamBo", item);
                }
                DaoFactory.Instance.CommitTransaction();
            }
            catch (Exception e)
            {
                DaoFactory.Instance.RollBackTransaction();
                iRetVal = 0;
                log.Error(e);
                throw e;
            }
            return iRetVal;
        }
        #endregion


        public IList<DamColMgtModel> GetDamColMgtList(Hashtable param)
        {
            IList<DamColMgtModel> list = new List<DamColMgtModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<DamColMgtModel>("DamBoObsMng.GetDamColMgtList", param);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
            return list;
        }

        public int SaveDamColMgt(IList<DamColMgtModel> param)
        {
            IList<DamColMgtModel> list = new List<DamColMgtModel>();
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (DamColMgtModel item in param)
                {
                    DamColMgtModel data = DaoFactory.Instance.QueryForObject<DamColMgtModel>("DamBoObsMng.GetDamColMgt", item);

                    if (data == null)
                    {
                        DaoFactory.Instance.Insert("DamBoObsMng.InsertDamColMgt", item);
                    }
                    else
                    {
                        DaoFactory.Instance.Update("DamBoObsMng.UpdateDamColMgt", item);
                    }

                    iRetVal++;
                }
                DaoFactory.Instance.CommitTransaction();
            }
            catch (Exception e)
            {
                DaoFactory.Instance.RollBackTransaction();
                iRetVal = 0;
                log.Error(e);
                throw e;
            }
            return iRetVal;
        }


        #region == 댐타입 조회() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.07.12
        /// 3. 설  명 : 댐타입 조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<DamTypeModel> GetDamTypeList()
        {
            IList<DamTypeModel> list = new List<DamTypeModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<DamTypeModel>("DamBoObsMng.GetDamTypeList", null);
            }
            catch (Exception e)
            {
                log.Error(e);
                throw e;
            }
            return list;
        }
        #endregion

        #region == 댐타입 Row 추가() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.07.12
        /// 3. 설  명 : 댐타입 Row 추가
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int InsertDamType(IList<DamTypeModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (DamTypeModel item in param)
                {
                    DaoFactory.Instance.Insert("DamBoObsMng.InsertDamType", item);
                    iRetVal++;
                }
                DaoFactory.Instance.CommitTransaction();
            }
            catch (Exception e)
            {
                DaoFactory.Instance.RollBackTransaction();
                iRetVal = 0;
                log.Error(e);
                throw e;
            }

            return iRetVal;
        }
        #endregion

        #region == 댐타입 Row 수정() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.07.12
        /// 3. 설  명 : 댐타입 Row 수정
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int UpdateDamType(IList<DamTypeModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (DamTypeModel item in param)
                {
                    iRetVal += DaoFactory.Instance.Update("DamBoObsMng.UpdateDamType", item);
                }
                DaoFactory.Instance.CommitTransaction();
            }
            catch (Exception e)
            {
                DaoFactory.Instance.RollBackTransaction();
                iRetVal = 0;
                log.Error(e);
                throw e;
            }

            return iRetVal;
        }
        #endregion

        #region == 댐타입 Row 삭제() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2011.07.12
        /// 3. 설  명 : 댐타입 Row 삭제
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int DeleteDamType(IList<DamTypeModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (DamTypeModel wl in param)
                {
                    iRetVal += DaoFactory.Instance.Delete("DamBoObsMng.DeleteDamType", wl);
                }
                DaoFactory.Instance.CommitTransaction();
            }
            catch (Exception e)
            {
                DaoFactory.Instance.RollBackTransaction();
                iRetVal = 0;
                log.Error(e);
                throw e;
            }

            return iRetVal;
        }
        #endregion
    }
}