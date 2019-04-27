using GradoService.Application.Exceptions;
using GradoService.Application.Files.Commands.DeleteFile;
using GradoService.Application.Files.Commands.InsertFile;
using GradoService.Application.Files.Commands.UpdateFie;
using GradoService.Application.Files.Queries.GetFileData;
using GradoService.Application.Files.Queries.GetObjectFiles;
using GradoService.Application.Files.Queries.GetTableFiles;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradoService.WebUI.Controllers
{
    [Route("api/table/{id}/file")]
    [ApiController]
    public class TableFileController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> GetTableFilesInfo([FromRoute]int id)
        {
            var files = await Mediator.Send(new GetTableFilesQuery { TableId = id });

            if (files == null) return NotFound();

            return Ok(JsonConvert.SerializeObject(files, Formatting.Indented));
        }

        [HttpGet("info")]
        public async Task<ActionResult> GetObjectFilesInfo([FromRoute]int id, [FromQuery] int objectId)
        {
            var files = await Mediator.Send(new GetObjectFilesQuery { TableId = id, ObjectId = objectId });

            if (files == null) return NotFound();

            return Ok(JsonConvert.SerializeObject(files, Formatting.Indented));
        }

        [HttpGet("data")]
        public async Task<ActionResult> GetFile([FromRoute] int id, [FromQuery] int objectId, [FromQuery] int fileId)
        {
            var file = await Mediator.Send(new GetFileDataQuery { TableId = id, ObjectId = objectId, FileId = fileId });

            if (file == null) return NotFound();

            return File(file.Data, file.ContentType, file.Name);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<JsonResult> Insert([FromRoute] int id, [FromQuery] int objectId, [FromForm] IFormFile file)
        {
            var data = new byte[file.Length];

            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    data = ms.ToArray();
                }
            }

            var insertFileCommand = new InsertFileCommand
            {
                FileName = file.FileName,
                Data = data,
                TableId = id,
                ObjectId = objectId
            };

            var insertedId = await Mediator.Send(insertFileCommand);

            return new JsonResult(insertedId);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Update([FromRoute] int id, [FromQuery] int fileId, [FromQuery] int objectId, [FromForm] IFormFile file)
        {
            var data = new byte[file.Length];

            if (file.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    file.CopyTo(ms);
                    data = ms.ToArray();
                }
            }

            var updateFileCommand = new UpdateFileCommand
            {
                FileId = fileId,
                FileName = file.FileName,
                Data = data,
                TableId = id,
                ObjectId = objectId
            };

            await Mediator.Send(updateFileCommand);

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> Delete([FromRoute] int id, [FromQuery] int fileId)
        {
            await Mediator.Send(new DeleteFileCommand { TableId = id, FileId = fileId });
            return NoContent();
        }
    }
}
