using AutoMapper;
using GradoService.Domain.Entities.Metadata;
using GradoService.Domain.Entities.Table;
using GradoService.Persistence.Mapping.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Persistence.Mapping
{
    public class FieldsMapping : IMapping
    {
        public void CreateMappings(Profile profile)
        {
            profile.CreateMap<MetaTableFieldInfo, Field>()
                .ForMember(x => x.Type, m => m.MapFrom(x => x.FieldType.Type));
        }
    }
}
