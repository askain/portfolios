using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using HDIMSAPP.Models.Verify;
using Infragistics.Controls.Grids;

namespace HDIMSAPP.Common.ConditionalFormatRule
{
    public class DateTimeContionalFormatRule : ConditionalFormattingRuleBase
    {
        protected override IConditionalFormattingRuleProxy CreateProxy()
        {
            return new DateTimeContionalFormatRuleProxy();
        }

    }

    public class DateTimeContionalFormatRuleProxy : ConditionalFormattingRuleBaseProxy
    {
        private Style MidNightStyle { get; set; }
        private Style HourStyle { get; set; }

        public DateTimeContionalFormatRuleProxy()
        {
            Style midStyle = new Style(typeof(ConditionalFormattingCellControl));
            midStyle.Setters.Add(new Setter() { Property = ConditionalFormattingCellControl.BackgroundProperty, Value = new SolidColorBrush(new Color() { A = 255, R = 233, G = 212, B = 255}) });
            this.MidNightStyle = midStyle;

            Style hrStyle = new Style(typeof(ConditionalFormattingCellControl));
            hrStyle.Setters.Add(new Setter() { Property = ConditionalFormattingCellControl.BackgroundProperty, Value = new SolidColorBrush(new Color() { A = 255, R = 224, G = 255, B = 224 }) });
            this.HourStyle = hrStyle;

        }

        protected override Style EvaluateCondition(object sourceDataObject, object sourceDataValue)
        {
            if (sourceDataObject == null) return null;

            string _trmdv = null;
            string _obsdt = null;
            if (sourceDataObject is Data)
            {
                Data _data = sourceDataObject as Data;
                _trmdv = _data.TRMDV;
                _obsdt = _data.OBSDT;
            }
            else if (sourceDataObject is IDictionary<string, string>)
            {
                IDictionary<string, string> _data = sourceDataObject as IDictionary<string, string>;
                _trmdv = "1";
                if (_data.ContainsKey("OBSDT"))
                {
                    _obsdt = _data["OBSDT"];
                }
                else
                {
                    _obsdt = "99999999999999";
                }
                if (_data.ContainsKey("TRMDV"))
                {
                    _trmdv = _data["TRMDV"];
                }
                else
                {
                    _trmdv = "DAY";
                }
            }
            else
            {
                Type _type = sourceDataObject.GetType();
                if (_type.GetProperty("OBSDT") != null & _type.GetProperty("TRMDV")!=null)
                {
                    _obsdt = _type.GetProperty("OBSDT").GetValue(sourceDataObject, null).ToString();
                    _trmdv = _type.GetProperty("TRMDV").GetValue(sourceDataObject, null).ToString();
                }
            }

            if (_trmdv == null || _obsdt == null) return null;

            if (_trmdv.Equals("60") || _trmdv.Equals("30"))
            {
                if (_obsdt.Substring(8, 2).Equals("24"))
                {
                    return MidNightStyle;
                }
            }
            else if (_trmdv.Equals("DAY"))
            {
                if (_obsdt.Substring(6, 2).Equals("01"))
                {
                    return MidNightStyle;
                }
            }
            else if(_trmdv.Equals("1") || _trmdv.Equals("10"))
            {
                if (_obsdt.Substring(8, 4).Equals("2400"))
                {
                    return MidNightStyle;
                }
                else if (_obsdt.Substring(10, 2).Equals("00"))
                {
                    return HourStyle;
                }
            }
            
            return null;  

        }

    }
}
