using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Configuration.Provider;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Web.Configuration;
using System.Web.SessionState;

/*
  CREATE TABLE HM_SESSIONS
  (
    SESSID       VARCHAR2(80)  NOT NULL,
    APPNM        VARCHAR2(255) NOT NULL,
    CREATEDT         DateTime  NOT NULL,
    EXPIREDT      DateTime  NOT NULL, 
    LOCKDT        DateTime  NOT NULL,
    LOCKID          INTEGER   NOT NULL,
    TIMEOUT         INTEGER   NOT NULL,
    LOCKED          NUMBER(1)     NOT NULL,
    ITEMS           VARCHAR2(4000),
    FLAGS           INTEGER   NOT NULL,
      CONSTRAINT HM_SESSIONS_PK PRIMARY KEY (SESSID,APPNM)
  )
Session State Store Privider는 자동적으로 만료된 세션을 삭제하지 않으므로 
정기적으로 EXPIREDT를 기준으로 삭제해야 된다.
 * HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Eventlog\Application\OdbcSessionStateStore
*/

namespace HDIMS.Utils.Module
{
    public sealed class OracleSessionStateStore: SessionStateStoreProviderBase
    {
        private SessionStateSection pConfig = null;
        private string connectionString;
        private ConnectionStringSettings pConnectionStringSettings;
        private string eventSource = "OracleSessionStateStore";
        private string eventLog = "Application";
        private string exceptionMessage = "An exception occurred. Please contact your administrator.";
        private string pApplicationName;

        //EventLog에 로그를 저장할지 여부
        private bool pWriteExceptionsToEventLog = false;

        public bool WriteExceptionsToEventLog
        {
            get { return pWriteExceptionsToEventLog; }
            set { pWriteExceptionsToEventLog = value; }
        }

        // Application마다 다른 세션을 관리할 경우 사용
        public string ApplicationName
        {
            get { return pApplicationName; }
        }

        /// <summary>
        /// Provider 초기화
        /// </summary>
        /// <param name="name"></param>
        /// <param name="config"></param>
        public override void Initialize(string name, NameValueCollection config)
        {
            if (config == null)
                throw new ArgumentNullException("config");

            if (name == null || name.Length == 0)
                name = "OracleSessionStateStore";

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("description", "Oracle Session State Store provider");
            }

            // Initialize the abstract base class.
            base.Initialize(name, config);


            //
            // Initialize the ApplicationName property.
            //

            pApplicationName =
              System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath;


            //
            // Get <sessionState> configuration element.
            //

            Configuration cfg =
              WebConfigurationManager.OpenWebConfiguration(ApplicationName);
            pConfig =
              (SessionStateSection)cfg.GetSection("system.web/sessionState");


            //
            // Initialize connection string.
            //

            pConnectionStringSettings =
              ConfigurationManager.ConnectionStrings[config["connectionStringName"]];

            if (pConnectionStringSettings == null ||
              pConnectionStringSettings.ConnectionString.Trim() == "")
            {
                throw new ProviderException("Connection string cannot be blank.");
            }

            connectionString = pConnectionStringSettings.ConnectionString;


            //
            // Initialize WriteExceptionsToEventLog
            //

            pWriteExceptionsToEventLog = false;

            if (config["writeExceptionsToEventLog"] != null)
            {
                if (config["writeExceptionsToEventLog"].ToUpper() == "TRUE")
                    pWriteExceptionsToEventLog = true;
            }
        }

        public override void Dispose()
        {
        }

        /// <summary>
        /// SessionStateProviderBase.SetItemExpireCallback 구현
        /// </summary>
        /// <param name="expireCallback"></param>
        /// <returns></returns>
        public override bool SetItemExpireCallback(SessionStateItemExpireCallback expireCallback)
        {
            return false;
        }

