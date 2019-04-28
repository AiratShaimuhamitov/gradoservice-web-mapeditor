using GradoService.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Application.Tables.Queries.GetTable
{
    public class GetTableQuery : IRequest<TableViewModel>, ITableRequest
    {
        public int TableId { get; set; }
    }
}
