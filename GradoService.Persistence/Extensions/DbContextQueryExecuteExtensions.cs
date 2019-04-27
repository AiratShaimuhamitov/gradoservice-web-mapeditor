using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GradoService.Persistence.Extensions
{
    public static class DbContextQueryExecuteExtensions
    {
        /// <summary>
        /// Select from database using ADO.net tools by sql query
        /// </summary>
        /// <param name="dbContext">DbContext</param>
        /// <param name="sql">Sql query</param>
        /// <param name="parameters">Query parameters</param>
        /// <returns>IEnumerable of dictionary that represents a table in database</returns>
        public static IEnumerable<IDictionary<string, object>> CollectFromExecuteSql(this DbContext dbContext, string sql,
             Dictionary<string, Tuple<object, DbType>> parameters = null)
        {
            using (var cmd = dbContext.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = sql;
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                AddParameters(cmd, parameters);

                var queryResult = new List<Dictionary<string, object>>();
                using (var dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        var dataRow = new Dictionary<string, object>();
                        for (var fieldCount = 0; fieldCount < dataReader.FieldCount; fieldCount++)
                        {
                            try
                            {
                                dataRow.Add(dataReader.GetName(fieldCount), dataReader[fieldCount]);
                            }
                            catch (NotSupportedException)
                            {
                                dataRow.Add(dataReader.GetName(fieldCount), null); //TODO: ignored for a while, redevelop this
                            }
                        }

                        queryResult.Add(dataRow);
                    }
                }

                return queryResult;
            }
        }

        /// <summary>
        /// Execute custom sql query
        /// </summary>
        public static int ExecuteSqlQuery(this DbContext dbContext, string query,
            Dictionary<string, Tuple<object, DbType>> parameters = null)
        {
            int handledRowId = -1;
            using (var dbConnection = dbContext.Database.GetDbConnection())
            {
                using (var dbCommand = dbConnection.CreateCommand())
                {
                    AddParameters(dbCommand, parameters);

                    dbCommand.CommandText = query;
                    dbConnection.Open();
                    object res = dbCommand.ExecuteScalar();
                    handledRowId = res != null ? (int)res : -1;
                }
            }
            return handledRowId;
        }

        /// <summary>
        /// Execute custom sql query async
        /// </summary>
        public static async Task<int> ExecuteSqlQueryAsync(this DbContext dbContext, string query,
            Dictionary<string, Tuple<object, DbType>> parameters = null)
        {
            int handledRowId = -1;
            using (var dbConnection = dbContext.Database.GetDbConnection())
            {
                using (var dbCommand = dbConnection.CreateCommand())
                {
                    AddParameters(dbCommand, parameters);

                    dbCommand.CommandText = query;
                    dbConnection.Open();
                    object res = await dbCommand.ExecuteScalarAsync();
                    handledRowId = res != null ? (int)res : -1;
                }
            }
            return handledRowId;
        }

        private static void AddParameters(DbCommand command, Dictionary<string, Tuple<object, DbType>> parameters)
        {
            if (parameters != null)
            {
                foreach (var param in parameters)
                {
                    if (param.Value.Item1 == null) continue;

                    DbParameter dbParameter = command.CreateParameter();
                    dbParameter.ParameterName = param.Key;
                    dbParameter.DbType = param.Value.Item2;
                    dbParameter.Value = param.Value.Item1;
                    command.Parameters.Add(dbParameter);
                }
            }
        }
    }
}
