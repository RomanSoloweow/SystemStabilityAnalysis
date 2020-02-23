using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemStabilityAnalysis.Models
{
    public enum Status
    {
        Error = 0,
        Success
    }



    public static class StatusExtension
    {
        public static string GetName(this Status parameter)
        {
            return Enum.GetName(typeof(Status), parameter);
        }

        public static void ChangeStatus(this Status parameter)
        {
            parameter = (parameter == Status.Success) ? Status.Error : Status.Success;
        }
    }
}
