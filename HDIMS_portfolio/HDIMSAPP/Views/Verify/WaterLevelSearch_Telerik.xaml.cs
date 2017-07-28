using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using HDIMSAPP.Common;
using HDIMSAPP.Common.Styles;
using HDIMSAPP.Models;
using HDIMSAPP.Models.Common;
using HDIMSAPP.Models.DamBoObsMng;
using HDIMSAPP.Models.Verify;
using HDIMSAPP.Utils;
using ImageTools;
using ImageTools.IO.Png;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Grids;
using Infragistics.Documents.Excel;
using Newtonsoft.Json;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using HDIMSAPP.Common.Converter;

namespace HDIMSAPP.Views.Verify
{

    public partial class WaterLevelSearch_Telerik : Page
    {
        //private string DamDataUri = "/Verify/GetDamDataVerifyList/?";
        //private string DamCodeUri = "/Common/GetDamCodeList/?allyn=Y"; //DamType, firstvalue, damcd, obscd
        //private string DamTypeUri = "/Common/GetDamTypeList/?allyn=Y";
        //private string ObsCodeUri = "/Common/ObsCodeList/?ObsTp=WL";
        private string WKUri = "/DamBoObsMng/GetWK/?allyn=Y";
        private WebClient client;
        private string[] dataTpKeys = { "10", "30", "60" };
        private string[] dataTpValues = { "10분", "30분", "60분" };
        private string[] searchTpKeys = { "WL", "FLW" };
        private string[] searchTpValues = { "수위", "유량" };
        private string[] selectColorKeys = { "Y", "N"};
        private string[] selectColorValues = { "표현", "없음" };
        private DateTime currDate = DateTime.Now;


        //현재 댐의 관측국 목록
        private IList<ObsCode> CurObsCodeList;

        private IDictionary<string, IDictionary<string, string>> HeaderColumns = new Dictionary<string, IDictionary<string, string>>();

        private IList<WaterLevelSearchData> CurDamDataList = new List<WaterLevelSearchData>();

        #region //챠트 관련 변수
        private bool isChartView = false;
        private CategoryXAxis CurXAxis = new CategoryXAxis();
        private NumericYAxis CurYAxis = new NumericYAxis();
        private IList<IDictionary> CurChartData = new List<IDictionary>();
        private MinMaxCalculator mmc;
        #endregion

        //준비
        private bool IsSearchReady { get { if (damTpCombo.ItemsSource == null || damCdCombo.ItemsSource == null) { return false; } return true; } }   //검색조건 데이터가 전부 바인딩 되었는가

        private enum LoadingType { SEARCH, EXCEL };

        private System.Text.StringBuilder noti = new System.Text.StringBuilder();

        public WaterLevelSearch_Telerik()
        {
            InitializeComponent();
            InitSearchPanel();
            //damGrid.mouse.CellDoubleClicked += new Controls.DoubleClickDataGrid.CellDoubleClickedHandler(damGrid_CellDoubleClicked);
            //damGrid.LoadingRow += new EventHandler<DataGridRowEventArgs>(damGrid_LoadingRow);
            //damGrid.LoadingRowDetails += new EventHandler<GridViewRowDetailsEventArgs>(damGrid_LoadingRowDetails);    //발동안됌       


            noti.Append("Please click [조회] button to start. it will take a few seconds.")
                .Append(System.Environment.NewLine).Append(System.Environment.NewLine)
                .Append("This is a demonstrating site to check performance of large data in RIA.")
                .Append(System.Environment.NewLine).Append(System.Environment.NewLine)
                .Append("It is over 144 colums X 1000 rows.")
                .Append(System.Environment.NewLine).Append(System.Environment.NewLine)
                .Append("And this gird also can....")
                .Append(System.Environment.NewLine).Append(System.Environment.NewLine)
                .Append(" 1.edit data. just press any key on any cell.")
                .Append(System.Environment.NewLine).Append(System.Environment.NewLine)
                .Append(" 2.copy & paste.(ctrl+c -> ctrl+v)")
                .Append(System.Environment.NewLine).Append(System.Environment.NewLine)
                .Append(" 3.show a linear graph. press [그래프] key.")
                .Append(System.Environment.NewLine).Append(System.Environment.NewLine)
                .Append(" 4.edited celles show different background color.")
                .Append(System.Environment.NewLine).Append(System.Environment.NewLine)
                .Append(" 5.save as excel. press [엑셀] key.")
                .Append(System.Environment.NewLine).Append(System.Environment.NewLine)
                .Append(" 6.Double Click to open a editing window.")
                .Append(System.Environment.NewLine).Append(System.Environment.NewLine)
                ;
            MessageBox.Show(noti.ToString());
        }

