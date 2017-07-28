using System;
using System.Collections.Generic;

using Common.Logging;
using HDIMS.Models;
using HDIMS.Models.Domain.Code;

namespace HDIMS.Services.Code
{
    public class EquipGroupManagementService : IEquipGroupManagementService
    {
        #region == Global Variable ==
        private static readonly ILog log = LogManager.GetLogger(typeof(EquipGroupManagementService));
        #endregion

        #region == Select() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2012.01.27
        /// 3. 설  명 : 설비상태그룹관리 조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<EquipGroupManagementModel> Select()
        {
            IList<EquipGroupManagementModel> list = new List<EquipGroupManagementModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<EquipGroupManagementModel>("Code.GetEquipGroupList", null);
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
        /// 2. 작성일 : 2012.01.27
        /// 3. 설  명 : 설비상태그룹관리 Row 추가 / 수정
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int InsertUpdate(IList<EquipGroupManagementModel> param)
        {
            IList<EquipGroupManagementModel> list = new List<EquipGroupManagementModel>();
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (EquipGroupManagementModel item in param)
                {
                    EquipGroupManagementModel equipGroup = DaoFactory.Instance.QueryForObject<EquipGroupManagementModel>("Code.GetEquipGroupList", item);

                    if (equipGroup != null)
                    {
                        DaoFactory.Instance.Update("Code.UpdateEquipGroup", item);
                    }
                    else
                    {
                        DaoFactory.Instance.Insert("Code.InsertEquipGroup", item);
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
        /// 2. 작성일 : 2012.01.27
        /// 3. 설  명 : 설비상태그룹관리 Row 삭제
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int Delete(IList<EquipGroupManagementModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (EquipGroupManagementModel item in param)
                {
                    iRetVal += DaoFactory.Instance.Delete("Code.DeleteEquipGroup", item);
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