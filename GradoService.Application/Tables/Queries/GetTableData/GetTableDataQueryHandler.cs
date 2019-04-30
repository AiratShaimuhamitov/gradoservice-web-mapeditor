using AutoMapper;
using GradoService.Application.Exceptions;
using GradoService.Application.Tables.Model;
using GradoService.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GradoService.Application.Tables.Queries.GetTableData
{
    public class GetTableDataQueryHandler : IRequestHandler<GetTableDataQuery, IEnumerable<IDictionary<string, object>>>
    {
        private readonly TableRepository _tableRepository;

        public GetTableDataQueryHandler(TableRepository tableRepository)
        {
            _tableRepository = tableRepository;
        }

        public async Task<IEnumerable<IDictionary<string, object>>> Handle(GetTableDataQuery request, CancellationToken cancellationToken)
        {
            var table = await _tableRepository.GetTableData(request.TableId, request.Offset, request.Limit);

            return table.Rows.Select(x => x.Data.ToDictionary(k => k.Key.Name, v => v.Value));
        }
    }
}
