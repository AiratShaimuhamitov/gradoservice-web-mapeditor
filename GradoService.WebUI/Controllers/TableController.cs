using System.Threading.Tasks;
using GradoService.Application.Table.Queries.GetAllTables;
using GradoService.Application.Table.Queries.GetTable;
using Microsoft.AspNetCore.Mvc;

namespace GradoService.WebUI.Controllers
{
    [Route("api/table")]
    [ApiController]
    public class TableController : BaseController
    {
        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            return new JsonResult(await Mediator.Send(new GetAllTablesQuery()));
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> Get(int id)
        {
            return new JsonResult(await Mediator.Send(new GetTableQuery { Id = id}));
        }

        // TODO Add new table, Update table, Delete table
    }
}
