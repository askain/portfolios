using System;
using System.Windows;
using System.Windows.Media;
using Infragistics.Controls.Grids;

namespace HDIMSAPP.Common.ConditionalFormatRule
{
    public class DamAreaConditionalFormatRule : ConditionalFormattingRuleBase
    {
        protected override IConditionalFormattingRuleProxy CreateProxy()
        {
            return new DamAreaConditionalFormatRuleProxy();
        }
    }

    public class DamAreaConditionalFormatRuleProxy : ConditionalFormattingRuleBaseProxy
    {
        private Style MidNightStyle { get; set; }
        private Style HourStyle { get; set; }

        public DamAreaConditionalFormatRuleProxy()
        {
            Style midStyle = new Style(typeof(ConditionalFormattingCellControl));
            midStyle.Setters.Add(new Setter() { Property = ConditionalFormattingCellControl.BackgroundProperty, Value = new SolidColorBrush(new Color() { A = 255, R = 233, G = 212, B = 255}) });
            this.MidNightStyle = midStyle;

            Style hrStyle = new Style(typeof(ConditionalFormattingCellControl));
            hrStyle.Setters.Add(new Setter() { Property = ConditionalFormattingCellControl.BackgroundProperty, Value = new SolidColorBrush(new Color() { A = 255, R = 224, G = 255, B = 224 }) });
            this.HourStyle = hrStyle;

        }

        private object GetPropertyValue(object _data, string _property)
        {
            if (_data!=null && _data.GetType()!=null && _data.GetType().GetProperty(_property) != null)
            {
                return _data.GetType().GetProperty(_property).GetValue(_data, null);
            }

            return null;
        }
        protected override Style EvaluateCondition(object sourceDataObject, object sourceDataValue)
        {
            object _data = sourceDataObject;
            Column _col = (Parent==null)?null:Parent.Column;

            if (_data != null && _col!=null && _col.Key!=null )
            {
                object _tmpDt = GetPropertyValue(_data, "OBSDT");
                if (_tmpDt != null && _col.Key.StartsWith("V_"))
                {
                    object _tmpVl = GetPropertyValue(_data, _col.Key);
                    string _obsdt = _tmpDt.ToString();
                    string _value = (_tmpVl==null)?null:_tmpVl.ToString();

                    Style _curStyle = new Style(typeof(ConditionalFormattingCellControl));

                    if (_value != null)
                    {
                        double _cval;
                        bool res = Double.TryParse(_value, out _cval);
                        if (res)
                        {
                            if (Double.Parse(_value) > 0 && Double.Parse(_value) < 10)
                            {
                                _curStyle.Setters.Add(new Setter() { Property = ConditionalFormattingCellControl.ForegroundProperty, Value = new SolidColorBrush(Color.FromArgb(255, 244, 124, 39)) });
                                _curStyle.Setters.Add(new Setter() { Property = ConditionalFormattingCellControl.FontWeightProperty, Value = FontWeights.Bold });
                            }
                            if (Double.Parse(_value) >= 10)
                            {
                                _curStyle.Setters.Add(new Setter() { Property = ConditionalFormattingCellControl.ForegroundProperty, Value = new SolidColorBrush(Colors.Red) });
                                _curStyle.Setters.Add(new Setter() { Property = ConditionalFormattingCellControl.FontWeightProperty, Value = FontWeights.Bold });
                            }
                        }
                    }
                    if (_curStyle.Setters.Count > 0) 
                        return _curStyle;
                }
                else if (_col.Key.Equals("OBSDT"))
                {
                    string _trmdv = GetPropertyValue(_data, "TRMDV").ToString();
                    string _obsdt = _tmpDt.ToString();
                    if (_trmdv.Equals("60") || _trmdv.Equals("30"))
                    {
                        if (_obsdt.Substring(8, 2).Equals("24"))
                        {
                            return MidNightStyle;
                        }
                    }
                    else if (_trmdv.Equals("DD"))
                    {
                        if (_obsdt.Substring(6, 2).Equals("01"))
                        {
                            return MidNightStyle;
                        }
                    }
                    else if (_trmdv.Equals("1") || _trmdv.Equals("10"))
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
                }
            }

            return null;

        }

    }

    
}
