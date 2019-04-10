using GradoService.Application.Tables.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Application.Tables.Queries.GetTable
{
    public class TableViewModel
    {
        public TableInfoDto Table { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
