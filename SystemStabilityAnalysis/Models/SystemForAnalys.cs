using CsvHelper;
using HeyRed.Mime;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemStabilityAnalysis.Models.Parameters;
using static SystemStabilityAnalysis.Controllers.Analysis;

namespace SystemStabilityAnalysis.Models
{
    public class SystemForAnalys: PropertiesSystem
    {
        public SystemForAnalys(string name):base(name)
        {
           
        }

    

        public List<object> GetParametersWithEnter(out List<string> messages)
        {
            List<object> parameters = new List<object>();
            messages = new List<string>();
            bool resultVerification;
            string message;
            foreach (var parameter in ParametersWithEnter.Values)
            {
                resultVerification = parameter.Verification(out message);

                if (!resultVerification)
                    messages.Add(message);

                parameters.Add(parameter.TypeParameter.ToParameter(parameter.Value, resultVerification));

            }
            return parameters;
        }

        public List<object>  GetParametersWithCalculate(out List<string> messages)
        {
         
            List<object> parameters = new List<object>();
            messages = new List<string>();
            bool resultVerification;
            string message;
            foreach (var parameter in ParametersWithCalculation.Values)
            {
                resultVerification = parameter.Verification(out message);

                if(!resultVerification)
                    messages.Add(message);

                parameters.Add(parameter.TypeParameter.ToParameter(parameter.Value, resultVerification));
               
            }
            return parameters;
        }

        public List<object> GetParametersForAnalysis(out List<string> messages)
        {

            List<object> parameters = new List<object>();
            messages = new List<string>();
            bool resultVerification;
            string message;
            foreach (var parameter in ParametersForAnalysis.Values)
            {
                resultVerification = parameter.Verification(out message);

                if (!resultVerification)
                    messages.Add(message);

                parameters.Add(parameter.TypeParameter.ToParameter(parameter.Value, resultVerification));

            }
            return parameters;
        }
        public object GetParameterU(out string result)
        {        
           bool resultVerification =  U.Verification(out string message);
            
            result = U.GetResult();
            if (!resultVerification)
                result += " " + message;
            return U.Value.HasValue ? U.Value.Value.ToString() : "_";
        }
        public List<object> GetParametersForChart()
        {
            List<object> parameters = new List<object>();
            parameters.AddRange(this.ParametersWithEnter.Values.Select(x => x.TypeParameter.ToPair()));
            //parameters.AddRange(this.ParametersWithCalculation.Values.Select(x => x.TypeParameter.ToPair()));
            //parameters.AddRange(this.ParametersForAnalysis.Values.Select(x => x.TypeParameter.ToPair()));
            return parameters;
        }

        public List<object> GetParametersU()
        {
            List<object> parameters = new List<object>();
            parameters.Add(this.U.ToPair());
            return parameters;
        }

        public List<object> GetParametersForDiagram()
        {
            List<object> parameters = new List<object>();
            parameters.AddRange(GetParametersU());
            parameters.AddRange(this.ParametersWithCalculation.Values.Select(x=>x.TypeParameter.ToPair()));
            parameters.AddRange(this.ParametersForAnalysis.Values.Select(x => x.TypeParameter.ToPair()));
            return parameters;
        }

        public double GetParameterValue(string parameterName)
        {
            if (Enum.TryParse(parameterName, out NameParameterWithEnter parameterWithEnter))
            {
                return ParametersWithEnter[parameterWithEnter].Value.Value;
            }
            else if (Enum.TryParse(parameterName, out NameParameterWithCalculation parameterWithCalculation))
            {
                return ParametersWithCalculation[parameterWithCalculation].Value.Value;
            }
            else if(Enum.TryParse(parameterName, out NameParameterForAnalysis parameterForAnalysis))
            {
                  return ParametersForAnalysis[parameterForAnalysis].Value.Value;
            }
            else
            {
                return U.Value.Value;
            }
        }

        public string GetParameterDesignation(string parameterName)
        {
            if (Enum.TryParse(parameterName, out NameParameterWithEnter parameterWithEnter))
            {
                return ParametersWithEnter[parameterWithEnter].Designation;
            }
            else if (Enum.TryParse(parameterName, out NameParameterWithCalculation parameterWithCalculation))
            {
                return ParametersWithCalculation[parameterWithCalculation].Designation;
            }
            else if(Enum.TryParse(parameterName, out NameParameterForAnalysis parameterForAnalysis))
            {
                return ParametersForAnalysis[parameterForAnalysis].Designation;
            }
            else
            {
                return U.Designation;
            }
        }

        public object GetCalculationsForChart(ParameterForCalculationChart parameterForCalculationChart)
        {
            List<object> values = new List<object>();
            string parameterName = parameterForCalculationChart.parameterName;
            double? parameterValue;
            double step = (parameterForCalculationChart.To.Value - parameterForCalculationChart.From.Value) / parameterForCalculationChart.CountDote.Value;
            double CurentValue = parameterForCalculationChart.From.Value - step;

             Enum.TryParse(parameterName, out NameParameterWithEnter parameterWithEnter);
           
                parameterValue = ParametersWithEnter[parameterWithEnter].Value;

                for (int i = 0; i < parameterForCalculationChart.CountDote.Value; i++)
                {
                    CurentValue += step;
                    ParametersWithEnter[parameterWithEnter].Value = CurentValue;
                    values.Add(new
                    {
                        x= CurentValue,
                        y = U.Value.Value
                    });
                }

               ParametersWithEnter[parameterWithEnter].Value= parameterValue;

               return new
               {
                   values = values,
                   nameSystem = Name
               };
        }

        public void SetAsCorrect()
        {
            foreach (var parameter in ParametersWithEnter.Values)
            {
                parameter.isCorrect = true;
            }
            foreach (var parameter in ParametersWithCalculation.Values)
            {
                parameter.isCorrect = true;
            }
            foreach (var parameter in ParametersForAnalysis.Values)
            {
                parameter.isCorrect = true;
            }
           
        }


    }
}