        /// <summary>
        /// SessionStateProviderBase.SetAndReleaseItemExclusive 구현
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <param name="lockId"></param>
        /// <param name="newItem"></param>
        public override void SetAndReleaseItemExclusive(HttpContext context,
          string id,
          SessionStateStoreData item,
          object lockId,
          bool newItem)
        {
            string sessItems = Serialize((SessionStateItemCollection)item.Items);

            OleDbConnection conn = new OleDbConnection(connectionString);
            OleDbCommand cmd;
            OleDbCommand deleteCmd = null;

            if (newItem)
            {
                deleteCmd = new OleDbCommand("DELETE FROM HM_SESSIONS" +
                    "WHERE SESSID = ? AND APPNM = ? AND EXPIREDT < ?", conn);
                deleteCmd.Parameters.Add("@SESSID", OleDbType.VarChar, 80).Value = id;
                deleteCmd.Parameters.Add("@APPNM", OleDbType.VarChar, 255).Value = ApplicationName;
                deleteCmd.Parameters.Add("@EXPIREDT", OleDbType.Date).Value = DateTime.Now;

                cmd = new OleDbCommand("INSERT INTO HM_SESSIONS" +
                  " (SESSID, APPNM, CREATEDT, EXPIREDT, " +
                  "  LOCKDT, LOCKID, TIMEOUT, LOCKED, ITEMS, FLAGS) " +
                  " Values(?, ?, ?, ?, ?, ? , ?, ?, ?, ?)", conn);
                cmd.Parameters.Add("@SESSID", OleDbType.VarChar, 80).Value = id;
                cmd.Parameters.Add("@APPNM", OleDbType.VarChar, 255).Value = ApplicationName;
                cmd.Parameters.Add("@CREATEDT", OleDbType.Date).Value = DateTime.Now;
                cmd.Parameters.Add("@EXPIREDT", OleDbType.Date).Value = DateTime.Now.AddMinutes((Double)item.Timeout);
                cmd.Parameters.Add("@LOCKDT", OleDbType.Date).Value = DateTime.Now;
                cmd.Parameters.Add("@LOCKID", OleDbType.Integer).Value = 0;
                cmd.Parameters.Add("@TIMEOUT", OleDbType.Integer).Value = item.Timeout;
                cmd.Parameters.Add("@LOCKED", OleDbType.Integer).Value = 0;
                cmd.Parameters.Add("@ITEMS", OleDbType.VarChar, sessItems.Length).Value = sessItems;
                cmd.Parameters.Add("@FLAGS", OleDbType.Integer).Value = 0;
            }
            else
            {
                cmd = new OleDbCommand(
                  "UPDATE HM_SESSIONS SET EXPIREDT = ?, ITEMS = ?, LOCKED = ? " +
                  " WHERE SESSID = ? AND APPNM = ? AND LOCKID = ?", conn);
                cmd.Parameters.Add("@EXPIREDT", OleDbType.Date).Value = DateTime.Now.AddMinutes((Double)item.Timeout);
                cmd.Parameters.Add("@ITEMS", OleDbType.VarChar, sessItems.Length).Value = sessItems;
                cmd.Parameters.Add("@LOCKED", OleDbType.Integer).Value = 0;
                cmd.Parameters.Add("@SESSID", OleDbType.VarChar, 80).Value = id;
                cmd.Parameters.Add("@APPNM", OleDbType.VarChar, 255).Value = ApplicationName;
                cmd.Parameters.Add("@LOCKID", OleDbType.Integer).Value = lockId;
            }

            try
            {
                conn.Open();

                if (deleteCmd != null)
                    deleteCmd.ExecuteNonQuery();

                cmd.ExecuteNonQuery();
            }
            catch (OleDbException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "SetAndReleaseItemExclusive");
                    throw new ProviderException(exceptionMessage);
                }
                else
                    throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// SessionStateProviderBase.GetItem 구현
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <param name="locked"></param>
        /// <param name="lockAge"></param>
        /// <param name="lockId"></param>
        /// <param name="actionFlags"></param>
        /// <returns></returns>
        public override SessionStateStoreData GetItem(HttpContext context,
          string id,
          out bool locked,
          out TimeSpan lockAge,
          out object lockId,
          out SessionStateActions actionFlags)
        {
            return GetSessionStoreItem(false, context, id, out locked,
              out lockAge, out lockId, out actionFlags);
        }

