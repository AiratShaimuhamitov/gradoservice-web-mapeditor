﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Dynamic;
using System.Text;
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

                foreach (KeyValuePair<string, object> param in parameters)
                {
                    DbParameter dbParameter = cmd.CreateParameter();
                    dbParameter.ParameterName = param.Key;
                    dbParameter.Value = param.Value;
                    cmd.Parameters.Add(dbParameter);
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
                                //TODO: ignored for a while, redevelop this
                            }
                        }

                        queryResult.Add(dataRow);
                    }
                }

                return queryResult;
            }
        }
    }
}