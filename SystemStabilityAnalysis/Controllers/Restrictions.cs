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
using SystemStabilityAnalysis.Models.Parameters;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Globalization;
using CsvHelper;
using HeyRed.Mime;
using System.Text;
using System.Dynamic;
using TemplateEngine.Docx;

namespace SystemStabilityAnalysis.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Produces("application/json")]
    public class Restrictions : BaseController
    {
        [HttpGet]
        public object GetParameters()
        {          
            var ParametersWithEnter = HelperEnum.GetValuesWithoutDefault<NameParameterWithEnter>().Where(x => !StaticData.ConditionsForParameterWithEnter.ContainsKey(x)).Select(x => x.ToJson());
            var ParametersWithCalculation = HelperEnum.GetValuesWithoutDefault<NameParameterWithCalculation>().Where(x => !StaticData.ConditionsForParameterWithCalculation.ContainsKey(x)).Select(x => x.ToJson());
            //var ParametersForAnalysis = HelperEnum.GetValuesWithoutDefault<NameParameterForAnalysis>().Where(x => !StaticData.ConditionsForParameterForAnalysis.ContainsKey(x)).Select(x => x.ToJson());
            QueryResponse.Add("Properties", ParametersWithEnter.Union(ParametersWithCalculation));
           
            return QueryResponse.ToResult();
        }

        [HttpGet]
        public object GetConditions()
        {
            
            QueryResponse.Add("Conditions", HelperEnum.GetValuesWithoutDefault<ConditionType>().Select(x => x.ToJson()));
            return QueryResponse.ToResult();
        }

        [HttpGet]
        public object GetRestrictions()
        {
            
            QueryResponse.Add("Restrictions", ParameterUniversal.GetRestrictions().Select(x => x.ToResponse()));
            return QueryResponse.ToResult();
        }

        [HttpGet]
        public object AddRestriction(string parameter = null, string condition = null, string value = null)
        {
            

            ConditionType conditionValue = ConditionType.NoCorrect;
            double Value = 0 ;

            if (string.IsNullOrEmpty(value))
            {
                QueryResponse.AddNegativeMessage("Значение ограничения не указано");
            }
            else
            {
                if (!double.TryParse(value, out Value))
                {
                    QueryResponse.AddNegativeMessage(String.Format("Указанное значение \"{0}\" не является числом", value));
                }
                else if (!(Value > 0))
                {
                    QueryResponse.AddNegativeMessage("Значение ограничения должно быть > 0");
                }
            }

            if (string.IsNullOrEmpty(condition))
            {
                QueryResponse.AddNegativeMessage("Условие для ограничения не указано");
            }
            else
            {
                HelperEnum.TryGetValue<ConditionType>(condition, out conditionValue);

                if (HelperEnum.IsDefault(conditionValue))
                {
                    QueryResponse.AddNegativeMessage(String.Format("Условие типа \"{0}\" не найдено", condition));
                }
            }

            if (string.IsNullOrEmpty(parameter))
            {
                QueryResponse.AddNegativeMessage("Параметр для ограничения не указан");
            }
            else 
            {               
               var result = ParameterUniversal.AddToRestriction(parameter, conditionValue, Value, QueryResponse.IsSuccess, out bool correct);

                if (QueryResponse.IsSuccess)
                {
                    QueryResponse.Add(result);
                }
                else if (!correct)
                {
                   
                    QueryResponse.AddNegativeMessage(String.Format("Параметр с именем \"{0}\" не найден", parameter));
                }
               
            }

            return QueryResponse.ToResult();

        }

        [HttpGet]
        public object DeleteRestriction([FromQuery]string restrictionName = null)
        {
            

            if (string.IsNullOrEmpty(restrictionName))
            {
                QueryResponse.AddNegativeMessage("Ограничение не указано");
            }
            else
            {
                bool contains = ParameterUniversal.DeleteFromRestrictions(restrictionName, out bool correct);

                if(!correct)
                {
                    QueryResponse.AddNegativeMessage(String.Format("Ограничение с именем \"{0}\" не найдено", restrictionName));
                }

                if(!contains)
                {
                    QueryResponse.AddNegativeMessage("Ограничение для данного параметра не найдено");
                }
               
            }

            return QueryResponse.ToResult();
        }

        [HttpGet]
        public object DeleteAllRestriction()
        {
            ParameterUniversal.DeleteAllRestriction();

            
            return QueryResponse.ToResult();
        }

        [HttpPost]
        public object LoadRestrictionsFromFile([FromQuery]IFormFile file)
        {
            

            if ((file == null) || (string.IsNullOrEmpty(file.FileName)))
            {
                QueryResponse.AddNegativeMessage("Файл не выбран.");
            }

            if (QueryResponse.IsSuccess)
            {
                ParameterUniversal.DeleteAllRestriction();

                if (!RestrictionsFromFile(file, out List<string> message))
                    QueryResponse.AddNegativeMessages(message);
            }
            return QueryResponse.ToResult();
        }

        [HttpPost]
        public object AddRestrictionsFromFile([FromQuery]IFormFile file)
        {
            

            if ((file == null) || (string.IsNullOrEmpty(file.FileName)))
            {
                QueryResponse.AddNegativeMessage("Файл не выбран.");
            }

            if (QueryResponse.IsSuccess)
            {
                if (!RestrictionsFromFile(file, out List<string> message))
                    QueryResponse.AddNegativeMessages(message);
            }
            return QueryResponse.ToResult();
        }


        private bool RestrictionsFromFile(IFormFile file , out List<string> message)
        {
            message = new List<string>();

            using (StreamReader streamReader = new StreamReader(file.OpenReadStream()))
            {
                using (CsvReader csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                {

                    csvReader.Configuration.Delimiter = ";";
                    try
                    {
                        List<Restriction> restrictions = csvReader.GetRecords<Restriction>().ToList();
                        foreach (var restriction in restrictions)
                        {

                            if (restriction.AddedToRestriction())
                            {
                                message.Add(String.Format("Ограничение для  параметра {0} уже добавлено.", restriction.GetName()));
                            }
                            else
                            {
                                if(!restriction.AddToRestriction())
                                {
                                    message.Add(String.Format("Файл содержит не корректный параметр {0}", restriction.GetName()));

                                    break;
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        message.Add(String.Format("Файл {0} не корректен, выберите файл, сохраненный системой", file.FileName));
                    }
                }
            }
            return message.Count()<1;

        }


        [HttpGet] 
        public object SaveRestrictionsToFile([FromQuery]string fileName)
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
          
                    csvWriter.WriteRecords(ParameterUniversal.GetRestrictions());
                }

            }

            return File(memory.ToArray(), MimeTypesMap.GetMimeType(filePath), filePath);
        }

        [HttpGet]
        public object ValidateRestrictionsBeforeSave()
        {           
            if(ParameterUniversal.GetRestrictions().Count<1)
                QueryResponse.AddNegativeMessage("Ограничения для сохранения не добавлены");

            return QueryResponse.ToResult();
        }

    }
}
