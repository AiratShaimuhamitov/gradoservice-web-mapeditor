using System;
using System.Collections.Generic;
using System.Text;
using GradoService.Domain.Entities.Table;

namespace GradoService.Application.Tables.Queries.GetTableData
{
    public class TableDataViewModel
    {
        public Table Table { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
