using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradoService.Application.Tables.Commands.InsertData;
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
        public async Task<string> Get(int id)
        {
            var table = await Mediator.Send(new GetTableDataQuery { TableId = id });

            return JsonConvert.SerializeObject(table, Formatting.Indented);
        }

        [HttpGet("row")]
        public JsonResult GetRow(int tableId, int rowId)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public JsonResult UpdateRow()
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public JsonResult DeleteRow(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<JsonResult> InsertRow(int id, [FromBody]InsertDataCommand command)
        {
            if (command.TableId != id) command.TableId = id;

            var insertedRowId = await Mediator.Send(command);
            return new JsonResult(insertedRowId);
        }
    }
}
