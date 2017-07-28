using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using HDIMSAPP.Models;
using HDIMSAPP.Utils;
using Infragistics.Controls.Editors;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;

namespace HDIMSAPP.Common.Styles
{
    public class DirtyCellStyleSelector : StyleSelector
    {
        public override Style SelectStyle(object item, DependencyObject container)
        {
            GridViewCell cell = container as GridViewCell;

            if (cell.Column.UniqueName.IndexOf('_') > 0)
            {
                string IsDirty_X = cell.Column.UniqueName.Replace("WL_", "IsDirty_").Replace("ACURF_", "IsDirty_").Replace("RF_", "IsDirty_");
                string ExColor_X = IsDirty_X.Replace("IsDirty_", "EXCOLOR_");
                string EdExLvl_X = IsDirty_X.Replace("IsDirty_", "EDEXLVL_");
                string _excolor = CommonUtil.GetValue(item, ExColor_X) as string;
                string _edexlvl = CommonUtil.GetValue(item, EdExLvl_X) as string;

                if (true.Equals(CommonUtil.GetValue(item, IsDirty_X)))
                {
                    //MessageBox.Show("hit!:" + targetPropertyName);
                    if (_edexlvl != null && _edexlvl.ToString().Length > 0)
                    {
                        return Constants.DirtyBoldCellStyle;
                    }
                    else
                    { 
                        return Constants.DirtyCellStyle; 
                    }
                }
                else if (_excolor != null && _excolor.ToString().Length == 6 && ColorTp.Equals("Y"))
                {
                    if (_edexlvl != null && _edexlvl.ToString().Length > 0)
                    {
                    }
                    else 
                    { 
                        return GetExColor(_excolor); 
                    }
                    
                }
                else if (cell.Column.Header.ToString().LastIndexOf("00") == 3)
                {
                    if (_edexlvl != null && _edexlvl.ToString().Length > 0)
                    {
                        return Constants.HalfHourBoldGridViewCell; 
                    }
                    else 
                    { 
                        return Constants.HalfHourGridViewCell; 
                    }
                }
                else if (_edexlvl != null && _edexlvl.ToString().Length > 0)
                {
                    return Constants.NormalBoldGridViewCell;
                }
                else
                {
                    return Constants.NormalGridViewCell;
                }
                
            }
            return Constants.NormalGridViewCell;
        }
        public XamComboEditor SelectColorCombo { get; internal set; }
        public string ColorTp
        {
            get
            {
                string colorTp = ((KeyValue<string>)SelectColorCombo.SelectedItem).Key;
                return colorTp;
            }
        }
        //public Style BaseStyle { get; set; }
        public static IDictionary<string, Style> ExcolorStylesStorage = new Dictionary<string, Style>();
        public Style GetExColor(string excolor)
        {
            //if (BaseStyle == null) throw new NullReferenceException("BaseStyle을 먼저 지정해야 합니다.");
            if (ExcolorStylesStorage.ContainsKey(excolor) == false)
            {
                Style NewStyle = new Style(typeof(GridViewCell));
                //NewStyle.BasedOn = BaseStyle;
                Brush newBrush = new SolidColorBrush(Constants.GetColorFromString(excolor.ToString()));    
                Setter newSetter = new Setter(GridViewCell.BackgroundProperty, newBrush);
                NewStyle.Setters.Add(newSetter);
                ExcolorStylesStorage.Add(excolor, NewStyle);
            }

            return ExcolorStylesStorage[excolor];
        }
    }
}
