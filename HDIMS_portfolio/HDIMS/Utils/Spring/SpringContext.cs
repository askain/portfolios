using System;
using System.Web;
using Spring.Core.IO;
using Spring.Objects.Factory;
using Spring.Objects.Factory.Xml;

namespace HDIMS.Utils.Spring
{
    public class SpringContext
    {
        private static string ROOT_PATH;
        private static IObjectFactory _factory;

        public static IObjectFactory GetObjectFactory() {
            if(_factory==null) {
                IResource input = new FileSystemResource(HttpContext.Current.Server.MapPath("~/Config/Objects.xml"));
                _factory = new XmlObjectFactory(input);
            }
            if (ROOT_PATH == null)
            {
                ROOT_PATH = HttpContext.Current.Server.MapPath("~/");
            }
            return _factory;
        }

        public static Object GetObject(string objectName)
        {
            if (_factory == null)
            {
                _factory = GetObjectFactory();
            }
            return _factory.GetObject(objectName);
        }

        public static string GetRootPath()
        {
            if (ROOT_PATH == null)
            {
                ROOT_PATH = HttpContext.Current.Server.MapPath("~/");
            }
            return ROOT_PATH;
        }
    }
}