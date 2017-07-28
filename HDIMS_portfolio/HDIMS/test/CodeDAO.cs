using System;
using System.Data;
using System.Data.OleDb;
using System.Text;
using System.Collections;
using Common.Logging;


namespace HDIMS.Services.Verify
{
    /// <summary>
    /// CodeDAO에 대한 요약 설명입니다.
    /// *  화   일   명  : CodeDAO
    /// *  버        젼  : 1.0
    /// *  설        명  : 댐, 수위국, 강우국 등의 코드 정보를 조회
    /// *  작   성   자  : 박은상
    /// *  작   성   일  : 
    /// *  수   정   자  :
    /// *  수   정   일  :
    /// *  수 정  이 력  :
    /// </summary>
    public class CodeDAO
    {
        #region 공용 Class 정의
        private OleDbConManagerCtrl dbCtrl = new OleDbConManagerCtrl();
        private static readonly ILog log = LogManager.GetLogger(typeof(CodeDAO));
        #endregion

        #region 전역변수 선언
        private String CLASS_NAME = "CodeDAO";
        private DataRow[] m_DrList = null;
        #endregion

        public CodeDAO()
        {
            //
            // TODO: 여기에 생성자 논리를 추가합니다.
            //
        }

        /// <summary>
        /// 댐코드별 수위관측소, 우량관측소 코드 조회
        /// </summary>
        /// <param name="dnum"></param>
        /// <param name="damcd_arr"></param>
        /// <returns></returns>
        public static DataTable selectByDamCdObsCd()
        {
            StringBuilder sb = new StringBuilder();
            string s_Query = string.Empty;
            OleDbConManagerCtrl dbCtrl = new OleDbConManagerCtrl();
            DataTable ds = null;
            try
            {

                sb.Append(@"SELECT * FROM DUBMMRF WHERE RFOBSCD='1003428' AND OBSDHM BETWEEN '201108010000' AND '201108102400' AND TRMDV='10'");
                
                //*******************************************************************************
                // 쿼리 끝
                //*******************************************************************************
                s_Query = sb.ToString();

                //logCtrl.LogText(CLASS_NAME, logCtrl.DEB(), s_Query);
                log.Debug("-------------------------------------------1");
                dbCtrl.DbConn();
                log.Debug("-------------------------------------------2");
                ds = dbCtrl.ExecuteDataTable(s_Query, "asdf");
                log.Debug("-------------------------------------------3" + ds.Rows.Count);
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                dbCtrl.DbClose();
            }
            return ds;
        }

    }
}