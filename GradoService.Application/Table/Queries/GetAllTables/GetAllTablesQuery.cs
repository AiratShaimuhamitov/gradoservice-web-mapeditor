using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Application.Table.Queries.GetAllTables
{
    public class GetAllTablesQuery : IRequest<TablesViewModel>
    {
    }
}
