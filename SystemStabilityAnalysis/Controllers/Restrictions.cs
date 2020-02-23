using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SystemStabilityAnalysis.Helpers;
using SystemStabilityAnalysis.Models;

namespace SystemStabilityAnalysis.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Restrictions : ControllerBase
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

            return ParameterName.Wv2.GetDesignation() + new Condition(ConditionType.MoreOrEqual, 5.5).ErrorMessage;
            //return "Здесь будет ввод ограничений";
        }
    }
}
