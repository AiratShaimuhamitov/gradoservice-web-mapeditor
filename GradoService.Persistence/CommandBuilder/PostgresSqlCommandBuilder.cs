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
        private bool _isOrderingAppended;

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

            _stringBuilder.AppendFormat(" AND {0} = '{1}'", field.Name, value);
        }

        public override void AddOrdering(Field field)
        {
            if(!_isOrderingAppended)
            {
                _isOrderingAppended = true;
                _stringBuilder.AppendFormat(" ORDER BY {0}", field.Name);
            }
        }

        public override void AddOrderingByDescending(Field field)
        {
            if (!_isOrderingAppended)
            {
                _isOrderingAppended = true;
                _stringBuilder.AppendFormat(" ORDER BY {0} DESC", field.Name);
            }
        }

        public override void CreateDeleteQuery(Table table)
        {
            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("DELETE FROM {0}.{1}", table.Schema, table.Name);
        }

        public override void CreateInsertQuery(Table table, Row insertingRow)
        {
            _stringBuilder.Clear();
            _stringBuilder.AppendFormat("INSERT INTO {0}.{1}(", table.Schema, table.Name);

            var insertingFields = insertingRow.Data.Where(x => x.Value != null).Select(x => x.Key);
            for(int i = 0; i < insertingFields.Count() - 1; i++)
            { 
                _stringBuilder.Append(insertingFields.ElementAt(i).Name + ", ");
            }
            _stringBuilder.Append(insertingFields.Last().Name + ") ");

            _stringBuilder.Append("VALUES(");

            for(int i = 0; i < insertingFields.Count() - 1; i++)
            {
                var value = insertingRow.Data[insertingFields.ElementAt(i)];
                
                _stringBuilder.AppendFormat("'{0}', ", value == null ? "" : value.ToString());
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

            var updatingFields = updatingRow.Data.Where(x => x.Value != null).Select(x => x.Key);
            for (int i = 0; i < updatingFields.Count() - 1; i++)
            {
                var field = updatingFields.ElementAt(i);
                _stringBuilder.AppendFormat("{0} = '{1}', ", field.Name, updatingRow.Data[field].ToString());
            }

            _stringBuilder.AppendFormat("{0} = '{1}'", updatingRow.Data.Last().Key.Name, updatingRow.Data.Last().Value.ToString());
        }

        public override void AddReturnAffectedId(Field idField)
        {
            _stringBuilder.AppendFormat(" RETURNING {0}", idField.Name);
        }

        public override void CreateCustomQuery(string query)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(query);
        }

        public override string CompleteQuery()
        {
            _isConditionAppended = false;
            _isOrderingAppended = false;
            return base.CompleteQuery();
        }
    }
}
