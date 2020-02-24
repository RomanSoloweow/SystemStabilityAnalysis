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
                Properties = HelperEnum.GetValuesWithoutDefault<NameParameterWithRestriction>().Select(x => x.ToJson())
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

        //[HttpGet("{parameter}/{condition}/{value}")]
        //public object AddRestriction(NameParameterWithRestriction parameter, ConditionType condition, double value)
        [HttpGet]
        public object AddRestriction([FromQuery]NameParameterWithRestriction parameter, [FromQuery]ConditionType condition, [FromQuery]double value)
        {
            Status status = Status.Success;

            List<string> Message = new List<string>();

            if (HelperEnum.IsDefault(parameter))
            {
                Message.Add("Параметр указано некорректно");
                status = Status.Error;
            }

            if (HelperEnum.IsDefault(condition))
            {
                Message.Add("Условие указано некорректно");
                status = Status.Error;
            }
            if (!(value > 0))
            {
                Message.Add("Значение параметра должно быть > 0");
                status = Status.Error;
            }

            if (status == Status.Error)
            {
                return new
                {
                    Status = status.GetName(),
                    Message = Message
                };
            }
            StaticData.Conditions.Add(parameter, new Condition(condition, value));

            return new
            {
                Status = status.GetName(),
                Name = parameter.GetDesignation(),
                Description = parameter.GetDescription(),
                Unit = parameter.GetUnit().GetDescription(),
                Condition = condition.GetDesignation(),
                Value = value,
                RestrictionName = parameter.GetName()
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
        [HttpGet]
        public object DeleteRestriction([FromQuery]NameParameterWithRestriction restrictionName)
        {
            Status status = Status.Success;
            List<string> Message = new List<string>();

            if (HelperEnum.IsDefault(restrictionName))
            {
                Message.Add("Параметр указано некорректно");
                status = Status.Error;
            }

            if (!StaticData.Conditions.ContainsKey(restrictionName))
            {
                Message.Add("Ограничение для данного параметра не найдено");
                status = Status.Error;
            }

            if (status == Status.Error)
            {
                return new
                {
                    Status = status.GetName(),
                    Message = Message
                };
            }

            StaticData.Conditions.Remove(restrictionName);
            return new
            {
                Status = Status.Success.GetName()
            };
        }
        //[HttpGet("{parameter}/{condition}/{value}")]
        //public object AddRestriction(string parameter = null, string condition = null, string value = null)
        //{

        //    Status status = Status.Success;

        //    HelperEnum.TryGetValue<NameParameterWithRestriction>(parameter, out NameParameterWithRestriction parameterValue);
        //    HelperEnum.TryGetValue<ConditionType>(condition, out ConditionType conditionValue);
        //    int.TryParse(value, out int Value);

        //    List<string> Message = new List<string>();

        //    if (HelperEnum.IsDefault(parameterValue))
        //    {
        //        Message.Add("Параметр указано некорректно");
        //        status = Status.Error;
        //    }

        //    if (HelperEnum.IsDefault(conditionValue))
        //    {
        //        Message.Add("Условие указано некорректно");
        //        status = Status.Error;
        //    }
        //    if (!(Value > 0))
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

        //    return new
        //    {
        //        Status = status.GetName()
        //    };
        //}
    }
}
