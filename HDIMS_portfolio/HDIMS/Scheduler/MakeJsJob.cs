using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Script.Serialization;
using Common.Logging;
using HDIMS.Models;
using Spring.Scheduling.Quartz;

namespace HDIMS.Scheduler
{
    class MakeJsJob : QuartzJobObject
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(MakeJsJob));
        

        protected override void ExecuteInternal(Quartz.JobExecutionContext context)
        {
            log.Info("JS파일 만들기 시작");
            //만들 파일명
            string jsFilePath = "d:/code.js";

            // 1. 댐구분 var gl_damtypes
            // 2. 댐 gl_damcodes
            // 3. 관측국 gl_obscodes
            // 4. 검정등급 우량 var gl_excd_W
            // 5. 검정등급 수위 var gl_excd_R 
            // 6. 검정등급 공통 var gl_excd_C
            // 7. 보정등급 var gl_edexlvl
            // 8. 보정등급2 var gl_edexlvl2 
            // 9. 보정방법수위 var gl_edexway_WL2
            // 10. 보정방법우량 var gl_edexway_RF2 
            // 11. 보정방법DD var gl_edexway_DD2 
            // 12. 사원 gl_empno
            //기존파일 백업?


            // jsonserializer를 이용하여 코드를 생성.
            try
            {
                StringBuilder codeJsSb = new StringBuilder();

                //기존파일 백업


                #region == 1. 댐구분 ==
                //var gl_damtypes = [
                //{ "DAMTYPE": "D", "DAMTPNM": "다목적댐" },
                Hashtable param = new Hashtable(); param.Add("Type", "all");
                IList<HDIMS.Models.Domain.DamBoObsMng.DamBoModel> damtypelist = DaoFactory.Instance.QueryForList<HDIMS.Models.Domain.DamBoObsMng.DamBoModel>("DamBoObsMng.GetDamType", param);

                IList<Hashtable> damlistForJson = new List<Hashtable>();
                Hashtable damForJson = null;
                foreach (HDIMS.Models.Domain.DamBoObsMng.DamBoModel damtype in damtypelist)
                {
                    damForJson = new Hashtable();
                    damForJson.Add("DAMTYPE", damtype.DAMTYPE);
                    damForJson.Add("DAMTPNM", damtype.DAMTPNM);
                    damlistForJson.Add(damForJson);
                }

                JavaScriptSerializer serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue, RecursionLimit = 1000 };
                string content = serializer.Serialize(damlistForJson);

                codeJsSb.Append("var gl_damtypes = ");
                codeJsSb.Append(content);
                codeJsSb.Append(";\n");

                damtypelist = null;
                #endregion

                /*
                #region == 2. 댐 ==
                IList<HDIMS.Models.Domain.Common.Code> damlist = DaoFactory.Instance.QueryForList<HDIMS.Models.Domain.Common.Code>("Common.GetDamCodeList", null);

                damlistForJson = new List<Hashtable>();
                damForJson = null;
                foreach (HDIMS.Models.Domain.Common.Code dam in damlist)
                {
                    damForJson = new Hashtable();
                    damForJson.Add("DAMCD", dam.DAMCD);
                    damForJson.Add("DAMNM", dam.DAMNM);
                    damForJson.Add("DAMTP", dam.DAMTYPE);
                    damlistForJson.Add(damForJson);
                }

                serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue, RecursionLimit = 1000 };
                content = serializer.Serialize(damlistForJson);

                codeJsSb.Append("var gl_damcodes = ");
                codeJsSb.Append(content);
                codeJsSb.Append(";\n");

                damlist = null;
                #endregion
                */
                 
                
                // 3. 관측국 
                //var gl_obscodes = [
                //{ DAMCD: '1001210', OBSCD: '9000040', OBSNM: '광동댐', OBSTP: 'RF' },
                #region ==  3. 관측국 ==
                IList<HDIMS.Models.Domain.Common.Code> obslist = DaoFactory.Instance.QueryForList<HDIMS.Models.Domain.Common.Code>("Common.GetObsCodeList", null);

                damlistForJson = new List<Hashtable>();
                damForJson = null;
                foreach (HDIMS.Models.Domain.Common.Code obs in obslist)
                {
                    damForJson = new Hashtable();
                    damForJson.Add("DAMCD", obs.PARENTKEY);
                    damForJson.Add("OBSCD", obs.KEY);
                    damForJson.Add("OBSNM", obs.VALUE);
                    damForJson.Add("OBSTP", obs.OBSTP);
                    damlistForJson.Add(damForJson);
                }

                serializer = new JavaScriptSerializer() { MaxJsonLength = Int32.MaxValue, RecursionLimit = 1000 };
                content = serializer.Serialize(damlistForJson);

                codeJsSb.Append("var gl_obscodes = ");
                codeJsSb.Append(content);
                codeJsSb.Append(";\n");

                obslist = null;
                #endregion


                // 4. 검정등급 우량 var gl_excd_W
                //var gl_excd_R = [
                //{ text: '0991', value: '결측 우량 자료' },

                // 5. 검정등급 수위 var gl_excd_R 
                //var gl_excd_W = [
                //{ text: '전체', value: '' },

                // 6. 검정등급 공통 var gl_excd_C
                //var gl_excd_C = [
                //{text:	'0100', value:	'양호한자료'	}

                // 7. 보정등급 var gl_edexlvl
                //var gl_edexlvl = [
                //{ text: '전체', value: '' },

                // 8. 보정등급2 var gl_edexlvl2 
                //var gl_edexlvl2 = [
                //{ text: '선택하세요', value: '' },

                // 9. 보정방법수위 var gl_edexway_WL2
                //var gl_edexway_WL2 = [
                //{ text: '선택하세요', value: '' },

                // 10. 보정방법우량 var gl_edexway_RF2 
                //var gl_edexway_RF2 = [
                //{ text: '선택하세요', value: '' },

                // 11. 보정방법DD var gl_edexway_DD2 
                //var gl_edexway_DD2 = [
                //{ text: '선택하세요', value: '' },

                // 12. 사원 gl_empno
                //var gl_empno = [
                //{ text: '전체', value: '' }


                using (FileStream fs = File.Create(jsFilePath))
                {
                    using (TextWriter tw = new StreamWriter(fs, Encoding.UTF8))
                    {
                        tw.WriteLine(codeJsSb.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                log.Error("JS파일을 만드는 중 에러가 발생하였습니다.");
                log.Error(e);
                throw e;
            }

            //IList<Hashtable> damList = DaoFactory.Instance.QueryForList<Hashtable>("damdata.GetDamList", null);
            //obsdhm = DaoFactory.Instance.QueryForObject<string>("damdata.GetMaxObsDhmDam", null);


            log.Info("JS파일 만들기 종료");
        }
    }
}
