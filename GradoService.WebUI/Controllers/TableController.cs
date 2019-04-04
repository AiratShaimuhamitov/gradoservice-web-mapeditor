using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GradoService.WebUI.Controllers
{
    [Route("api/table")]
    [ApiController]
    public class TableController
    {
        [HttpGet("list")]
        public JsonResult GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public JsonResult Get(string id)
        {
            throw new NotImplementedException();
        }

        // TODO Add new table, Update table, Delete table
    }
}
