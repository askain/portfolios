using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

using HDIMS.Models.Domain.Code;
using HDIMS.Models;
using IBatisNet.DataMapper;
using Common.Logging;

namespace HDIMS.Services.Code
{
    public class EquipManagementService : IEquipManagementService
    {
        #region == Global Variable ==
        private static readonly ILog log = LogManager.GetLogger(typeof(EquipManagementService));
        #endregion

        #region == SelectCombo() ==
        /// <summary>
        /// 1. 작성자 : 임장식
        /// 2. 작성일 : 2012.01.27
        /// 3. 설  명 : 설비상태코드 관리 그룹명 콤보 조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<EquipGroupComboModel> SelectCombo()
        {
            IList<EquipGroupComboModel> list = new List<EquipGroupComboModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<EquipGroupComboModel>("Code.GetEquipGroupCombo", null);
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
        /// 3. 설  명 : 설비상태관리 조회
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public IList<EquipManagementModel> Select(Hashtable param)
        {
            IList<EquipManagementModel> list = new List<EquipManagementModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<EquipManagementModel>("Code.GetEquipList", param);
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
        /// 3. 설  명 : 설비상태관리 Row 추가
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int Insert(IList<EquipManagementModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (EquipManagementModel item in param)
                {
                    DaoFactory.Instance.Insert("Code.InsertEquip", item);
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
        /// 3. 설  명 : 설비상태관리 Row 수정
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int Update(IList<EquipManagementModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (EquipManagementModel item in param)
                {
                    iRetVal += DaoFactory.Instance.Update("Code.UpdateEquip", item);
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
        /// 2. 작성일 : 2011.07.26
        /// 3. 설  명 : 설비상태관리 Row 삭제
        /// 4. 이  력 :
        /// </summary>
        /// <returns></returns>
        public int Delete(IList<EquipManagementModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (EquipManagementModel item in param)
                {
                    iRetVal += DaoFactory.Instance.Delete("Code.DeleteEquip", item);
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