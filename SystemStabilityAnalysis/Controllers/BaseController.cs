using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemStabilityAnalysis.Models;

namespace SystemStabilityAnalysis.Controllers
{
    public class BaseController: ControllerBase
    {
        public QueryResponse QueryResponse { get; set; } = new QueryResponse();
    } 
}
