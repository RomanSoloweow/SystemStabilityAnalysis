﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            ResponceResult responceResult = new ResponceResult(); 

            if (parameterForCalculationChart.namesSystems.Count < 1)
            {
                responceResult.AddError("Для построения графика необходимо выбрать одну или несколько систем");
            }
            if (string.IsNullOrWhiteSpace(parameterForCalculationChart.parameterName))
            {
                responceResult.AddError("Для построения графика необходимо выбрать параметр");
            }
            if (!parameterForCalculationChart.From.HasValue)
            {
                responceResult.AddError("Для построения графика необходимо указать начальное значение параметра");
            }

            if (!parameterForCalculationChart.To.HasValue)
            {
                responceResult.AddError("Для построения графика необходимо указать конечное значение параметра");
            }

            if (!parameterForCalculationChart.CountDote.HasValue)
            {
                responceResult.AddError("Для построения графика необходимо указать количество точек");
            }
            
            if(!responceResult.IsCorrect)
            {
                return responceResult.ToResult();
            }
            

            List<object> calculations = new List<object>();
   
            List<object> values = new List<object>();
            int count = 100;
            foreach (var t in parameterForCalculationChart.namesSystems)
            {                       
                values.Clear();
                for(int i = 0; i<count;i++)
                {
                    values.Add(new
                    {
                        x = random.NextDouble(),
                        y = random.NextDouble()
                    });
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
                ParameterNameX = "БлаБлаБла",
                ParameterNameY = "U",
                Calculations = calculations
            };
    
        }

        [HttpGet]
        public object GetCalculationForDiagram([FromQuery]string queryString)
        {
            ParameterForCalculationDiagram parameterForCalculationDiagram = JsonConvert.DeserializeObject<ParameterForCalculationDiagram>(queryString);

            ResponceResult responceResult = new ResponceResult();

            if (parameterForCalculationDiagram.namesSystems.Count < 1)
            {
                responceResult.AddError("Для построения диаграммы необходимо выбрать одну или несколько систем");
            }
            if (string.IsNullOrWhiteSpace(parameterForCalculationDiagram.parameterName))
            {
                responceResult.AddError("Для построения диаграммы необходимо выбрать параметр");
            }

            if (!responceResult.IsCorrect)
            {
                return responceResult.ToResult();
            }

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
                ParameterName = "БлаБлаБла сюда верну имя параметра",
                Calculations = calculations
            };
        }

        [HttpPost]
        public object LoadSystemFromFile([FromQuery]IFormFile file)
        {
            if ((file == null) || (string.IsNullOrEmpty(file.FileName)))
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


            var ParametersWithEnter = StaticData.CurrentSystems.GetParametersWithEnter(out List<string> message);
            return new
            {

                Status = Status.Success.GetName(),
                Parameters = ParametersWithEnter,
                Message = message
            };
        }

        [HttpGet]
        public object DeleteSystem([FromQuery]string nameSystem)
        {
            ResponceResult responceResult = new ResponceResult();
            if(string.IsNullOrEmpty(nameSystem))
            {
                responceResult.AddError("Система не указана");
            }

            return responceResult.ToResult();
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

        static Random random { get; set; } = new Random();
    }
}
