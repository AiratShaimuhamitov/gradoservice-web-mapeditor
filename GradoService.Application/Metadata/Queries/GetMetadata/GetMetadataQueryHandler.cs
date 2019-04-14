using AutoMapper;
using GradoService.Application.Exceptions;
using GradoService.Domain.Entities.Metadata;
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
        private readonly GradoServiceDbContext _GradoServiceDbContext;

        public GetMetadataQueryHandler(GradoServiceDbContext GradoServiceDbContext, IMapper mapper)
        {
            _mapper = mapper;
            _GradoServiceDbContext = GradoServiceDbContext;
        }

        public async Task<MetadataViewModel> Handle(GetMetadataQuery request, CancellationToken cancellationToken)
        {
            var metadata = await _GradoServiceDbContext.TableInfos.Where(x => x.Id == request.Id)
                                                         .Include(p => p.FieldInfos)
                                                         .SingleOrDefaultAsync();
            
            if(metadata == null)
            {
                throw new NotFoundException(nameof(MetaTableInfo), request.Id);
            }

            return _mapper.Map<MetadataViewModel>(metadata);
        }
    }
}
