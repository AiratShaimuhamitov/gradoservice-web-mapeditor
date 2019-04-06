using AutoMapper;
using GradoService.Application.Exceptions;
using GradoService.Domain.Entities;
using GradoService.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GradoService.Application.Metadata.Queries.GetMetadata
{
    public class GetMetadataQueryHandler : IRequestHandler<GetMetadataQuery, MetadataViewModel>
    {
        private readonly IMapper _mapper;
        private readonly MetadataDbContext _metadataDbContext;

        public GetMetadataQueryHandler(MetadataDbContext metadataDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _metadataDbContext = metadataDbContext;
        }

        public async Task<MetadataViewModel> Handle(GetMetadataQuery request, CancellationToken cancellationToken)
        {
            var metadata = await _metadataDbContext.TableInfos.Where(x => x.Id == request.Id)
                                                         .Include(p => p.FieldInfos)
                                                         .SingleOrDefaultAsync();
            
            if(metadata == null)
            {
                throw new NotFoundException(nameof(TableInfo), request.Id);
            }

            return _mapper.Map<MetadataViewModel>(metadata);
        }
    }
}
