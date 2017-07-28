using System.Xml;
using Common.Logging;
using IBatisNet.Common.Utilities;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.Configuration;
using IBatisNet.DataMapper.SessionStore;

namespace HDIMS.Models
{
    public class DaoFactory
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DaoFactory));
        private static object syncLock = new object();
        private static ISqlMapper mapper = null;

        public static ISqlMapper Instance
        {
            get
            {
                try
                {
                    if (mapper == null)
                    {
                        lock (syncLock)
                        {
                            if (mapper == null)
                            {
                                DomSqlMapBuilder dom = new DomSqlMapBuilder();

                                //XmlDocument sqlMapConfig = Resources.GetEmbeddedResourceAsXmlDocument("HDIMS.SqlMap.config, HDIMS");
                                XmlDocument sqlMapConfig = Resources.GetResourceAsXmlDocument("SqlMap.config");
                                log.Debug("sqlMapConfig loading....");
                                mapper = dom.Configure(sqlMapConfig);
                                mapper.SessionStore = new HybridWebThreadSessionStore(mapper.Id);
                                
                                log.Debug("Mapper configure....[" + mapper.Id + "]");
                            }
                        }
                    }

                    return mapper;
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}