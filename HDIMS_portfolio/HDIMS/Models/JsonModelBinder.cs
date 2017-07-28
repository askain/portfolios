using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web.Mvc;
using Common.Logging;

namespace HDIMS.Models
{
    public class JsonModelBinder : IModelBinder
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(JsonModelBinder));
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            if (!controllerContext.HttpContext.Request.ContentType.StartsWith("application/json", StringComparison.OrdinalIgnoreCase))
            {
                // not JSON request                
                return null;
            }
            var request = controllerContext.HttpContext.Request;
            var incomingData = new StreamReader(request.InputStream).ReadToEnd();
            if (log.IsDebugEnabled)
            {
                log.Debug("incoming Data : " + incomingData);
            }
            if (String.IsNullOrEmpty(incomingData))
            {
                // no JSON data                
                return null;
            }
            var instance = new Object() ;
            try
            {
                var serializer = new DataContractJsonSerializer(bindingContext.ModelType);

                var stream = new MemoryStream(UTF8Encoding.Unicode.GetBytes(incomingData));
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