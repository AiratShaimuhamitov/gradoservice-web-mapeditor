using AutoMapper;
using GradoService.Application.Interfaces.Mapping;
using GradoService.Domain.Entities.Metadata;
using System.Collections.Generic;

namespace GradoService.Application.Tables.Model
{
    public class TableInfoDto : IHaveCustomMapping
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string PresentationName { get; set; }

        public string Geom { get; set; }

        public int GeomType { get; set; }

        public int Type { get; set; }

        public bool ContainsDocument { get; set; }

        public IEnumerable<FieldInfoDto> Fields { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<MetaTableInfo, TableInfoDto>()
                .ForMember(tDTO => tDTO.Fields, x => x.MapFrom<FieldsResolver>());
        }

        class FieldsResolver : IValueResolver<MetaTableInfo, TableInfoDto, IEnumerable<FieldInfoDto>>
        {
            public IEnumerable<FieldInfoDto> Resolve(MetaTableInfo source, TableInfoDto destination, IEnumerable<FieldInfoDto> destMember, ResolutionContext context)
            {
                var list = new List<FieldInfoDto>();

                foreach(var fieldInfo in source.FieldInfos)
                {
                    list.Add(context.Mapper.Map<FieldInfoDto>(fieldInfo));
                }

                return list;
            }
        }
    }
}
