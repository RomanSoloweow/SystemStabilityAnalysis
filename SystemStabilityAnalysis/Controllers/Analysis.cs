using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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

        

    }
}
