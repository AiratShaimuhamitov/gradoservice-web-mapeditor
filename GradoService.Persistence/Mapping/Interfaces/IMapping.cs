using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Persistence.Mapping.Interfaces
{
    public interface IMapping
    {
        void CreateMappings(Profile profile);
    }
}
