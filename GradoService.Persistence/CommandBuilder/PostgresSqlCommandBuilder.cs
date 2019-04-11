using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GradoService.Domain.Entities.Table;

namespace GradoService.Persistence.CommandBuilder
{
    public class PostgresSqlCommandBuilder : SqlCommandBuilder
    {
        private bool _isConditionAppended;

        public PostgresSqlCommandBuilder()
        {
            _isConditionAppended = false;
        }

        public override void AddCondition(Field field, string value)
        {
            if(!_isConditionAppended)
            {
                _stringBuilder.AppendFormat(" WHERE {0} = '{1}'", field.Name, value);
                _isConditionAppended = true;
                return;
            }

            _stringBuilder.AppendFormat(" and {0} = '{1}'", field.Name, value);
        }

        public override void CreateDeleteQuery(Table table)
        {
            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("DELETE * FROM {0}.{1}", table.Schema, table.Name);
        }

        public override void CreateInsertQuery(Table table, Row insertingRow)
        {
            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("INSERT INTO {0}.{1}(", table.Schema, table.Name);

            var fields = table.Fields.ToList();
            for(int i = 0; i < fields.Count() - 1; i++)
            { 
                _stringBuilder.Append(fields[i].Name + ", ");
            }
            _stringBuilder.Append(fields.Last().Name + ") ");

            _stringBuilder.Append("VALUES(");

            for(int i = 0; i < insertingRow.Data.Keys.Count() - 1; i++)
            {
                _stringBuilder.AppendFormat("'{0}', ", insertingRow.Data.ElementAt(i).Value.ToString());
            }
            _stringBuilder.AppendFormat("'{0}')", insertingRow.Data.Last().Value.ToString());
        }

        public override void CreateSelectQuery(Table table)
        {
            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("SELECT * FROM {0}.{1}", table.Schema, table.Name);
        }

        public override void CreateUpdateQuery(Table table, Row updatingRow)
        {
            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("UPDATE {0}.{1} ", table.Schema, table.Name)
                .Append("SET ");

            for(int i = 0; i < updatingRow.Data.Keys.Count() - 1; i++)
            {
                _stringBuilder.AppendFormat("{0} = '{1}', ", updatingRow.Data.ElementAt(i).Key.Name, updatingRow.Data.ElementAt(i).Value.ToString());
            }

            _stringBuilder.AppendFormat("{0} = '{1}'", updatingRow.Data.Last().Key.Name, updatingRow.Data.Last().Value.ToString());
        }

        public override void CreateCustomQuery(string query)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(query);
        }
    }
}
