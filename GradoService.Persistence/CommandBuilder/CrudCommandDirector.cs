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

        public virtual string BuildInsertCommand(Table table, Row insertingRow)
        {
            sqlCommandBuilder.CreateInsertQuery(table, insertingRow);
            sqlCommandBuilder.AddReturnField(table.Fields.First(x => x.Name == "gid"));
            return sqlCommandBuilder.CompleteQuery();
        }

        public virtual string BuildUpdateCommand(Table table, Row updatingRow)
        {
            sqlCommandBuilder.CreateUpdateQuery(table, updatingRow);
            sqlCommandBuilder.AddCondition(table.Fields.First(x => x.Name == "gid"), 
                                               updatingRow.Data.First(x => x.Key.Name == "gid").Value.ToString());
            return sqlCommandBuilder.CompleteQuery();
        }

        public virtual string BuildDeleteCommand(Table table, int deletingRowId)
        {
            sqlCommandBuilder.CreateDeleteQuery(table);
            sqlCommandBuilder.AddCondition(table.Fields.First(x => x.Name == "gid"), deletingRowId.ToString());
            return sqlCommandBuilder.CompleteQuery();
        }

        /// <summary>
        /// Builds select command
        /// </summary>
        /// <param name="table">Table for query</param>
        /// <param name="viewQuery">Optional select to view query, if exists will use it</param>
        /// <returns>Select command that affects all data</returns>
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

            sqlCommandBuilder.AddOrdering(table.Fields.First(x => x.Name == "gid"));
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

            sqlCommandBuilder.AddOrdering(table.Fields.First(x => x.Name == "gid"));
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
            sqlCommandBuilder.AddCondition(table.Fields.Where(x => x.Name == "gid").FirstOrDefault(), rowId.ToString());
            return sqlCommandBuilder.CompleteQuery();
        }
    }
}
