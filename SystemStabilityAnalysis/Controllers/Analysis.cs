using System;
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
            QueryResponse responceResult = new QueryResponse(); 

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

            QueryResponse responceResult = new QueryResponse();

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
            QueryResponse responceResult = new QueryResponse();
            if ((file == null) || (string.IsNullOrEmpty(file.FileName)))
            {
                responceResult.AddError("Файл не выбран");

            }

            if (responceResult.IsCorrect)
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
                                responceResult.AddError(String.Format("Файл {0} не корректен, выберите файл, сохраненный системой", file.FileName));
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
                            responceResult.AddError(String.Format("Файл {0} не корректен, выберите файл, сохраненный системой", file.FileName));
                        }
                    }
                }
            }


            return responceResult.ToResult();
        }

        [HttpGet]
        public object DeleteSystem([FromQuery]string nameSystem)
        {
            QueryResponse responceResult = new QueryResponse();
            if(string.IsNullOrEmpty(nameSystem))
            {
                responceResult.AddError("Система не указана");
            }

            StaticData.Systems.Remove(nameSystem);

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
