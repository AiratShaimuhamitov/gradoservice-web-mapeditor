﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Application.Files.Commands.DeleteFile
{
    public class DeleteFileCommand : IRequest<Unit>
    {
        public int TableId { get; set; }

        public int FileId { get; set; }
    }
}