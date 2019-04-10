using AutoMapper;
using GradoService.Application.Exceptions;
using GradoService.Application.Tables.Model;
using GradoService.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GradoService.Application.Tables.Queries.GetTable
{
    public class GetTableQueryHandler : IRequestHandler<GetTableQuery, TableViewModel>
    {
        private readonly GradoServiceDbContext _GradoServiceDbContext;
        private readonly IMapper _mapper;

        public GetTableQueryHandler(GradoServiceDbContext GradoServiceDbContext, IMapper mapper)
        {
            _GradoServiceDbContext = GradoServiceDbContext;
            _mapper = mapper;
        }

        public async Task<TableViewModel> Handle(GetTableQuery request, CancellationToken cancellationToken)
        {
            var tableMeta = await _GradoServiceDbContext.TableInfos.Where(x => x.Id == request.Id)
                                                .Include(x => x.FieldInfos)
                                                .SingleOrDefaultAsync();
            if(tableMeta == null)
            {
                throw new NotFoundException("Table", request.Id);
            }
            var table = _mapper.Map<TableInfoDto>(tableMeta);

            var viewModel = new TableViewModel
            {
                CreateEnabled = false,
                Table = table
            };

            return viewModel;
        }
    }
}
