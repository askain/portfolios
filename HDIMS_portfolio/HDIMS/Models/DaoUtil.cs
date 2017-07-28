using System.Collections;
using System.Collections.Generic;
using System.Data;
using Common.Logging;
using IBatisNet.DataMapper;
using IBatisNet.DataMapper.MappedStatements;
using IBatisNet.DataMapper.Scope;

namespace HDIMS.Models
{
    public class DaoUtil
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DaoUtil));

        public static DataTable QueryForTable(string statementName, object param)
        {

            ISqlMapper mapper = DaoFactory.Instance;
            if (!mapper.IsSessionStarted) mapper.OpenConnection();
            DataTable dataTable = new DataTable(statementName);

            ISqlMapSession session = mapper.LocalSession;

            IMappedStatement statement = mapper.GetMappedStatement(statementName);
            RequestScope request = statement.Statement.Sql.GetRequestScope(statement, param, session);
            statement.PreparedCommand.Create(request, session, statement.Statement, param);

            using (request.IDbCommand)
            {
                dataTable.Load(request.IDbCommand.ExecuteReader());
            }
            if (mapper.IsSessionStarted) mapper.CloseConnection();
            return dataTable;
        }


        public static IList<IDictionary> QueryForList(string statementName, object param)
        {


            ISqlMapper mapper = DaoFactory.Instance;
            if (!mapper.IsSessionStarted) mapper.OpenConnection();
            IList<IDictionary> data = new List<IDictionary>();

            ISqlMapSession session = mapper.LocalSession;

            IMappedStatement statement = mapper.GetMappedStatement(statementName);
            RequestScope request = statement.Statement.Sql.GetRequestScope(statement, param, session);
            statement.PreparedCommand.Create(request, session, statement.Statement, param);

            using (request.IDbCommand)
            {
                using(IDataReader reader = request.IDbCommand.ExecuteReader()) {
                    while (reader.Read())
                    {
                        IDictionary a = new Dictionary<object, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            a.Add(reader.GetName(i), reader.GetValue(i));
                        }
                        data.Add(a);
                    }   
                }
            }
            if (mapper.IsSessionStarted) mapper.CloseConnection();
            return data;
        }
    }
}