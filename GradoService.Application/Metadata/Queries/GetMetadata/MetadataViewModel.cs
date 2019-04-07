using AutoMapper;
using GradoService.Application.Interfaces.Mapping;
using GradoService.Application.Metadata.Model;
using GradoService.Domain.Entities.Metadata;

namespace GradoService.Application.Metadata.Queries.GetMetadata
{
    public class MetadataViewModel : IHaveCustomMapping
    {
        public MetadataDto Metadata { get; set; }

        public bool CreateEnabled { get; set; }

        public bool EditEnabled { get; set; }

        public bool DeleteEnabled { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<MetaTableInfo, MetadataViewModel>()
                .ForMember(mDTO => mDTO.Metadata, opt => opt.MapFrom(
                    (source, destination, destMember, context) => { return context.Mapper.Map<MetadataDto>(source); }))
                .ForMember(mDTO => mDTO.CreateEnabled, opt => opt.MapFrom<MetadataPermissionResolver>());
        }

        class MetadataPermissionResolver : IValueResolver<MetaTableInfo, MetadataViewModel, bool>
        {
            public bool Resolve(MetaTableInfo source, MetadataViewModel destination, bool destMember, ResolutionContext context)
            {
                //TODO: Permission service for tables
                return false;
            }
        }
    }
}
