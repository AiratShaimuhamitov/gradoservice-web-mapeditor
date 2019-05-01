using GradoService.Domain.Entities.Metadata;
using GradoService.Domain.Entities.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GradoService.Persistence.CommandBuilder
{
    public class CrudCommandDirector
    {
        private readonly SqlCommandBuilder sqlCommandBuilder;

        public SqlCommandBuilder CommandBuilder { get { return sqlCommandBuilder; }  }

        public CrudCommandDirector(SqlCommandBuilder sqlCommandBuilder)
        {
            this.sqlCommandBuilder = sqlCommandBuilder;
        }

        /// <summary>
        /// Builds insert command with data from row
        /// </summary>
        public virtual string BuildInsertCommand(Table table, Row insertingRow)
        {
            sqlCommandBuilder.CreateInsertQuery(table, insertingRow);
            sqlCommandBuilder.AddReturnField(table.Fields.First(x => x.Name == table.Key));
            return sqlCommandBuilder.CompleteQuery();
        }


        /// <summary>
        /// Builds insert command without data from row. Use this command with parameters.
        /// </summary>
        public virtual string BuildInsertCommandParameterized(Table table, Row insertingRow)
        {
            sqlCommandBuilder.CreateInsertQueryParameterized(table, insertingRow);
            sqlCommandBuilder.AddReturnField(table.Fields.First(x => x.Name == table.Key));
            return sqlCommandBuilder.CompleteQuery();
        }

        public virtual string BuildUpdateCommand(Table table, Row updatingRow)
        {
            sqlCommandBuilder.CreateUpdateQuery(table, updatingRow);
            sqlCommandBuilder.AddCondition(table.Fields.First(x => x.Name == table.Key), 
                                               updatingRow.Data.First(x => x.Key.Name == table.Key).Value.ToString());
            return sqlCommandBuilder.CompleteQuery();
        }

        /// <summary>
        /// Build update command without data from row. Use this command with parameters.
        /// </summary>
        public virtual string BuildUpdateCommandParameterized(Table table, Row updatingRow)
        {
            sqlCommandBuilder.CreateUpdateQueryParameterized(table, updatingRow);
            sqlCommandBuilder.AddCondition(table.Fields.First(x => x.Name == table.Key),
                                                 updatingRow.Data.First(x => x.Key.Name == table.Key).Value.ToString());
            return sqlCommandBuilder.CompleteQuery();
        }

        public virtual string BuildDeleteCommand(Table table, int deletingRowId)
        {
            sqlCommandBuilder.CreateDeleteQuery(table);
            sqlCommandBuilder.AddCondition(table.Fields.First(x => x.Name == table.Key), deletingRowId.ToString());
            return sqlCommandBuilder.CompleteQuery();
        }

        public virtual string BuildSelectCommand(Table table, string viewQuery = null)
        {
            if(string.IsNullOrEmpty(viewQuery))
            {
                sqlCommandBuilder.CreateSelectQuery(table);
            }
            else
            {
                sqlCommandBuilder.CreateCustomQuery(viewQuery);
            }

            sqlCommandBuilder.AddOrdering(table.Fields.First(x => x.Name == table.Key));
            return sqlCommandBuilder.CompleteQuery();
        }

        public virtual string BuildPaginationSelectCommand(Table table, int offset, int limit, string viewQuery = null)
        {
            if (string.IsNullOrEmpty(viewQuery))
            {
                sqlCommandBuilder.CreateSelectQuery(table);
            }
            else
            {
                sqlCommandBuilder.CreateCustomQuery(viewQuery);
            }

            sqlCommandBuilder.AddOrdering(table.Fields.First(x => x.Name == table.Key));
            sqlCommandBuilder.AddSelectLimit(limit);
            sqlCommandBuilder.AddSelectOffset(offset);
            return sqlCommandBuilder.CompleteQuery();
        }

        public virtual string BuildSelectSpecificRow(Table table, int rowId, string viewQuery = null)
        {
            if (string.IsNullOrEmpty(viewQuery))
            {
                sqlCommandBuilder.CreateSelectQuery(table);
            }
            else
            {
                sqlCommandBuilder.CreateCustomQuery(viewQuery);
            }          
            sqlCommandBuilder.AddCondition(table.Fields.Where(x => x.Name == table.Key).FirstOrDefault(), rowId.ToString());
            return sqlCommandBuilder.CompleteQuery();
        }

        public virtual string BuildSelectViewByName(Table table, int rowId, string viewName)
        {
            sqlCommandBuilder.CreateViewQueryByName(table, viewName);
            sqlCommandBuilder.AddCondition(table.Fields.Where(x => x.Name == "id_obj").FirstOrDefault(), rowId.ToString());
            return sqlCommandBuilder.CompleteQuery();
        }

        /// <summary>
        /// Builds select query for all files in table
        /// </summary>
        /// <param name="table">Table</param>
        /// <param name="viewName">Name of view in database</param>
        /// <returns>Query</returns>
        public virtual string BuildSelectViewByName(Table table, string viewName)
        {
            sqlCommandBuilder.CreateViewQueryByName(table, viewName);
            return sqlCommandBuilder.CompleteQuery();
        }

        public virtual string BuildPaginationSelectViewByName(Table table, int offset, int limit, string viewName = null)
        {
            if (string.IsNullOrEmpty(viewName))
            {
                sqlCommandBuilder.CreateSelectQuery(table);
            }
            else
            {
                sqlCommandBuilder.CreateViewQueryByName(table, viewName);
            }

            sqlCommandBuilder.AddOrdering(table.Fields.First(x => x.Name == table.Key));
            sqlCommandBuilder.AddSelectLimit(limit);
            sqlCommandBuilder.AddSelectOffset(offset);
            return sqlCommandBuilder.CompleteQuery();
        }

        /// <summary>
        /// Builds select query for one speceific file
        /// </summary>
        /// <param name="table">Table</param>
        /// <param name="rowId">Row id</param>
        /// <param name="fileId">File id</param>
        /// <param name="viewName">Name of view in database</param>
        /// <returns>Query</returns>
        public virtual string BuildSelectViewByName(Table table, int rowId, int fileId, string viewName)
        {
            sqlCommandBuilder.CreateViewQueryByName(table, viewName);
            sqlCommandBuilder.AddCondition(table.Fields.Where(x => x.Name == "id_obj").FirstOrDefault(), rowId.ToString());
            sqlCommandBuilder.AddCondition(table.Fields.Where(x => x.Name == table.Key).FirstOrDefault(), fileId.ToString());
            return sqlCommandBuilder.CompleteQuery();
        }
    }
}
