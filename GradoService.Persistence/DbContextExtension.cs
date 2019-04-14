using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Dynamic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GradoService.Persistence
{
    public static class DbContextExtensions
    {
        /// <summary>
        /// Select from database using ADO.net tools by sql query
        /// </summary>
        /// <param name="dbContext">DbContext</param>
        /// <param name="sql">Sql query</param>
        /// <param name="parameters">Query parameters</param>
        /// <returns>IEnumerable of dictionary that represents a database table</returns>
        public static IEnumerable<IDictionary<string, object>> CollectFromExecuteSql(this DbContext dbContext, string sql,
            Dictionary<string, object> parameters = null)
        {
            using (var cmd = dbContext.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = sql;
                if (cmd.Connection.State != ConnectionState.Open)
                    cmd.Connection.Open();

                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> param in parameters)
                    {
                        DbParameter dbParameter = cmd.CreateParameter();
                        dbParameter.ParameterName = param.Key;
                        dbParameter.Value = param.Value;
                        cmd.Parameters.Add(dbParameter);
                    }
                }

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

        public static int ExecuteSqlQuery(this DbContext dbContext, string query)
        {
            int insertedRawId = -1;
            using (var dbConnection = dbContext.Database.GetDbConnection())
            {
                using (var dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText = query;
                    dbConnection.Open();
                    object res = dbCommand.ExecuteScalar();
                    insertedRawId = res != null ? (int)res : -1;
                }
            }
            return insertedRawId;
        }

        public static async Task<int> ExcecuteSqlQueryAsync(this DbContext dbContext, string query)
        {
            int insertedRawId = -1;
            using (var dbConnection = dbContext.Database.GetDbConnection())
            {
                using (var dbCommand = dbConnection.CreateCommand())
                {
                    dbCommand.CommandText = query;
                    dbConnection.Open();
                    object res = await dbCommand.ExecuteScalarAsync();
                    insertedRawId = res != null ? (int)res : -1;
                }
            }
            return insertedRawId;
        }
    }
}
