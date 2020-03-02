using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemStabilityAnalysis.Helpers;
using SystemStabilityAnalysis.Models.Parameters;
namespace SystemStabilityAnalysis.Models
{
    //public class Restriction
        //{
        //    public Restriction()
        //    {

        //    }
        //    public Restriction(string parameterName, ConditionType condition, double value)
        //    {
        //        ParameterName = parameterName;
        //        Condition = condition;
        //        Value = value;
        //    }
        //    public Restriction(string parameterName, Condition condition)
        //    {
        //        ParameterName = parameterName;
        //        Condition = condition.ConditionType;
        //        Value = condition.Value;
        //    }
        //    //[Name("Наименование показателя")]
        //    //public string Description { get; set; }
        //    //[Name("Наименование ")]
        //    //public string Designation { get; set; }
        //    //[Name("Единица измерения")]
        //    //public string Unit { get; set; }
        //    //[Name("Единица измерения")]
        //    //public string Condition { get; set; }

        //    public string ParameterName { get; set; }
        //    public ConditionType Condition { get; set; }
        //    public double Value { get; set; }

        //    public static List<Restriction> GetRestctions()
        //    {
        //        List<Restriction> restrictions = new List<Restriction>();

        //        foreach (var condition in StaticData.ConditionsForParameterWithEnter)
        //        {
        //            restrictions.Add(new Restriction(condition.Key.GetName(), condition.Value));
        //        }

        //        foreach (var condition in StaticData.ConditionsForParameterWithCalculation)
        //        {
        //            restrictions.Add(new Restriction(condition.Key.GetName(), condition.Value));
        //        }

        //        foreach (var condition in StaticData.ConditionsForParameterForAnalysis)
        //        {
        //            restrictions.Add(new Restriction(condition.Key.GetName(), condition.Value));
        //        }
        //        return restrictions;
        //    }
        //}
    public class Restriction
    {
        public Restriction()
        {

        }

        public Restriction(NameParameterWithEnter parameter, Condition condition)
        {
            ParameterWithEnter = parameter;
            ConditionType = condition.ConditionType;
            Value = condition.Value;
        }
        public Restriction(NameParameterWithCalculation parameter, Condition condition)
        {
            ParameterWithCalculation = parameter;
            ConditionType = condition.ConditionType;
            Value = condition.Value;
        }
        public Restriction(NameParameterForAnalysis parameter, Condition condition)
        {
            ParameterForAnalysis = parameter;
            ConditionType = condition.ConditionType;
            Value = condition.Value;
        }

        [Name("Наименование показателя")]
        public string Description 
        { 
            get { return GetDescription(); }
            set { }
        
        }

        [Name("Обозначение")]
        public string Designation 
        { 
            get { return GetDesignation(); }
            set { SetParameterType(value); }
        }

        [Name("Единица измерения")]
        public string Unit 
        { 
            get { return GetUnum(); }
        }
        [Name("Условие")]
        public string Condition 
        { 
            get {return ConditionType.GetDesignation(); }
            set { SetConditionType(value); }
        }
        [Name("Значение")]
        public double Value { get; set; }

        [Ignore]
        public NameParameterWithEnter ParameterWithEnter { get; set; }

        [Ignore]
        public NameParameterWithCalculation ParameterWithCalculation { get; set; }

        [Ignore]
        public NameParameterForAnalysis ParameterForAnalysis { get; set; }


        public string GetDesignation()
        {
            if (!HelperEnum.IsDefault(ParameterWithEnter))
                return ParameterWithEnter.GetDesignation();

            if (!HelperEnum.IsDefault(ParameterWithCalculation))
                return ParameterWithCalculation.GetDesignation();

            if (!HelperEnum.IsDefault(ParameterForAnalysis))
                return ParameterForAnalysis.GetDesignation();

            return "";
        }

        public string GetDescription()
        {
            if (!HelperEnum.IsDefault(ParameterWithEnter))
                return ParameterWithEnter.GetDescription();

            if (!HelperEnum.IsDefault(ParameterWithCalculation))
                return ParameterWithCalculation.GetDescription();

            if (!HelperEnum.IsDefault(ParameterForAnalysis))
                return ParameterForAnalysis.GetDescription();

            return "";
        }

