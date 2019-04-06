using System;
using System.Threading.Tasks;
using GradoService.Application.Metadata.Queries.GetAllMetadata;
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
        public JsonResult Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
