using GradoService.Application.Interfaces;
using MediatR;

namespace GradoService.Application.Tables.Queries.GetTableData
{
    public class GetTableDataQuery : IRequest<TableDataViewModel>, ITableRequest
    {
        public int TableId { get; set; }

        public int Offset { get; set; }

        public int Limit { get; set; }
    }
}
