using GradoService.Application.Files.Queries.Models;
using GradoService.Application.Interfaces;
using MediatR;
using System.Collections.Generic;

namespace GradoService.Application.Files.Queries.GetTableFiles
{
    public class GetTableFilesQuery : IRequest<IEnumerable<FileInfoDto>>, ITableRequest
    {
        public int TableId { get; set; }
    }
}
