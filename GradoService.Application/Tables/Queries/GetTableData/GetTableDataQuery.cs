using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Application.Tables.Queries.GetTableData
{
    public class GetTableDataQuery : IRequest<TableDataViewModel>
    {
        public int TableId { get; set; }
    }
}
