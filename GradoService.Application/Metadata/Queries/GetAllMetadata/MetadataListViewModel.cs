using GradoService.Application.Metadata.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Application.Metadata.Queries.GetAllMetadata
{
    public class MetadataListViewModel
    {
        public IEnumerable<MetadataDto> Metadata { get; set; }

        public bool CreateEnabled { get; set; }
    }
}
