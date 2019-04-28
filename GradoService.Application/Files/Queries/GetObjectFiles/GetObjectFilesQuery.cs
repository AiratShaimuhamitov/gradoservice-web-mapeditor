using GradoService.Application.Files.Queries.Models;
using GradoService.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Application.Files.Queries.GetObjectFiles
{
    public class GetObjectFilesQuery : IRequest<IEnumerable<FileInfoDto>>, ITableRequest
    {
        public int TableId { get; set; }

        public int ObjectId { get; set; }
    }
}
