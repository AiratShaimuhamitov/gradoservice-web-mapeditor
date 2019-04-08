using AutoMapper;
using GradoService.Application.Exceptions;
using GradoService.Application.Table.Model;
using GradoService.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GradoService.Application.Table.Queries.GetTable
{
    public class GetTableQueryHandler : IRequestHandler<GetTableQuery, TableViewModel>
    {
        private readonly MetadataDbContext _metadataDbContext;
        private readonly IMapper _mapper;

        public GetTableQueryHandler(MetadataDbContext metadataDbContext, IMapper mapper)
        {
            _metadataDbContext = metadataDbContext;
            _mapper = mapper;
        }

        public async Task<TableViewModel> Handle(GetTableQuery request, CancellationToken cancellationToken)
        {
            var tableMeta = await _metadataDbContext.TableInfos.Where(x => x.Id == request.Id)
                                                .Include(x => x.FieldInfos)
                                                .SingleOrDefaultAsync();
            if(tableMeta == null)
            {
                throw new NotFoundException("Table", request.Id);
            }
            var table = _mapper.Map<TableDto>(tableMeta);

            var viewModel = new TableViewModel
            {
                CreateEnabled = false,
                Table = table
            };

            return viewModel;
        }
    }
}
