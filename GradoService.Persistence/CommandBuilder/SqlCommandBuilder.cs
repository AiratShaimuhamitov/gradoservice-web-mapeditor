using GradoService.Domain.Entities.Table;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Persistence.CommandBuilder
{
    public abstract class SqlCommandBuilder
    {
        protected StringBuilder _stringBuilder;

        public SqlCommandBuilder()
        {
            _stringBuilder = new StringBuilder();
        }

        public abstract void CreateSelectQuery(Table table);

        public abstract void CreateUpdateQuery(Table table, Row updatingRow);

        public abstract void CreateDeleteQuery(Table table);

        public abstract void CreateInsertQuery(Table table, Row instertingRow);

        public abstract void AddCondition(Field field, string value);

        public abstract void AddOrdering(Field field);

        public abstract void AddSelectLimit(int limit);

        public abstract void AddSelectOffset(int offset);

        public abstract void AddOrderingByDescending(Field field);

        public abstract void AddReturnField(Field field);

        public abstract void CreateCustomQuery(string query);

        public abstract void CreateViewQueryByName(Table table, string viewName);

        public virtual string CompleteQuery()
        {
            var result = _stringBuilder.ToString();
            _stringBuilder.Clear();

            return result;
        }
    }
}
