﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SystemStabilityAnalysis.Helpers;
using SystemStabilityAnalysis.Models;
using System.Text.Json;
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
            return new
            {
                Status = Status.Success.GetName(),
                Properties = HelperEnum.GetValuesWithoutDefault<NameParameterWithEnter>().Where(x=> !StaticData.Conditions.ContainsKey(x)).Select(x => x.ToJson())
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
            NameParameterWithEnter parameterValue = NameParameterWithEnter.NoCorrect;
            ConditionType conditionValue = ConditionType.NoCorrect;
            int Value = 0 ;

            if (parameter == null)
            {
                Message.Add("Параметр для ограничения не указан");
                status = Status.Error;
            }
            else
            {
                HelperEnum.TryGetValue<NameParameterWithEnter>(parameter, out parameterValue);

                if (HelperEnum.IsDefault(parameterValue))
                {
                    Message.Add(String.Format("Параметр с именем \"{0}\" не найден", parameter));
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

            if (value == null)
            {
                Message.Add("Значение ограничения не указано");
                status = Status.Error;
            }
            else
            {
                if (!int.TryParse(value, out Value))
                {
                    Message.Add(String.Format("Указанное значение {0} не является числом", value));
                    status = Status.Error;
                }
                else if (!(Value > 0))
                {
                    Message.Add("Значение ограничения должно быть > 0");
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

            StaticData.Conditions.Add(parameterValue, new Condition(conditionValue, Value));

            return new
            {
                Status = status.GetName(),
                Name = parameterValue.GetDesignation(),
                Description = parameterValue.GetDescription(),
                Unit = parameterValue.GetUnit().GetDescription(),
                Condition = conditionValue.GetDesignation(),
                Value = value,
                RestrictionName = parameterValue.GetName()
            };
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

        [HttpGet]
        public object DeleteRestriction([FromQuery]string restrictionName = null)
        {
            Status status = Status.Success;
            List<string> Message = new List<string>();
            NameParameterWithEnter parameterValue = NameParameterWithEnter.NoCorrect;
            if (restrictionName == null)
            {
                Message.Add("Ограничение не указано");
                status = Status.Error;
            }
            else
            {
                HelperEnum.TryGetValue<NameParameterWithEnter>(restrictionName, out parameterValue);

                if (HelperEnum.IsDefault(parameterValue))
                {
                    Message.Add(String.Format("Ограничение с именем \"{0}\" не найдено", restrictionName));
                    status = Status.Error;
                }
                else if(!StaticData.Conditions.ContainsKey(parameterValue))
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

            StaticData.Conditions.Remove(parameterValue);

            return new
            {
                Status = Status.Success.GetName()
            };
        }

        [HttpGet]
        public object DeleteAllRestriction()
        {
            StaticData.Conditions.Clear();
            return new
            {
                Status = Status.Success.GetName()
            };
        }


    }
}
