using System;
using System.Collections;
using System.Collections.Generic;
using HDIMS.Models;

namespace HDIMS.Utils.Login
{
    public class SessionUtil
    {
        public static void InsertSession(Hashtable param)
        {
            try
            {
                DaoFactory.Instance.Insert("Common.InsertSession", param);
            }
            catch (Exception e) { }
        }

        public static void UpdateSession(Hashtable param)
        {
            try
            {
                DaoFactory.Instance.Update("Common.UpdateSession", param);
            }
            catch (Exception e) { }
        }

        public static void DeleteSession(Hashtable param)
        {
            try
            {
                DaoFactory.Instance.Delete("Common.DeleteSession", param);
            }
            catch (Exception e) { }
        }

        public static IList<Hashtable> GetSessionUsers()
        {
            IList<Hashtable> list = new List<Hashtable>();
            try
            {
                list = DaoFactory.Instance.QueryForList<Hashtable>("Common.GetSessionUsers", null);
            }
            catch (Exception e) { }

            return list;
        }

        public static IList<Hashtable> GetSession(Hashtable param)
        {
            IList<Hashtable> list = new List<Hashtable>();
            try
            {
                list = DaoFactory.Instance.QueryForList<Hashtable>("Common.GetSession", param);
            }
            catch (Exception e) { }

            return list;
        }
    }
}