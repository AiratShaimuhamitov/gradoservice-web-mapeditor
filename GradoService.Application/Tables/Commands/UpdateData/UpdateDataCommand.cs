using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Application.Tables.Commands.UpdateData
{
    public class UpdateDataCommand : IRequest
    {
        public int TableId { get; set; }

        public Dictionary<string, object> Row { get; set; }
    }
}
