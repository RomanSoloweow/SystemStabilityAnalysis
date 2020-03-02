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
    public class Analysis : BaseController
    {

        [HttpGet]
        public object GetSystems()
        {
            
            QueryResponse.Add("Systems", StaticData.Systems.Keys.ToList());

            return QueryResponse.ToResult();
        }

        [HttpGet]
        public object GetParametersForChart()
        {

            
            QueryResponse.Add("ParametersForChart", StaticData.CurrentSystems.GetParametersForChart());

            return QueryResponse.ToResult();
        }

        [HttpGet]
        public object GetParametersForDiagram()
        {
            
            QueryResponse.Add("ParametersForDiagram", StaticData.CurrentSystems.GetParametersForDiagram());

            return QueryResponse.ToResult();
        }

        [HttpGet]
        public object GetCalculationForChart([FromQuery]string queryString)
        {
            ParameterForCalculationChart parameterForCalculationChart = JsonConvert.DeserializeObject<ParameterForCalculationChart>(queryString);
             

            if (parameterForCalculationChart.namesSystems.Count < 1)
            {
                QueryResponse.AddNegativeMessage("Для построения графика необходимо выбрать одну или несколько систем");
            }
            if (string.IsNullOrWhiteSpace(parameterForCalculationChart.parameterName))
            {
                QueryResponse.AddNegativeMessage("Для построения графика необходимо выбрать параметр");
            }
            if (!parameterForCalculationChart.From.HasValue)
            {
                QueryResponse.AddNegativeMessage("Для построения графика необходимо указать начальное значение параметра");
            }

            if (!parameterForCalculationChart.To.HasValue)
            {
                QueryResponse.AddNegativeMessage("Для построения графика необходимо указать конечное значение параметра");
            }

            if (!parameterForCalculationChart.CountDote.HasValue)
            {
                QueryResponse.AddNegativeMessage("Для построения графика необходимо указать количество точек");
            }

            if (QueryResponse.IsSuccess)
            {
                List<object> calculations = new List<object>();
                foreach (var nameSystem in parameterForCalculationChart.namesSystems)
                {
                    calculations.Add(StaticData.Systems[nameSystem].GetCalculationsForChart(parameterForCalculationChart));
                }

                QueryResponse.Add("ParameterNameX", StaticData.CurrentSystems.GetParameterDesignation(parameterForCalculationChart.parameterName));
                QueryResponse.Add("ParameterNameY", StaticData.CurrentSystems.GetParameterDesignation("U"));
                QueryResponse.Add("Calculations", calculations);

            }
            return QueryResponse.ToResult();

        }

        [HttpGet]
        public object GetCalculationForDiagram([FromQuery]string queryString)
        {
            ParameterForCalculationDiagram parameterForCalculationDiagram = JsonConvert.DeserializeObject<ParameterForCalculationDiagram>(queryString);

            

            if (parameterForCalculationDiagram.namesSystems.Count < 1)
            {
                QueryResponse.AddNegativeMessage("Для построения диаграммы необходимо выбрать одну или несколько систем");
            }
            if (string.IsNullOrWhiteSpace(parameterForCalculationDiagram.parameterName))
            {
                QueryResponse.AddNegativeMessage("Для построения диаграммы необходимо выбрать параметр");
            }
            if (QueryResponse.IsSuccess)
            {
                List<object> calculations = new List<object>();
                foreach (var nameSystem in parameterForCalculationDiagram.namesSystems)
                {
                    calculations.Add(new
                    {

                        NameSystem = nameSystem,
                        Value = StaticData.Systems[nameSystem].GetParameterValue(parameterForCalculationDiagram.parameterName)
                    });

                }
                QueryResponse.Add("ParameterName", StaticData.CurrentSystems.GetParameterDesignation(parameterForCalculationDiagram.parameterName));
                QueryResponse.Add("Calculations", calculations);
            }
            return QueryResponse.ToResult();
        }

        [HttpPost]
        public object LoadSystemFromFile([FromQuery]IFormFile file)
        {
            
            if ((file == null) || (string.IsNullOrEmpty(file.FileName)))
            {
                QueryResponse.AddNegativeMessage("Файл не выбран");

            }

            if (QueryResponse.IsSuccess)
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
                                QueryResponse.AddNegativeMessage(String.Format("Файл {0} не корректен, выберите файл, сохраненный системой", file.FileName));
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
                            QueryResponse.AddNegativeMessage(String.Format("Файл {0} не корректен, выберите файл, сохраненный системой", file.FileName));
                        }
                    }
                }
            }


            return QueryResponse.ToResult();
        }

        [HttpGet]
        public object DeleteSystem([FromQuery]string nameSystem)
        {
            
            if(string.IsNullOrEmpty(nameSystem))
            {
                QueryResponse.AddNegativeMessage("Система для удаления не указана");
            }
            else if(!StaticData.Systems.Keys.Contains(nameSystem))
            {
                QueryResponse.AddNegativeMessage($"Невозможно удалить систему. Система  с именем \"{nameSystem}\" не найдена");
            }
            else
            {
                StaticData.Systems.Remove(nameSystem);
            }
           

            return QueryResponse.ToResult();
        }


        [HttpGet]
        public object SaveChartToFile([FromQuery]string chart)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "test.csv");

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }

            memory.Position = 0;
            return File(memory, "text/csv", Path.ChangeExtension("chart", ".csv"));

            //if (string.IsNullOrEmpty(chart))
            //{
            //    QueryResponse.AddNegativeMessage("Строка пустая");

            //}
            //else
            //{
            //    QueryResponse.AddSuccessMessage("Что-то дошло");
            //}
            //return QueryResponse.ToResult();
        }

        [HttpGet]
        public object SaveDiagramToFile([FromQuery]string diagram)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "test.csv");

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }

            memory.Position = 0;
            return File(memory, "text/csv", Path.ChangeExtension("diagram", ".csv"));

            //if (string.IsNullOrEmpty(diagram))
            //{
            //    QueryResponse.AddNegativeMessage("Строка пустая");

            //}
            //else
            //{
            //    QueryResponse.AddSuccessMessage("Что-то дошло");
            //}
            //return QueryResponse.ToResult();
        }

        [HttpGet]
        public object ValidateDiagramBeforeSave([FromQuery]string queryString)
        {
            if (string.IsNullOrEmpty(queryString))
            {
                QueryResponse.AddNegativeMessage("Строка пустая");

            }
            else
            {
                QueryResponse.AddSuccessMessage("График ок");
            }
            

            return QueryResponse.ToResult();
        }

        [HttpGet]
        public object ValidateChartBeforeSave([FromQuery]string queryString)
        {
            if (string.IsNullOrEmpty(queryString))
            {
                QueryResponse.AddNegativeMessage("Строка пустая");

            }
            else
            {
                QueryResponse.AddSuccessMessage("Диаграмма ок");
            }
            

            return QueryResponse.ToResult();
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
