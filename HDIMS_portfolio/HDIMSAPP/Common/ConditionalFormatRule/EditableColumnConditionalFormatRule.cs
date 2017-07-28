using System.Windows;
using System.Windows.Media;
using HDIMSAPP.Models.Verify;
using Infragistics.Controls.Grids;

namespace HDIMSAPP.Common.ConditionalFormatRule
{
    public class EditableColumnConditionalFormatRule : ConditionalFormattingRuleBase
    {
        protected override IConditionalFormattingRuleProxy CreateProxy()
        {
            return new EditableColumnContionalFormatRuleProxy();
        }
    }

    public class EditableColumnContionalFormatRuleProxy : ConditionalFormattingRuleBaseProxy
    {
        protected override Style EvaluateCondition(object sourceDataObject, object sourceDataValue)
        {
            Style s = new Style(typeof(ConditionalFormattingCellControl));
            Data data1 = sourceDataObject as Data;
            if (data1 != null)
            {
                //s.Setters.Add(new Setter(CellControl.CursorProperty, Cursors.Hand));
                if (data1.IsDirty == true)
                {
                    s.Setters.Add(new Setter(ConditionalFormattingCellControl.BackgroundProperty, new SolidColorBrush(Infragistics.ColorConverter.FromString("#FFA500"))));
                    
                }
                return s;
            }

            return null;
        }
    }
}