        #region 초기화
        private void InitSearchPanel()
        {
            GetWKList();
            GetDamTypeList();
            GetDamCodeList();
            InitSearchDate();
            InitDataTpCombo();
            InitSearchTpCombo();
            InitSelectColorCombo();
            CreateGridColumns();
            ToggleButtonStatus(false);
            ToggleGridView();
            dataTpCombo.SelectionChanged += new EventHandler(dataTpCombo_SelectionChanged);
            searchTpCombo.SelectionChanged += new EventHandler(dataTpCombo_SelectionChanged);
            selectColorCombo.SelectionChanged += new EventHandler(selectColorCombo_SelectionChanged);
            damGrid.MouseRightButtonDown +=new MouseButtonEventHandler(damGrid_MouseRightButtonDown);
            damGrid.MouseRightButtonUp += new MouseButtonEventHandler(damGrid_MouseRightButtonUp);
            damGrid.RowActivated += new EventHandler<Telerik.Windows.Controls.GridView.RowEventArgs>(damGrid_CellDoubleClicked);
            damGrid.RowLoaded += new EventHandler<RowLoadedEventArgs>(damGrid_LoadingRowDetails);
            damGrid.DataLoaded += new EventHandler<EventArgs>(HideLoadingBar);
            //damGrid.PastingCellClipboardContent += new EventHandler<GridViewCellClipboardEventArgs>(damGrid_PastingCellClipboardContent); //시스템 붙여넣기가 아니면 실행안돼
            damGrid.KeyboardCommandProvider = new CustomKeyboardCommandProvider(damGrid);
            //damGrid.Pasted += new EventHandler<Telerik.Windows.RadRoutedEventArgs>(damGrid_Pasted); //시스템 붙여넣기가 아니면 실행안돼
            //damGrid.KeyDown += new KeyEventHandler(damGrid_KeyDown);
            //damGrid.KeyUp += new KeyEventHandler(damGrid_KeyUp);
            //IList<ICommand> commands = damGrid.KeyboardCommandProvider.ProvideCommandsForKey(Key.V).ToList<ICommand>();
            //RadGridView.PastingEvent += new RoutedEventHandler()
            
            #region 쓰레기 
            //damGrid.Pasting += new EventHandler<GridViewClipboardEventArgs>(damGrid_Pasting);
            //damGrid.Pasted += new EventHandler<Telerik.Windows.RadRoutedEventArgs>(damGrid_Pasted);
            #endregion
        }

        //void damGrid_Pasted(object sender, Telerik.Windows.RadRoutedEventArgs e)
        //{
        //    RadGridView radGridView = sender as RadGridView;
        //    MessageBox.Show("Pasted");

        //    radGridView.CurrentCell.Value = "11111";
        //}

        //void damGrid_Pasting(object sender, GridViewClipboardEventArgs e)
        //{
        //    RadGridView radGridView = sender as RadGridView;

        //    e.Cancel = true;
        //}


        
        #endregion

        //void radGridView_ContextMenuOpening(object sender, Telerik.WinControls.UI.ContextMenuOpeningEventArgs e)
        //{
        //    if (e.ContextMenuProvider is GridDataCellElement || e.ContextMenuProvider is GridTableBodyElement)
        //    {
        //        if (!(((GridDataCellElement)e.ContextMenuProvider).RowInfo is GridViewNewRowInfo))
        //        {
        //            RadMenuItem itemCopy = new RadMenuItem();
        //            itemCopy.Text = "Copy Row(s)";
        //            itemCopy.Click += new EventHandler(itemCopy_Click);
        //            e.ContextMenu.Items.Insert(0, itemCopy);

        //            if (clipBoard != String.Empty)
        //            {
        //                RadMenuItem itemPaste = new RadMenuItem();
        //                itemPaste.Text = "Paste Row(s)";
        //                itemPaste.Click += new EventHandler(itemPaste_Click);
        //                e.ContextMenu.Items.Insert(0, itemPaste);
        //            }
        //        }
        //    }
        //    contextMenuInvoker = ((GridDataCellElement)e.ContextMenuProvider).GridControl;
        //} 

        #region == 복사 & 붙여넣기 ==
        //void damGrid_PastingCellClipboardContent(object sender, GridViewCellClipboardEventArgs e)
        //{
        //    RadGridView grid = sender as RadGridView;
        //    GridViewDataColumn col = e.Cell.Column as GridViewDataColumn;
            
        //    if (col.DataMemberBinding.Path.Path.Contains("_") == false)
        //    {
        //        e.Cancel = true;
        //    }
        //    else
        //    {

        //    }
        //    //else
        //    //{
        //    //    MessageBox.Show("1");
        //    //    GridViewCell cell = grid.GetRowForItem(e.Cell.Item).Cells[e.Cell.Column.DisplayIndex] as GridViewCell;
        //    //    cell.Background = Constants.DirtyColor;
        //    //}
        //}
        #endregion

        #region -- 수계타입 목록 읽어오기 --
        private void GetWKList()
        {
            IList<DamBoModel> Data = new List<DamBoModel>();
            Data.Add(new DamBoModel() { WKNM = "All" });

            WKCombo.DisplayMemberPath = "WKNM";
            WKCombo.ItemsSource = Data;
            WKCombo.SelectedIndex = 0;
        }
        #endregion
        
        #region -- 댐타입 콤보 --
        private void GetDamTypeList()
        {
            IList<DamType> Data = new List<DamType>();
            Data.Add(new DamType() { DAMTPNM = "All" });

            damTpCombo.DisplayMemberPath = "DAMTPNM";
            damTpCombo.ItemsSource = Data;

            damTpCombo.SelectedIndex = 0;
            
        }
        #endregion

        #region == 댐코드 콤보 ==
        private void GetDamCodeList()
        {
            IList<DamCode> Data = new List<DamCode>();
            Data.Add(new DamCode() { DAMNM = "All", DAMCD = "" });

            damCdCombo.DisplayMemberPath = "DAMNM";
            damCdCombo.ItemsSource = Data;

            //MessageBox.Show("DAMCD:" + empdata.DEFAULT_DAMCD + " INDEX:" + empdata.INDEX_OF_DEFAULT_DAMCD);
            damCdCombo.SelectedIndex = 0;

        }
        #endregion

