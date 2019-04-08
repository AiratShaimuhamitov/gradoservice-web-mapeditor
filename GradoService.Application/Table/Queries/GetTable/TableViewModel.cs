using GradoService.Application.Table.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Application.Table.Queries.GetTable
{
    public class TableViewModel
    {
        public TableDto Table { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
