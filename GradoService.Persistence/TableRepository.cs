using GradoService.Domain.Entities.Table;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Persistence
{
    public class TableRepository
    {
        private readonly DbContext _dbContext;
        private readonly ILogger<TableRepository> _logger;

        public TableRepository(DbContext dbContext, ILogger<TableRepository> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        } 

        public IEnumerable<Table> GetAllTables()
        {
            throw new NotImplementedException();
        }

        public Table GetTableDataByTableId(int id)
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
