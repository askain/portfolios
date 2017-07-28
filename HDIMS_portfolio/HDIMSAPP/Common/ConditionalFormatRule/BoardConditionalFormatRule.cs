using System.Windows;
using HDIMSAPP.Models.Board;
using Infragistics.Controls.Grids;

namespace HDIMSAPP.Common.ConditionalFormatRule
{
    public class BoardConditionalFormatRule : ConditionalFormattingRuleBase
    {
        protected override IConditionalFormattingRuleProxy CreateProxy()
        {
            return new BoardConditionalFormatRuleProxy();
        }
    }

    public class BoardConditionalFormatRuleProxy : ConditionalFormattingRuleBaseProxy
    {
        protected override Style EvaluateCondition(object sourceDataObject, object sourceDataValue)
        {
            BoardContentModel _data = sourceDataObject as BoardContentModel;
            //Column _col = Parent.Column;

            if (_data != null)
            {
                if ("Y".Equals(_data.ALWAYSONTOP))  //항상위 알림 일 경우
                {
                    return App.Current.Resources["ImportantBoardCellControl"] as Style; ;
                }
            }

            return null;

        }

    }

}
