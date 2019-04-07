using GradoService.Domain.Entities.Table;
using GradoService.Persistence.CommandBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace GradoService.Persistence.Tests.CommandBuilderTest
{
    public class PostrgresSqlCommandBuilderTest
    {
        private Table _table;
        private SqlCommandBuilder _sqlCommandBuilder;

        public PostrgresSqlCommandBuilderTest()
        {
            _table = InitializeTable();
            _sqlCommandBuilder = new PostgresSqlCommandBuilder();
        }

        [Fact]
        public void TestCreateSelectCommand()
        {
            _sqlCommandBuilder.CreateSelectQuery(_table);
            var sqlQuery = _sqlCommandBuilder.CompleteQuery();

            Assert.Equal("SELECT * FROM data.Car", sqlQuery);
        }

        [Fact]
        public void TestCreateDeleteCommand()
        {
            _sqlCommandBuilder.CreateDeleteQuery(_table);
            var sqlQuery = _sqlCommandBuilder.CompleteQuery();

            Assert.Equal("DELETE * FROM data.Car", sqlQuery);
        }

        [Fact]
        public void TestCreateDeleteCommandWithCondition()
        {
            _sqlCommandBuilder.CreateDeleteQuery(_table);

            var idField = _table.Fields.FirstOrDefault(x => x.Name == "Id");
            _sqlCommandBuilder.AddCondition(idField, "0");
            var sqlQuery = _sqlCommandBuilder.CompleteQuery();

            Assert.Equal("DELETE * FROM data.Car WHERE id = 0".ToLower(), sqlQuery.ToLower());
        }

        private Table InitializeTable()
        {
            var idFeild = new Field { Name = "Id", Type = "INTEGER" };
            var brandField = new Field { Name = "Brand", Type = "character varying" };
            var costFeild = new Field { Name = "Cost", Type = "numeric" };

            var fields = new List<Field> { idFeild, brandField, costFeild };

            var row = new Dictionary<Field, object>
            {
                [idFeild] = "0",
                [brandField] = "Audi",
                [costFeild] = "1000"
            };

            var rows = new List<Row>()
            {
                new Row {TableId = 0, Data = row  }
            };

            return new Table
            {
                Id = 0,
                Name = "Car",
                Schema = "data",
                Fields = fields,
                Rows = rows
            };
        }
    }
}