        #region == 날짜 ==
        private void InitSearchDate()
        {
            selectDtCal.SelectedDate = currDate;
        }
        #endregion

        #region == 구분 콤보 ==

        private void InitDataTpCombo()
        {
            dataTpCombo.DisplayMemberPath = "Value";
            dataTpCombo.ItemsSource = GetDataTpList();
            dataTpCombo.SelectedIndex = 0;
        }
        private IList<KeyValue<string>> GetDataTpList()
        {
            IList<KeyValue<string>> dataTpOptions = new List<KeyValue<string>>();
            for (var i = 0; i < dataTpKeys.Length; i++)
            {
                KeyValue<string> item = new KeyValue<string>();
                item.Key = dataTpKeys[i];
                item.Value = dataTpValues[i];
                dataTpOptions.Add(item);
            }

            return dataTpOptions;
        }
        private void dataTpCombo_SelectionChanged(object sender, EventArgs e)
        {
            CreateGridColumns();
        }
        #endregion

        #region == 검색구분 ==
        private void InitSearchTpCombo()
        {
            searchTpCombo.DisplayMemberPath = "Value";
            searchTpCombo.ItemsSource = GetSearchTpList();
            searchTpCombo.SelectedIndex = 0;
        }
        private IList<KeyValue<string>> GetSearchTpList()
        {
            IList<KeyValue<string>> dataTpOptions = new List<KeyValue<string>>();
            for (var i = 0; i < searchTpKeys.Length; i++)
            {
                KeyValue<string> item = new KeyValue<string>();
                item.Key = searchTpKeys[i];
                item.Value = searchTpValues[i];
                dataTpOptions.Add(item);
            }

            return dataTpOptions;
        }
        
        #endregion

        #region == 보정색표현 ==
        private void InitSelectColorCombo()
        {
            selectColorCombo.DisplayMemberPath = "Value";
            selectColorCombo.ItemsSource = GetSelectColorList();
            selectColorCombo.SelectedIndex = 0;
        }
        private IList<KeyValue<string>> GetSelectColorList()
        {
            IList<KeyValue<string>> dataTpOptions = new List<KeyValue<string>>();
            for (var i = 0; i < selectColorKeys.Length; i++)
            {
                KeyValue<string> item = new KeyValue<string>();
                item.Key = selectColorKeys[i];
                item.Value = selectColorValues[i];
                dataTpOptions.Add(item);
            }

            return dataTpOptions;
        }
        private void selectColorCombo_SelectionChanged(object sender, EventArgs e)
        {
            //string dataTp = ((KeyValue<string>)dataTpCombo.SelectedItem).Key;
            //string searchTp = ((KeyValue<string>)searchTpCombo.SelectedItem).Key;
            //string colorTp = ((KeyValue<string>)selectColorCombo.SelectedItem).Key;
            //if (dataTp.Equals("10") && searchTp.Equals("WL") && colorTp.Equals("Y"))
            //{
            //    foreach (Column _col in damGrid.Columns)
            //    {
            //        if (_col.ConditionalFormatCollection.Count == 0)
            //        {
            //            _col.ConditionalFormatCollection.Add(new WLRFColorConditionalFormatRule());
            //        }
            //    }
            //}
            //else
            //{
            //    foreach (Column _col in damGrid.Columns)
            //    {
            //        _col.ConditionalFormatCollection.Clear();
            //    }
            //}
        }
        #endregion

        #region -- 챠트 관련 --

        private void SetupDamChart()
        {
            string dataTp = ((KeyValue<string>)dataTpCombo.SelectedItem).Key;

            damDataChart.Axes.Clear();
            damDataChart.Series.Clear();

            CurXAxis = new CategoryXAxis();
            AxisLabelSettings xAxisLabelSettings = new AxisLabelSettings();
            CurXAxis.MajorStroke = new SolidColorBrush(Colors.Gray);
            CurXAxis.MajorStrokeThickness = 0.5;
            CurXAxis.Label = "{P_OBSDT}";
            CurXAxis.LabelSettings = xAxisLabelSettings;
            CurXAxis.ItemsSource = CurChartData.ToDataSource();

            CurYAxis = new NumericYAxis();
            CurYAxis.MajorStroke = new SolidColorBrush(Colors.Gray);
            CurYAxis.MajorStrokeThickness = 0.5;
            CurYAxis.Label = "{:F2}";
            
            damDataChart.Axes.Add(CurXAxis);
            damDataChart.Axes.Add(CurYAxis);

            //시리즈 추가
            int i = 0;
            //MessageBox.Show("CurObsCodeList : " + CurObsCodeList.Count.ToString());
            if (dataTp.Equals("1"))
            {
                foreach (ObsCode ddc in CurObsCodeList)
                {
                    LineSeries ss = CreateSeries(ddc.KEY, ddc.VALUE, new SolidColorBrush(Constants.GetColorFromString(Constants.ChartColors[i])));
                    if (i == 0)
                    {
                        ss.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        ss.Visibility = Visibility.Collapsed;
                    }
                    i++;
                    damDataChart.Series.Add(ss);
                }
            }
            else
            {
                foreach (WaterLevelSearchData ddc in CurDamDataList)
                {
                    LineSeries ss = CreateSeries(ddc.OBSCD, ddc.OBSNM, new SolidColorBrush(Constants.GetColorFromString(Constants.ChartColors[i])));
                    if (i == 0)
                    {
                        ss.Visibility = Visibility.Visible;
                        ss.ItemsSource = CurChartData.ToDataSource();// CurChartData;
                    }
                    else
                    {
                        ss.Visibility = Visibility.Collapsed;
                    }
                    i++;
                    damDataChart.Series.Add(ss);
                }
            }
            
        }

