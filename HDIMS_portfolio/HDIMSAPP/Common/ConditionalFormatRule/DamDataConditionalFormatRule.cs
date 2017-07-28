using System.Windows;
using System.Windows.Media;
using HDIMSAPP.Models.Verify;
using Infragistics.Controls.Grids;

namespace HDIMSAPP.Common.ConditionalFormatRule
{
    public class DamDataConditionalFormatRule : ConditionalFormattingRuleBase
    {
        protected override IConditionalFormattingRuleProxy CreateProxy()
        {
            return new DamDataConditionalFormatRuleProxy();
        }

    }

    public class DamDataConditionalFormatRuleProxy : ConditionalFormattingRuleBaseProxy
    {
        private Style EditedStyle { get; set; }
        private Style MidStyle { get; set; }
        private Style Mid_EditedStyle { get; set; }
        private Style HrStyle { get; set; }
        private Style Hr_EditedStyle { get; set; }
        private Style EditingStyle { get; set; }

        private Setter _midBColor { get {return new Setter() { Property = ConditionalFormattingCellControl.BackgroundProperty, Value = new SolidColorBrush(new Color() { A = 255, R = 233, G = 212, B = 255 }) };}}
        private Setter _hourBColor { get {return new Setter() { Property = ConditionalFormattingCellControl.BackgroundProperty, Value = new SolidColorBrush(new Color() { A = 255, R = 224, G = 255, B = 224 }) };}}
        private Setter _editing { get { return new Setter() { Property = ConditionalFormattingCellControl.BackgroundProperty, Value = new SolidColorBrush(Infragistics.ColorConverter.FromString("#FFA500")) }; } }
        //"FF5500", "FFA500",
        private Setter _edited { get { return new Setter() { Property = ConditionalFormattingCellControl.FontWeightProperty, Value = FontWeights.Bold }; } }

        public DamDataConditionalFormatRuleProxy()
        {
            EditedStyle = new Style(typeof(ConditionalFormattingCellControl));  //1
            EditedStyle.Setters.Add(_edited);

            MidStyle = new Style(typeof(ConditionalFormattingCellControl));
            MidStyle.Setters.Add(_midBColor);

            Mid_EditedStyle = new Style(typeof(ConditionalFormattingCellControl));
            Mid_EditedStyle.Setters.Add(_midBColor);
            Mid_EditedStyle.Setters.Add(_edited);

            HrStyle = new Style(typeof(ConditionalFormattingCellControl));
            HrStyle.Setters.Add(_hourBColor);

            Hr_EditedStyle = new Style(typeof(ConditionalFormattingCellControl));
            Hr_EditedStyle.Setters.Add(_hourBColor);
            Hr_EditedStyle.Setters.Add(_edited);

            EditingStyle = new Style(typeof(ConditionalFormattingCellControl));
            EditingStyle.Setters.Add(_editing);

        }

