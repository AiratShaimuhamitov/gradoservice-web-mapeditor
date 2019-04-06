using AutoMapper;
using GradoService.Application.Interfaces.Mapping;
using GradoService.Application.Metadata.Model;
using GradoService.Domain.Entities;

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
            configuration.CreateMap<TableInfo, MetadataViewModel>()
                .ForMember(mDTO => mDTO.Metadata, opt => opt.MapFrom<MetadataDtoResolver>())
                .ForMember(mDTO => mDTO.CreateEnabled, opt => opt.MapFrom<MetadataPermissionResolver>());
        }

        class MetadataDtoResolver : IValueResolver<TableInfo, MetadataViewModel, MetadataDto>
        {
            public MetadataDto Resolve(TableInfo source, MetadataViewModel destination, MetadataDto destMember, ResolutionContext context)
            {
                return context.Mapper.Map<MetadataDto>(source);
            }
        }

        class MetadataPermissionResolver : IValueResolver<TableInfo, MetadataViewModel, bool>
        {
            public bool Resolve(TableInfo source, MetadataViewModel destination, bool destMember, ResolutionContext context)
            {
                //TODO: Permission service for tables
                return false;
            }
        }
    }
}
