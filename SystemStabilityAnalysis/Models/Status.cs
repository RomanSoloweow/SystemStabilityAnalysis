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

    public class QueryResponse
    {
        public Status Status { get; set; } = Status.Success;

        public void SetNotCorrect()
        {
            Status = Status.Error;
        }
        public void SetCorrect()
        {
            Status = Status.Success;
        }
        public void AddError(string error)
        {
            SetNotCorrect();
            ErrorMessages.Add(error);
        }
        public void AddRangeError(List<string> errors)
        {
            SetNotCorrect();
            ErrorMessages.AddRange(errors);
        }
        public void AddRangeErrorWithIfNotEmpty(List<string> errors)
        {
            if ((errors!=null)&&(errors.Count > 0))
                AddRangeError(errors);
        }
        public bool IsCorrect { get { return Status == Status.Success; } }

        public object ToResult()
        {
            return new
            {
                Status = Status.GetName(),
                Message = ErrorMessages
            };
        }

        public List<string> ErrorMessages { get; } = new List<string>();
    }

}
