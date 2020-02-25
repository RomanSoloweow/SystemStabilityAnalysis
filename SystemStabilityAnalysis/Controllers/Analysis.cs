using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SystemStabilityAnalysis.Models;

namespace SystemStabilityAnalysis.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [Produces("application/json")]
    public class Analysis : ControllerBase
    {
        
        [HttpGet]
        public object GetSystems()
        {
            return new
            {
                Status = Status.Success.GetName(),
                Systems = new List<string>() { "Система1", "Система2", "Система3", "Система4", "Система5", "Система6", "Система7" }
            };
        }
        [HttpGet]
        public object GetParametersForChart()
        {
            return new
            {
                Status = Status.Success.GetName(),
                ParametersForChart = StaticData.CurrentSystems.GetParametersForChart()
            };
        }

        [HttpGet]
        public object GetParametersForDiagram()
        {
            return new
            {
                Status = Status.Success.GetName(),
                ParametersForDiagram = StaticData.CurrentSystems.GetParametersForDiagram()
            };
        }

        [HttpGet]
        public object GetCalculationForChart([FromQuery]string queryString)
        {
            ParameterForCalculationChart parameterForCalculationChart = JsonConvert.DeserializeObject<ParameterForCalculationChart>(queryString);
            if(parameterForCalculationChart.namesSystems.Count<1)
            {
                return new
                {
                    Status = Status.Error.GetName(),
                    Message ="Для построения графика выберите системы"
                };
            }
            

            List<object> calculations = new List<object>();
            Random random = new Random();
            List<double> values = new List<double>();
            int count = 100;
            foreach (var t in parameterForCalculationChart.namesSystems)
            {
                values.Clear();
                for(int i = 0; i<count;i++)
                {
                    values.Add(random.NextDouble());
                }
                calculations.Add(new
                {
                    NameSystem = t,
                    Values = values
                });

            }

            return new
            {
                Status = Status.Success.GetName(),
                ParameterName = parameterForCalculationChart.parameterName,
                Calculations = calculations
            };
    
        }


        [HttpGet]
        public object GetCalculationForDiagram([FromQuery]string queryString)
        {
            ParameterForCalculationDiagram parameterForCalculationDiagram = JsonConvert.DeserializeObject<ParameterForCalculationDiagram>(queryString);
            List<object> calculations = new List<object>();
            Random random = new Random();
            foreach (var t in parameterForCalculationDiagram.namesSystems)
            {
                calculations.Add(new 
                {
                    NameSystem = t,
                    Value = random.NextDouble()
                });

            }

              

            return new
            {             
                Status = Status.Success.GetName(),
                ParameterName = parameterForCalculationDiagram.parameterName,
                Calculations = calculations
            };
        }

        public class ParameterForCalculationChart
        {
            public List<string> namesSystems { get; set; }
            public double? From { get; set; }
            public double? To { get; set; }
            public int? CountDote { get; set; }
            public string parameterName { get; set; }
        }

        public class ParameterForCalculationDiagram
        {
            public List<string> namesSystems { get; set; }
            public string parameterName { get; set; }
        }
    }
}
