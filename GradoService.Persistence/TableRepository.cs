using AutoMapper;
using GradoService.Domain.Entities.Table;
using GradoService.Persistence.CommandBuilder;
using GradoService.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            var table = _mapper.Map<Table>(tableMeta);

            string selectQuery;
            if (offset == 0 && limit == 0)
            {
                selectQuery = _commandDirector.BuildSelectViewByName(table, tableMeta.ViewName);
            }
            else
            {
                selectQuery = _commandDirector.BuildPaginationSelectViewByName(table, offset, limit, tableMeta.ViewName);
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

            var table = _mapper.Map<Table>(tableMeta);

            var insertQuery = _commandDirector.BuildInsertCommand(table, insertingRow);

            var insertedRowId = _dbContext.ExecuteSqlQuery(insertQuery);

            return insertedRowId;
        }

        public async Task UpdateData(int tableId, Row updatingRow)
        {
            var tableMeta = _dbContext.TableInfos.Where(x => x.Id == tableId)
                .Include(x => x.FieldInfos).First();

            var table = _mapper.Map<Table>(tableMeta);

            var updateQuery = _commandDirector.BuildUpdateCommand(table, updatingRow);

            await _dbContext.ExecuteSqlQueryAsync(updateQuery);
        }

        public async Task DeleteData(int tableId, int deletingRowId)
        {
            var tableMeta = _dbContext.TableInfos.Where(x => x.Id == tableId)
                .Include(x => x.FieldInfos).First();

            var table = _mapper.Map<Table>(tableMeta);

            var deleteQuery = _commandDirector.BuildDeleteCommand(table, deletingRowId);

            await _dbContext.ExecuteSqlQueryAsync(deleteQuery);
        }
    }
}
