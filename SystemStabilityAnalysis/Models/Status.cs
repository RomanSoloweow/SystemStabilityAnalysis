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
        private Status status = Status.Success;

        private List<string> messages = new List<string>();

        public bool IsSuccess { get { return status == Status.Success; } }

        public bool IsNegative { get { return status == Status.Error; } }

        private void CheckBeforeSet()
        {
            if (status == Status.Error)
            {
                throw new ArgumentException(paramName: "status", message: "Status already set as Success");
            }
        }

        private void Success()
        {
            CheckBeforeSet();
            status = Status.Success;
        }
        private void Error()
        {
            status = Status.Error;
        }

        public void AddNegativeMessage(string error, bool checkOnEmpty = false)
        {
            Error();
            messages.Add(error);
        }

        public void AddNegativeMessages(List<string> errors)
        {
            Error();
            messages.AddRange(errors);
        }

        public void AddSuccessMessage(string error)
        {
            Success();
            messages.Add(error);
        }

        public void AddSuccessMessages(List<string> errors)
        {
            Success();
            messages.AddRange(errors);
        }

        public object ToResult()
        {
            return new
            {
                Status = status.GetName(),
                Message = messages
            };
        }

        
    }

}
