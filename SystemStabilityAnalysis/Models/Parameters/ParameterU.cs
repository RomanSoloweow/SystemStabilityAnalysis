using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemStabilityAnalysis.Helpers;

namespace SystemStabilityAnalysis.Models.Parameters
{
    public class ParameterU
    {
        public PropertiesSystem propertiesSystem;
        public Func<double?> Calculate;
        public string Name { get; set; } = "U";
        public string Designation { get; set; } = "U";
        public string Description { get; set; } = "Показатель неустойчивости системы";
        public ParameterU(PropertiesSystem _propertiesSystem, Func<double?> calculate)
        {
            Calculate = calculate;
            propertiesSystem = _propertiesSystem;
        }
        
        public bool IsCorrect { get; set; }

        public double? Value
        {
            get
            {
                return IsCorrect ? Calculate.Invoke() : null;

            }
        }
        public string GetResult()
        {
            if (Value.HasValue)
            {
                if (Value.Value > 0)
                {
                    return String.Format("Cистема устойчива в течении периода \"{0}\" при заданных условиях и ограничениях.", propertiesSystem.deltaT.Value.Value.ToString());
                }
                else if (Value.Value == 0)
                {
                    return String.Format("Cистема находится на пределе своей устойчивости в течении периода \"{0}\" при заданных условиях и ограничениях.", propertiesSystem.deltaT.Value.Value.ToString());
                }
                else
                {
                    return String.Format("Cистема не устойчива в течении периода \"{0}\" при заданных условиях и ограничениях.", propertiesSystem.deltaT.Value.Value.ToString());
                }
            }
            else
            {
                return String.Format("Невозможно сделать вывод об устойчивости системы т.к. показатель устойчивости не может быть вычислен.");
            }
        }

        public List<NameParameterForAnalysis> GetDependences()
        {
            return HelperEnum.GetValuesWithoutDefault<NameParameterForAnalysis>();
        }

        public object ToPair()
        {
            return new
            {
                Name = Designation,
                Description = Description,
                Value = Name
            };
        }
        public bool Verification(out string message)
        {
            message = "";
            IsCorrect = propertiesSystem.VerificationParametersForAnalysis(GetDependences(), out List<string> messages);         
            if (!Value.HasValue)
            {
                message = string.Format("Проверьте правильность полей: {0}", string.Join(',', messages));
                IsCorrect = false;
            }
            return IsCorrect;
        }
       

    }
}
