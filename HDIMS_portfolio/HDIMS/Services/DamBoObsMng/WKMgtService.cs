using System;
using System.Collections;
using System.Collections.Generic;
using Common.Logging;
using HDIMS.Models;
using HDIMS.Models.Domain.DamBoObsMng;

namespace HDIMS.Services.DamBoObsMng
{
    public class WKMgtService : IWKMgtService
    {
        #region == Global Variable ==
        private static readonly ILog log = LogManager.GetLogger(typeof(WKMgtService));
        #endregion

        #region == Select() ==
        public IList<DamBoModel> Select(Hashtable param)
        {
            IList<DamBoModel> list = new List<DamBoModel>();

            try
            {
                list = DaoFactory.Instance.QueryForList<DamBoModel>("DamBoObsMng.SelectWK", param);
            }
            catch (Exception e)
            {
                throw e;
            }
            return list;
        }
        #endregion

        #region == Insert() ==
        public int Insert(IList<DamBoModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (DamBoModel item in param)
                {
                   DaoFactory.Instance.Insert("DamBoObsMng.InsertWK", item);
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
        public int Update(IList<DamBoModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (DamBoModel item in param)
                {
                    iRetVal += DaoFactory.Instance.Update("DamBoObsMng.UpdateWK", item);
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
        public int Delete(IList<DamBoModel> param)
        {
            int iRetVal = 0;

            try
            {
                DaoFactory.Instance.BeginTransaction();
                foreach (DamBoModel item in param)
                {
                    iRetVal += DaoFactory.Instance.Delete("DamBoObsMng.DeleteWK", item);
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