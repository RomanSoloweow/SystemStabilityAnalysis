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
    public class SystemsAnalysis : ControllerBase
    {
        
        [HttpGet]
        public string Get()
        {
            return "Здесь будет анализ систем";
        }

        [HttpGet("{name}")]
        public string Get(string name)
        {
            return name;
        }
    }
}
