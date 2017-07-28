using System;
using System.Windows;
using System.Windows.Media;
using HDIMSAPP.Models.DataSearch;
using Infragistics.Controls.Grids;

namespace HDIMSAPP.Common.ConditionalFormatRule
{
    public class EquipConditionalFormatRule : ConditionalFormattingRuleBase
    {
        protected override IConditionalFormattingRuleProxy CreateProxy()
        {
            return new EquipConditionalFormatRuleProxy();
        }
    }

    public class EquipConditionalFormatRuleProxy : ConditionalFormattingRuleBaseProxy
    {

        private object GetPropertyValue(object _data, string _property)
        {
            if (_data.GetType().GetProperty(_property) != null)
            {
                return _data.GetType().GetProperty(_property).GetValue(_data, null);
            }

            return null;
        }

        protected override Style EvaluateCondition(object sourceDataObject, object sourceDataValue)
        {
            EquipModel _data = sourceDataObject as EquipModel;
            Column _col = Parent.Column;
            if (_data != null && _col.Key!=null )
            {
                string _key = _col.Key;
                Style _curStyle = new Style(typeof(ConditionalFormattingCellControl));
                string _obsdt = _data.OBSDT;
                double _cval;
                if (_obsdt.Substring(8, 4).Equals("2400"))
                {
                    _curStyle.Setters.Add(new Setter() { Property = ConditionalFormattingCellControl.BackgroundProperty, Value = new SolidColorBrush(new Color() { A = 255, R = 233, G = 212, B = 255 }) });
                }
                else if (_obsdt.Substring(10, 2).Equals("00"))
                {
                    _curStyle.Setters.Add(new Setter() { Property = ConditionalFormattingCellControl.BackgroundProperty, Value = new SolidColorBrush(new Color() { A = 255, R = 224, G = 255, B = 224 }) });
                }

                if (_key.Equals("DAMCD") || _key.Equals("DAMNM") || _key.Equals("OBSCD") || _key.Equals("OBSNM") || _key.Equals("OBSDT") || _key.Equals("P_OBSDT")  || _key.Equals("SNR")) 
                {
                }
                else if (_key.Equals("BATTVOLT") && _data.BATTVOLT != null) 
                {
                    _cval = 1;
                    bool _res = Double.TryParse(_data.BATTVOLT, out _cval);
                    if(_res && _cval <= 0) 
                        _curStyle.Setters.Add(new Setter() { Property = ConditionalFormattingCellControl.ForegroundProperty, Value = new SolidColorBrush(Colors.Red) });
                }
                else if (_key.Equals("SECONDARY_CALL") && _data.SECONDARY_CALL != null && Double.TryParse(_data.SECONDARY_CALL, out _cval) && _cval > 0)
                {
                    _curStyle.Setters.Add(new Setter() { Property = ConditionalFormattingCellControl.ForegroundProperty, Value = new SolidColorBrush(Colors.Green) });
                }
                else
                {
                    object _pval = GetPropertyValue(_data, _key);
                    if (_pval != null)
                    {
                        _cval = 0;
                        bool _res = Double.TryParse(_pval.ToString(), out _cval);
                        if (_res && _cval > 0) 
                            _curStyle.Setters.Add(new Setter() { Property = ConditionalFormattingCellControl.ForegroundProperty, Value = new SolidColorBrush(Colors.Red) });
                    }
                } 
                

                if (_curStyle.Setters.Count > 0)
                {
                    return _curStyle;
                }
            }

            return null;

        }

    }

    
}
