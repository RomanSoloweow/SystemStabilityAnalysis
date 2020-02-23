using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SystemStabilityAnalysis.Helpers;
using SystemStabilityAnalysis.Models;
using System.Text.Json;
namespace SystemStabilityAnalysis.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Produces("application/json")]
    public class Restrictions : ControllerBase
    {
        [HttpGet]
        public Dictionary<string, object> GetParameters()
        {
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("Status", Status.Success.GetName());
            Data.Add("Properties", Enum.GetValues(typeof(ParametersName)).OfType<ParametersName>().Select(x => x.ToJson()));
            return Data;
        }

        [HttpGet]
        public Dictionary<string, object> GetConditions()
        {
            Dictionary<string, object> Data = new Dictionary<string, object>();
            Data.Add("Status", Status.Success.GetName());
            Data.Add("Properties", Enum.GetValues(typeof(ConditionType)).OfType<ConditionType>().Select(x => x.ToJson()));
            return Data;
        }
    }
}
