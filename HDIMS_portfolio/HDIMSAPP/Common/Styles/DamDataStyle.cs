using System;
using System.Windows;
using System.Windows.Media;
using Infragistics.Controls.Grids;
using Infragistics.Controls.Grids.Primitives;

namespace HDIMSAPP.Common.Styles
{
    public class DamDataStyle
    {
        private Style _headerCellStyle = new Style(typeof(HeaderCellControl));
        private Style _headerReadonlyStyle = new Style(typeof(HeaderCellControl));
        private Style _cellStyle = new Style(typeof(CellControl));
        private Style _readonlyStyle = new Style(typeof(CellControl));

        public Style HeaderCellStyle { get { return _headerCellStyle; } }
        public Style HeaderReadonlyStyle { get { return _headerReadonlyStyle; } }
        public Style CellStyle { get { return _cellStyle; } }
        public Style ReadonlyStyle { get { return _readonlyStyle; } }

        //Background 색상및
        private Color _headerBackColor = new Color() { R=224, G=233, B=241};
        private Color _readonlyBackColor = new Color() { R = 239, G = 239, B = 239 };
        private Thickness _cellThickness = new Thickness(0, 0, 1, 1);
        public DamDataStyle()
        {
            //헤더셀 설정
            AddStyle(_headerCellStyle, HeaderCellControl.BackgroundProperty, _headerBackColor);
            //헤더 Readonly 셀 설정
            AddStyle(_headerReadonlyStyle, HeaderCellControl.BackgroundProperty, _readonlyBackColor);
            //컬럼 셀 설정
            AddStyle(_cellStyle, CellControl.BackgroundProperty, Colors.White);
            AddStyle(_cellStyle, CellControl.BorderThicknessProperty, _cellThickness);
            //컬럼 Readonly 셀 설정
            AddStyle(_readonlyStyle, CellControl.BackgroundProperty, _readonlyBackColor);
            AddStyle(_readonlyStyle, CellControl.BorderThicknessProperty, _cellThickness);
        }

        private void AddStyle(Style style, DependencyProperty property, Object value )
        {
            //Setter set = new Setter() { Property = property, Value = value };
            //if (style.Setters.Contains(set))
            //{
            //    style.Setters.SetValue(property, value);
            //}
            //else
            //{
            //    style.Setters.Add(set);
            //}
            style.Setters.SetValue(property, value);
        }
    }
}
