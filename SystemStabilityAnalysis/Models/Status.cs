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

        private Dictionary<Status, string> Headers { get; } = new Dictionary<Status, string>()
        {
            {Status.negative,   "Ошибка" },
            {Status.success,    "Успешно" },
            {Status.info,       "Информация" },
            {Status.warning,    "Предупреждение" },
        };

        private string _header
        {
            get { return Headers[_status]; }
        }

        public QueryResponse()
        {
            Clear();
        }
        private dynamic _result;

        private List<string> _properties;

        private Status _status;

        public List<string> Messages { get; private set; }

        public bool IsSuccess { get { return _status == Status.success; } }

        public bool IsNegative { get { return _status == Status.negative; } }

        public bool IsWarning { get { return _status == Status.warning; } }

        public bool IsInfo { get { return _status == Status.info; } }

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
            _status = Status.success;
        }

        private void Negative()
        {
            _status = Status.negative;
        }

        private void Warning()
        {
            CheckBeforeSet();
            _status = Status.warning;
        }

        private void Info()
        {
            CheckBeforeSet();
            _status = Status.info;
        }

        public void AddNegativeMessage(string negativeMessage, bool checkOnEmpty = false)
        {
            if ((checkOnEmpty) && (string.IsNullOrEmpty(negativeMessage)))
                return;

            Negative();
            Messages.Add(negativeMessage);
        }

        public void AddNegativeMessages(List<string> negativeMessages, bool checkOnEmpty = false)
        {
            if ((checkOnEmpty) && (negativeMessages.Count < 1))
                return;

            Negative();
            Messages.AddRange(negativeMessages);
        }

        public void AddSuccessMessage(string successMessage, bool checkOnEmpty = false)
        {
            if ((checkOnEmpty) && (string.IsNullOrEmpty(successMessage)))
                return;

            Success();
            Messages.Add(successMessage);
        }

        public void AddSuccessMessages(List<string> successMessages, bool checkOnEmpty = false)
        {
            if ((checkOnEmpty) && (successMessages.Count < 1))
                return;

            Success();
            Messages.AddRange(successMessages);
        }

        public void AddWarningMessage(string warningMessage, bool checkOnEmpty = false)
        {
            if ((checkOnEmpty) && (string.IsNullOrEmpty(warningMessage)))
                return;

            Warning();
            Messages.Add(warningMessage);
        }

        public void AddWarningMessages(List<string> warningMessages, bool checkOnEmpty = false)
        {
            if ((checkOnEmpty) && (warningMessages.Count < 1))
                return;

            Warning();
            Messages.AddRange(warningMessages);
        }

        public void AddInfoMessage(string infoMessage, bool checkOnEmpty = false)
        {
            if ((checkOnEmpty) && (string.IsNullOrEmpty(infoMessage)))
                return;

            Info();
            Messages.Add(infoMessage);
        }

        public void AddInfoMessages(List<string> infoMessages, bool checkOnEmpty = false)
        {
            if ((checkOnEmpty) && (infoMessages.Count < 1))
                return;

            Info();
            Messages.AddRange(infoMessages);
        }

        public void Add(string name, object value)
        {
    
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(message: "property is empty or null");
            }

            name = char.ToLower(name[0]) + name.Substring(1);

            if ((_properties.Contains(name)))
            {
                throw new ArgumentException(message: "Property already exist");
            }



            ((IDictionary<String, Object>)_result)[name] = value;
            _properties.Add(name);
        }

        public void Add(dynamic obj)
        {
            if (obj == null)
            {
                throw new ArgumentException(message: "Object is null");
            }
            var Result = ((IDictionary<String, Object>)_result);
            var Obj = ((IDictionary<String, Object>)obj);
            string name;
            foreach (var property in Obj)
            {
                if ((string.IsNullOrEmpty(property.Key)) || (_properties.Contains(property.Key)))
                {
                    throw new ArgumentException(message: "Property already exist");
                }
                name = char.ToLower(property.Key[0]) + property.Key.Substring(1);
                Result[name] = property.Value;
                _properties.Add(name);
            } 
        }

        private void Clear()
        {
            _properties = new List<string>() { "status", "message", "header" };
            _result = new ExpandoObject();
            Messages = new List<string>();
            _status = Status.success;
        }

        public object ToResult()
        {
            _result.status = HelperEnum.GetName(_status);
            _result.message = Messages;
            _result.header = _header;
            var resultForReturn = _result;
            Clear();
            return resultForReturn;
        }

        //public Response Export()
        //{
        //    return new Response() {Status = (int)status, Messages = messages };
        //}

        //public void Import(Response response)
        //{
        //    status =  (Status)response.Status;
        //    messages.AddRange(response.Messages);
        //}
    }


}
