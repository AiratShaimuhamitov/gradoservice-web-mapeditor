using GradoService.Application.Files.Queries.GetFileData;
using GradoService.Application.Files.Queries.GetObjectFiles;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradoService.WebUI.Controllers
{
    [Route("api/table/{id}/file")]
    [ApiController]
    public class TableFileController : BaseController
    {
        public async Task<string> Get([FromRoute]int id)
        {
            throw new NotImplementedException();
        }

        [HttpGet("info")]
        public async Task<string> Get([FromRoute]int id, [FromQuery] int objectId)
        {
            var files = await Mediator.Send(new GetObjectFilesQuery { TableId = id, ObjectId = objectId });

            return JsonConvert.SerializeObject(files, Formatting.Indented);
        }

        [HttpGet("data")]
        public async Task<FileResult> Get([FromRoute] int id, [FromQuery] int objectId, [FromQuery] int fileId)
        {
            var file = await Mediator.Send(new GetFileDataQuery { TableId = id, ObjectId = objectId, FileId = fileId });

            return File(file.Data, file.ContentType, file.Name);
        }
    }
}
