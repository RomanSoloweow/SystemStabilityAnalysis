using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemStabilityAnalysis.Models.Parameters;
namespace SystemStabilityAnalysis.Models
{
    public class SystemForAnalys: PropertiesSystem
    {
        public SystemForAnalys(string name)
        {
            Name = name;
        }

        public string Name { get; private set; }

        public IEnumerable<object> GetParametersWithEnter()
        {
            return ParametersWithEnter.Select(x => x.Key.ToJson());
        }

        public List<object>  GetParametersWithCalculate(out List<string> message)
        {

            
            List<object> parameters = new List<object>();
            message = new List<string>();
            ResultVerification resultVerification;
            foreach (var parameter in ParametersWithCalculation.Values)
            {
                resultVerification = parameter.Verification();
                parameters.Add(parameter.TypeParameter.ToParameter(parameter.Value, resultVerification.IsCorrect));
                //message.AddRange(resultVerification.ErrorMessages);
            }
            return parameters;
        }
        
    }
}
