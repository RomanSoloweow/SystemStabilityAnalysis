using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SystemStabilityAnalysis.Helpers;
using SystemStabilityAnalysis.Models;
using SystemStabilityAnalysis.Models.Parameters;

namespace SystemStabilityAnalysis.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Produces("application/json")]
    public class Systems : ControllerBase
    {
        [HttpGet] 
        public object GetParameters()
        {
            return new
            {
                Status = Status.Success.GetName(),
                Properties = HelperEnum.GetValuesWithoutDefault<NameParameterWithEnter>().Select(x => x.ToJson())
            };
        }
    }
}
