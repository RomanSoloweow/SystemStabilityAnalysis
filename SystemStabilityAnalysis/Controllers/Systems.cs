using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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
                ParametersWithEnter = StaticData.CurrentSystems.GetParametersWithEnter(),
                Message = new List<string> { "Blabla1", "Blabla2" }
            };
        }
        [HttpGet]
        public object GetParametersWithCalculate()
        {
            //var ParametersWithEnter = HelperEnum.GetValuesWithoutDefault<NameParameterWithEnter>().Select(x => x.ToParameter(0.123, false));
            //var ParametersWithCalculation = HelperEnum.GetValuesWithoutDefault<NameParameterWithCalculation>().Select(x => x.ToParameter(0.123, true));

            //return new
            //{
            //    Status = Status.Success.GetName(),
            //    ParametersWithCalculate = ParametersWithEnter.Union(ParametersWithCalculation),
            //    Message = new List<string> { "Blabla1", "Blabla2" }
            //};
            var t = new
            {
                Status = Status.Success.GetName(),
                ParametersWithCalculate = StaticData.CurrentSystems.GetParametersWithCalculate(out List<string> message),
                Message = message
            };

            string json = JsonConvert.SerializeObject(t, new JsonSerializerSettings {FloatFormatHandling = FloatFormatHandling.Symbol});

            return json;
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
                ParametersForAnalysis = ParametersForAnalysis,
                Message = new List<string> { "Blabla1", "Blabla2" }
            };
        }
        [HttpGet]
        public object Validate([FromQuery]string validateArr)
        {
            //string json = JsonSerializer.Serialize<Param>(new Param());
            //List <Param> t = JsonSerializer.Deserialize<List<Param>>(()validateArr);
            return "qq";

       
        }

        public class Param
        {
            public Param()
            {

            }
            public string parameterName { get; set; }
            public string value { get; set; }
        }
    }
}
