using GradoService.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Application.Tables.Commands.DeleteData
{
    public class DeleteDataCommand : IRequest, ITableRequest
    {
        public int TableId { get; set; }

        public int RowId { get; set; }
    }
}
