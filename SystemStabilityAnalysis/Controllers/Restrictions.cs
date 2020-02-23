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
    [Route("[controller]/[action]")]
    public class Restrictions : ControllerBase
    {
        [HttpGet]
        public ActionResult GetParameters()
        {
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("Status", Status.Success.GetName());
            Data.Add("Properties", StaticData.PropertiesSystem.Properties);
            var result = JsonConvert.SerializeObject(Data);
            return new JsonResult(result);
        }

        [HttpGet]
        public string GetTest()
        {
            return "Здесь будет запрос для получения условий";
        }
    }
}
