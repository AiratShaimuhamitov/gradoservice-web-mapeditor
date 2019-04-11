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

            Assert.Equal("SELECT * FROM data.Car".ToLower(), sqlQuery.ToLower());
        }

        [Fact]
        public void TestCreateSelectCommandWithCondition()
        {
            _sqlCommandBuilder.CreateSelectQuery(_table);

            var idField = _table.Fields.FirstOrDefault(x => x.Name == "Id");
            _sqlCommandBuilder.AddCondition(idField, "0");
            var sqlQuery = _sqlCommandBuilder.CompleteQuery();

            Assert.Equal("SELECT * FROM data.Car WHERE Id = '0'".ToLower(), sqlQuery.ToLower());
        }

        [Fact]
        public void TestCreateSelectCommandWithDoubleCondition()
        {
            _sqlCommandBuilder.CreateSelectQuery(_table);

            var idField = _table.Fields.FirstOrDefault(x => x.Name == "Id");
            _sqlCommandBuilder.AddCondition(idField, "0");

            var brandField = _table.Fields.FirstOrDefault(x => x.Name == "Brand");
            _sqlCommandBuilder.AddCondition(brandField, "Audi");
            var sqlQuery = _sqlCommandBuilder.CompleteQuery();

            Assert.Equal("SELECT * FROM data.Car WHERE Id = '0' and Brand = 'Audi'", sqlQuery);
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

            Assert.Equal("DELETE * FROM data.Car WHERE Id = '0'", sqlQuery);
        }

        [Fact]
        public void TestCreateInsertCommand()
        {
            _sqlCommandBuilder.CreateInsertQuery(_table, _table.Rows.FirstOrDefault());
            var sqlQuery = _sqlCommandBuilder.CompleteQuery();

            Assert.Equal("INSERT INTO data.Car(Id, Brand, Cost) VALUES('0', 'Audi', '1000')", sqlQuery);
        }

        [Fact]
        public void TestCreateUpdateCommand()
        {
            _sqlCommandBuilder.CreateUpdateQuery(_table, _table.Rows.FirstOrDefault());
            var sqlQuery = _sqlCommandBuilder.CompleteQuery();

            Assert.Equal("UPDATE data.Car SET Id = '0', Brand = 'Audi', Cost = '1000'", sqlQuery);
        }

        [Fact]
        public void TestCreateUpdateCommandWithCondition()
        {
            _sqlCommandBuilder.CreateUpdateQuery(_table, _table.Rows.FirstOrDefault());

            var idField = _table.Fields.FirstOrDefault(x => x.Name == "Id");
            _sqlCommandBuilder.AddCondition(idField, "0");
            var sqlQuery = _sqlCommandBuilder.CompleteQuery();

            Assert.Equal("UPDATE data.Car SET Id = '0', Brand = 'Audi', Cost = '1000' WHERE Id = '0'", sqlQuery);
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
