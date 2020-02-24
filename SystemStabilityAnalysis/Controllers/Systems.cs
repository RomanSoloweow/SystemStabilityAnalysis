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
        public object GetParametersWithEnter()
        {
            return new
            {
                Status = Status.Success.GetName(),
                ParametersWithEnter = HelperEnum.GetValuesWithoutDefault<NameParameterWithEnter>().Select(x => x.ToJson())
            };
        }
        [HttpGet]
        public object GetParametersWithCalculate()
        {
            var ParametersWithEnter = HelperEnum.GetValuesWithoutDefault<NameParameterWithEnter>().Select(x => x.ToParameter(0.123, false));
            var ParametersWithCalculation = HelperEnum.GetValuesWithoutDefault<NameParameterWithCalculation>().Select(x => x.ToParameter(0.123, true));

            return new
            {
                Status = Status.Success.GetName(),
                ParametersWithCalculate = ParametersWithEnter.Union(ParametersWithCalculation)
            };
        }
        [HttpGet]
        public object GetParametersForAnalysis()
        {
            var ParametersForAnalysis = HelperEnum.GetValuesWithoutDefault<NameParameterForAnalysis>().Select(x => x.ToParameter(0.123, false));

            return new
            {
                Status = Status.Success.GetName(),
                U = 0,
                Result = "Cистема «Блаблабла» находится на пределе своей устойчивости в течении периода «56» при заданных условиях и ограничениях.",
                ParametersForAnalysis = ParametersForAnalysis
            };
        }
    }
}
