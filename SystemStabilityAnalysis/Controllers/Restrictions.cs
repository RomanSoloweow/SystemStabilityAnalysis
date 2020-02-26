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
        public object AddRestriction(string parameter = null, string condition = null, string value = null)
        {
            ResponceResult responceResult = new ResponceResult();

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
                var result = ParameterUniversal.AddToRestriction(parameter, conditionValue, Value, out bool correct);
                if(correct)
                {
                    return result;
                }
                else
                {
                    responceResult.AddError(String.Format("Параметр с именем \"{0}\" не найден", parameter));
                }
               
            }

            return responceResult.ToResult();

        }

        [HttpGet]
        public object DeleteRestriction([FromQuery]string restrictionName = null)
        {
            ResponceResult responceResult = new ResponceResult();

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

        [HttpPost]
        public object LoadRestrictionsFromFile([FromQuery]IFormFile file)
        {
            ResponceResult responceResult = new ResponceResult();

            if ((file == null) || (string.IsNullOrEmpty(file.FileName)))
            {
                responceResult.AddError("Файл не выбран.");
                return responceResult.ToResult();
            }
            
            using (StreamReader streamReader = new StreamReader(file.OpenReadStream()))
            {            
                using (CsvReader csvReader = new CsvReader(streamReader, CultureInfo.CurrentCulture))
                {

                    csvReader.Configuration.Delimiter = ",";
                    //csvReader.Configuration.HasHeaderRecord = false;
                    try
                    {
                       foreach(var restriction in  csvReader.GetRecords<Restriction>().ToList())
                        {
                            var result = ParameterUniversal.AddToRestriction(restriction.ParameterName, restriction.Condition, restriction.Value, out bool correct);
                            if (!correct)
                            {
                                responceResult.AddError(String.Format("Не корректный параметр {0}", restriction.ParameterName));
                                break;
                            }
                        }
                    }
                    catch(Exception ex)
                    {
                        responceResult.AddError(String.Format("Файл {0} не корректен, выберите файл, сохраненный системой", file.FileName));
                        return responceResult.ToResult();
                    }                  
                }
            }

            return responceResult.ToResult();
            //var ParametersWithEnter = HelperEnum.GetValuesWithoutDefault<NameParameterWithEnter>().Where(x => !StaticData.ConditionsForParameterWithEnter.ContainsKey(x)).Select(x=> x.ToRestriction(ConditionType.More, 0.111));
            //var ParametersWithCalculation = HelperEnum.GetValuesWithoutDefault<NameParameterWithCalculation>().Where(x => !StaticData.ConditionsForParameterWithCalculation.ContainsKey(x)).Select(x => x.ToRestriction(ConditionType.More, 0.111));
            //var ParametersForAnalysis = HelperEnum.GetValuesWithoutDefault<NameParameterForAnalysis>().Where(x => !StaticData.ConditionsForParameterForAnalysis.ContainsKey(x)).Select(x => x.ToRestriction(ConditionType.More, 0.111));
            //return new
            //{
             
            //    Status = Status.Success.GetName(),
            //    Restrictions = ParametersWithEnter.Union(ParametersWithCalculation).Union(ParametersForAnalysis)
            //};
        }

        [HttpGet] 
        public object SaveRestrictionsToFile([FromQuery]string fileName)
        {
            ResponceResult responceResult = new ResponceResult();

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
            using (StreamWriter streamWriter = new StreamWriter(memory))
            {
                using (var csv = new CsvWriter(streamWriter, CultureInfo.CurrentCulture))
                {
                    csv.WriteRecords(Restriction.GetRestctions());
                }

            }

            //memory.Position = 0;
            return File(memory.ToArray(), MimeTypesMap.GetMimeType(filePath), filePath);
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
