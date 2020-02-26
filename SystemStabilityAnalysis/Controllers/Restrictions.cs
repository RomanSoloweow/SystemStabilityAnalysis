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
            QueryResponse responceResult = new QueryResponse();

            ConditionType conditionValue = ConditionType.NoCorrect;
            double Value = 0 ;

            if (string.IsNullOrEmpty(value))
            {
                responceResult.AddError("Значение ограничения не указано");
            }
            else
            {
                if (!double.TryParse(value, out Value))
                {
                    responceResult.AddError(String.Format("Указанное значение \"{0}\" не является числом", value));
                }
                else if (!(Value > 0))
                {
                    responceResult.AddError("Значение ограничения должно быть > 0");
                }
            }

            if (string.IsNullOrEmpty(condition))
            {
                responceResult.AddError("Условие для ограничения не указано");
            }
            else
            {
                HelperEnum.TryGetValue<ConditionType>(condition, out conditionValue);

                if (HelperEnum.IsDefault(conditionValue))
                {
                    responceResult.AddError(String.Format("Условие типа \"{0}\" не найдено", condition));
                }
            }

            if (string.IsNullOrEmpty(parameter))
            {
                responceResult.AddError("Параметр для ограничения не указан");
            }
            else 
            {               
               var result = ParameterUniversal.AddToRestriction(parameter, conditionValue, Value, responceResult.IsCorrect, out bool correct);

                if (responceResult.IsCorrect)
                {
                   return result;
                }

                if (!correct)
                {
                   
                    responceResult.AddError(String.Format("Параметр с именем \"{0}\" не найден", parameter));
                }
               
            }

            return responceResult.ToResult();

        }

        [HttpGet]
        public object DeleteRestriction([FromQuery]string restrictionName = null)
        {
            QueryResponse responceResult = new QueryResponse();

            if (string.IsNullOrEmpty(restrictionName))
            {
                responceResult.AddError("Ограничение не указано");
            }
            else
            {
                bool contains = ParameterUniversal.DeleteFromRestrictions(restrictionName, out bool correct);

                if(!correct)
                {
                    responceResult.AddError(String.Format("Ограничение с именем \"{0}\" не найдено", restrictionName));
                }

                if(!contains)
                {
                    responceResult.AddError("Ограничение для данного параметра не найдено");
                }
               
            }

            return responceResult.ToResult();
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
        //    QueryResponse responceResult = new QueryResponse();

        //    if ((file == null) || (string.IsNullOrEmpty(file.FileName)))
        //    {
        //        responceResult.AddError("Файл не выбран.");
        //        return responceResult.ToResult();
        //    }
        //    if (responceResult.IsCorrect)
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
        //                            responceResult.AddError(String.Format("Ограничение для  параметра {0} уже добавлено.", restriction.GetName()));
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
        //                                responceResult.AddError(String.Format("Файл содержит не корректный параметр {0}", restriction.GetName()));

        //                                break;
        //                            }
        //                        }
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    responceResult.AddError(String.Format("Файл {0} не корректен, выберите файл, сохраненный системой", file.FileName));
        //                    return responceResult.ToResult();
        //                }
        //            }
        //        }

        //        if (!responceResult.IsCorrect)
        //            return responceResult.ToResult();

        //    }
        //    return responceResult.ToResult();
        //}


        [HttpPost]
        public object LoadRestrictionsFromFile([FromQuery]IFormFile file)
        {
            QueryResponse responceResult = new QueryResponse();

            if ((file == null) || (string.IsNullOrEmpty(file.FileName)))
            {
                responceResult.AddError("Файл не выбран.");
            }

            if (responceResult.IsCorrect)
            {
                ParameterUniversal.DeleteAllRestriction();

                if (!RestrictionsFromFile(file, out List<string> message))
                    responceResult.AddRangeError(message);
            }
            return responceResult.ToResult();
        }

        [HttpPost]
        public object AddRestrictionsFromFile([FromQuery]IFormFile file)
        {
            QueryResponse responceResult = new QueryResponse();

            if ((file == null) || (string.IsNullOrEmpty(file.FileName)))
            {
                responceResult.AddError("Файл не выбран.");
            }

            if (responceResult.IsCorrect)
            {
                if (!RestrictionsFromFile(file, out List<string> message))
                    responceResult.AddRangeError(message);
            }
            return responceResult.ToResult();
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
            QueryResponse responceResult = new QueryResponse();

            if (string.IsNullOrEmpty(fileName))
            {
                responceResult.AddError("Имя файла не указано");
                return responceResult.ToResult();
            }

            string filePath = Path.ChangeExtension(fileName, ".csv");


            //using (var stream = new FileStream(path, FileMode.Open))
            //{
            //    stream.CopyTo(memory);
            //}


            MemoryStream memory = new MemoryStream();
            using (StreamWriter streamWriter = new StreamWriter(memory, Encoding.UTF8))
            {
                using (CsvWriter csvWriter = new CsvWriter(streamWriter, CultureInfo.CurrentCulture))
                {
                    csvWriter.Configuration.Delimiter = ";";
          
                    csvWriter.WriteRecords(ParameterUniversal.GetRestrictions());
                }

            }

            //memory.Position = 0;
            return File(memory.ToArray(), MimeTypesMap.GetMimeType(filePath), filePath);
        }

        [HttpGet]
        public object ValidateRestrictionsBeforeSave()
        {
            QueryResponse responceResult = new QueryResponse();

            if(ParameterUniversal.GetRestrictions().Count<1)
                responceResult.AddError("Ограничения для сохранения не добавлены");

            return responceResult.ToResult();
        }

        //[HttpGet("{parameter}/{condition}/{value}")]
        //public object AddRestriction(NameParameterWithRestriction parameter, ConditionType condition, double value)
        //[HttpGet]
        //public object AddRestriction([FromQuery]NameParameterWithEnter parameter, [FromQuery]ConditionType condition, [FromQuery]double value)
        //{
        //    Status status = Status.Success;

        //    List<string> Message = new List<string>();

        //    if (HelperEnum.IsDefault(parameter))
        //    {
        //        Message.Add("Параметр указано некорректно");
        //        status = Status.Error;
        //    }

        //    if (HelperEnum.IsDefault(condition))
        //    {
        //        Message.Add("Условие указано некорректно");
        //        status = Status.Error;
        //    }
        //    if (!(value > 0))
        //    {
        //        Message.Add("Значение параметра должно быть > 0");
        //        status = Status.Error;
        //    }

        //    if (status == Status.Error)
        //    {
        //        return new
        //        {
        //            Status = status.GetName(),
        //            Message = Message
        //        };
        //    }
        //    StaticData.Conditions.Add(parameter, new Condition(condition, value));

        //    return new
        //    {
        //        Status = status.GetName(),
        //        Name = parameter.GetDesignation(),
        //        Description = parameter.GetDescription(),
        //        Unit = parameter.GetUnit().GetDescription(),
        //        Condition = condition.GetDesignation(),
        //        Value = value,
        //        RestrictionName = parameter.GetName()
        //    };
        //}

        //[HttpGet]
        //public IActionResult GetBlobDownload()
        //{
        //    var net = new System.Net.WebClient();
        //    var data = net.DownloadData("test");
        //    var content = new System.IO.MemoryStream(data);
        //    var contentType = "APPLICATION/octet-stream";
        //    var fileName = "something.bin";
        //    return File(content, contentType, fileName);
        //}
    }
}
