using GradoService.Application.Files.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Application.Files.Queries.GetFileData
{
    public class GetFileDataQuery : IRequest<FileDataDto>
    {
        public int TableId { get; set; }

        public int ObjectId { get; set; }

        public int FileId { get; set; }
    }
}
