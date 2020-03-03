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
        public string Description { get; set; } = "Показатель устойчивости системы";
        public ParameterU(PropertiesSystem _propertiesSystem, Func<double?> calculate)
        {
            Calculate = calculate;
            propertiesSystem = _propertiesSystem;
        }
        
        public bool IsCorrect { get; set; } = true;

        public double? Value
        {
            get
            {
                return IsCorrect ? Calculate.Invoke() : null;

            }
        }
        public string GetResult(string newName = null)
        {
            if (Value.HasValue)
            {
                if (!string.IsNullOrEmpty(newName))
                    StaticData.CurrentSystems.Name = newName;

                string name = string.IsNullOrEmpty(StaticData.CurrentSystems.Name)? " ": " \""+StaticData.CurrentSystems.Name+"\" ";
                string value = propertiesSystem.deltaT.Value.Value.ToString();
                if (Value.Value > 0)
                {
                    return String.Format("Cистема{0}устойчива в течении периода \"{1} суток\" при заданных условиях и ограничениях.", name, value);
                }
                else if (Value.Value == 0)
                {
                    return String.Format("Cистема{0}находится на пределе своей устойчивости в течении периода \"{1} суток\"  при заданных условиях и ограничениях.", name, value);
                }
                else
                {
                    return String.Format("Cистема{0}не устойчива в течении периода \"{1} суток\" при заданных условиях и ограничениях.", name, value);
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
