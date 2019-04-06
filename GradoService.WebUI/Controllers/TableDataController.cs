using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GradoService.WebUI.Controllers
{
    [Route("api/table/{id}/data")]
    [ApiController]
    public class TableDataController
    {
        [HttpGet]
        public JsonResult Get(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public JsonResult UpdateRow(int id)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public JsonResult DeleteRow(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public JsonResult InsertRow(int id)
        {
            throw new NotImplementedException();
        }
    }
}
