using AutoMapper;
using GradoService.Application.Interfaces.Mapping;
using GradoService.Domain.Entities.Metadata;

namespace GradoService.Application.Table.Model
{
    public class FieldDto : IHaveCustomMapping
    {
        public string Name { get; set; }

        public string FieldType { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<
                MetaTableFieldInfo, FieldDto>()
                .ForMember(tfDTO => tfDTO.Name, x => x.MapFrom(m => m.PresentationName))
                .ForMember(tfDTO => tfDTO.FieldType, x => x.MapFrom(m => m.FieldType.Name));
        }
    }
}
