using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Mvc;
using Common.Logging;

namespace HDIMS.Models
{
    public class PutModelBinder : IModelBinder
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(PutModelBinder));

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            log.Debug("start PutModelBinder");

            if (controllerContext == null)
            {
                log.Debug("controllerContext is null");
                throw new ArgumentNullException("controllerContext");
            }
            if (bindingContext == null)
            {
                log.Debug("bindingContext is null");
                throw new ArgumentNullException("bindingContext");
            }

            //log.Debug("check Params");
            Stream st = controllerContext.HttpContext.Request.InputStream;
            StreamReader rs = new StreamReader(st,Encoding.UTF8);
            string txt = rs.ReadToEnd();
            rs.Close();
            st.Close();
            
            //log.Debug("form string : " + txt);
            txt = "{\"Data\":" + txt + "}";
            log.Debug("convert string : " + txt);

            log.Debug("bindingContext modelType : " + bindingContext.ModelType.Name);
            var instance = new Object() ;
            try
            {
                var serializer = new DataContractJsonSerializer(bindingContext.ModelType);
                var stream = new MemoryStream(Encoding.UTF8.GetBytes(txt));
                instance = serializer.ReadObject(stream);
            }
            catch (Exception e)
            {
                log.Error(e);
            }
            return instance;
        }


    }
}