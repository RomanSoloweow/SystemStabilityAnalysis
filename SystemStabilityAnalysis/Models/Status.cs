using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemStabilityAnalysis.Models
{
    public enum Status
    {
        negative = 0,
        success,
        info,
        warning
    }



    public static class StatusExtension
    {
        public static string GetName(this Status parameter)
        {
            return Enum.GetName(typeof(Status), parameter);
        }
    }

    public class QueryResponse
    {
        private dynamic result = new ExpandoObject();

        List<string> properties = new List<string>() { "status", "message", "header" };

        private Status status = Status.success;

        private List<string> messages = new List<string>();

        public bool IsSuccess { get { return status == Status.success; } }

        public bool IsNegative { get { return status == Status.negative; } }

        public bool IsWarning { get { return status == Status.warning; } }

        public bool IsInfo { get { return status == Status.info; } }

        private void CheckBeforeSet()
        {
            if (IsNegative)
            {
                throw new ArgumentException(paramName: "status", message: "Status already set as Success");
            }
        }

        private void Success()
        {
            CheckBeforeSet();
            status = Status.success;
        }
        private void Negative()
        {
            status = Status.negative;
        }
        private void Warning()
        {
            CheckBeforeSet();
            status = Status.warning;
        }
        private void Info()
        {
            CheckBeforeSet();
            status = Status.info;
        }

        public void AddNegativeMessage(string negativeMessage, bool checkOnEmpty = false)
        {
            if ((checkOnEmpty) && (string.IsNullOrEmpty(negativeMessage)))
                return;

            Negative();
            messages.Add(negativeMessage);
        }
        public void AddNegativeMessages(List<string> negativeMessages, bool checkOnEmpty = false)
        {
            if ((checkOnEmpty) && (negativeMessages.Count < 1))
                return;

            Negative();
            messages.AddRange(negativeMessages);
        }

        public void AddSuccessMessage(string successMessage, bool checkOnEmpty = false)
        {
            if ((checkOnEmpty) && (string.IsNullOrEmpty(successMessage)))
                return;

            Success();
            messages.Add(successMessage);
        }
        public void AddSuccessMessages(List<string> successMessages, bool checkOnEmpty = false)
        {
            if ((checkOnEmpty) && (successMessages.Count < 1))
                return;

            Success();
            messages.AddRange(successMessages);
        }

        public void AddWarningMessage(string warningMessage, bool checkOnEmpty = false)
        {
            if ((checkOnEmpty) && (string.IsNullOrEmpty(warningMessage)))
                return;

            Warning();
            messages.Add(warningMessage);
        }

        public void AddWarningMessages(List<string> warningMessages, bool checkOnEmpty = false)
        {
            if ((checkOnEmpty) && (warningMessages.Count < 1))
                return;

            Warning();
            messages.AddRange(warningMessages);
        }

        public void AddInfoMessage(string infoMessage, bool checkOnEmpty = false)
        {
            if ((checkOnEmpty) && (string.IsNullOrEmpty(infoMessage)))
                return;

            Info();
            messages.Add(infoMessage);
        }

        public void AddInfoMessages(List<string> infoMessages, bool checkOnEmpty = false)
        {
            if ((checkOnEmpty) && (infoMessages.Count < 1))
                return;

            Info();
            messages.AddRange(infoMessages);
        }

        public void Add(string name, object value)
        {
            name = name?.ToLower();

            if ((string.IsNullOrEmpty(name))||(properties.Contains(name)))
            {
                throw new ArgumentException(message: "Property already exist");
            }

            ((IDictionary<String, Object>)result)[name] = value;
            properties.Add(name);
        }

        public object ToResult()
        {
            result.status = status.GetName();
            result.message = messages;
            result.header = status.GetName();

            return result;
        }

        
    }

}
