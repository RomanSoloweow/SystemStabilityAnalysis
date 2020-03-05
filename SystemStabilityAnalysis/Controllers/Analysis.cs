using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SystemStabilityAnalysis.Helpers;
using SystemStabilityAnalysis.Models;
using SystemStabilityAnalysis.Models.Parameters;
using TemplateEngine.Docx;

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
            ParameterForCalculationChart parameterForCalculationChart = ValidateChart(queryString);

            if (QueryResponse.IsSuccess)
            {
                ChartCalculationResult chartCalculationResult = new ChartCalculationResult();
                chartCalculationResult.calculations = new List<ChartCalculation>();
                foreach (var nameSystem in parameterForCalculationChart.namesSystems)
                {
                    chartCalculationResult.calculations.Add(StaticData.Systems[nameSystem].GetCalculationsForChart(parameterForCalculationChart));
                }
                chartCalculationResult.parameterNameX = StaticData.CurrentSystems.GetParameterDesignation(parameterForCalculationChart.parameterName);
                chartCalculationResult.parameterNameY = StaticData.CurrentSystems.GetParameterDesignation("U");

                StaticData.ChartCalculation = chartCalculationResult;

                QueryResponse.Add("ParameterNameX", chartCalculationResult.parameterNameX);
                QueryResponse.Add("ParameterNameY", chartCalculationResult.parameterNameY);
                QueryResponse.Add("Calculations", chartCalculationResult.calculations.Select(x=>x.ToObject()));
            }
            return QueryResponse.ToResult();

        }

        [HttpGet]
        public object GetCalculationForDiagram([FromQuery]string queryString)
        {
            ParameterForCalculationDiagram parameterForCalculationDiagram = ValidateDiagram(queryString);

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

                StaticData.DiagramCalculation = new ExpandoObject();
                StaticData.DiagramCalculation.ParameterName = StaticData.CurrentSystems.GetParameterDesignation(parameterForCalculationDiagram.parameterName);
                StaticData.DiagramCalculation.Calculations = calculations;
                QueryResponse.Add(StaticData.DiagramCalculation);
                //QueryResponse.Add("ParameterName", StaticData.CurrentSystems.GetParameterDesignation(parameterForCalculationDiagram.parameterName));
                //QueryResponse.Add("Calculations", calculations);
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

        [HttpPost]
        public object SaveDataChart([FromForm]string chart)
        {
            StaticData.DataChart = chart;
            return QueryResponse.ToResult();
        }

        [HttpPost]
        public object SaveDataDiagram([FromForm]string diagram)
        {
            StaticData.DataDiagram = diagram;
            return QueryResponse.ToResult();
        }

        [HttpGet]
        public object SaveChartToFile([FromQuery]string fileName)
        {
            byte[] chart = Convert.FromBase64String(StaticData.DataChart);

            string filePath = Path.ChangeExtension(fileName + " отчет с графиком", ".docx");

            System.IO.File.Copy("TemplatesReportsWord/ChartReportTemplate.docx", filePath);

            using (FileStream fstream = System.IO.File.Open(filePath, FileMode.Open))
            {
                List<IContentItem> fieldContents = new List<IContentItem>();


                ListContent listContent = new ListContent("systems");
                ListItemContent contentItems;
                TableContent tableContent;

                List<FieldContent> rows = new List<FieldContent>();

                //fieldContents.Add(new FieldContent("nameParameterX", "Name2"));
                //fieldContents.Add(new FieldContent("nameParameterY", "Role2"));
                //List<string> tablets = new List<string>() { "Project one", "Project two", "Project three" };
                //List<string> names = new List<string>() { "Eric", "Kel", "Bob" };
                //List<string> roles = new List<string>() { "Program Manager", "Developer", "blalbla" };


                //foreach(var calculations in StaticData.DataChart.Calculations)
                //{

                //}
                foreach(var  calculation in StaticData.ChartCalculation.calculations)
                {
                    tableContent = TableContent.Create("systemsMembers");
                    foreach (var value in calculation.values)
                    {
                        rows.Clear();
                        rows.Add(new FieldContent("parameterX", value.X.ToString()));
                        rows.Add(new FieldContent("parameterY", value.Y.ToString()));
                        tableContent.AddRow(rows.ToArray());
                    }

                    contentItems = new ListItemContent("system", calculation.nameSystem);
                    contentItems.AddTable(tableContent);
                    listContent.AddItem(contentItems);                 
                }

                fieldContents.Add(listContent);
                fieldContents.Add(new FieldContent("nameParameterX", StaticData.ChartCalculation.parameterNameX));
                fieldContents.Add(new FieldContent("nameParameterY", StaticData.ChartCalculation.parameterNameY));

                //fieldContents.Add(listContent);
                //List<FieldContent> fieldForTable = new List<FieldContent>();


                //fieldForTable.Add(new FieldContent("nameParameterX", "Test x"));
                //fieldForTable.Add(new FieldContent("nameParameterY", "Test y"));
                //fieldForTable.Add(new FieldContent("parameterX", "10"));
                //fieldForTable.Add(new FieldContent("parameterY", "15"));


                //TableContent sustem1 = new TableContent("system", new TableRowContent(fieldForTable));
                //List<ListItemContent> tablets = new List<ListItemContent>() { sustem1 };
                //ListItemContent contentItems = new ListItemContent("systems", );

                //fieldContents.Add(contentItems);




                fieldContents.Add(new ImageContent("chart", chart));
                using (var outputDocument = new TemplateProcessor(fstream).SetRemoveContentControls(true))
                {
                    outputDocument.FillContent(new Content(fieldContents.ToArray()));
                    outputDocument.SaveChanges();
                }

            }
            System.IO.File.Delete(filePath);

            //var path = Path.Combine(Directory.GetCurrentDirectory(), "test.csv");

            //var memory = new MemoryStream();
            //using (var stream = new FileStream(path, FileMode.Open))
            //{
            //    stream.CopyTo(memory);
            //}
            //var t = new
            //{
            //    fileData = File(memory, "text/csv", Path.ChangeExtension("chart", ".csv")),
            //    fileType = "csv"
            //};
            //return t;
            //memory.Position = 0;
            //return ;
            //return null;
            //if (string.IsNullOrEmpty(chart))
            //{
            //    QueryResponse.AddNegativeMessage("Строка пустая");

            //}
            //else
            //{
            //    QueryResponse.AddSuccessMessage("Что-то дошло");
            //}
            //return QueryResponse.ToResult();

            return null;
        }

        [HttpGet]
        public object SaveDiagramToFile([FromQuery]string fileName)
        {
            byte[] chart = Convert.FromBase64String(StaticData.DataDiagram);

            string filePath = Path.ChangeExtension(fileName + " отчет с диаграммой", ".docx");

            System.IO.File.Copy("TemplatesReportsWord/DiagramReportTemplate.docx", filePath);

            using (FileStream fstream = System.IO.File.Open(filePath, FileMode.Open))
            {
                List<IContentItem> fieldContents = new List<IContentItem>();

                TableContent tableContent = TableContent.Create("Team members");
                List<FieldContent> rows = new List<FieldContent>();

                //List<string> tablets = new List<string>() { "Project one", "Project two", "Project three" };
                //List<string> names = new List<string>() { "Eric", "Kel", "Bob" };
                //List<string> roles = new List<string>() { "Program Manager", "Developer", "blalbla" };

                //for (int i = 0; i < tablets.Count; i++)
                //{
                //    rows.Clear();
                //    //rows.Add(new FieldContent("Name", names[i]));
                //    //rows.Add(new FieldContent("Role", roles[i]));
                //    tableContent = TableContent.Create("Team members");
                //    tableContent.AddRow(rows.ToArray());


                //    //contentItems = new ListItemContent("Project", "Project one");
                //    //contentItems.AddTable(TableContent.Create("Team members", tableRow.ToArray()));
                //    //listContent.AddItem(contentItems);

                //}

                fieldContents.Add(new ImageContent("diagram", chart));
                using (var outputDocument = new TemplateProcessor(fstream).SetRemoveContentControls(true))
                {
                    outputDocument.FillContent(new Content(fieldContents.ToArray()));
                    outputDocument.SaveChanges();
                }

            }
            System.IO.File.Delete(filePath);

            return null;
        }

        [HttpGet]
        public object ValidateChartBeforeSave([FromQuery]string queryString)
        {
            //ParameterForCalculationChart parameterForCalculationChart = ValidateChart(queryString);

            return QueryResponse.ToResult();
        }

        [HttpGet]
        public object ValidateDiagramBeforeSave([FromQuery]string queryString)
        {
            ParameterForCalculationDiagram parameterForCalculationDiagram = ValidateDiagram(queryString);

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

        private ParameterForCalculationChart ValidateChart(string queryString)
        {
            ParameterForCalculationChart parameterForCalculationChart = null;

            if (string.IsNullOrEmpty(queryString))
            {
                QueryResponse.AddNegativeMessage("Параметры не указаны");
            }
            else
            {
                parameterForCalculationChart = JsonConvert.DeserializeObject<ParameterForCalculationChart>(queryString);

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
            }

            return parameterForCalculationChart;
        }

        private ParameterForCalculationDiagram ValidateDiagram(string queryString)
        {
            ParameterForCalculationDiagram parameterForCalculationDiagram = null;
            if (string.IsNullOrEmpty(queryString))
            {
                QueryResponse.AddNegativeMessage("Параметры не указаны");
            }
            else
            {
                parameterForCalculationDiagram = JsonConvert.DeserializeObject<ParameterForCalculationDiagram>(queryString);

                if (parameterForCalculationDiagram.namesSystems.Count < 1)
                {
                    QueryResponse.AddNegativeMessage("Для построения диаграммы необходимо выбрать одну или несколько систем");
                }
                if (string.IsNullOrWhiteSpace(parameterForCalculationDiagram.parameterName))
                {
                    QueryResponse.AddNegativeMessage("Для построения диаграммы необходимо выбрать параметр");
                }
            }

            return parameterForCalculationDiagram;
        }
    }
}
