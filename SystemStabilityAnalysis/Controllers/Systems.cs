using CsvHelper;
using HeyRed.Mime;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using SystemStabilityAnalysis.Models;
using SystemStabilityAnalysis.Models.Parameters;
using TemplateEngine.Docx;

namespace SystemStabilityAnalysis.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Produces("application/json")]
    public class Systems : BaseController
    {
        [HttpGet]
        public object GetParametersWithEnter()
        {
            
            QueryResponse.Add("ParametersWithEnter", StaticData.CurrentSystems.GetParametersWithEnter(out List<string> message));
            //QueryResponse.AddNegativeMessages(message, true);
            return QueryResponse.ToResult();
        }

        [HttpGet]
        public object GetParametersWithCalculate()
        {

            
            QueryResponse.Add("ParametersWithCalculate", StaticData.CurrentSystems.GetParametersWithCalculate(out List<string> message));
            QueryResponse.AddNegativeMessages(message, true);
            return QueryResponse.ToResult();
        }

        [HttpGet]
        public object GetParametersForAnalysis()
        {
            
            QueryResponse.Add("ParametersForAnalysis", StaticData.CurrentSystems.GetParametersForAnalysis(out List<string> message));
            QueryResponse.AddNegativeMessages(message, true);
            QueryResponse.Add("U", StaticData.CurrentSystems.GetParameterU(out string result));
            QueryResponse.Add("result", result);
            return QueryResponse.ToResult();
        }

        [HttpGet]
        public object Validate([FromQuery]string validateArr)
        {
            
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
                        QueryResponse.AddNegativeMessage(message);

                    parametersCorrect.Add(new
                    {
                        parameterName = parameter.parameterName.GetName(),
                        Correct = resultVerification
                    });


                }
            }

            QueryResponse.Add("parametersCorrect", parametersCorrect);
            return QueryResponse.ToResult();
        }

        [HttpPost]
        public object LoadSystemFromFile([FromQuery]IFormFile file)
        {
            
            if ((file == null) || (string.IsNullOrEmpty(file.FileName)))
            {
                QueryResponse.AddNegativeMessage("Файл не выбран");
                  
            }

            if (QueryResponse.IsSuccess)
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
                            QueryResponse.AddNegativeMessage(String.Format("Файл {0} не корректен, выберите файл, сохраненный системой", file.FileName));
                        }
                    }
                }
                //QueryResponse.AddRangeErrorWithIfNotEmpty(messages);
            }
            StaticData.CurrentSystems.SetAsCorrect();

            return QueryResponse.ToResult();
        }

        [HttpGet]
        public object SaveSystemToFile([FromQuery]string fileName)
        {
          
            if (string.IsNullOrEmpty(fileName))
            {
                QueryResponse.AddNegativeMessage("Имя файла не указано");
                return QueryResponse.ToResult();
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
        public object GenerateReport([FromQuery]string fileName)
        {

            fileName = Path.GetFileNameWithoutExtension(fileName);

            StaticData.CurrentSystems.Name = fileName;

            string filePath = Path.ChangeExtension(fileName + " отчет", ".docx");
            
            System.IO.File.Copy("resultTemplate.docx", filePath);

            using (FileStream fstream = System.IO.File.Open(filePath, FileMode.Open))
            {
                List<FieldContent> fieldContents = new List<FieldContent>();
                fieldContents.AddRange(StaticData.CurrentSystems.ParametersWithEnter.Values.Select(x => new FieldContent(x.Name, x.Value.ToString())));
                fieldContents.AddRange(StaticData.CurrentSystems.ParametersWithCalculation.Values.Select(x => new FieldContent(x.Name, x.Value.ToString())));
                fieldContents.AddRange(StaticData.CurrentSystems.ParametersForAnalysis.Values.Select(x => new FieldContent(x.Name, x.Value.ToString())));
                fieldContents.Add(new FieldContent(StaticData.CurrentSystems.U.Name, StaticData.CurrentSystems.U.Value.ToString()));
                fieldContents.Add(new FieldContent("nameSystem", StaticData.CurrentSystems.Name));
                fieldContents.Add(new FieldContent("Result", StaticData.CurrentSystems.U.GetResult()));
                using (var outputDocument = new TemplateProcessor(fstream).SetRemoveContentControls(true))
                {
                    outputDocument.FillContent(new Content(fieldContents.ToArray()));
                    outputDocument.SaveChanges();
                }
               
            }
            var memory = new MemoryStream();
            using (var stream = new FileStream(filePath, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
            System.IO.File.Delete(filePath);

            memory.Position = 0;
            return File(memory, MimeTypesMap.GetMimeType(filePath), filePath);
        }

        [HttpGet]
        public object ValidateSystemBeforeSave()
        {
            
            if(!StaticData.CurrentSystems.U.Value.HasValue)
            {
                QueryResponse.AddNegativeMessage("Невозможно сохранить систему т.к. данные некорректны");
            }

           return QueryResponse.ToResult();
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
