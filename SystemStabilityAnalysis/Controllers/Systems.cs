using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using HeyRed.Mime;
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
            QueryResponse queryResponse = new QueryResponse();
            queryResponse.Add("ParametersWithEnter", StaticData.CurrentSystems.GetParametersWithEnter(out List<string> message));
            queryResponse.AddNegativeMessages(message, true);
            return queryResponse.ToResult();
        }

        [HttpGet]
        public object GetParametersWithCalculate()
        {

            QueryResponse queryResponse = new QueryResponse();
            queryResponse.Add("ParametersWithCalculate", StaticData.CurrentSystems.GetParametersWithCalculate(out List<string> message));
            queryResponse.AddNegativeMessages(message, true);
            return queryResponse.ToResult();
        }

        [HttpGet]
        public object GetParametersForAnalysis()
        {
            QueryResponse queryResponse = new QueryResponse();
            queryResponse.Add("ParametersForAnalysis", StaticData.CurrentSystems.GetParametersForAnalysis(out List<string> message));
            queryResponse.AddNegativeMessages(message, true);
            queryResponse.Add("U", StaticData.CurrentSystems.GetParameterU(out string result));
            queryResponse.Add("result", result);
            return queryResponse.ToResult();
        }

        [HttpGet]
        public object Validate([FromQuery]string validateArr)
        {
            QueryResponse queryResponse = new QueryResponse();
            var Parameters = JsonConvert.DeserializeObject<List<ParameterForValidate>>(validateArr);

            List<object> parametersCorrect = new List<object>();
            bool resultVerification;
            string message;
            foreach (var parameter in Parameters)
            {
                if(StaticData.CurrentSystems.ParametersWithEnter.TryGetValue(parameter.parameterName, out ParameterWithEnter parameterWithEnter))
                {
                    parameterWithEnter.Value = parameter.value;
                    resultVerification = parameterWithEnter.Verification(out message);

                    if (!resultVerification)
                        queryResponse.AddNegativeMessage(message);

                    parametersCorrect.Add(new
                    {
                        parameterName = parameter.parameterName.GetName(),
                        Correct = resultVerification
                    });


                }
            }

            queryResponse.Add("parametersCorrect", parametersCorrect);
            return queryResponse.ToResult();
        }

        [HttpPost]
        public object LoadSystemFromFile([FromQuery]IFormFile file)
        {
            QueryResponse queryResponse = new QueryResponse();
            if ((file == null) || (string.IsNullOrEmpty(file.FileName)))
            {
                queryResponse.AddNegativeMessage("Файл не выбран");
                  
            }

            if (queryResponse.IsSuccess)
            {
                //bool resultVerification;
                //string message;
                //List<string> messages = new List<string>();
                using (StreamReader streamReader = new StreamReader(file.OpenReadStream()))
                {
                    using (CsvReader csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {

                        csvReader.Configuration.Delimiter = ";";
                        try
                        {
                            List<ParameterWithEnter> parametersWithEnter = csvReader.GetRecords<ParameterWithEnter>().ToList();
                            foreach (var parameterWithEnter in parametersWithEnter)
                            {
                               StaticData.CurrentSystems.ParametersWithEnter[parameterWithEnter.TypeParameter].Value = parameterWithEnter.Value;
                                //resultVerification = StaticData.CurrentSystems.ParametersWithEnter[parameterWithEnter.TypeParameter].Verification(out message);
                                //if (!resultVerification)
                                //    messages.Add(message);
                            }
                        }
                        catch (Exception ex)
                        {
                            queryResponse.AddNegativeMessage(String.Format("Файл {0} не корректен, выберите файл, сохраненный системой", file.FileName));
                        }
                    }
                }
                //queryResponse.AddRangeErrorWithIfNotEmpty(messages);
            }


            return queryResponse.ToResult();
        }

        [HttpGet]
        public object SaveSystemToFile([FromQuery]string fileName)
        {

            QueryResponse queryResponse = new QueryResponse();

            if (string.IsNullOrEmpty(fileName))
            {
                queryResponse.AddNegativeMessage("Имя файла не указано");
                return queryResponse.ToResult();
            }

            string filePath = Path.ChangeExtension(fileName, ".csv");

            MemoryStream memory = new MemoryStream();
            using (StreamWriter streamWriter = new StreamWriter(memory, Encoding.UTF8))
            {
                using (CsvWriter csvWriter = new CsvWriter(streamWriter, CultureInfo.CurrentCulture))
                {
                    csvWriter.Configuration.Delimiter = ";";

                    csvWriter.WriteRecords(StaticData.CurrentSystems.ParametersWithEnter.Values);
                }

            }

            StaticData.CurrentSystems.Name = Path.GetFileNameWithoutExtension(filePath);
            return File(memory.ToArray(), MimeTypesMap.GetMimeType(filePath), filePath);
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
            QueryResponse queryResponse = new QueryResponse();
            if(!StaticData.CurrentSystems.U.Value.HasValue)
            {
                queryResponse.AddNegativeMessage("Невозможно сохранить систему т.к. данные некорректны");
            }
           return queryResponse.ToResult();
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
