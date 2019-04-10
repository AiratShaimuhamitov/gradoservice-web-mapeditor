using GradoService.Application.Exceptions;
using GradoService.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GradoService.Application.Tables.Queries.GetTableData
{
    public class GetTableDataQueryHandler : IRequestHandler<GetTableDataQuery, TableDataViewModel>
    {
        public TableRepository _tableRepository;

        public GetTableDataQueryHandler(TableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }

        public async Task<TableDataViewModel> Handle(GetTableDataQuery request, CancellationToken cancellationToken)
        {
            var table = await _tableRepository.GetTableData(request.TableId);

            if(table == null)
            {
                throw new NotFoundException("table", request.TableId);
            }

            return new TableDataViewModel()
            {
                Table = table,
                CreateEnabled = true
            };
        }
    }
}