        private void toggleChartPanel()
        {
            if (isChartView == false)
            {
                isChartView = true;
                SubLayoutRoot.RowDefinitions[3].Height = new GridLength(300); ;
            }
            else
            {
                isChartView = false;
                SubLayoutRoot.RowDefinitions[3].Height = new GridLength(10);
            }
        }

        private void chartBtn_Click(object sender, RoutedEventArgs e)
        {
            toggleChartPanel();
        }
        private LineSeries CreateSeries(string key, string text, Brush brush)
        {
            string name = key + "_SERIES";
            string valueKey = "OBSCD_" + key;

            LineSeries ser = new LineSeries();
            ser.Name = name;
            ser.ValueMemberPath = valueKey;
            ser.Title = text;
            ser.XAxis = CurXAxis;
            ser.YAxis = CurYAxis;
            ser.MarkerType = MarkerType.None;
            ser.Brush = brush;
            ser.MarkerBrush = brush;
            ser.Thickness = 1.5;
            ser.LegendItemTemplate = this.Resources["CheckBoxLegendItem"] as DataTemplate;
            ser.Tag = valueKey; //멀티 시리즈일 경우 해당 컬럼값을 찾기 위해서
            return ser;
        }

        private void exportImageGridChart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                WriteableBitmap bmp = new WriteableBitmap((int)damDataChart.ActualWidth, (int)damDataChart.ActualHeight);
                bmp.Render(damDataChart, new TranslateTransform());
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.DefaultExt = "png";
                saveDialog.Filter = "이미지파일 (.png)|*.png";
                if (saveDialog.ShowDialog().Value)
                {
                    using (Stream _stream = saveDialog.OpenFile())
                    {
                        PngEncoder encoder = new PngEncoder();
                        encoder.Encode(bmp.ToImage(), _stream);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void resizeGridChart()
        {
            damDataChart.WindowRect = new Rect(0, 0, 1, 1);
        }

        private void viewAllGridChart_Click(object sender, RoutedEventArgs e)
        {
            resizeGridChart();
        }

        private void closeGridChart_Click(object sender, RoutedEventArgs e)
        {
            //resizeGridChart();
            toggleChartPanel();
        }

        
        private void LegendCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox _cb = e.OriginalSource as CheckBox;
            string _serName = _cb.Tag.ToString(); //Series 명
            LineSeries _series = damDataChart.Series[_serName] as LineSeries;

            _series.ItemsSource = CurChartData.ToDataSource();// CurChartData.ToDataSource();
            
            #region 차트 MIN & MAX 구하기
            mmc.AddLegend(_series.ValueMemberPath);
            _series.YAxis.MaximumValue = mmc.Max + mmc.TopOffset;
            _series.YAxis.MinimumValue = mmc.Min - mmc.BottomOffset;
            DifferenceTextBlock.Text = "최대 최소차: " + mmc.DifferenceTandB + "EL.m";
            #endregion
        }

        private void LegendCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox _cb = e.OriginalSource as CheckBox;
            string _serName = _cb.Tag.ToString(); //Series 명
            LineSeries _series = damDataChart.Series[_serName] as LineSeries;
            _series.ItemsSource = null;

            #region 차트 MIN & MAX 구하기
            //MessageBox.Show(mmc.Min + " " + mmc.Max);
            //MessageBox.Show(mmc.Offset.ToString());
            mmc.RemoveLegend(_series.ValueMemberPath);
            _series.YAxis.MaximumValue = mmc.Max + mmc.TopOffset;
            _series.YAxis.MinimumValue = mmc.Min - mmc.BottomOffset;
            DifferenceTextBlock.Text = "최대 최소차: " + mmc.DifferenceTandB + "EL.m";
            #endregion
        }

        /// <summary>
        /// Legend CheckBox All Select
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AllLegendSelect_Click(object sender, RoutedEventArgs e)
        {
            Button sendBtn = sender as Button;

            foreach (LineSeries ss in damDataChart.Series)
            {
                //버튼의 네임으로 콤보박스 체크 여부를 결정함.
                if (sendBtn.Name == "AllLegendSelectBtn")
                {
                    ss.Visibility = Visibility.Visible;
                    ss.ItemsSource = CurChartData.ToDataSource();
                }
                else
                {
                    ss.Visibility = Visibility.Collapsed;

                }
            }
        }

        #endregion

