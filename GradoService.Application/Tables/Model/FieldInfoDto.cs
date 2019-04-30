using AutoMapper;
using GradoService.Application.Interfaces.Mapping;
using GradoService.Domain.Entities.Metadata;

namespace GradoService.Application.Tables.Model
{
    public class FieldInfoDto : IHaveCustomMapping
    {
        public string Name { get; set; }

        public string FieldType { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<MetaTableFieldInfo, FieldInfoDto>()
                .ForMember(tfDTO => tfDTO.Name, x => x.MapFrom(m => m.PresentationName))
                .ForMember(tfDTO => tfDTO.FieldType, x => x.MapFrom(m => m.FieldType.Type));
        }
    }
}
