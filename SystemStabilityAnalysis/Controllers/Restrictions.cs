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

namespace SystemStabilityAnalysis.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Produces("application/json")]
    public class Restrictions : ControllerBase
    {
        [HttpGet]
        public object GetParameters()
        {
            var ParametersWithEnter = HelperEnum.GetValuesWithoutDefault<NameParameterWithEnter>().Where(x => !StaticData.ConditionsForParameterWithEnter.ContainsKey(x)).Select(x => x.ToJson());
            var ParametersWithCalculation = HelperEnum.GetValuesWithoutDefault<NameParameterWithCalculation>().Where(x => !StaticData.ConditionsForParameterWithCalculation.ContainsKey(x)).Select(x => x.ToJson());
            var ParametersForAnalysis = HelperEnum.GetValuesWithoutDefault<NameParameterForAnalysis>().Where(x => !StaticData.ConditionsForParameterForAnalysis.ContainsKey(x)).Select(x => x.ToJson());
            
            return new
            {
                Status = Status.Success.GetName(),
                Properties = ParametersWithEnter.Union(ParametersWithCalculation).Union(ParametersForAnalysis)
            };
        }

        [HttpGet]
        public object GetConditions()
        {
            return new
            {
                Status = Status.Success.GetName(),
                Conditions = HelperEnum.GetValuesWithoutDefault<ConditionType>().Select(x => x.ToJson())
            };
        }

        [HttpGet]
        public object GetRestrictions()
        {
            return new
            {
                Status = Status.Success.GetName(),
                Restrictions = ParameterUniversal.GetRestrictions().Select(x => x.ToResponse())
            };
        }

        [HttpGet]
        public object AddRestriction(string parameter = null, string condition = null, string value = null)
        {
            QueryResponse queryResponse = new QueryResponse();

            ConditionType conditionValue = ConditionType.NoCorrect;
            double Value = 0 ;

            if (string.IsNullOrEmpty(value))
            {
                queryResponse.AddNegativeMessage("Значение ограничения не указано");
            }
            else
            {
                if (!double.TryParse(value, out Value))
                {
                    queryResponse.AddNegativeMessage(String.Format("Указанное значение \"{0}\" не является числом", value));
                }
                else if (!(Value > 0))
                {
                    queryResponse.AddNegativeMessage("Значение ограничения должно быть > 0");
                }
            }

            if (string.IsNullOrEmpty(condition))
            {
                queryResponse.AddNegativeMessage("Условие для ограничения не указано");
            }
            else
            {
                HelperEnum.TryGetValue<ConditionType>(condition, out conditionValue);

                if (HelperEnum.IsDefault(conditionValue))
                {
                    queryResponse.AddNegativeMessage(String.Format("Условие типа \"{0}\" не найдено", condition));
                }
            }

            if (string.IsNullOrEmpty(parameter))
            {
                queryResponse.AddNegativeMessage("Параметр для ограничения не указан");
            }
            else 
            {               
               var result = ParameterUniversal.AddToRestriction(parameter, conditionValue, Value, queryResponse.IsSuccess, out bool correct);

                if (queryResponse.IsSuccess)
                {
                   return result;
                }

                if (!correct)
                {
                   
                    queryResponse.AddNegativeMessage(String.Format("Параметр с именем \"{0}\" не найден", parameter));
                }
               
            }

            return queryResponse.ToResult();

        }

        [HttpGet]
        public object DeleteRestriction([FromQuery]string restrictionName = null)
        {
            QueryResponse queryResponse = new QueryResponse();

            if (string.IsNullOrEmpty(restrictionName))
            {
                queryResponse.AddNegativeMessage("Ограничение не указано");
            }
            else
            {
                bool contains = ParameterUniversal.DeleteFromRestrictions(restrictionName, out bool correct);

                if(!correct)
                {
                    queryResponse.AddNegativeMessage(String.Format("Ограничение с именем \"{0}\" не найдено", restrictionName));
                }

                if(!contains)
                {
                    queryResponse.AddNegativeMessage("Ограничение для данного параметра не найдено");
                }
               
            }

            return queryResponse.ToResult();
        }

        [HttpGet]
        public object DeleteAllRestriction()
        {
            ParameterUniversal.DeleteAllRestriction();
            return new
            {
                Status = Status.Success.GetName()
            };
        }

        //[HttpPost]
        //public object LoadRestrictionsFromFile([FromQuery]IFormFile file)
        //{
        //    QueryResponse queryResponse = new QueryResponse();

        //    if ((file == null) || (string.IsNullOrEmpty(file.FileName)))
        //    {
        //        queryResponse.AddError("Файл не выбран.");
        //        return queryResponse.ToResult();
        //    }
        //    if (queryResponse.IsCorrect)
        //    {
        //        List<object> Restrictions = new List<object>();
        //        using (StreamReader streamReader = new StreamReader(file.OpenReadStream()))
        //        {
        //            using (CsvReader csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
        //            {

        //                csvReader.Configuration.Delimiter = ";";
        //                //csvReader.Configuration.HasHeaderRecord = false;
        //                try
        //                {
        //                    List<Restriction> restrictions = csvReader.GetRecords<Restriction>().ToList();
        //                    foreach (var restriction in restrictions)
        //                    {

        //                        if (restriction.AddedToRestriction())
        //                        {
        //                            queryResponse.AddError(String.Format("Ограничение для  параметра {0} уже добавлено.", restriction.GetName()));
        //                        }
        //                        else
        //                        {
        //                            bool correct = restriction.AddToRestriction();
        //                            if (correct)
        //                            {
        //                                Restrictions.Add(restriction.ToResponse());
        //                            }
        //                            else
        //                            {
        //                                queryResponse.AddError(String.Format("Файл содержит не корректный параметр {0}", restriction.GetName()));

        //                                break;
        //                            }
        //                        }
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    queryResponse.AddError(String.Format("Файл {0} не корректен, выберите файл, сохраненный системой", file.FileName));
        //                    return queryResponse.ToResult();
        //                }
        //            }
        //        }

        //        if (!queryResponse.IsCorrect)
        //            return queryResponse.ToResult();

        //    }
        //    return queryResponse.ToResult();
        //}


        [HttpPost]
        public object LoadRestrictionsFromFile([FromQuery]IFormFile file)
        {
            QueryResponse queryResponse = new QueryResponse();

            if ((file == null) || (string.IsNullOrEmpty(file.FileName)))
            {
                queryResponse.AddNegativeMessage("Файл не выбран.");
            }

            if (queryResponse.IsSuccess)
            {
                ParameterUniversal.DeleteAllRestriction();

                if (!RestrictionsFromFile(file, out List<string> message))
                    queryResponse.AddNegativeMessages(message);
            }
            return queryResponse.ToResult();
        }

        [HttpPost]
        public object AddRestrictionsFromFile([FromQuery]IFormFile file)
        {
            QueryResponse queryResponse = new QueryResponse();

            if ((file == null) || (string.IsNullOrEmpty(file.FileName)))
            {
                queryResponse.AddNegativeMessage("Файл не выбран.");
            }

            if (queryResponse.IsSuccess)
            {
                if (!RestrictionsFromFile(file, out List<string> message))
                    queryResponse.AddNegativeMessages(message);
            }
            return queryResponse.ToResult();
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
          
                    csvWriter.WriteRecords(ParameterUniversal.GetRestrictions());
                }

            }

            return File(memory.ToArray(), MimeTypesMap.GetMimeType(filePath), filePath);
        }

        [HttpGet]
        public object ValidateRestrictionsBeforeSave()
        {
            QueryResponse queryResponse = new QueryResponse();

            if(ParameterUniversal.GetRestrictions().Count<1)
                queryResponse.AddNegativeMessage("Ограничения для сохранения не добавлены");

            return queryResponse.ToResult();
        }

    }
}
