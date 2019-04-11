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

namespace GradoService.Persistence
{
    public class TableRepository    
    {
        private readonly GradoServiceDbContext _dbContext;
        private readonly SqlCommandBuilder _sqlCommandBuilder;
        private readonly IMapper _mapper;

        public TableRepository(GradoServiceDbContext dbContext, SqlCommandBuilder sqlCommandBuilder, IMapper mapper)
        {
            _dbContext = dbContext;
            _sqlCommandBuilder = sqlCommandBuilder;
            _mapper = mapper;
        } 

        public async Task<Table> GetTableData(int tableId)
        {
            var tableMeta = await _dbContext.TableInfos.Where(x => x.Id == tableId)
                .Include(x => x.FieldInfos)
                    .ThenInclude(x => x.FieldType)
                .FirstAsync();

            if (tableMeta == null) return null;

            var table = _mapper.Map<Table>(tableMeta);

//#region Replace with Director (element of builder pattern)
//            _sqlCommandBuilder.CreateSelectQuery(table);
//            var sqlQuery = _sqlCommandBuilder.CompleteQuery();
//#endregion

            var unhandledRows = _dbContext.CollectFromExecuteSql(tableMeta.ViewQuery);

            var rows = new List<Row>();
            foreach(var unhandledRow in unhandledRows)
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

        public Row GetTableRow(int tableId, int rowId)
        {
            throw new NotImplementedException();
        }

        public void InsertData(int tableId, Row insertingRow)
        {
            throw new NotImplementedException();
        }


        public void UpdateData(int tableId, Row updatingRow)
        {
            throw new NotImplementedException();
        }

        public void DeleteData(int tableId, int deletingRowId)
        {
            throw new NotImplementedException();
        }
    }
}
