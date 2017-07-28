using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Infragistics.Controls.Charts;

namespace HDIMSAPP.Common.Behavior
{
    public class ChartCrosshairBehaviors : DependencyObject
    {
        #region Chart Crosshair Visibility Behavior

        internal const string CrosshairVisibilityPropertyName = "CrosshairVisibility";
        public static readonly DependencyProperty CrosshairVisibilityProperty =
            DependencyProperty.RegisterAttached(CrosshairVisibilityPropertyName,
            typeof(ChartCrosshairVisibilityBehavior), typeof(ChartCrosshairBehaviors),
            new PropertyMetadata(null, (o, e) => OnChartCrosshairsBehaviorChanged(o as XamDataChart,
                    e.OldValue as ChartCrosshairVisibilityBehavior,
                    e.NewValue as ChartCrosshairVisibilityBehavior)));

        public static ChartCrosshairVisibilityBehavior GetCrosshairVisibility(DependencyObject target)
        {
            return target.GetValue(CrosshairVisibilityProperty) as ChartCrosshairVisibilityBehavior;
        }
        public static void SetCrosshairVisibility(DependencyObject target, ChartCrosshairVisibilityBehavior behavior)
        {
            target.SetValue(CrosshairVisibilityProperty, behavior);
        }

        private static void OnChartCrosshairsBehaviorChanged(XamDataChart chart, ChartCrosshairVisibilityBehavior oldValue, ChartCrosshairVisibilityBehavior newValue)
        {
            if (chart == null)
            {
                return;
            }
            if (oldValue != null)
            {
                oldValue.OnDetach(chart);
            }
            if (newValue != null)
            {
                newValue.OnAttach(chart);
            }
        }
        #endregion
    }

    public class ChartCrosshairVisibilityBehavior
    {
        public ChartCrosshairVisibilityBehavior()
        {
            _invisibleStyle = new Style(typeof(Line));
            _invisibleStyle.Setters.Add(new Setter(Shape.StrokeProperty, new SolidColorBrush(Colors.Transparent)));
        }

        #region Properties
        private bool _first = true;
        private Line _vertical;
        private Line _horizontal;
        private readonly Style _invisibleStyle;
        public bool ShowHorizontalCrosshair { get; set; }
        public bool ShowVerticalCrosshair { get; set; }
        #endregion

        #region Methods
        private void MakeInvisible(XamDataChart chart, Line crosshairLine)
        {
            crosshairLine.Style = _invisibleStyle;
        }
        private void MakeVisible(XamDataChart chart, Line crosshairLine)
        {
            if (chart == null || crosshairLine == null)
            {
                return;
            }
            crosshairLine.Style = chart.CrosshairLineStyle;
        }
        #endregion

        #region Event Handlers
        public void OnAttach(XamDataChart chart)
        {
            chart.MouseEnter += OnChartMouseEnter;
        }
        public void OnDetach(XamDataChart chart)
        {
            chart.MouseEnter -= OnChartMouseEnter;
            MakeVisible(chart, _vertical);
            MakeVisible(chart, _horizontal);
            _vertical = null;
            _horizontal = null;
            _first = true;
        }

        void OnChartMouseEnter(object sender, MouseEventArgs e)
        {
            XamDataChart chart = sender as XamDataChart;
            if (_first && chart != null)
            {
                _first = false;
                var crosshairs =
                    from line in chart.VisualDescendants()
                    where line is Line && (line as Line).Style
                          == chart.CrosshairLineStyle
                    select line as Line;
                List<Line> lines = crosshairs.ToList();
                if (lines.Count != 2)
                {
                    _first = true;
                    chart.MouseMove += OnChartMouseEnter;
                    return;
                }
                bool uninitialized = lines[0].X1 == 0.0 && lines[0].X2 == 0.0 &&
                                     lines[1].X1 == 0.0 && lines[1].X2 == 0.0;
                if (uninitialized)
                {
                    _first = true;
                    chart.MouseMove += OnChartMouseEnter;
                    return;
                }
                if (lines[0].X1 == lines[0].X2)
                {
                    _vertical = lines[0];
                    _horizontal = lines[1];
                }
                else
                {
                    _vertical = lines[1];
                    _horizontal = lines[0];
                }

                if (!this.ShowHorizontalCrosshair)
                {
                    MakeInvisible(chart, _horizontal);
                }
                if (!this.ShowVerticalCrosshair)
                {
                    MakeInvisible(chart, _vertical);
                }
                chart.MouseMove -= OnChartMouseEnter;
            }
        }
        #endregion
    }

