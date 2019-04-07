using AutoMapper;
using GradoService.Application.Interfaces.Mapping;
using GradoService.Domain.Entities.Metadata;

namespace GradoService.Application.Metadata.Model
{
    public class MetadataDto : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string SchemeName { get; set; }
        
        public string Name { get; set; }

        public string PresentationName { get; set; }

        public string GeomField { get; set; }

        public string StyleField { get; set; }

        public int Geomtype { get; set; }

        public int Type { get; set; }

        public bool DefaultStyle { get; set; }

        public bool ContainsDocument { get; set; }

        public string ViewQuery { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<MetaTableInfo, MetadataDto>();
        }
    }
}
