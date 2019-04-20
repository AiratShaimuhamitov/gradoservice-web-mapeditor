using AutoMapper;
using GradoService.Domain.Entities.Metadata;
using GradoService.Domain.Entities.Table;
using GradoService.Persistence.Mapping.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Persistence.Mapping
{
    public class TableMapping : IMapping
    {
        public void CreateMappings(Profile profile)
        {
            profile.CreateMap<MetaTableInfo, Table>()
                .ForMember(t => t.Rows, m => m.Ignore())
                .ForMember(t => t.Key, m => m.MapFrom(x => x.PkKey))
                .ForMember(t => t.Fields, m => m.MapFrom((source, dest, destMember, context) => 
                { return context.Mapper.Map<IEnumerable<Field>>(source.FieldInfos); }));
        }
    }
}
