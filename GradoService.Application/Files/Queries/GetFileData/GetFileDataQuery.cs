using GradoService.Application.Files.Queries.Models;
using GradoService.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Application.Files.Queries.GetFileData
{
    public class GetFileDataQuery : IRequest<FileDataDto>, ITableRequest
    {
        public int TableId { get; set; }

        public int ObjectId { get; set; }

        public int FileId { get; set; }
    }
}
