using AutoMapper;
using GradoService.Application.Interfaces.Mapping;
using GradoService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Application.Metadata.Queries.GetAllMetadata
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
            configuration.CreateMap<TableInfo, MetadataDto>();
        }
    }
}
