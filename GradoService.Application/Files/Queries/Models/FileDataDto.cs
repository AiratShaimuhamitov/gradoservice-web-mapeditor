using AutoMapper;
using GradoService.Application.Interfaces.Mapping;
using GradoService.Domain.Entities.File;
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Text;

namespace GradoService.Application.Files.Queries.Models
{
    public class FileDataDto : IHaveCustomMapping
    {
        public byte[] Data { get; set; }

        public byte[] Preview { get; set; }

        public string Name { get; set; }

        public string ContentType { get; set; }

        public void CreateMappings(Profile profile)
        {
            profile.CreateMap<File, FileDataDto>()
                .ForMember(x => x.Preview, opt => opt.MapFrom(x => x.ImagePreview))
                .ForMember(x => x.ContentType, opt => opt.MapFrom<FileTypeResolver>());
        }

        class FileTypeResolver : IValueResolver<File, FileDataDto, string>
        {
            public string Resolve(File source, FileDataDto destination, string destMember, ResolutionContext context)
            {
                return MimeTypes.GetMimeType(source.Name);
            }
        }
    }
}
