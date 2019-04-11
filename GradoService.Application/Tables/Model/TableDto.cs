using AutoMapper;
using GradoService.Application.Interfaces.Mapping;
using GradoService.Domain.Entities.Table;
using GradoService.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GradoService.Application.Tables.Model
{
    public class TableDto : IHaveCustomMapping
    {
        public TableDto()
        {
            Fields = new List<FieldDto>();
            Data = new List<Dictionary<string, object>>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string PresentationName { get; set; }

        public IEnumerable<FieldDto> Fields { get; set; }

        public IEnumerable<IDictionary<string, object>> Data { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Table, TableDto>()
                .ForMember(tDTO => tDTO.Data, opt => opt.MapFrom((src, dest) =>
                            src.Rows.Select(x => x.Data.ToDictionary(k => k.Key.Name, v => v.Value))))
                .ForMember(tDTO => tDTO.PresentationName, opt => opt.MapFrom<TablePresentationNameResolver>())
                .ForMember(tDTO => tDTO.Fields, opt => opt.MapFrom((src, dest, destMember, ctx) =>
                            ctx.Mapper.Map<IEnumerable<FieldDto>>(src.Fields)));
        }

        class TablePresentationNameResolver : IValueResolver<Table, TableDto, string>
        {
            private readonly GradoServiceDbContext dbContext;

            public TablePresentationNameResolver(GradoServiceDbContext context)
            {
                dbContext = context;
            }

            public string Resolve(Table source, TableDto destination, string destMember, ResolutionContext context)
            {
                var metaTable = dbContext.TableInfos.Where(x => x.Id == source.Id).FirstOrDefault();
                if (metaTable != null) return metaTable.PresentationName;
                return "";
            }
        }
    }
}
