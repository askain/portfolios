using System;
using System.Windows;
using System.Windows.Media;
using HDIMSAPP.Models.DataSearch;
using Infragistics.Controls.Grids;

namespace HDIMSAPP.Common.ConditionalFormatRule
{
    public class NetworkConditionalFormatRule : ConditionalFormattingRuleBase
    {
        protected override IConditionalFormattingRuleProxy CreateProxy()
        {
            return new NetworkConditionalFormatRuleProxy();
        }
    }

    public class NetworkConditionalFormatRuleProxy : ConditionalFormattingRuleBaseProxy
    {

        protected override Style EvaluateCondition(object sourceDataObject, object sourceDataValue)
        {
            NetworkModel _data = sourceDataObject as NetworkModel;
            Column _col = Parent.Column;
            if (_data != null && _col.Key!=null )
            {
                Style _curStyle = new Style(typeof(ConditionalFormattingCellControl));
                string _obsdt = _data.OBSDT;

                if (_obsdt.Substring(8, 4).Equals("2400"))
                {
                    _curStyle.Setters.Add(new Setter() { Property = ConditionalFormattingCellControl.BackgroundProperty, Value = new SolidColorBrush(new Color() { A = 255, R = 233, G = 212, B = 255 }) });
                }
                else if (_obsdt.Substring(10, 2).Equals("00"))
                {
                    _curStyle.Setters.Add(new Setter() { Property = ConditionalFormattingCellControl.BackgroundProperty, Value = new SolidColorBrush(new Color() { A = 255, R = 224, G = 255, B = 224 }) });
                }

                if (_col.Key.Equals("CHVAL") && _data.CHVAL != null && Double.Parse(_data.CHVAL) <= 0)
                {
                    _curStyle.Setters.Add(new Setter() { Property = ConditionalFormattingCellControl.ForegroundProperty, Value = new SolidColorBrush(Colors.Red) });
                }
                else if (_col.Key.Equals("SATVAL") && _data.SATVAL != null && Double.Parse(_data.SATVAL) <= 0)
                {
                    _curStyle.Setters.Add(new Setter() { Property = ConditionalFormattingCellControl.ForegroundProperty, Value = new SolidColorBrush(Colors.Red) });
                }
                else if (_col.Key.Equals("CDMAVAL") && _data.CDMAVAL != null && Double.Parse(_data.CDMAVAL) <= 0)
                {
                    _curStyle.Setters.Add(new Setter() { Property = ConditionalFormattingCellControl.ForegroundProperty, Value = new SolidColorBrush(Colors.Red) });
                }
                else if (_col.Key.Equals("WIREVAL") && _data.WIREVAL != null && Double.Parse(_data.WIREVAL) <= 0)
                {
                    _curStyle.Setters.Add(new Setter() { Property = ConditionalFormattingCellControl.ForegroundProperty, Value = new SolidColorBrush(Colors.Red) });
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
