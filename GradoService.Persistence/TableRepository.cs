using AutoMapper;
using GradoService.Domain.Entities.Table;
using GradoService.Persistence.CommandBuilder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradoService.Persistence.Exceptions;

namespace GradoService.Persistence
{
    public class TableRepository
    {
        private readonly GradoServiceDbContext _dbContext;
        private readonly CrudCommandDirector _commandDirector;
        private readonly IMapper _mapper;

        public TableRepository(GradoServiceDbContext dbContext, CrudCommandDirector commandDirector, IMapper mapper)
        {
            _dbContext = dbContext;
            _commandDirector = commandDirector;
            _mapper = mapper;
        }

        public async Task<Table> GetTableData(int tableId, int offset = 0, int limit = 0)
        {
            var tableMeta = await _dbContext.TableInfos.Where(x => x.Id == tableId)
                .Include(x => x.FieldInfos)
                    .ThenInclude(x => x.FieldType)
                .FirstAsync();

            if (tableMeta == null) throw new TableNotFoundException(tableId);

            var table = _mapper.Map<Table>(tableMeta);

            string selectQuery;
            if (offset == 0 && limit == 0)
            {
                selectQuery = _commandDirector.BuildSelectCommand(table, tableMeta.ViewQuery);
            }
            else
            {
                selectQuery = _commandDirector.BuildPaginationSelectCommand(table, offset, limit, tableMeta.ViewQuery);
            }
            var unhandledRows = _dbContext.CollectFromExecuteSql(selectQuery);

            var rows = new List<Row>();
            foreach (var unhandledRow in unhandledRows)
            {
                var row = new Row { TableId = table.Id };

                foreach (var field in table.Fields)
                {
                    unhandledRow.TryGetValue(field.Name, out object obj);
                    if (obj != null)
                    {
                        row.Data[field] = obj;
                    }
                }
                rows.Add(row);
            }

            table.Rows = rows;

            return table;
        }

        public async Task<Row> GetTableRow(int tableId, int rowId)
        {
            var tableMeta = await _dbContext.TableInfos.Where(x => x.Id == tableId)
                .Include(x => x.FieldInfos).FirstAsync();

            if (tableMeta == null) throw new TableNotFoundException(tableId);

            var table = _mapper.Map<Table>(tableMeta);

            var selectSpecificRowQuery = _commandDirector.BuildSelectSpecificRow(table, rowId, tableMeta.ViewQuery);

            var unhandledRow = _dbContext.CollectFromExecuteSql(selectSpecificRowQuery).First();

            var row = new Row { TableId = table.Id };
            foreach (var field in table.Fields)
            {
                unhandledRow.TryGetValue(field.Name, out object obj);
                if (obj != null)
                {
                    row.Data[field] = obj;
                }
            }

            return row;
        }

        public async Task<int> InsertData(int tableId, Row insertingRow)
        {
            var tableMeta = await _dbContext.TableInfos.Where(x => x.Id == tableId)
                .Include(x => x.FieldInfos).FirstAsync();

            if (tableMeta == null) throw new TableNotFoundException(tableId);

            var table = _mapper.Map<Table>(tableMeta);

            var insertQuery = _commandDirector.BuildInsertCommand(table, insertingRow);

            var insertedRowId = _dbContext.ExecuteSqlQuery(insertQuery);

            return insertedRowId;
        }


        public async Task UpdateData(int tableId, Row updatingRow)
        {
            var tableMeta = _dbContext.TableInfos.Where(x => x.Id == tableId)
                .Include(x => x.FieldInfos).First();

            if (tableMeta == null) throw new TableNotFoundException(tableId);

            var table = _mapper.Map<Table>(tableMeta);

            var updateQuery = _commandDirector.BuildUpdateCommand(table, updatingRow);

            await _dbContext.ExcecuteSqlQueryAsync(updateQuery);
        }

        public async Task DeleteData(int tableId, int deletingRowId)
        {
            var tableMeta = _dbContext.TableInfos.Where(x => x.Id == tableId)
                .Include(x => x.FieldInfos).First();

            if (tableMeta == null) throw new TableNotFoundException(tableId);

            var table = _mapper.Map<Table>(tableMeta);

            var deleteQuery = _commandDirector.BuildDeleteCommand(table, deletingRowId);

            await _dbContext.ExcecuteSqlQueryAsync(deleteQuery);
        }


    }
}
