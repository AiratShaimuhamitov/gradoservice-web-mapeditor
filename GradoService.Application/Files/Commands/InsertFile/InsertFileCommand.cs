using GradoService.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Application.Files.Commands.InsertFile
{
    public class InsertFileCommand : IRequest<int>, ITableRequest
    {
        public int TableId { get; set; }

        public int ObjectId { get; set; }

        public string FileName { get; set; }

        public byte[] Data { get; set; }

        public byte[] Preview { get; set; }
    }
}
