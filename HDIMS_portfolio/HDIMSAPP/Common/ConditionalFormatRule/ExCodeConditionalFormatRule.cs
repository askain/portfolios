using System.Windows;
using System.Windows.Media;
using HDIMSAPP.Models.Verify;
using Infragistics.Controls.Grids;

namespace HDIMSAPP.Common.ConditionalFormatRule
{
    public class ExCodeConditionalFormatRule : ConditionalFormattingRuleBase
    {
        protected override IConditionalFormattingRuleProxy CreateProxy()
        {
            return new ExCodeContionalFormatRuleProxy();
        }
    }

    public class ExCodeContionalFormatRuleProxy : ConditionalFormattingRuleBaseProxy
    {
        protected override Style EvaluateCondition(object sourceDataObject, object sourceDataValue)
        {
            Style s = new Style(typeof(ConditionalFormattingCellControl));

            WaterLevelData data1;
            data1 = sourceDataObject as WaterLevelData;

            if (data1 != null)
            {
                if (string.IsNullOrEmpty(data1.EXCOLOR)) return null;

                Color c = Infragistics.ColorConverter.FromString("#" + data1.EXCOLOR);
                s.Setters.Add(new Setter(ConditionalFormattingCellControl.BackgroundProperty, new SolidColorBrush(c)));
                return s;
            }
            return null;
        }
    }
}