        #region == 그리드 ==
        private void CreateGridColumns()
        {
            damGrid.ItemsSource = null;
            damGrid.Columns.Clear();

            string dataTp = ((KeyValue<string>)dataTpCombo.SelectedItem).Key;
            string searchTp = ((KeyValue<string>)searchTpCombo.SelectedItem).Key;
            string colorTp = ((KeyValue<string>)selectColorCombo.SelectedItem).Key;

            DirtyCellStyleSelector sSelector = new DirtyCellStyleSelector()
            {
                SelectColorCombo = selectColorCombo
            };

            Style HeaderCellStyle = Constants.NormalGridViewHeaderCell;
            Style DefaultStyle = this.Resources["DataGridColumnDefaultStyle"] as Style;
            Style DefaultCellStyle = this.Resources["DataGridColumnCellStyle"] as Style;
            Style HalfHourGridViewHeaderCell = Constants.HalfHourGridViewHeaderCell;
            //Style HalfHourGridViewCell = this.Resources["HalfHourGridViewCell"] as Style;


          
            int addTime = 10;
            int timelen = 144;
            DateTime selDate = selectDtCal.SelectedDate.Value;
            DateTime firstTime = new DateTime(selDate.Year, selDate.Month, selDate.Day, 0, 0, 0);
            if (dataTp.Equals("30"))
            {
                addTime = 30;
                timelen = 48;
            }
            else if (dataTp.Equals("60"))
            {
                addTime = 60;
                timelen = 24;
            }

            string[] keys = { "DAMNM", "OBSNM", "OBSCD" };
            string[] names = { "댐명", "관측국", "관측국코드" };
            int[] widths = { 80, 120, 80 };
            for (var j = 0; j < keys.Length; j++)
            {
                GridViewDataColumn _tc = new GridViewDataColumn()
                {
                    DataMemberBinding = new Binding(keys[j]),
                    Header = names[j],
                    HeaderCellStyle = HeaderCellStyle,
                    CellStyle = DefaultStyle,
                    HeaderTextAlignment = System.Windows.TextAlignment.Center,
                    TextAlignment = System.Windows.TextAlignment.Center,
                    Width = new GridViewLength(widths[j])
                };

                damGrid.Columns.Add(_tc);
            }
                

            //수위 소수점 2자리수 까지 표현
            DamDataConverter damDataConverter = new DamDataConverter();
            DamDataColumn ddc = new DamDataColumn()
            {
                Key = "WL",
                Text = "수위",
                Digit = 2,
                Readonly = false,
                Width = 70,
                IsFixed = FixedState.NotFixed,
                IsChart = true,
                DataType = GridDataType.NUMBER,
                ApplyStyle = true,
                Alignment = HorizontalAlignment.Right
            };
            for (var i = 0; i < timelen; i++)
            {
                string cd = firstTime.AddMinutes((i + 1) * addTime).ToString("HH:mm");
                if (cd.Equals("00:00")) cd = "24:00";
                string idnm = searchTp + "_" + i;
                Style _headStyle = HeaderCellStyle;
                Style _cellStyle = DefaultCellStyle;
                if ((dataTp.Equals("10") || dataTp.Equals("30")) && cd.Substring(3, 2).Equals("00"))
                {
                    _headStyle = HalfHourGridViewHeaderCell;
                    //_cellStyle = HalfHourGridViewCell;
                }
                Binding b = new Binding(idnm);
                b.Mode = BindingMode.TwoWay;    //수정도 가능하게 함!
                if ("WL".Equals(searchTp) == true)
                {
                    b.Converter = damDataConverter; //소수점 두자리 까지 표현
                    b.ConverterParameter = ddc;
                }
                //DataGridColumn __uc = new DataGridColumn();

                GridViewDataColumn _uc = new GridViewDataColumn()
                {
                    DataMemberBinding = b,
                    Header = cd,
                    HeaderCellStyle = _headStyle,
                    CellStyle = _cellStyle,
                    HeaderTextAlignment = System.Windows.TextAlignment.Center,
                    TextAlignment = System.Windows.TextAlignment.Right,
                    CellStyleSelector = sSelector,
                    Width = new GridViewLength(60)
                };
                //DataGridTextColumn _uc = new DataGridTextColumn()
                //{
                //    Binding = b,
                //    Header = cd,
                //    HeaderStyle = _headStyle,
                //    CellStyle = _cellStyle,
                //    Width=new DataGridLength(60),
                //    Foreground = new Binding(selected_i) { Converter = new HDIMSAPP.Common.Converter.ColorConverter() },
                //};
                damGrid.Columns.Add(_uc);
            }
            damGrid.FrozenColumnCount = 3;
        }

        


        private void damGrid_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }
        private void damGrid_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            IEnumerable<UIElement> elements = VisualTreeHelper.FindElementsInHostCoordinates(e.GetPosition(null), this);

            if (null == elements || elements.Count() == 0)
            {
                return;
            }

