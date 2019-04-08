using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Application.Table.Queries.GetTable
{
    public class GetTableQuery : IRequest<TableViewModel>
    {
        public int Id { get; set; }
    }
}
