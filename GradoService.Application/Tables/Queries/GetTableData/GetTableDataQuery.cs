﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Application.Tables.Queries.GetTableData
{
    public class GetTableDataQuery : IRequest<TableDataViewModel>
    {
        public int TableId { get; set; }

        public int Offset { get; set; }

        public int Limit { get; set; }
    }
}
