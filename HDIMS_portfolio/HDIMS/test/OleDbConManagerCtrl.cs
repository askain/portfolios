using System;
using System.Data;
using System.IO;
using System.Text;
using System.Collections;
using System.Data.OleDb;
using HDIMS.Services.Verify;



/// <summary>
/// DB에 대한 요약 설명입니다.
/// </summary>
public class OleDbConManagerCtrl
{
    private String CLASS_NAME = "OleDbConManagerCtrl";
    private int memCount = 0;
    private OleDbConnection memDbConn = null;
    private OleDbCommand memCmd = new OleDbCommand();
    private OleDbDataReader memReader = null;

    private const string SEE_PASSWORD = "ksee36524";
    private const string HDAPS_PASSWORD = "HDAPS";
    private const string KOSEL_PASSWORD = "selelql";
    private const string RWISDB_PASSWORD = "rwio";
    private const string KOCOM_PASSWORD = "comelql";
    private const string TESTDB_PASSWORD = "irwms05";

    private string connectionString = "Provider=MSDAORA.1;Data Source=CIMS;User Id=CIMS;Password=elpen; providerName=System.Data.OleDb;";
    public enum DB_ACCOUNT
    {
        HDAPS = 1,
        SEE = 2
    }

    /* DataBase Connection Open */
    public void DbConn()
    {
        try
        {
            if (memCmd == null)
                memCmd = new OleDbCommand();

            memDbConn = new OleDbConnection(connectionString);
            memDbConn.Open();
            memCmd.Connection = memDbConn;
        }
        catch (Exception e)
        {
            
        }
    }

    /* DataBase Connection Open */
    public void DbConn(string accountId)
    {
        string password = string.Empty;
        string serviceName = string.Empty;
        try
        {
            if (memCmd == null)
                memCmd = new OleDbCommand();

            StringBuilder sb = new StringBuilder();
            if (accountId.Equals("HDAPS"))
            {
                password = HDAPS_PASSWORD;
                serviceName = "HDAPS";
            }
            else if (accountId.Equals("KOSEL"))
            {
                password = KOSEL_PASSWORD;
                serviceName = "TECH";
            }
            else if (accountId.Equals("RWIO"))
            {
                password = RWISDB_PASSWORD;
                serviceName = "RWISDB";
            }
            else if (accountId.Equals("KOCOM"))
            {
                password = KOCOM_PASSWORD;
                serviceName = "TECH";
            }
            else if (accountId.Equals("BEAVER"))
            {
                password = TESTDB_PASSWORD;
                serviceName = "TESTDB";
            }
            else
            {
                password = SEE_PASSWORD;
                serviceName = "HDAPS";
            }

            sb.Append("Provider=OraOLEDB.Oracle;Data Source=").Append(serviceName).Append(";User Id=");
            sb.Append(accountId).Append(";Password=").Append(password);

            memDbConn = new OleDbConnection(sb.ToString());
            memDbConn.Open();
            memCmd.Connection = memDbConn;
        }
        catch (Exception e)
        {

        }
    }

    /* DataBase Connection Close */
    public void DbClose()
    {
        if (memDbConn == null)
        {
            return;
        }

        try
        {
            if (memDbConn.State.ToString().Equals("Open"))
            {
                memDbConn.Close();
            }

            memCmd = null;
            memDbConn = null;
        }
        catch (Exception e)
        {
        }
    }

    /* Excute Query DataTable */
    public DataTable ExecuteDataTable(String sQuery, String sTableName)
    {
        DataTable dt = new DataTable(sTableName);

        try
        {
            memCmd.CommandText = sQuery;

            OleDbDataAdapter ocDataAdapter = new OleDbDataAdapter(memCmd);

            ocDataAdapter.Fill(dt);
        }
        catch (Exception ex)
        {
            
        }

        return dt;
    }

}
