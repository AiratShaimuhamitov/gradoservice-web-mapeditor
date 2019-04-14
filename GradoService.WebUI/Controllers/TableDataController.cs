using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradoService.Application.Tables.Commands.DeleteData;
using GradoService.Application.Tables.Commands.InsertData;
using GradoService.Application.Tables.Commands.UpdateData;
using GradoService.Application.Tables.Queries.GetTableData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GradoService.WebUI.Controllers
{
    [Route("api/table/{id}/data")]
    [ApiController]
    public class TableDataController : BaseController
    {
        [HttpGet]
        public async Task<string> Get([FromRoute]int id, [FromQuery]int offset, [FromQuery]int limit)
        {
            var table = await Mediator.Send(new GetTableDataQuery { TableId = id, Limit = limit, Offset = offset });

            return JsonConvert.SerializeObject(table, Formatting.Indented);
        }

        [HttpGet("row")]
        public JsonResult GetRow([FromRoute]int id, int rowId)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> UpdateRow([FromRoute]int id, [FromBody]UpdateDataCommand command)
        {
            if (command.TableId != id) command.TableId = id;

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> DeleteRow([FromRoute]int id, [FromQuery] int rowId)
        {
            await Mediator.Send(new DeleteDataCommand { TableId = id, RowId = rowId });

            return NoContent();
        }

        [HttpPost]
        public async Task<JsonResult> InsertRow([FromRoute]int id, [FromBody]InsertDataCommand command)
        {
            if (command.TableId != id) command.TableId = id;

            var insertedRowId = await Mediator.Send(command);
            return new JsonResult(insertedRowId);
        }
    }
}
