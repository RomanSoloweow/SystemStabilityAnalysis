using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using SystemStabilityAnalysis.Helpers;

namespace SystemStabilityAnalysis.Models
{
    
    public class QueryResponse
    {
        private enum Status
        {
            negative = 0,
            success,
            info,
            warning
        }

        public QueryResponse()
        {
            Clear();
        }
        private dynamic result;

        List<string> properties;

        private Status status;

        private List<string> messages;

        public bool IsSuccess { get { return status == Status.success; } }

        public bool IsNegative { get { return status == Status.negative; } }

        public bool IsWarning { get { return status == Status.warning; } }

        public bool IsInfo { get { return status == Status.info; } }

        private void CheckBeforeSet()
        {
            if (IsNegative)
            {
                throw new ArgumentException(paramName: "status", message: "Status already set as negative");
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
    
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(message: "property is empty or null");
            }

            name = char.ToLower(name[0]) + name.Substring(1);

            if ((properties.Contains(name)))
            {
                throw new ArgumentException(message: "Property already exist");
            }



            ((IDictionary<String, Object>)result)[name] = value;
            properties.Add(name);
        }

        public void Add(dynamic obj)
        {
            if (obj == null)
            {
                throw new ArgumentException(message: "Object is null");
            }
            var Result = ((IDictionary<String, Object>)result);
            var Obj = ((IDictionary<String, Object>)obj);
            string name;
            foreach (var property in Obj)
            {
                if ((string.IsNullOrEmpty(property.Key)) || (properties.Contains(property.Key)))
                {
                    throw new ArgumentException(message: "Property already exist");
                }
                name = char.ToUpper(property.Key[0]) + property.Key.Substring(1);
                Result[name] = property.Value;
                properties.Add(name);
            } 
        }
        private void Clear()
        {
            properties = new List<string>() { "status", "message", "header" };
            result = new ExpandoObject();
            messages = new List<string>();
            status = Status.success;
        }

        public object ToResult()
        {
            result.status = HelperEnum.GetName(status);
            result.message = messages;
            result.header = HelperEnum.GetName(status);
            var resultForReturn = result;
            Clear();
            return resultForReturn;
        }

        
    }

}
