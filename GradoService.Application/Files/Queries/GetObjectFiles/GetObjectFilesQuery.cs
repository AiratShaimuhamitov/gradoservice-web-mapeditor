using GradoService.Application.Files.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Application.Files.Queries.GetObjectFiles
{
    public class GetObjectFilesQuery : IRequest<IEnumerable<FileInfoDto>>
    {
        public int TableId { get; set; }

        public int ObjectId { get; set; }
    }
}
