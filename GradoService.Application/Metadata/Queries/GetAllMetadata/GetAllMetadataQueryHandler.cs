using AutoMapper;
using GradoService.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GradoService.Application.Metadata.Model;

namespace GradoService.Application.Metadata.Queries.GetAllMetadata
{
    public class GetAllMetadataQueryHandler : IRequestHandler<GetAllMetadataQuery, MetadataListViewModel>
    {
        private readonly MetadataDbContext _metadataDbContext;
        private readonly IMapper _mapper;

        public GetAllMetadataQueryHandler(MetadataDbContext metadataDbContext, IMapper mapper)
        {
            _metadataDbContext = metadataDbContext;
            _mapper = mapper;
        }

        public async Task<MetadataListViewModel> Handle(GetAllMetadataQuery request, CancellationToken cancellationToken)
        {
            var tables = await _metadataDbContext.TableInfos.OrderBy(p => p.Id)
                .Include(p => p.FieldInfos)
                .ToListAsync(cancellationToken);

            var model = new MetadataListViewModel
            {
                Metadata = _mapper.Map<IEnumerable<MetadataDto>>(tables),
                CreateEnabled = false
            };

            return model;
        }
    }
}
