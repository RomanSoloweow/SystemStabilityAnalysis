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
                Properties = HelperEnum.GetValuesWithoutDefault<ParametersName>().Select(x => x.ToJson())
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
        //public object AddRestriction([FromBody]string parameter, string condition, double value = 0)
        //{
        //    Status status = Status.Success;

        //    //List<string> Message = new List<string>();

        //    //if (HelperEnum.IsDefault(parameter))
        //    //{
        //    //    Message.Add("Параметр указано некорректно");
        //    //    status = Status.Error;
        //    //}

        //    //if (HelperEnum.IsDefault(condition))
        //    //{
        //    //    Message.Add("Условие указано некорректно");
        //    //    status = Status.Error;
        //    //}
        //    //if (!(value > 0))
        //    //{
        //    //    Message.Add("Значение параметра должно быть > 0");
        //    //    status = Status.Error;
        //    //}

        //    //if (status == Status.Error)
        //    //{
        //    //    return new
        //    //    {
        //    //        Status = status.GetName(),
        //    //        Message = Message
        //    //    };
        //    //}

        //    return new
        //    {
        //        Status = status.GetName()
        //    };
        //}
        [HttpPost("{parameter}/{condition}/{value}")]
        public object AddRestriction(ParametersName parameter = ParametersName.NoCorrect, ConditionType condition = ConditionType.NoCorrect, double value = 0)
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

            return new
            {
                Status = status.GetName()
            };
        }
    }
}
