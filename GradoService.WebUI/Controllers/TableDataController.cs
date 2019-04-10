using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public JsonResult GetRow(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public JsonResult UpdateRow(int id)
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

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public JsonResult InsertRow(int id)
        {
            throw new NotImplementedException();
        }
    }
}
