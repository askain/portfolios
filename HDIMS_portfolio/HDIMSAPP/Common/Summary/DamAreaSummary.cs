
using System;
using System.Linq;
using Infragistics;
namespace HDIMSAPP.Common.Summary
{
    public class DamAreaSummaryCalc : SynchronousSummaryCalculator
    {
        public override SummaryExecution? SummaryExecution
        {
            get
            {
                return Infragistics.SummaryExecution.AfterFilteringAndPaging;
            }
        }

        private object GetPropertyValue(object _data, string _property)
        {
            if (_data!=null && _data.GetType()!=null && _data.GetType().GetProperty(_property) != null)
            {
                return _data.GetType().GetProperty(_property).GetValue(_data, null);
            }

            return null;
        }

        public override object Summarize(IQueryable data, string fieldKey)
        {
            double _ret = 0;
            if (data != null && fieldKey != null)
            {
                
                if (fieldKey.Equals("OBSDT"))
                {
                    return "총누계            ";
                }
                else
                {
                    IQueryable<object> convertedData = data.OfType<object>();

                    if (convertedData != null)
                    {
                        foreach (object item in convertedData)
                        {
                            try
                            {
                                object _dt = GetPropertyValue(item, fieldKey);
                                if (_dt != null)
                                {
                                    double _val;
                                    bool res = Double.TryParse(_dt.ToString(), out _val);
                                    if (res) _ret += _val;
                                }
                            }
                            catch (Exception e) { }
                        }
                        _ret = Math.Round(_ret, 1);
                    }
                }
            }
            return _ret;
        }
    }

    public class 
        DamAreaSummaryOperand : SummaryOperandBase
    {
        private DamAreaSummaryCalc myCalc;
   
        protected override string DefaultRowDisplayLabel   {
            get { return ""; }   
        }
   
        protected override string DefaultSelectionDisplayLabel   {
            get { return ""; }   
        }
   
        public override SummaryCalculatorBase SummaryCalculator   {      
            get      {
             if (myCalc == null)         {
                this.myCalc = new DamAreaSummaryCalc();         
             }
            return this.myCalc;         
            }      
        }   
    }
}
