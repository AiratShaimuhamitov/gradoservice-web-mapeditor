using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;
using GradoService.Application.Exceptions;
using GradoService.Domain.Entities;
using GradoService.Persistence;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GradoService.WebUI.Controllers
{
    [Route("api/meta")]
    [ApiController]
    public class MetadataController : ControllerBase
    {
        [HttpGet]
        public JsonResult GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
