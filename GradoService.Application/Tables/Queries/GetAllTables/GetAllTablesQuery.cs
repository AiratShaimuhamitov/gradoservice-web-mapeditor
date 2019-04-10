using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradoService.Application.Tables.Queries.GetAllTables
{
    public class GetAllTablesQuery : IRequest<TablesViewModel>
    {
    }
}