        protected override Style EvaluateCondition(object sourceDataObject, object sourceDataValue)
        {
            DamData _data = sourceDataObject as DamData;
            Column _col = Parent.Column;
            //"RWL", "OSPILWL", "ETCIQTY2", "EDQTY", "ETCEDQTY", "SPDQTY", "ETCDQTY1"
            //, "ETCDQTY2", "ETCDQTY3", "OTLTDQTY", "ITQTY1", "ITQTY2", "ITQTY3"

            //////////////////////////////////////////////
            // 점수표
            //////////////////////////////////////////////
            // 수정이력 있음             →        1점
            // HR데이터                 →         2점
            // HR데이터 + 수정이력있음   →  2+1 = 3점
            // Mid데이터                 →        4점
            // Mid데이터 + 수정이력있음   →  4+1 = 5점
            // 아무해당 없음             →         0점
            //////////////////////////////////////////////


            int score = 0;  
            if (_data.TRMDV.Equals("10"))
            {
                if (_col.Key.Equals("RWL") && _data.RWL_CK != null)
                {
                    if (_data.RWL_CK.Equals("1")) return EditingStyle;
                    else if (_data.RWL_CK.Equals("Y")) score = 1;
                }
                else if (_col.Key.Equals("OSPILWL") && _data.OSPILWL_CK != null)
                {
                    if (_data.OSPILWL_CK.Equals("1")) return EditingStyle;
                    else if (_data.OSPILWL_CK.Equals("Y")) score = 1;
                }
                else if (_col.Key.Equals("ETCIQTY2") && _data.ETCIQTY2_CK != null)
                {
                    if (_data.ETCIQTY2_CK.Equals("1")) return EditingStyle;
                    else if (_data.ETCIQTY2_CK.Equals("Y")) score = 1;
                }
                else if (_col.Key.Equals("EDQTY") && _data.EDQTY_CK != null)
                {
                    if (_data.EDQTY_CK.Equals("1")) return EditingStyle;
                    else if (_data.EDQTY_CK.Equals("Y")) score = 1;
                }
                else if (_col.Key.Equals("ETCEDQTY") && _data.ETCEDQTY_CK != null)
                {
                    if (_data.ETCEDQTY_CK.Equals("1")) return EditingStyle;
                    else if (_data.ETCEDQTY_CK.Equals("Y")) score = 1;
                }
                else if (_col.Key.Equals("SPDQTY") && _data.SPDQTY_CK != null)
                {
                    if (_data.SPDQTY_CK.Equals("1")) return EditingStyle;
                    else if (_data.SPDQTY_CK.Equals("Y")) score = 1;
                }
                else if (_col.Key.Equals("ETCDQTY1") && _data.ETCDQTY1_CK != null)
                {
                    if (_data.ETCDQTY1_CK.Equals("1")) return EditingStyle;
                    else if (_data.ETCDQTY1_CK.Equals("Y")) score = 1;
                }
                else if (_col.Key.Equals("ETCDQTY2") && _data.ETCDQTY2_CK != null)
                {
                    if (_data.ETCDQTY2_CK.Equals("1")) return EditingStyle;
                    else if (_data.ETCDQTY2_CK.Equals("Y")) score = 1;
                }
                else if (_col.Key.Equals("ETCDQTY3") && _data.ETCDQTY3_CK != null)
                {
                    if (_data.ETCDQTY3_CK.Equals("1")) return EditingStyle;
                    else if (_data.ETCDQTY3_CK.Equals("Y")) score = 1;
                }
                else if (_col.Key.Equals("OTLTDQTY") && _data.OTLTDQTY_CK != null)
                {
                    if (_data.OTLTDQTY_CK.Equals("1")) return EditingStyle;
                    else if (_data.OTLTDQTY_CK.Equals("Y")) score = 1;
                }
                else if (_col.Key.Equals("ITQTY1") && _data.ITQTY1_CK != null)
                {
                    if (_data.ITQTY1_CK.Equals("1")) return EditingStyle;
                    else if (_data.ITQTY1_CK.Equals("Y")) score = 1;
                }
                else if (_col.Key.Equals("ITQTY2") && _data.ITQTY2_CK != null)
                {
                    if (_data.ITQTY2_CK.Equals("1")) return EditingStyle;
                    else if (_data.ITQTY2_CK.Equals("Y")) score = 1;
                }
                else if (_col.Key.Equals("ITQTY3") && _data.ITQTY3_CK != null)
                {
                    if (_data.ITQTY3_CK.Equals("1")) return EditingStyle;
                    else if (_data.ITQTY3_CK.Equals("Y")) score = 1;
                }
            }

            if (_data.TRMDV.Equals("60") || _data.TRMDV.Equals("30"))
            {
                if (_data.OBSDT.Substring(8, 2).Equals("24"))
                {
                    score += 4; //MID
                }
            }
            else if (_data.TRMDV.Equals("DAY"))
            {
                if (_data.OBSDT.Substring(6, 2).Equals("01"))
                {
                    score += 4; //MID
                }
            }
            else
            {
                if (_data.OBSDT.Substring(8, 4).Equals("2400"))
                {
                    score += 4; //MID
                }
                else if (_data.OBSDT.Substring(10, 2).Equals("00"))
                {
                    score += 2; //HR
                }
            }

            //////////////////////////////////////////////
            // 점수표
            //////////////////////////////////////////////
            // 수정이력 있음             →        1점
            // HR데이터                 →         2점
            // HR데이터 + 수정이력있음   →  2+1 = 3점
            // Mid데이터                 →        4점
            // Mid데이터 + 수정이력있음   →  4+1 = 5점
            // 아무해당 없음             →         0점
            //////////////////////////////////////////////
            // 위에 있는 점수표 참조.
            switch (score)
            {
                case 0:
                    return null;
                case 1:
                    return EditedStyle;
                case 2:
                    return HrStyle;
                case 3:
                    return Hr_EditedStyle;
                case 4:
                    return MidStyle;
                case 5:
                    return Mid_EditedStyle;
            }
            return null;  

        }

    }
}
