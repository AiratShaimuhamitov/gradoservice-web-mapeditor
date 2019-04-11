using AutoMapper;
using GradoService.Application.Exceptions;
using GradoService.Application.Tables.Model;
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
        private readonly TableRepository _tableRepository;
        private readonly IMapper _mapper;

        public GetTableDataQueryHandler(TableRepository tableRepository, IMapper mapper)
        {
            _tableRepository = tableRepository;
            _mapper = mapper;
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
                Table = _mapper.Map<TableDto>(table),
                CreateEnabled = true
            };
        }
    }
}
