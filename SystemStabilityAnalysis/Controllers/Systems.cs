using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace SystemStabilityAnalysis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Systems : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Здесь будут системы";
        }
    }
}
