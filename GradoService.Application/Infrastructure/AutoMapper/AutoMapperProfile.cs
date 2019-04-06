using AutoMapper;
using System.Reflection;

namespace GradoService.Application.Infrastructure.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            LoadConvertors();
            LoadStandardMappings();
            LoadCustomMappings();
        }

        private void LoadConvertors()
        {

        }

        private void LoadStandardMappings()
        {
            var mapsFrom = MappingProfileHelper.LoadStandardMappings(Assembly.GetExecutingAssembly());

            foreach(var map in mapsFrom)
            {
                CreateMap(map.Source, map.Destination).ReverseMap();
            }
        }

        private void LoadCustomMappings()
        {
            var mapsFrom = MappingProfileHelper.LoadCustomMappings(Assembly.GetExecutingAssembly());

            foreach(var map in mapsFrom)
            {
                map.CreateMappings(this);
            }
        }
    }
}
