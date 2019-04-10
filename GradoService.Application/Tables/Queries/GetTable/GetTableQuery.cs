using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Application.Tables.Queries.GetTable
{
    public class GetTableQuery : IRequest<TableViewModel>
    {
        public int Id { get; set; }
    }
}
