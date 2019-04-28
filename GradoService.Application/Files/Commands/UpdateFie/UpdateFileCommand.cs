using GradoService.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Application.Files.Commands.UpdateFie
{
    public class UpdateFileCommand : IRequest<Unit>, ITableRequest
    {
        public int TableId { get; set; }

        public int ObjectId { get; set; }

        public int FileId { get; set; }

        public string FileName { get; set; }

        public byte[] Data { get; set; }
    }
}
