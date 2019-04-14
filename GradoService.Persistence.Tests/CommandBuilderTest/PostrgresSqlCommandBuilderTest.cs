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
        private CrudCommandDirector _crudCommandDirector;

        public PostrgresSqlCommandBuilderTest()
        {
            _table = InitializeTable();
            _sqlCommandBuilder = new PostgresSqlCommandBuilder();
            _crudCommandDirector = new CrudCommandDirector(_sqlCommandBuilder);
        }

        [Fact]
        public void TestCreateSelectCommand()
        {
            var sqlQuery = _crudCommandDirector.BuildSelectCommand(_table);

            Assert.Equal("SELECT * FROM data.Car ORDER BY gid", sqlQuery);
        }

        [Fact]
        public void TestCreateSelectCommandWithCondition()
        {
            var sqlQuery = _crudCommandDirector.BuildSelectSpecificRow(_table, 0);

            Assert.Equal("SELECT * FROM data.Car WHERE gid = '0'", sqlQuery);
        }

        [Fact]
        public void TestCreateSelectCommandWithDoubleCondition()
        {
            _sqlCommandBuilder.CreateSelectQuery(_table);

            var idField = _table.Fields.FirstOrDefault(x => x.Name == "gid");
            _sqlCommandBuilder.AddCondition(idField, "0");

            var brandField = _table.Fields.FirstOrDefault(x => x.Name == "Brand");
            _sqlCommandBuilder.AddCondition(brandField, "Audi");
            var sqlQuery = _sqlCommandBuilder.CompleteQuery();

            Assert.Equal("SELECT * FROM data.Car WHERE gid = '0' AND Brand = 'Audi'", sqlQuery);
        }

        [Fact]
        public void TestSelectCommandWithPagination()
        {
            var query = _crudCommandDirector.BuildPaginationSelectCommand(_table, 10, 10);

            Assert.Equal("SELECT * FROM data.Car ORDER BY gid LIMIT 10 OFFSET 10", query);
        }

        [Fact]
        public void TestCreateDeleteCommand()
        {
            _sqlCommandBuilder.CreateDeleteQuery(_table);
            var sqlQuery = _sqlCommandBuilder.CompleteQuery();

            Assert.Equal("DELETE FROM data.Car", sqlQuery);
        }

        [Fact]
        public void TestCreateDeleteCommandWithCondition()
        {
            var sqlQuery = _crudCommandDirector.BuildDeleteCommand(_table, 0);

            Assert.Equal("DELETE FROM data.Car WHERE gid = '0'", sqlQuery);
        }

        [Fact]
        public void TestCreateInsertCommand()
        {
            _sqlCommandBuilder.CreateInsertQuery(_table, _table.Rows.FirstOrDefault());
            var sqlQuery = _sqlCommandBuilder.CompleteQuery();

            Assert.Equal("INSERT INTO data.Car(gid, Brand, Cost) VALUES('0', 'Audi', '1000')", sqlQuery);
        }

        [Fact]
        public void TestCreateUpdateCommand()
        {
            _sqlCommandBuilder.CreateUpdateQuery(_table, _table.Rows.FirstOrDefault());
            var sqlQuery = _sqlCommandBuilder.CompleteQuery();

            Assert.Equal("UPDATE data.Car SET gid = '0', Brand = 'Audi', Cost = '1000'", sqlQuery);
        }

        [Fact]
        public void TestCreateUpdateCommandWithCondition()
        {
            var sqlQuery = _crudCommandDirector.BuildUpdateCommand(_table, _table.Rows.FirstOrDefault());

            Assert.Equal("UPDATE data.Car SET gid = '0', Brand = 'Audi', Cost = '1000' WHERE gid = '0'", sqlQuery);
        }
        
        [Fact]
        public void TestInsertCommandWithReturnAffectedId()
        {
            var insertingRow = _table.Rows.FirstOrDefault();
            var query = _crudCommandDirector.BuildInsertCommand(_table, insertingRow);

            Assert.Equal("INSERT INTO data.Car(gid, Brand, Cost) VALUES('0', 'Audi', '1000') RETURNING gid", query);
        }

        private Table InitializeTable()
        {
            var idFeild = new Field { Name = "gid", Type = "INTEGER" };
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
