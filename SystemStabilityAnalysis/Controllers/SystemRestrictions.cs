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
    public class SystemRestrictions : ControllerBase
    {
        //Для логирования. Пока что не нужно
        //private readonly ILogger<SystemRestrictions> _logger;

        //public SystemRestrictions(ILogger<SystemRestrictions> logger)
        //{
        //    _logger = logger;
        //}

        [HttpGet]
        public string Get()
        {
            return "Здесь будет ввод ограничений";
        }
    }
}
