using GradoService.Application.Interfaces;
using GradoService.Application.Tables.Model;
using MediatR;
using System.Collections.Generic;

namespace GradoService.Application.Tables.Queries.GetTableData
{
    public class GetTableDataQuery : IRequest<IEnumerable<IDictionary<string, object>>>, ITableRequest
    {
        public int TableId { get; set; }

        public int Offset { get; set; }

        public int Limit { get; set; }
    }
}
