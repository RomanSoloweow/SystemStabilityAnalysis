using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemStabilityAnalysis.Models.Parameters;
namespace SystemStabilityAnalysis.Models
{
    public class SystemForAnalys: PropertiesSystem
    {
        public SystemForAnalys(string name):base(name)
        {
           
        }

    

        public List<object> GetParametersWithEnter(out List<string> message)
        {
            List<object> parameters = new List<object>();
            message = new List<string>();
            QueryResponse resultVerification;
            foreach (var parameter in ParametersWithEnter.Values)
            {
                resultVerification = parameter.Verification();
                parameters.Add(parameter.TypeParameter.ToParameter(parameter.Value, resultVerification.IsCorrect));
                if (!resultVerification.IsCorrect)
                    message.AddRange(resultVerification.ErrorMessages);
            }
            return parameters;
        }

        public List<object>  GetParametersWithCalculate(out List<string> message)
        {
         
            List<object> parameters = new List<object>();
            message = new List<string>();
            QueryResponse resultVerification;
            foreach (var parameter in ParametersWithCalculation.Values)
            {
                resultVerification = parameter.Verification();
                parameters.Add(parameter.TypeParameter.ToParameter(parameter.Value, resultVerification.IsCorrect));
                if(!resultVerification.IsCorrect)
                 message.AddRange(resultVerification.ErrorMessages);
            }
            return parameters;
        }

        public List<object> GetParametersForAnalysis(out List<string> message)
        {

            List<object> parameters = new List<object>();
            message = new List<string>();
            QueryResponse resultVerification;
            foreach (var parameter in ParametersForAnalysis.Values)
            {
                resultVerification = parameter.Verification();
                parameters.Add(parameter.TypeParameter.ToParameter(parameter.Value, resultVerification.IsCorrect));
                if (!resultVerification.IsCorrect)
                    message.AddRange(resultVerification.ErrorMessages);
            }
            return parameters;
        }

        public List<object> GetParametersForChart()
        {
            List<object> parameters = new List<object>();
            parameters.AddRange(this.ParametersWithCalculation.Values.Select(x => x.TypeParameter.ToPair()));
            parameters.AddRange(this.ParametersForAnalysis.Values.Select(x => x.TypeParameter.ToPair()));
            return parameters;
        }

        public List<object> GetParametersForDiagram()
        {
            List<object> parameters = new List<object>();
            parameters.AddRange(this.ParametersWithCalculation.Values.Select(x=>x.TypeParameter.ToPair()));
            parameters.AddRange(this.ParametersForAnalysis.Values.Select(x => x.TypeParameter.ToPair()));
            return parameters;
        }




    }
}