    public static class ChartExtension
    {
        public static IEnumerable<DependencyObject> VisualDescendants(this DependencyObject current)
        {
            yield return current;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(current); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(current, i);
                foreach (DependencyObject sub in child.VisualDescendants())
                {
                    yield return sub;
                }
            }
        }
    }

    public class ChartBehaviors : DependencyObject
    {
        public static readonly DependencyProperty CursorTooltipProperty =
            DependencyProperty.RegisterAttached("CursorTooltip",
            typeof(CursorTooltipBehavior), typeof(ChartBehaviors),
            new PropertyMetadata(null,
                (o, e) => CursorTooltipChanged(
                    o as XamDataChart,
                    e.OldValue as CursorTooltipBehavior,
                    e.NewValue as CursorTooltipBehavior)));

        public static CursorTooltipBehavior GetCursorTooltip(
            DependencyObject target)
        {
            return target.GetValue(CursorTooltipProperty)
                as CursorTooltipBehavior;
        }

        public static void SetCursorTooltip(
            DependencyObject target, CursorTooltipBehavior behavior)
        {
            target.SetValue(CursorTooltipProperty, behavior);
        }

        private static void CursorTooltipChanged(
            XamDataChart chart,
            CursorTooltipBehavior oldValue,
            CursorTooltipBehavior newValue)
        {
            if (chart == null)
            {
                return;
            }
       
            if (oldValue != null)
            {
                oldValue.OnDetach(chart);
            }
            if (newValue != null)
            {
                newValue.OnAttach(chart);
            }
        }
    }

    public class ChartItemInfo : INotifyPropertyChanged
    {
        private object _item;
        private SeriesItemInfoCollection _seriesItemCollection;

        public object Item
        {
            get { return _item; }
            set
            {
                _item = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Item"));
                }
            }
        }
        public SeriesItemInfoCollection SeriesItemCollection
        {
            get { return _seriesItemCollection; }
            set
            {
                _seriesItemCollection = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SeriesItemCollection"));
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class SeriesItemInfo : INotifyPropertyChanged
    {
        private Series _series;
        public Series Series
        {
            get { return _series; }
            set
            {
                _series = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(
                        this,
                        new PropertyChangedEventArgs("Series"));
                }
            }
        }

        private object _item;
        public object Item
        {
            get { return _item; }
            set
            {
                _item = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(
                        this,
                        new PropertyChangedEventArgs("Item"));
                }
            }
        }

        private object _value;
        public object Value
        {
            get { return _value; }
            set
            {
                _value = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(
                        this,
                        new PropertyChangedEventArgs("Value"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class SeriesItemInfoCollection : ObservableCollection<SeriesItemInfo>
    {
        public void UpdateSeriesItem(
            Series series,
            object item)
        {
            object value = null;
            bool found = false;
            int selIndex = -1;
            string key = (series.Tag!=null)?series.Tag.ToString():series.Name;
            var indexes = from curr in this
                          where curr.Series == series
                          select this.IndexOf(curr);

            for (var i = 0; i < this.Count; i++)
            {
                Series _ser = this[i].Series;
                if (_ser.Visibility == Visibility.Collapsed)
                {
                }
            }
            foreach (var index in indexes)
            {
                found = true;
                this[index].Item = item;
                selIndex = index;
            }
            // 해당객체의 Property에서 값을 뽑아온다.
            
            PropertyInfo _pi = item.GetType().GetProperty(key);
            if (_pi != null)
            {
                
                value = _pi.GetValue(item, null);
                //MessageBox.Show("TAG : " + key + " type : " + item.GetType().ToString() + "," + value);
            }
            else
            {
                //MessageBox.Show("TAG : " + key + " type : " + item.GetType().ToString());
            }

            if (!found)
            {
                this.Add(new SeriesItemInfo()
                    {
                        Series = series,
                        Item = item,
                        Value = value
                    });
            }
            else
            {
                this[selIndex].Value = value;
            }
            
        }
    }

    public class CursorTooltipBehavior
    {
        private bool _isOverChart = false;
        private Popup _popup = new Popup();

        private ContentControl _content = new ContentControl();
        private Panel _container;
        private XamDataChart _owner = null;

        private DataTemplate _tooltipTemplate;

        //private SeriesItemInfoCollection _items = new SeriesItemInfoCollection();
        private ChartItemInfo _item = new ChartItemInfo();

        public DataTemplate TooltipTemplate
        {
            get { return _tooltipTemplate; }
            set
            {
                _tooltipTemplate = value;
                _content.ContentTemplate = _tooltipTemplate;

            }
        }

        protected bool IsOverChart
        {
            get { return _isOverChart; }
            set
            {
                bool last = _isOverChart;
                _isOverChart = value;
                if (_isOverChart && !last)
                {
                    ShowPopup();
                }
                if (!_isOverChart && last)
                {
                    HidePopup();
                }
            }
        }

        private void HidePopup()
        {
            _popup.IsOpen = false;
        }

        private void ShowPopup()
        {

            //HERE IS THE CHNAGE I MADE
            //_popup.Placement = PlacementMode.RelativePoint;
            _popup.Width = 170;
            _popup.IsOpen = true;
        }

        public void OnAttach(XamDataChart chart)
        {
            if (_owner != null)
            {
                OnDetach(_owner);
            }
            chart.MouseLeave += Chart_MouseLeave;
            chart.MouseMove += Chart_MouseMove;
            chart.SeriesCursorMouseMove += Chart_SeriesCursorMouseMove;
            _popup.IsOpen = false;
            _popup.Child = _content;
            _content.ContentTemplate = TooltipTemplate;
            //_content.Content = _items;
            _content.Content = _item;
            if (chart.Parent != null && chart.Parent is Panel)
            {
                _container = chart.Parent as Panel;
                _container.Children.Add(_popup);
            }
        }

        public void OnDetach(XamDataChart chart)
        {
            if (_owner != chart)
            {
                return;
            }
            chart.MouseLeave -= Chart_MouseLeave;
            chart.MouseMove -= Chart_MouseMove;
            chart.SeriesCursorMouseMove -= Chart_SeriesCursorMouseMove;
            IsOverChart = false;
            //_items.Clear();
            _content.Content = null;
            _item = null;
            _owner = null;
        }

        void Chart_MouseMove(object sender, MouseEventArgs e)
        {

            //I ADDED THE ROW BELOW, TOO
            //_popup.PlacementTarget = sender as XamDataChart;

            _owner = sender as XamDataChart;
            SetPopupOffsets(e.GetPosition(null), e.GetPosition(_owner));
            _owner = null;

            if (_content.Content == null || _item.Item==null)
            {
                IsOverChart = false;
            }
            else
            {
                IsOverChart = true;
            }
        }

        private void SetPopupOffsets(Point absolutPoint, Point relatedPoint)
        {
            try
            {
                _popup.VerticalOffset = absolutPoint.Y;
                _popup.HorizontalOffset = absolutPoint.X + 2;

                #region 테스용
                //Point originPoint = _container.RenderTransformOrigin;
                //MessageBox.Show(string.Format("x / y = {0} / {1}", originPoint.X, originPoint.Y));

                //MessageBox.Show("2");
                //Point oPoint = _owner.RenderTransformOrigin;
                //Rect re = _owner.WindowRect;
                //Transform t = _owner.RenderTransform;

                //oPoint.X == 0 무조건-_-
                //_owner.Width == NaN 무조건-_-
                //_owner.WindowRect.Right == 1 
                //
                //MessageBox.Show(string.Format("point.X={0} _owner.ActualWidth={1} _popup.ActualWidth={2}", point.X, _owner.ActualWidth, _popup.ActualWidth));
                #endregion

                #region 좌우 대칭이 안돼서 못씀
                //_popup.VerticalOffset = absolutPoint.Y;

                //if (relatedPoint.X  < _owner.ActualWidth / 2)
                //{
                //    _popup.HorizontalOffset = absolutPoint.X + 2;
                //}
                //else
                //{
                //    _popup.HorizontalOffset = absolutPoint.X - _popup.ActualWidth;
                //}
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.InnerException.ToString() + Environment.NewLine + ex.InnerException.StackTrace);
            }
        }

        void Chart_SeriesCursorMouseMove(object sender, ChartCursorEventArgs e)
        {
            if (e.Series != null && e.Item != null)
            {
                _item.Item = e.Item;
                if (_item.SeriesItemCollection == null)
                {
                    _item.SeriesItemCollection = new SeriesItemInfoCollection();
                }
                 //_items.UpdateSeriesItem(e.Series, e.Item);
                if (e.Series.Visibility == Visibility.Visible)
                {
                    _item.SeriesItemCollection.UpdateSeriesItem(e.Series, e.Item);
                }
            }
        }

        void Chart_MouseLeave(object sender, MouseEventArgs e)
        {
            IsOverChart = false;
            _item = new ChartItemInfo();
            _content.Content = _item;
        }
    }
}
