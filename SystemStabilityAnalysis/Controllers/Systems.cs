using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
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
                ParametersWithEnter = StaticData.CurrentSystems.GetParametersWithEnter(out List<string> message),
                Message = message
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
                U = StaticData.CurrentSystems.U.Value.HasValue? StaticData.CurrentSystems.U.Value.Value.ToString():"_",
                Result = StaticData.CurrentSystems.U.GetResult(),
                ParametersForAnalysis = StaticData.CurrentSystems.GetParametersForAnalysis(out List<string> message),
                Message = message
            };
        }

        [HttpGet]
        public object Validate([FromQuery]string validateArr)
        {
            var Parameters = JsonConvert.DeserializeObject<List<ParameterForValidate>>(validateArr);

            List<object> parametersCorrect = new List<object>();
            List<string> message = new List<string>();
            QueryResponse resultVerification;
            foreach(var parameter in Parameters)
            {
                if(StaticData.CurrentSystems.ParametersWithEnter.TryGetValue(parameter.parameterName, out ParameterWithEnter parameterWithEnter))
                {
                    parameterWithEnter.Value = parameter.value;
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

        [HttpPost]
        public object LoadSystemFromFile([FromQuery]IFormFile file)
        {
            QueryResponse responceResult = new QueryResponse();
            //responceResult.AddError("Тестовая ошибка");

            //return responceResult.ToResult();


            if ((file == null) || (string.IsNullOrEmpty(file.FileName)))
            {
                return new
                {
                    Message = "Файл не выбран",
                    Status = Status.Error.GetName(),
                };
            }


            //using (var reader = new StreamReader(file.OpenReadStream()))
            //{
            //    while (!sr.EndOfStream)
            //    {
            //    }
            //        var record = csv.GetRecords<Foo>();
            //    var t = record.First();
            //}


            var ParametersWithEnter = StaticData.CurrentSystems.GetParametersWithEnter(out List<string> message);
            return new
            {

                Status = Status.Success.GetName(),
                Parameters = ParametersWithEnter,
                Message = message
            };
        }

        [HttpGet]
        public object SaveSystemToFile([FromQuery]string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return new
                {
                    Message = "Имя файла не указано",
                    Status = Status.Error.GetName(),
                };
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(), "test.csv");

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            //FileExtensionContentTypeProvider.tr
            //GetMimeMapping
            memory.Position = 0;
            return File(memory, "text/csv", Path.ChangeExtension(fileName, ".csv"));
        }

        [HttpGet]
        public object GenerateReport()
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "test.csv");

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }

            memory.Position = 0;
            return File(memory, "text/csv", Path.ChangeExtension("отчет", ".docx"));
        }

        [HttpGet]
        public object ValidateSystemBeforeSave()
        {
            QueryResponse responceResult = new QueryResponse();
            responceResult.AddError("Какая-то ошибка");
           return responceResult.ToResult();
        }

        public class ParameterForValidate
        {
            public ParameterForValidate()
            {

            }
            public NameParameterWithEnter parameterName { get; set; }
            public double? value { get; set; }
        }

    }
}
