using GradoService.Application.Files.Queries.Models;
using MediatR;
using System.Collections.Generic;

namespace GradoService.Application.Files.Queries.GetTableFiles
{
    public class GetTableFilesQuery : IRequest<IEnumerable<FileInfoDto>>
    {
        public int TableId { get; set; }
    }
}
