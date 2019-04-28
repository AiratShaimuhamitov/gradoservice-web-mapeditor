using System.Threading.Tasks;
using GradoService.Application.Tables.Queries.GetAllTables;
using GradoService.Application.Tables.Queries.GetTable;
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
            return new JsonResult(await Mediator.Send(new GetTableQuery { TableId = id}));
        }

        // TODO Add new table, Update table, Delete table
    }
}