        /// <summary>
        /// SessionStateProviderBase.GetItemExclusive 구현
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <param name="locked"></param>
        /// <param name="lockAge"></param>
        /// <param name="lockId"></param>
        /// <param name="actionFlags"></param>
        /// <returns></returns>
        public override SessionStateStoreData GetItemExclusive(HttpContext context,
          string id,
          out bool locked,
          out TimeSpan lockAge,
          out object lockId,
          out SessionStateActions actionFlags)
        {
            return GetSessionStoreItem(true, context, id, out locked,
              out lockAge, out lockId, out actionFlags);
        }

        /// <summary>
        ///  GetItem과 GetItemExclusive에서 호출되는 메소드
        ///  lockRecord=='Y'이면 record를 lock한다.
        /// </summary>
        /// <param name="lockRecord"></param>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <param name="locked"></param>
        /// <param name="lockAge"></param>
        /// <param name="lockId"></param>
        /// <param name="actionFlags"></param>
        /// <returns></returns>
        private SessionStateStoreData GetSessionStoreItem(bool lockRecord,
          HttpContext context,
          string id,
          out bool locked,
          out TimeSpan lockAge,
          out object lockId,
          out SessionStateActions actionFlags)
        {
            SessionStateStoreData item = null;
            lockAge = TimeSpan.Zero;
            lockId = null;
            locked = false;
            actionFlags = 0;

            OleDbConnection conn = new OleDbConnection(connectionString);
            OleDbCommand cmd = null;
            OleDbDataReader reader = null;
            DateTime expires;
            string serializedItems = "";
            bool foundRecord = false;
            bool deleteData = false;
            int timeout = 0;

            try
            {
                conn.Open();
                if (lockRecord)
                {
                    cmd = new OleDbCommand(
                      "UPDATE HM_SESSIONS SET LOCKED = ?, LOCKDT = ? " +
                      " WHERE SESSID = ? AND APPNM = ? AND LOCKED = ? AND EXPIREDT > ?", conn);
                    cmd.Parameters.Add("@LOCKED", OleDbType.Integer).Value = 1;
                    cmd.Parameters.Add("@LOCKDT", OleDbType.Date).Value = DateTime.Now;
                    cmd.Parameters.Add("@SESSID", OleDbType.VarChar, 80).Value = id;
                    cmd.Parameters.Add("@APPNM", OleDbType.VarChar,255).Value = ApplicationName;
                    cmd.Parameters.Add("@LOCKED", OleDbType.Integer).Value = 0;
                    cmd.Parameters.Add("@EXPIREDT", OleDbType.Date).Value = DateTime.Now;

                    if (cmd.ExecuteNonQuery() == 0)
                        locked = true;
                    else
                        locked = false;
                }

                cmd = new OleDbCommand(
                  "SELECT EXPIREDT, ITEMS, LOCKID, LOCKDT, FLAGS, TIMEOUT " +
                  "  FROM HM_SESSIONS" +
                  "  WHERE SESSID = ? AND APPNM = ?", conn);
                cmd.Parameters.Add("@SESSID", OleDbType.VarChar, 80).Value = id;
                cmd.Parameters.Add("@APPNM", OleDbType.VarChar, 255).Value = ApplicationName;

                reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
                while (reader.Read())
                {
                    expires = reader.GetDateTime(0);

                    if (expires < DateTime.Now)
                    {
                        locked = false;
                        deleteData = true;
                    }
                    else
                        foundRecord = true;

                    serializedItems = reader.IsDBNull(1)==false ? reader.GetString(1) : "";
                    lockId = reader.GetDecimal(2);
                    lockAge = DateTime.Now.Subtract(reader.GetDateTime(3));
                    actionFlags = (SessionStateActions)reader.GetDecimal(4);
                    timeout = (int)reader.GetDecimal(5);
                }
                reader.Close();

                if (deleteData)
                {
                    cmd = new OleDbCommand("DELETE FROM HM_SESSIONS WHERE SESSID = ? AND APPNM = ?", conn);
                    cmd.Parameters.Add("@SESSID", OleDbType.VarChar, 80).Value = id;
                    cmd.Parameters.Add("@APPNM", OleDbType.VarChar, 255).Value = ApplicationName;

                    cmd.ExecuteNonQuery();
                }

                if (!foundRecord)
                    locked = false;

                if (foundRecord && !locked)
                {
                    lockId = Int32.Parse(lockId.ToString()) + 1;

                    cmd = new OleDbCommand("UPDATE HM_SESSIONS SET LOCKID = ?, FLAGS = 0 " +
                      " WHERE SESSID = ? AND APPNM = ?", conn);
                    cmd.Parameters.Add("@LOCKID", OleDbType.Integer).Value = lockId;
                    cmd.Parameters.Add("@SESSID", OleDbType.VarChar, 80).Value = id;
                    cmd.Parameters.Add("@APPNM", OleDbType.VarChar, 255).Value = ApplicationName;

                    cmd.ExecuteNonQuery();

                    if (actionFlags == SessionStateActions.InitializeItem)
                        item = this.CreateNewStoreData(context, (int)pConfig.Timeout.TotalMinutes);
                    else
                        item = Deserialize(context, serializedItems, timeout);
                }
            }
            catch (OleDbException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "GetSessionStoreItem");
                    throw new ProviderException(exceptionMessage);
                }
                else
                    throw e;
            }
            finally
            {
                if (reader != null) { reader.Close(); }
                conn.Close();
            }

            return item;
        }

        private string Serialize(SessionStateItemCollection items)
        {
            MemoryStream ms = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(ms);

            if (items != null)
                items.Serialize(writer);

            writer.Close();

            return Convert.ToBase64String(ms.ToArray());
        }

        private SessionStateStoreData Deserialize(HttpContext context,
          string serializedItems, int timeout)
        {
            MemoryStream ms =
              new MemoryStream(Convert.FromBase64String(serializedItems));

            SessionStateItemCollection sessionItems =
              new SessionStateItemCollection();

            if (ms.Length > 0)
            {
                BinaryReader reader = new BinaryReader(ms);
                sessionItems = SessionStateItemCollection.Deserialize(reader);
            }

            return new SessionStateStoreData(sessionItems,
              SessionStateUtility.GetSessionStaticObjects(context),
              timeout);
        }

        public override void ReleaseItemExclusive(HttpContext context,
          string id,
          object lockId)
        {
            OleDbConnection conn = new OleDbConnection(connectionString);
            OleDbCommand cmd =
              new OleDbCommand("UPDATE HM_SESSIONS SET LOCKED = 0, EXPIREDT = ? " +
              "WHERE SESSID = ? AND APPNM = ? AND LOCKID = ?", conn);
            cmd.Parameters.Add("@EXPIREDT", OleDbType.Date).Value = DateTime.Now.AddMinutes(pConfig.Timeout.TotalMinutes);
            cmd.Parameters.Add("@SESSID", OleDbType.VarChar, 80).Value = id;
            cmd.Parameters.Add("@APPNM", OleDbType.VarChar, 255).Value = ApplicationName;
            cmd.Parameters.Add("@LOCKID", OleDbType.Integer).Value = lockId;

            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            catch (OleDbException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ReleaseItemExclusive");
                    throw new ProviderException(exceptionMessage);
                }
                else
                    throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// SessionStateProviderBase.RemoveItem 구현
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <param name="lockId"></param>
        /// <param name="item"></param>
        public override void RemoveItem(HttpContext context,
          string id,
          object lockId,
          SessionStateStoreData item)
        {
            OleDbConnection conn = new OleDbConnection(connectionString);
            OleDbCommand cmd = new OleDbCommand("DELETE * FROM HM_SESSIONS " +
              "WHERE SESSID = ? AND APPNM = ? AND LOCKID = ?", conn);
            cmd.Parameters.Add("@SESSID", OleDbType.VarChar, 80).Value = id;
            cmd.Parameters.Add("@APPNM", OleDbType.VarChar, 255).Value = ApplicationName;
            cmd.Parameters.Add("@LOCKID", OleDbType.Integer).Value = lockId;

            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            catch (OleDbException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "RemoveItem");
                    throw new ProviderException(exceptionMessage);
                }
                else
                    throw e;
            }
            finally
            {
                conn.Close();
            }
        }



        /// <summary>
        /// SessionStateProviderBase.CreateUninitializedItem 구현
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        /// <param name="timeout"></param>
        public override void CreateUninitializedItem(HttpContext context,
          string id,
          int timeout)
        {
            OleDbConnection conn = new OleDbConnection(connectionString);
            OleDbCommand cmd = new OleDbCommand("INSERT INTO HM_SESSIONS" +
              " (SESSID, APPNM, CREATEDT, EXPIREDT, LOCKDT, LOCKID, TIMEOUT, LOCKED, ITEMS, FLAGS) " +
              " Values(?, ?, ?, ?, ?, ? , ?, ?, ?, ?)", conn);
            cmd.Parameters.Add("@SESSID", OleDbType.VarChar, 80).Value = id;
            cmd.Parameters.Add("@APPNM", OleDbType.VarChar, 255).Value = ApplicationName;
            cmd.Parameters.Add("@CREATEDT", OleDbType.Date).Value = DateTime.Now;
            cmd.Parameters.Add("@EXPIREDT", OleDbType.Date).Value = DateTime.Now.AddMinutes((Double)timeout);
            cmd.Parameters.Add("@LOCKDT", OleDbType.Date).Value = DateTime.Now;
            cmd.Parameters.Add("@LOCKID", OleDbType.Integer).Value = 0;
            cmd.Parameters.Add("@TIMEOUT", OleDbType.Integer).Value = timeout;
            cmd.Parameters.Add("@LOCKED", OleDbType.Integer).Value = 0;
            cmd.Parameters.Add("@ITEMS", OleDbType.VarChar, 0).Value = "";
            cmd.Parameters.Add("@FLAGS", OleDbType.Integer).Value = 1;

            try
            {
                conn.Open();

                cmd.ExecuteNonQuery();
            }
            catch (OleDbException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "CreateUninitializedItem");
                    throw new ProviderException(exceptionMessage);
                }
                else
                    throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// SessionStateProviderBase.CreateNewStoreData 구현
        /// </summary>
        /// <param name="context"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        public override SessionStateStoreData CreateNewStoreData(HttpContext context, int timeout)
        {
            return new SessionStateStoreData(new SessionStateItemCollection(),
              SessionStateUtility.GetSessionStaticObjects(context),
              timeout);
        }


        /// <summary>
        /// SessionStateProviderBase.ResetItemTimeout 구현
        /// </summary>
        /// <param name="context"></param>
        /// <param name="id"></param>
        public override void ResetItemTimeout(HttpContext context, string id)
        {
            OleDbConnection conn = new OleDbConnection(connectionString);
            OleDbCommand cmd =
              new OleDbCommand("UPDATE HM_SESSIONS SET EXPIRES = ? WHERE SESSID = ? AND APPNM=?", conn);
            cmd.Parameters.Add("@EXPIRES", OleDbType.Date).Value = DateTime.Now.AddMinutes(pConfig.Timeout.TotalMinutes);
            cmd.Parameters.Add("@SESSID", OleDbType.VarChar, 80).Value = id;
            cmd.Parameters.Add("@APPNM", OleDbType.VarChar, 255).Value = ApplicationName;

            try
            {
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (OleDbException e)
            {
                if (WriteExceptionsToEventLog)
                {
                    WriteToEventLog(e, "ResetItemTimeout");
                    throw new ProviderException(exceptionMessage);
                }
                else
                    throw e;
            }
            finally
            {
                conn.Close();
            }
        }

        /// <summary>
        /// SessionStateProviderBase.InitializeRequest 구현
        /// </summary>
        /// <param name="context"></param>
        public override void InitializeRequest(HttpContext context)
        {
        }



        /// <summary>
        /// SessionStatePriverBase.EndRequest 구현
        /// </summary>
        /// <param name="context"></param>
        public override void EndRequest(HttpContext context)
        {
        }


        /// <summary>
        /// EventLog에 에러로그 저장하기
        /// </summary>
        /// <param name="e">에러</param>
        /// <param name="action">작동상태</param>
        private void WriteToEventLog(Exception e, string action)
        {
            EventLog log = new EventLog();
            log.Source = eventSource;
            log.Log = eventLog;

            string message = "An exception occurred communicating with the data source.\n\n";
            message += "Action: " + action + "\n\n";
            message += "Exception: " + e.ToString();

            log.WriteEntry(message);
        }
    }
}