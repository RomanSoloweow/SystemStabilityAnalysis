using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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

        [HttpPost]
        public ActionResult GetParameters()
        {
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("Status", Status.Success.GetName());
            Data.Add("Properties", StaticData.PropertiesSystem.Properties);
            var result = JsonConvert.SerializeObject(Data);
            return new JsonResult(result);
        }

        [HttpGet]
        public string Get()
        {

            return "Здесь будет ввод ограничений";
        }
    }
}
