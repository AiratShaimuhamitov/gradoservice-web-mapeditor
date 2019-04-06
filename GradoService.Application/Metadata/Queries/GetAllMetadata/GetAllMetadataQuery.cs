using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace GradoService.Application.Metadata.Queries.GetAllMetadata
{
    public class GetAllMetadataQuery : IRequest<MetadataListViewModel>
    {
    }
}
