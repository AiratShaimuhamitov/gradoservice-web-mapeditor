using System;
using System.Collections.Generic;
using System.Text;
using GradoService.Application.Tables.Model;
using GradoService.Domain.Entities.Table;

namespace GradoService.Application.Tables.Queries.GetTableData
{
    public class TableDataViewModel
    {
        public TableDto Table { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
