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
    public class FieldDto : IHaveCustomMapping
    {
        public string Name { get; set; }

        public string PresentationName { get; set; }

        public string Type { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Field, FieldDto>()
                .ForMember(fDTO => fDTO.PresentationName, opt => opt.MapFrom<FieldPresentationNameResolver>());
        }

        class FieldPresentationNameResolver : IValueResolver<Field, FieldDto, string>
        {
            private readonly GradoServiceDbContext _dbContext;

            public FieldPresentationNameResolver(GradoServiceDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public string Resolve(Field source, FieldDto destination, string destMember, ResolutionContext context)
            {
                var metaField = _dbContext.TableFieldInfos.Where(x => x.Id == source.Id).FirstOrDefault();

                if (metaField != null) return metaField.PresentationName;

                return "";
            }
        }
    }
}
