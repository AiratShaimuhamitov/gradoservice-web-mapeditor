using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Application.Metadata.Queries.GetMetadata
{
    public class GetMetadataQuery : IRequest<MetadataViewModel>
    {
        public int Id { get; set; }
    }
}
