﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SystemStabilityAnalysis.Helpers;
using SystemStabilityAnalysis.Models;
using SystemStabilityAnalysis.Models.Parameters;

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
                Systems = StaticData.Systems.Keys.ToList()
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
            QueryResponse queryResponse = new QueryResponse(); 

            if (parameterForCalculationChart.namesSystems.Count < 1)
            {
                queryResponse.AddNegativeMessage("Для построения графика необходимо выбрать одну или несколько систем");
            }
            if (string.IsNullOrWhiteSpace(parameterForCalculationChart.parameterName))
            {
                queryResponse.AddNegativeMessage("Для построения графика необходимо выбрать параметр");
            }
            if (!parameterForCalculationChart.From.HasValue)
            {
                queryResponse.AddNegativeMessage("Для построения графика необходимо указать начальное значение параметра");
            }

            if (!parameterForCalculationChart.To.HasValue)
            {
                queryResponse.AddNegativeMessage("Для построения графика необходимо указать конечное значение параметра");
            }

            if (!parameterForCalculationChart.CountDote.HasValue)
            {
                queryResponse.AddNegativeMessage("Для построения графика необходимо указать количество точек");
            }
            
            if(!queryResponse.IsSuccess)
            {
                return queryResponse.ToResult();
            }
   
            List<object> calculations = new List<object>();
            foreach (var nameSystem in parameterForCalculationChart.namesSystems)
            {
                calculations.Add(StaticData.Systems[nameSystem].GetCalculationsForChart(parameterForCalculationChart));
            }

            return new
            {
                Status = Status.Success.GetName(),
                ParameterNameX = StaticData.CurrentSystems.GetParameterDesignation(parameterForCalculationChart.parameterName),
                ParameterNameY = StaticData.CurrentSystems.GetParameterDesignation("U"),
                Calculations = calculations
            };
    
        }

        [HttpGet]
        public object GetCalculationForDiagram([FromQuery]string queryString)
        {
            ParameterForCalculationDiagram parameterForCalculationDiagram = JsonConvert.DeserializeObject<ParameterForCalculationDiagram>(queryString);

            QueryResponse queryResponse = new QueryResponse();

            if (parameterForCalculationDiagram.namesSystems.Count < 1)
            {
                queryResponse.AddNegativeMessage("Для построения диаграммы необходимо выбрать одну или несколько систем");
            }
            if (string.IsNullOrWhiteSpace(parameterForCalculationDiagram.parameterName))
            {
                queryResponse.AddNegativeMessage("Для построения диаграммы необходимо выбрать параметр");
            }
            if (!queryResponse.IsSuccess)
            {
                return queryResponse.ToResult();
            }

            List<object> calculations = new List<object>();
            foreach (var nameSystem in parameterForCalculationDiagram.namesSystems)
            {
                calculations.Add(new 
                {

                    NameSystem = nameSystem,
                    Value = StaticData.Systems[nameSystem].GetParameterValue(parameterForCalculationDiagram.parameterName)
                });

            }
            
            return new
            {
                Status = Status.Success.GetName(),
                ParameterName = StaticData.CurrentSystems.GetParameterDesignation(parameterForCalculationDiagram.parameterName),
                Calculations = calculations
            };
        }

        [HttpPost]
        public object LoadSystemFromFile([FromQuery]IFormFile file)
        {
            QueryResponse queryResponse = new QueryResponse();
            if ((file == null) || (string.IsNullOrEmpty(file.FileName)))
            {
                queryResponse.AddNegativeMessage("Файл не выбран");

            }

            if (queryResponse.IsSuccess)
            {
                using (StreamReader streamReader = new StreamReader(file.OpenReadStream()))
                {
                    using (CsvReader csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture))
                    {

                        csvReader.Configuration.Delimiter = ";";
                        try
                        {
                            List<ParameterWithEnter> parametersWithEnter = csvReader.GetRecords<ParameterWithEnter>().ToList();
                            if (parametersWithEnter.Count != HelperEnum.GetValuesWithoutDefault<NameParameterWithEnter>().Count)
                            {
                                queryResponse.AddNegativeMessage(String.Format("Файл {0} не корректен, выберите файл, сохраненный системой", file.FileName));
                            }
                            else
                            {
                                string nameSystem = Path.GetFileNameWithoutExtension(file.FileName);
                                SystemForAnalys systemForAnalys = new SystemForAnalys(nameSystem);
                                foreach (var parameterWithEnter in parametersWithEnter)
                                {
                                    systemForAnalys.ParametersWithEnter[parameterWithEnter.TypeParameter].Value = parameterWithEnter.Value;
                                }
                                systemForAnalys.SetAsCorrect();
                                StaticData.Systems.Add(nameSystem, systemForAnalys);
                            }
                        }
                        catch (Exception ex)
                        {
                            queryResponse.AddNegativeMessage(String.Format("Файл {0} не корректен, выберите файл, сохраненный системой", file.FileName));
                        }
                    }
                }
            }


            return queryResponse.ToResult();
        }

        [HttpGet]
        public object DeleteSystem([FromQuery]string nameSystem)
        {
            QueryResponse queryResponse = new QueryResponse();
            if(string.IsNullOrEmpty(nameSystem))
            {
                queryResponse.AddNegativeMessage("Система для удаления не указана");
            }
            else if(!StaticData.Systems.Keys.Contains(nameSystem))
            {
                queryResponse.AddNegativeMessage($"Система  с именем \"{nameSystem}\" не найдена");
            }
            else
            {
                StaticData.Systems.Remove(nameSystem);
            }
           

            return queryResponse.ToResult();
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
