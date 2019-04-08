using AutoMapper;
using GradoService.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GradoService.Application.Table.Model;
using Microsoft.EntityFrameworkCore;

namespace GradoService.Application.Table.Queries.GetAllTables
{
    public class GetAllTablesQueryHandler : IRequestHandler<GetAllTablesQuery, TablesViewModel>
    {
        private readonly MetadataDbContext _metadataDbContext;
        private readonly IMapper _mapper;

        public GetAllTablesQueryHandler(MetadataDbContext metadataDbContext, IMapper mapper)
        {
            _metadataDbContext = metadataDbContext;
            _mapper = mapper;
        }

        public async Task<TablesViewModel> Handle(GetAllTablesQuery request, CancellationToken cancellationToken)
        {
            var tables = _mapper.Map<IEnumerable<TableDto>>(await _metadataDbContext.TableInfos
                                                                        .Include(x => x.FieldInfos).ToListAsync());

            var viewModel = new TablesViewModel
            {
                CreateEnabled = false,
                Tables = tables
            };

            return viewModel;
        }
    }
}