        public string GetUnum()
        {
            if (!HelperEnum.IsDefault(ParameterWithEnter))
                return ParameterWithEnter.GetUnit().GetDesignation();

            if (!HelperEnum.IsDefault(ParameterWithCalculation))
                return ParameterWithCalculation.GetUnit().GetDesignation();

            if (!HelperEnum.IsDefault(ParameterForAnalysis))
                return ParameterForAnalysis.GetUnit().GetDesignation();

            return "";
        }

        public string GetName()
        {
            if (!HelperEnum.IsDefault(ParameterWithEnter))
                return ParameterWithEnter.GetName();

            if (!HelperEnum.IsDefault(ParameterWithCalculation))
                return ParameterWithCalculation.GetName();

            if (!HelperEnum.IsDefault(ParameterForAnalysis))
                return ParameterForAnalysis.GetName();

            return "";
        }

        [Ignore]
        public ConditionType ConditionType { get; set; }

        public bool AddToRestriction()
        {
 
            if (!HelperEnum.IsDefault(ParameterWithEnter))
            {
                ParameterWithEnter.AddToRestrictions(ConditionType, Value);
            }
            else if (!HelperEnum.IsDefault(ParameterWithCalculation))
            {
                ParameterWithCalculation.AddToRestrictions(ConditionType, Value);

            }
            else if (!HelperEnum.IsDefault(ParameterWithCalculation))
            {
                ParameterWithEnter.AddToRestrictions(ConditionType, Value);
            }
            else
            {
                return false;
            }

            return true;
        }

        public bool AddedToRestriction()
        {

            if (!HelperEnum.IsDefault(ParameterWithEnter))
            {
                return ParameterWithEnter.AddedToRestrictions();
            }
            else if (!HelperEnum.IsDefault(ParameterWithCalculation))
            {
                return ParameterWithCalculation.AddedToRestrictions();

            }
            else if (!HelperEnum.IsDefault(ParameterWithCalculation))
            {
                return ParameterWithEnter.AddedToRestrictions();
            }

            throw new ArgumentNullException(paramName: "AddedToRestriction", "Неизвестный параметр");
        }

        public void SetParameterType(string designation)
        {
            
        
            ParameterWithEnter = HelperEnum.GetValuesWithoutDefault<NameParameterWithEnter>().SingleOrDefault(x=>x.GetDesignation()==designation);
            if (!HelperEnum.IsDefault(ParameterWithEnter))
                return;

            ParameterWithCalculation = HelperEnum.GetValuesWithoutDefault<NameParameterWithCalculation>().SingleOrDefault(x => x.GetDesignation() == designation);
            if (!HelperEnum.IsDefault(ParameterWithCalculation))
                return;

            ParameterForAnalysis = HelperEnum.GetValuesWithoutDefault<NameParameterForAnalysis>().SingleOrDefault(x => x.GetDesignation() == designation);
            if (!HelperEnum.IsDefault(ParameterForAnalysis))
                return;

            throw new ArgumentException(paramName: designation, message: "Неккоректное описание");
        }

        public void SetConditionType(string designation)
        {
            ConditionType = HelperEnum.GetValuesWithoutDefault<ConditionType>().SingleOrDefault(x => x.GetDesignation() == designation);
            if (!HelperEnum.IsDefault(ConditionType))
                return;

            throw new ArgumentException(paramName: designation, message: "Неккоректное условие");
        }

        public dynamic ToResponse()
        {
            if (!HelperEnum.IsDefault(ParameterWithEnter))
            {
                return ParameterWithEnter.ToRestriction(ConditionType, Value);
            }
            else if (!HelperEnum.IsDefault(ParameterWithCalculation))
            {
                return ParameterWithCalculation.ToRestriction(ConditionType, Value);
            }
            else 
            {
                return ParameterWithEnter.ToRestriction(ConditionType, Value);
            }
        }
    }
}
