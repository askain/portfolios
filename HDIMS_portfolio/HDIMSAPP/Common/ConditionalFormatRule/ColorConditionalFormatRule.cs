using System.Windows;
using System.Windows.Media;
using HDIMSAPP.Models;
using Infragistics.Controls.Grids;

namespace HDIMSAPP.Common.ConditionalFormatRule
{
    public class WLRFColorConditionalFormatRule : ConditionalFormattingRuleBase
    {
        protected override IConditionalFormattingRuleProxy CreateProxy()
        {
            return new WLRFColorConditionalFormatRuleProxy();
        }
    }

    public class WLRFColorConditionalFormatRuleProxy : ConditionalFormattingRuleBaseProxy
    {


        protected override Style EvaluateCondition(object sourceDataObject, object sourceDataValue)
        {
            object _data = sourceDataObject;
            Column _col = Parent.Column;
            if (_data != null && _col.Key!=null )
            {
                string _colorNm = null;
                string _edexlvlNm = null;
                if(_col.Key.StartsWith("WL_")) {
                    _colorNm = _col.Key.Replace("WL_", "EXCOLOR_");
                    _edexlvlNm = _col.Key.Replace("WL_", "EDEXLVL_");
                }
                else if (_col.Key.StartsWith("ACURF_"))
                {
                    _colorNm = _col.Key.Replace("ACURF_", "EXCOLOR_");
                    _edexlvlNm = _col.Key.Replace("ACURF_", "EDEXLVL_");
                }
                if (_colorNm != null || _edexlvlNm != null)
                {
                    Style _colorStyle = new Style(typeof(ConditionalFormattingCellControl));
                    
                    if (_colorNm != null && _data.GetType().GetProperty(_colorNm)!=null)
                    {
                        object _color = _data.GetType().GetProperty(_colorNm).GetValue(_data, null);
                        if(_color!=null && _color.ToString().Length==6)
                            _colorStyle.Setters.Add(new Setter(ConditionalFormattingCellControl.BackgroundProperty, new SolidColorBrush(Constants.GetColorFromString(_color.ToString()))));
                    }
                    if (_edexlvlNm != null && _data.GetType().GetProperty(_edexlvlNm) != null)
                    {
                        object _edexlvl = _data.GetType().GetProperty(_edexlvlNm).GetValue(_data, null);
                        if (_edexlvl != null && _edexlvl.ToString().Length > 0)
                        {
                            _colorStyle.Setters.Add(new Setter(ConditionalFormattingCellControl.FontWeightProperty, FontWeights.Bold));
                            _colorStyle.Setters.Add(new Setter(ConditionalFormattingCellControl.FontSizeProperty, 12));
                        }
                    }

                    if (_colorStyle.Setters.Count > 0)
                    {
                        return _colorStyle;
                    }
                }

            }

            return null;

        }

    }

    
}
