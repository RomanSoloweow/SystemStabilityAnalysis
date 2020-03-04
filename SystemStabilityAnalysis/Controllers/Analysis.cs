using System;
using System.Collections.Generic;
using System.Dynamic;
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
                List<object> calculations = new List<object>();
                foreach (var nameSystem in parameterForCalculationChart.namesSystems)
                {
                    calculations.Add(StaticData.Systems[nameSystem].GetCalculationsForChart(parameterForCalculationChart));
                }
                StaticData.ChartCalculation = new ExpandoObject();
                StaticData.ChartCalculation.Calculations = calculations;
                StaticData.ChartCalculation.ParameterNameX = StaticData.CurrentSystems.GetParameterDesignation(parameterForCalculationChart.parameterName);
                StaticData.ChartCalculation.ParameterNameY = StaticData.CurrentSystems.GetParameterDesignation("U");
                QueryResponse.Add(StaticData.ChartCalculation);


                //QueryResponse.Add("ParameterNameX", StaticData.CurrentSystems.GetParameterDesignation(parameterForCalculationChart.parameterName));
                //QueryResponse.Add("ParameterNameY", StaticData.CurrentSystems.GetParameterDesignation("U"));
                //QueryResponse.Add("Calculations", calculations);

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
            return QueryResponse.ToResult();
        }

        [HttpPost]
        public object SaveDataDiagram([FromForm]string diagram)
        {

            return QueryResponse.ToResult();
        }


        [HttpGet]
        public object SaveChartToFile([FromQuery]string fileName)
        {

            var path = Path.Combine(Directory.GetCurrentDirectory(), "test.csv");

            var memory = new MemoryStream();
            using (var stream = new FileStream(path, FileMode.Open))
            {
                stream.CopyTo(memory);
            }

            memory.Position = 0;
            return File(memory, "text/csv", Path.ChangeExtension("diagram", ".csv"));


            byte[] imageBytes = Convert.FromBase64String("");
            //List<IContentItem> fieldContents = new List<IContentItem>();


            //string fileName = "test";
            string filePath = Path.ChangeExtension(fileName + " отчет", ".docx");

            System.IO.File.Copy("ChartReportTemplate.docx", filePath);
            //using (FileStream fstream = System.IO.File.Open(filePath, FileMode.Open))
            //{
            //    fieldContents.Add(new ImageContent("chart", imageBytes));
            //    using (var outputDocument = new TemplateProcessor(fstream).SetRemoveContentControls(true))
            //    {
            //        outputDocument.FillContent(new Content(fieldContents.ToArray()));
            //        //outputDocument.FillContent(new Content(listContent));
            //        //outputDocument.FillContent(valuesToFill);
            //        outputDocument.SaveChanges();
            //    }
            //}
            using (FileStream fstream = System.IO.File.Open(filePath, FileMode.Open))
            {
                List<IContentItem> fieldContents = new List<IContentItem>();
                fieldContents.Add(new ImageContent("chart", imageBytes));
                ListContent listContent = new ListContent("Projects List");
                ListItemContent contentItems;
                TableContent tableContent;

                List<FieldContent> rows = new List<FieldContent>();


                List<string> tablets = new List<string>() { "Project one", "Project two", "Project three" };
                List<string> names = new List<string>() { "Eric", "Kel", "Bob" };
                List<string> roles = new List<string>() { "Program Manager", "Developer", "blalbla" };

                for (int i = 0; i < tablets.Count; i++)
                {
                    rows.Clear();
                    rows.Add(new FieldContent("Name", names[i]));
                    rows.Add(new FieldContent("Role", roles[i]));
                    tableContent = TableContent.Create("Team members");
                    tableContent.AddRow(rows.ToArray());
                    contentItems = new ListItemContent("Project", tablets[i]);
                    contentItems.AddTable(tableContent);
                    listContent.AddItem(contentItems);


                    //contentItems = new ListItemContent("Project", "Project one");
                    //contentItems.AddTable(TableContent.Create("Team members", tableRow.ToArray()));
                    //listContent.AddItem(contentItems);

                }
                fieldContents.Add(listContent);
                //fieldContents.Add(listContent);
                //   List<FieldContent> fieldForTable = new List<FieldContent>();
                //   fieldForTable.Add(new FieldContent("nameParameterX","Test x"));
                //   fieldForTable.Add(new FieldContent("nameParameterY", "Test y"));
                //   fieldForTable.Add(new FieldContent("parameterX", "10"));
                //   fieldForTable.Add(new FieldContent("parameterY", "15"));


                //   TableContent sustem1 = new TableContent("system", new TableRowContent(fieldForTable));
                //   List<ListItemContent> tablets = new List<ListItemContent>() { sustem1 };
                //ListItemContent contentItems = new ListItemContent("systems", );

                //   fieldContents.Add(contentItems);





                //fieldContents.Add(new ImageContent("Chart", imageBytes));
                using (var outputDocument = new TemplateProcessor(fstream).SetRemoveContentControls(true))
                {
                    outputDocument.FillContent(new Content(fieldContents.ToArray()));
                    //outputDocument.FillContent(new Content(listContent));
                    //outputDocument.FillContent(valuesToFill);
                    outputDocument.SaveChanges();
                }

            }


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
        }

        [HttpGet]
        public object SaveDiagramToFile([FromQuery]string fileName)
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
        public object ValidateChartBeforeSave([FromQuery]string queryString)
        {
            ParameterForCalculationChart parameterForCalculationChart = ValidateChart(queryString);

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
