using GradoService.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GradoService.Application.Tables.Commands.InsertData
{
    public class InsertDataCommand : IRequest<int>, ITableRequest
    {
        public int TableId { get; set; }

        public Dictionary<string, object>  Row { get; set; }
    }
}
