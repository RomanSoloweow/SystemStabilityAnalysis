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
            Status status = Status.Success;
            List<string> Message = new List<string>();
            ConditionType conditionValue = ConditionType.NoCorrect;
            int Value = 0 ;

            if (value == null)
            {
                Message.Add("Значение ограничения не указано");
                status = Status.Error;
            }
            else
            {
                if (!int.TryParse(value, out Value))
                {
                    Message.Add(String.Format("Указанное значение \"{0}\" не является числом", value));
                    status = Status.Error;
                }
                else if (!(Value > 0))
                {
                    Message.Add("Значение ограничения должно быть > 0");
                    status = Status.Error;
                }
            }

            if (parameter == null)
            {
                Message.Add("Условие для ограничения не указано");
                status = Status.Error;
            }
            else
            {
                HelperEnum.TryGetValue<ConditionType>(condition, out conditionValue);

                if (HelperEnum.IsDefault(conditionValue))
                {
                    Message.Add(String.Format("Условие типа \"{0}\" не найдено", condition));
                    status = Status.Error;
                }
            }

            if (parameter == null)
            {
                Message.Add("Параметр для ограничения не указан");
                status = Status.Error;
            }
            else 
            {

                if (Enum.TryParse(parameter, out NameParameterWithEnter parameterWithEnter))
                {
                    if (status != Status.Error)
                    {
                        parameterWithEnter.AddToRestrictions(conditionValue, Value);
                        return parameterWithEnter.ToRestriction(conditionValue, Value);
                    }
                }
                else if (Enum.TryParse(parameter, out NameParameterWithCalculation parameterWithCalculation))
                {
                    if (status != Status.Error)
                    {
                        parameterWithCalculation.AddToRestrictions(conditionValue, Value);
                        return parameterWithCalculation.ToRestriction(conditionValue, Value);
                    }
                }
                else if (Enum.TryParse(parameter, out NameParameterForAnalysis parameterForAnalysis))
                {
                    if (status != Status.Error)
                    {
                        parameterForAnalysis.AddToRestrictions(conditionValue, Value);
                        return parameterForAnalysis.ToRestriction(conditionValue, Value);
                    }
                }
                else
                {
                    Message.Add(String.Format("Параметр с именем \"{0}\" не найден", parameter));
                    status = Status.Error;
                }
               
            }

            return new
            {
                Status = status.GetName(),
                Message = Message
            };

        }

        [HttpGet]
        public object DeleteRestriction([FromQuery]string restrictionName = null)
        {
            Status status = Status.Success;
            List<string> Message = new List<string>();
            if (restrictionName == null)
            {
                Message.Add("Ограничение не указано");
                status = Status.Error;
            }
            else
            {
                bool contains = true;
                if (Enum.TryParse(restrictionName, out NameParameterWithEnter parameterWithEnter))
                {
                    contains = parameterWithEnter.DeleteFromRestrictions();
                }
                else if (Enum.TryParse(restrictionName, out NameParameterWithCalculation parameterWithCalculation))
                {
                    contains = parameterWithCalculation.DeleteFromRestrictions();
                }
                else if (Enum.TryParse(restrictionName, out NameParameterForAnalysis parameterForAnalysis))
                {
                    contains = parameterForAnalysis.DeleteFromRestrictions();
                }
                else
                {
                    Message.Add(String.Format("Ограничение с именем \"{0}\" не найдено", restrictionName));
                    status = Status.Error;
                }

                if(!contains)
                {
                    Message.Add("Ограничение для данного параметра не найдено");
                    status = Status.Error;
                }
               
            }
            
            if (status == Status.Error)
            {
                return new
                {
                    Status = status.GetName(),
                    Message = Message
                };
            }
            return new
            {
                Status = Status.Success.GetName()
            };
        }

        [HttpGet]
        public object DeleteAllRestriction()
        {
            StaticData.ConditionsForParameterWithCalculation.Clear();
            StaticData.ConditionsForParameterForAnalysis.Clear();
            StaticData.ConditionsForParameterWithEnter.Clear();
            return new
            {
                Status = Status.Success.GetName()
            };
        }

        public class Foo
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        [HttpPost]
        public object LoadRestrictionsFromFile([FromQuery]IFormFile file)
        {
           if((file==null)||(string.IsNullOrEmpty(file.FileName)))
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


            var ParametersWithEnter = HelperEnum.GetValuesWithoutDefault<NameParameterWithEnter>().Where(x => !StaticData.ConditionsForParameterWithEnter.ContainsKey(x)).Select(x => x.ToRestriction(ConditionType.More, 0.111));
            var ParametersWithCalculation = HelperEnum.GetValuesWithoutDefault<NameParameterWithCalculation>().Where(x => !StaticData.ConditionsForParameterWithCalculation.ContainsKey(x)).Select(x => x.ToRestriction(ConditionType.More, 0.111));
            var ParametersForAnalysis = HelperEnum.GetValuesWithoutDefault<NameParameterForAnalysis>().Where(x => !StaticData.ConditionsForParameterForAnalysis.ContainsKey(x)).Select(x => x.ToRestriction(ConditionType.More, 0.111));
            return new
            {
             
                Status = Status.Success.GetName(),
                Restrictions = ParametersWithEnter.Union(ParametersWithCalculation).Union(ParametersForAnalysis)
            };
        }

        [HttpGet] 
        public object SaveRestrictionsToFile([FromQuery]string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                return new
                {
                    Message = "Имя файла не указано",
                    Status = Status.Error.GetName(),
                };
            }
            var path = Path.Combine(Directory.GetCurrentDirectory(),"test.csv");

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }
           
            memory.Position = 0;
            return File(memory, "text/csv", Path.ChangeExtension(fileName, ".csv"));
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
    }
}