            var cellQuery = from gridCell in elements
                            where gridCell is GridViewCell
                            select gridCell as GridViewCell;
            GridViewCell dataGridCell = cellQuery.FirstOrDefault();
            GridViewColumn dataGridColumn = dataGridCell.DataColumn;

            
            if (dataGridCell != null)
            {
                dataGridCell.Focus();

                string damCd, obsCd, obsDt, trmdv;
                WaterLevelSearchData _row = dataGridCell.DataContext as WaterLevelSearchData;
                damCd = _row.DAMCD.ToString();
                obsCd = _row.OBSCD.ToString();
                obsDt = _row.OBSDT.ToString() + dataGridColumn.Header.ToString().Replace(":", "");
                trmdv = _row.TRMDV.ToString();

                //MessageBox.Show(damCd + " " + obsCd + " " + obsDt + " " + trmdv);

                if (trmdv.Equals("10"))
                {
                    DamDataHistoryDialog dialog = new DamDataHistoryDialog("WL", damCd, obsCd, obsDt, trmdv);
                    dialog.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    dialog.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    //dialog.Height = 700;
                    //dialog.Width = 980;
                    dialog.Show();
                }

            }
            e.Handled = false;

        }
        private void damGrid_CellDoubleClicked(object sender, Telerik.Windows.Controls.GridView.RowEventArgs e)
        {
            GridViewRowItem _rw = e.Row;

            WaterLevelSearchData _row = _rw.DataContext as WaterLevelSearchData;

            if (_row != null && _row.TRMDV != null && _row.TRMDV.Equals("10"))
            {
                //damTp = ((DamType)damTpCombo.SelectedItem).DAMTYPE;
                string damTp = "";
                string damCd = _row.DAMCD.ToString(); //_row["DAMCD"].ToString();
                string obsCd = _row.OBSCD.ToString();
                string obsDt = _row.OBSDT.ToString();


                WaterLevelVerify dialog = new WaterLevelVerify(_row);
                dialog.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                dialog.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                dialog.Height = 700;
                dialog.Width = 1140;
                dialog.Show();
            }
        }

        private void damGrid_LoadingRowDetails(object sender, RowLoadedEventArgs e)
        {
            string colorTp = ((KeyValue<string>)selectColorCombo.SelectedItem).Key;
            object _rowVal = e.Row.DataContext;
            if (_rowVal != null)
            {
                object _trmdv = CommonUtil.GetValue(_rowVal, "TRMDV");
                object _obsdt = CommonUtil.GetValue(_rowVal, "OBSDT");

                if (_trmdv != null)
                {
                    foreach(GridViewCell _cell in e.Row.Cells)
                    {
                        
                        //GridViewDataColumn _col = this.damGrid.Columns[i] as GridViewDataColumn;
                        GridViewDataColumn _col = _cell.Column as GridViewDataColumn;

                        //검정색상및 보정강조(bold) 설정
                        if (_col.DataMemberBinding != null && _col.DataMemberBinding.Path != null)
                        {

                            string _path = _col.DataMemberBinding.Path.Path;
                            object _edexlvl = null;
                            object _excolor = null;
                            if (_path.StartsWith("WL_"))
                            {
                                _edexlvl = CommonUtil.GetValue(_rowVal, _path.Replace("WL_", "EDEXLVL_"));
                                _excolor = CommonUtil.GetValue(_rowVal, _path.Replace("WL_", "EXCOLOR_"));
                            }
                            if (_edexlvl != null && _edexlvl.ToString().Length > 0)
                            {
                                _cell.FontWeight = FontWeights.Bold;
                            }
                            if (_excolor != null && _excolor.ToString().Length == 6 && colorTp.Equals("Y"))
                            {
                                _cell.Background = new SolidColorBrush(Constants.GetColorFromString(_excolor.ToString()));
                            }
                        }

                    }
                }
            }
        }
        #endregion

        #region == 조회버튼 ==
        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            ToggleButtonStatus(false);
            ToggleGridView();
            GetDamDataList();

            //댐명이 전체일 경우 그래프 레전드쪽 전체선택, 전체해제 버튼 비활성화.
            AllLegendSelectBtn.IsEnabled = false;
            AllLegendDeselectBtn.IsEnabled = false;

        }

        private void GetDamDataList()
        {
            ShowLoadingBar(LoadingType.SEARCH);

            damGrid.ItemsSource = null;
            CurDamDataList = new List<WaterLevelSearchData>();
            damDataChart.Axes.Clear();
            damDataChart.Series.Clear();


                    //client = new WebClient();
                    //client.OpenReadCompleted += new OpenReadCompletedEventHandler(GetDamDataListCompleted);
                    //client.OpenReadAsync(new Uri("/data/wl.json", UriKind.Relative));

                    this.GetDamDataListCompleted(null, null);// 그냥 랜덤으로 데이터 생성

                    //System.Threading.Thread thread = new System.Threading.Thread(() => GetDamDataListCompleted(null, null));
                    //thread.IsBackground = true;
                    //thread.Start();

        }

        private void ConvertChartData()
        {
            string dataTp = ((KeyValue<string>)dataTpCombo.SelectedItem).Key;
            string searchTp = ((KeyValue<string>)searchTpCombo.SelectedItem).Key;

            CurChartData.Clear();
            
            int addTime = 10;
            int timelen = 144;
            DateTime selDate = selectDtCal.SelectedDate.Value;
            DateTime firstTime = new DateTime(selDate.Year, selDate.Month, selDate.Day, 0, 0, 0);
            if (dataTp.Equals("30"))
            {
                addTime = 30;
                timelen = 48;
            }
            else if (dataTp.Equals("60"))
            {
                addTime = 60;
                timelen = 24;
            }

            for (var i = 0; i < timelen; i++)
            {
                DateTime _addTime = firstTime.AddMinutes((i + 1) * addTime);
                string cd = _addTime.ToString("HH:mm");
                string _obsdt = (dataTp.Equals("60")) ? _addTime.ToString("yyyyMMddHH") : _addTime.ToString("yyyyMMddHHmm");
                if (cd.Equals("00:00")) cd = "24:00";
                string idnm = searchTp + "_" + i;

                IDictionary _row = new Dictionary<string, string>();
                _row.Add("OBSDT", _obsdt);
                _row.Add("P_OBSDT", DateUtil.formatDate(_obsdt));

                foreach (WaterLevelSearchData _dc in CurDamDataList)
                {
                    //string _obscd = _dc["OBSCD"];
                    string _key = "OBSCD_" + _dc.OBSCD;
                    object value = CommonUtil.GetValue(_dc, idnm);

                    _row.Add(_key, value);

                    //if (_dc.ContainsKey(idnm))
                    //{
                    //    _row.Add(_key, _dc[idnm]);
                    //}
                    //else
                    //{
                    //    _row.Add(_key, null);
                    //}
                }
                CurChartData.Add(_row);
            }
        }


        readonly private Random random = new Random();
        private String RandomlyChange(ref Double d)
        {
            // 1000분의 1확률로 결측자료
            // 1000분의 1확률로 값이 0
            // 그외 100분의 1확률로 값이 변하고 그 중 2분의 1은 상승, 나머지는 하강
            int updown = random.Next(0, 1001);
            if (updown < 10)
            {
                return " "; //결측자료
            }
            else if (updown > 900 && updown <= 940)
            {
                d = d - 0.1;
                return "0";
            }
            else if (updown > 940 && updown <= 950)
            {
                d = d - 1;
                return "0";
            }
            else if (updown > 950 && updown <= 990)
            {
                d = d + 0.1;
                return "0.1";
            }
            else if (updown > 990)
            {
                d = d + 1;
                return "1";
            }
            return "0";
            //return d.ToString();
        }

        private void CreateData()
        {
            for (int i = 0; i < 1000; i++)
            {
                double wl = random.Next(2, 20);
                WaterLevelSearchData temp = new WaterLevelSearchData();
                temp.DAMNM = "DAM" + i;
                temp.DAMCD = i.ToString();
                temp.OBSCD = i.ToString();
                temp.OBSNM = "OBSERVER" + i;
                temp.OBSDT = selectDtCal.SelectedDate.Value.ToString("yyyyMMdd");
                temp.TRMDV = "10";

                for (int j = 0; j < 144; j++)
                {
                    CommonUtil.SetValue(temp, "FLW_" + j, this.RandomlyChange(ref wl));

                    if (" ".Equals(CommonUtil.GetValue(temp, "FLW_" + j).ToString()))
                    {
                        if (j % 2 == 1)
                        {
                            CommonUtil.SetValue(temp, "EDEXLVL_" + j, wl.ToString());
                            CommonUtil.SetValue(temp, "EXCOLOR_" + j, "00D600");
                            CommonUtil.SetValue(temp, "WL_" + j, wl.ToString());
                        }
                        else
                        {
                            // no input
                        }
                        
                    }
                    else if ( double.Parse( CommonUtil.GetValue(temp, "FLW_" + j).ToString() ) >= 0)
                    {
                        CommonUtil.SetValue(temp, "WL_" + j, wl.ToString());
                    }
                }

                temp.Initialize();
                CurDamDataList.Add(temp);
            }
        }

        private void GetDamDataListCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e == null)
            {   // 데이터 생성할 경우
                CreateData();
            }
            else
            {   // 데이터 받아올 경우
                if (e.Error != null) return;
                string dataTp = ((KeyValue<string>)dataTpCombo.SelectedItem).Key;
                string strJson = "";
                Stream stream = e.Result;
                using (StreamReader reader = new StreamReader(stream))
                {
                    strJson = reader.ReadToEnd();
                }

                CurDamDataList = JsonConvert.DeserializeObject<IList<WaterLevelSearchData>>(strJson);

                foreach (WaterLevelSearchData r in CurDamDataList)
                {
                    r.Initialize();
                }
            }

            System.Threading.Thread thread = new System.Threading.Thread(() =>
            {
                Dispatcher.BeginInvoke(() =>
                {
                    damGrid.ItemsSource = CurDamDataList;

                    ConvertChartData();
                    IList obj = (IList)CurChartData;
                    mmc = new MinMaxCalculator(obj);
                    SetupDamChart();

                    ToggleButtonStatus(true);
                    ToggleGridView();


                });
            });
            thread.IsBackground = true;
            thread.Start();
            
        }
        #endregion

        #region == 고장보고 ==
        private void cimsBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Reporting is not available.");
        }
        #endregion

        #region -- 엑셀 다운로드 --
        private void excelBtn_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.DefaultExt = "xls";
            saveDialog.Filter = "Excel Workbook (.xls)|*.xls";

            if (saveDialog.ShowDialog().Value)
            {
                ShowLoadingBar(LoadingType.EXCEL);

                Workbook _workbook = ExcelUtil.CreateWorkbook("수위자료");
                Worksheet _worksheet = _workbook.Worksheets[0];
                Stream _stream = null;
                WorksheetRow _worksheetRow;
                WorksheetCell _worksheetCell;

                try
                {
                    _stream = saveDialog.OpenFile();
                    
                    //Freeze Cell
                    _worksheet.DisplayOptions.PanesAreFrozen = true;
                    _worksheet.DisplayOptions.FrozenPaneSettings.FrozenRows = 1;
                    _worksheet.DisplayOptions.FrozenPaneSettings.FrozenColumns = 3;
                    //Header Row
                    for (int ix = 0; ix < this.damGrid.Columns.Count; ix++) {
                        _worksheetRow = _worksheet.Rows[0];
                        _worksheetCell = _worksheetRow.Cells[ix];
                        _worksheetRow.Cells[ix].Value = this.damGrid.Columns[ix].Header;
                        ExcelUtil.SetWorksheetCellFormat(_worksheetCell, RowType.HeaderRow);
                    }
                    //Data Row
                    object _rowItem;
                    object _trmdv;
                    int _fixIDx = 3;
                    IList<WaterLevelSearchData> _itemSource = ((IList<WaterLevelSearchData>)damGrid.ItemsSource);
                    for (int _rowIndex = 0; _rowIndex < _itemSource.Count; _rowIndex++)
                    {
                        _worksheetRow = _worksheet.Rows[_rowIndex + 1]; //Excel Next Row Number 1 start
                        _rowItem = _itemSource[_rowIndex];
                        _trmdv = CommonUtil.GetValue(_rowItem, "TRMDV");
                        if (_trmdv != null && _trmdv.ToString().Equals("1")) _fixIDx = 1;
                        else _fixIDx = 3;
                        for (int _colIdx = 0; _colIdx < this.damGrid.Columns.Count; _colIdx++)
                        {
                            GridViewDataColumn _column = damGrid.Columns[_colIdx] as GridViewDataColumn;
                            _worksheetCell = _worksheetRow.Cells[_colIdx];
                            _worksheetCell.Value = CommonUtil.GetValue(_rowItem, _column.DataMemberBinding.Path.Path); //_gridRow.Cells[_colIdx].Value;
                            if (_colIdx >= _fixIDx && _worksheetCell.Value != null)
                            {
                                double _val;
                                bool _res = Double.TryParse(_worksheetCell.Value.ToString(), out _val);
                                if (_res) _worksheetCell.Value = _val;
                            }
                            ExcelUtil.SetWorksheetCellFormat(_worksheetCell, RowType.DataRow);
                        }
                    }
                    _workbook.Save(_stream);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                } finally {
                    if(_stream!=null) _stream.Close();
                    HideLoadingBar(null,null);
                }
            }
           
        }

        #endregion

        #region -- 버튼 활성화 여부 체크 --
        private void ToggleButtonStatus(bool enable)
        {
            if (enable)
            {
                if (damGrid.ItemsSource != null && damGrid.Items.Count > 0)
                {
                    chartBtn.IsEnabled = true;
                    excelBtn.IsEnabled = true;
                }
                else
                {
                    chartBtn.IsEnabled = false;
                    excelBtn.IsEnabled = false;
                }

                string dataTp = ((KeyValue<string>)dataTpCombo.SelectedItem).Key;
                string searchTp = ((KeyValue<string>)searchTpCombo.SelectedItem).Key;

                saveBtn.IsEnabled = true;
            }
            else
            {
                chartBtn.IsEnabled = false;
                excelBtn.IsEnabled = false;
                saveBtn.IsEnabled = false;
            }
        }
        #endregion

        #region 붙여넣기 및 저장 기능 설정
        private void ToggleGridView()
        {
            //string damCd = ((DamCode)damCdCombo.SelectedItem).DAMCD;
            string dataTp = ((KeyValue<string>)dataTpCombo.SelectedItem).Key;
            string searchTp = ((KeyValue<string>)searchTpCombo.SelectedItem).Key;
            string colorTp = ((KeyValue<string>)selectColorCombo.SelectedItem).Key;

            damGrid.ClipboardPasteMode = GridViewClipboardPasteMode.Cells;

        }
        #endregion

        #region --- 기타 --

        private void ShowLoadingBar(LoadingType type)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    switch (type)
                    {
                        case LoadingType.EXCEL:
                            LoadingBar.BusyContent = "Save as excel...";
                            break;
                        default:
                            LoadingBar.BusyContent = "Creating data...";
                            break;
                    }
                    LoadingBar.IsBusy = true;
                }
            );
        }

        private void HideLoadingBar(object sender, EventArgs e)
        {
            //LoadingBar.IsBusy = false;

            LoadingBar.Visibility = Visibility.Collapsed;
        }
        // 사용자가 이 페이지를 탐색할 때 실행됩니다.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void legendBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(noti.ToString());
        }
        #endregion

        #region == 저장 ==
        private IList<WaterLevelData> getDirtyDatas() 
        {
            IList<WaterLevelData> list = new List<WaterLevelData>();

            IList<GridViewCellBase> cells = new List<GridViewCellBase>();
            
            foreach(object item in damGrid.Items) {
                foreach (GridViewCellBase cell in damGrid.GetRowForItem(item).Cells)
                {
                    if (cell.Style == Constants.DirtyCellStyle)
                    {
                        cells.Add(cell);
                    }
                }
                //IEnumerable<GridViewCellBase> temp = from cell in damGrid.GetRowForItem(item).Cells
                //                                  where cell.Background == Constants.DirtyColor
                //                                  select cell;
                //cells.Union(temp);
            }


            foreach (GridViewCellBase cell in cells)
            {

                GridViewRowItem _rw = cell.ParentRow;
                if (_rw != null && _rw.Item != null
                    && cell.Column.Header.ToString().Length == 5 && cell.Column.Header.ToString().Contains(':') == true)
                {

                    WaterLevelSearchData _row = _rw.Item as WaterLevelSearchData;

                    WaterLevelData data = new WaterLevelData();
                    data.OBSDT = _row.OBSDT.ToString() + cell.Column.Header.ToString().Replace(":", "");  //날짜 + 시간
                    data.OBSCD = _row.OBSCD.ToString();
                    data.TRMDV = _row.TRMDV.ToString();
                    data.DAMCD = _row.DAMCD.ToString(); //_row["DAMCD"].ToString();
                    data.WL = ((TextBlock)cell.Content).Text;

                    list.Add(data);
                }

            }

            return list;
        }
        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {

            DamDataSaveDialog diag = new DamDataSaveDialog(this);
            ////diag.Width = 500;
            ////diag.Height = 300;

            diag.Show();
        }
        public void saveData(string edExWay, string edExLvl, string cnRsn, string cnrsnText, string cndsText)
        {
            MessageBox.Show("Saving data is not available.");
        }
        #endregion 

        private void testBtn_Click(object sender, RoutedEventArgs e)
        {
            XamGridTest mp = new XamGridTest();

            this.Content = mp;
        }

    }
}
