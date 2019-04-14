using AutoMapper;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace GradoService.Persistence.Mapping.Profiles
{
    public class DbMapperProfile : Profile
    {
        public DbMapperProfile()
        {
            LoadCustomMappings();
        }

        private void LoadCustomMappings()
        {
            var mapsFrom = MappingProfileHelper.LoadCustomMappings(Assembly.GetExecutingAssembly());

            foreach (var map in mapsFrom)
            {
                map.CreateMappings(this);
            }
        }
    }
}
