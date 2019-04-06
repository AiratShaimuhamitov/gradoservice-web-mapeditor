using System;
using System.Threading.Tasks;
using GradoService.Application.Metadata.Queries.GetAllMetadata;
using GradoService.Application.Metadata.Queries.GetMetadata;
using Microsoft.AspNetCore.Mvc;

namespace GradoService.WebUI.Controllers
{
    [Route("api/meta")]
    [ApiController]
    public class MetadataController : BaseController
    {
        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            return new JsonResult(await Mediator.Send(new GetAllMetadataQuery()));
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> Get(int id)
        {
            return new JsonResult(await Mediator.Send(new GetMetadataQuery { Id = id }));
        }
    }
}
