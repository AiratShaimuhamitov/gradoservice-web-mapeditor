using AutoMapper;
using GradoService.Application.Interfaces.Mapping;
using GradoService.Domain.Entities.Metadata;
using System.Collections.Generic;

namespace GradoService.Application.Table.Model
{
    public class TableDto : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Schema { get; set; }

        public string Geom { get; set; }

        public int GeomType { get; set; }

        public int Type { get; set; }

        public bool ContainsDocument { get; set; }

        public IEnumerable<FieldDto> Fields { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<MetaTableInfo, TableDto>()
                .ForMember(tDTO => tDTO.Name, x => x.MapFrom(m => m.PresentationName))
                .ForMember(tDTO => tDTO.Fields, x => x.MapFrom<FieldsResolver>());
        }

        class FieldsResolver : IValueResolver<MetaTableInfo, TableDto, IEnumerable<FieldDto>>
        {
            public IEnumerable<FieldDto> Resolve(MetaTableInfo source, TableDto destination, IEnumerable<FieldDto> destMember, ResolutionContext context)
            {
                var list = new List<FieldDto>();

                foreach(var fieldInfo in source.FieldInfos)
                {
                    list.Add(context.Mapper.Map<FieldDto>(fieldInfo));
                }

                return list;
            }
        }
    }
}
