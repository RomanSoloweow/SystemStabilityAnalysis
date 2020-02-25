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

            return new
            {
                Status = Status.Success.GetName(),
                ParametersWithCalculate = StaticData.CurrentSystems.GetParametersWithCalculate(out List<string> message),
                Message = message
            }; ;
        }
        [HttpGet]
        public object GetParametersForAnalysis()
        {
            //var ParametersForAnalysis = HelperEnum.GetValuesWithoutDefault<NameParameterForAnalysis>().Select(x => x.ToParameter(0.123, false));


            return new
            {
                Status = Status.Success.GetName(),
                U = 0,
                Result = "Cистема «Блаблабла» находится на пределе своей устойчивости в течении периода «56» при заданных условиях и ограничениях.",
                ParametersForAnalysis = StaticData.CurrentSystems.GetParametersForAnalysis(out List<string> message),
                Message = message
            };
        }
        [HttpGet]
        public object Validate([FromQuery]string validateArr)
        {
            var Parameters = JsonConvert.DeserializeObject<List<Param>>(validateArr);

            List<object> parametersCorrect = new List<object>();
            List<string> message = new List<string>();
            ResultVerification resultVerification;
            foreach(var parameter in Parameters)
            {
                if(StaticData.CurrentSystems.ParametersWithEnter.TryGetValue(parameter.parameterName, out ParameterWithEnter parameterWithEnter))
                {
                    if(parameter.value!=null)
                    {
                        parameterWithEnter.Value = parameter.value.Value;
                    }
                    resultVerification = parameterWithEnter.Verification();

                    if (!resultVerification.IsCorrect)
                        message.AddRange(resultVerification.ErrorMessages);

                    parametersCorrect.Add(new
                    {
                        parameterName = parameter.parameterName.GetName(),
                        Correct = resultVerification.IsCorrect
                    });


                }
            }
           


            //string json = JsonSerializer.Serialize<Param>(new Param());
            //List <Param> t = JsonSerializer.Deserialize<List<Param>>(()validateArr);
            return new
            {
                Status = Status.Success.ToString(),
                parametersCorrect = parametersCorrect,
                Message = message

            };

       
        }

        public class Param
        {
            public Param()
            {

            }
            public NameParameterWithEnter parameterName { get; set; }
            public double? value { get; set; }
        }
    }
}
