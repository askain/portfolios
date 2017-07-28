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

namespace HDIMSAPP.Views.Verify
{
    public partial class WaterLevelSearch : Page
    {
        //private string DamDataUri = "/Verify/GetDamDataVerifyList/?";
        private string DamCodeUri = "/Common/GetDamCodeList/?allyn=Y"; //DamType, firstvalue, damcd, obscd
        private string DamTypeUri = "/Common/GetDamTypeList/?allyn=Y";
        private string ObsCodeUri = "/Common/ObsCodeList/?ObsTp=WL";
        private string WKUri = "/DamBoObsMng/GetWK/?allyn=Y";
        private WebClient client;
        private string[] dataTpKeys = { "1", "10", "30", "60" };
        private string[] dataTpValues = { "1분", "10분", "30분", "60분" };
        private string[] searchTpKeys = { "WL", "FLW" };
        private string[] searchTpValues = { "수위", "유량" };
        private string[] selectColorKeys = { "N", "Y" };
        private string[] selectColorValues = { "없음", "표현" };
        private DateTime currDate = DateTime.Now;


        //현재 댐의 관측국 목록
        private IList<ObsCode> CurObsCodeList;

        private IDictionary<string, IDictionary<string, string>> HeaderColumns = new Dictionary<string, IDictionary<string, string>>();

        private IList<WaterLevelSearchData> CurDamDataList = new List<WaterLevelSearchData>();
        private IList<IDictionary<string, string>> CurDamDataList1M = new List<IDictionary<string, string>>();
        

        #region //챠트 관련 변수
        private bool isChartView = false;
        private CategoryXAxis CurXAxis = new CategoryXAxis();
        private NumericYAxis CurYAxis = new NumericYAxis();
        private IList<IDictionary> CurChartData = new List<IDictionary>();
        private IList<IDictionary> CurChartData1M = new List<IDictionary>();
        private MinMaxCalculator mmc;
        #endregion

        //준비
        private bool firstLoad = true;
        private EmpData empdata = new EmpData();
        private bool IsSearchReady { get { if (damTpCombo.ItemsSource == null || damCdCombo.ItemsSource == null) { return false; } return true; } }   //검색조건 데이터가 전부 바인딩 되었는가

        private enum LoadingType { SEARCH, EXCEL };

        public WaterLevelSearch()
        {
            InitializeComponent();
            InitSearchPanel();
            damGrid.CellDoubleClicked += new Controls.DoubleClickDataGrid.CellDoubleClickedHandler(damGrid_CellDoubleClicked);
            damGrid.LoadingRow += new EventHandler<DataGridRowEventArgs>(damGrid_LoadingRow);
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
            SetupButtonStatus(false);
            ToggleSaveEnable();
            damCdCombo.SelectionChanged += new EventHandler(damCdCombo_SelectionChanged);
            dataTpCombo.SelectionChanged += new EventHandler(dataTpCombo_SelectionChanged);
            searchTpCombo.SelectionChanged += new EventHandler(dataTpCombo_SelectionChanged);
            selectColorCombo.SelectionChanged += new EventHandler(selectColorCombo_SelectionChanged);
            damGrid.MouseRightButtonDown += new MouseButtonEventHandler(damGrid_MouseRightButtonDown);
            damGrid.MouseRightButtonUp += new MouseButtonEventHandler(damGrid_MouseRightButtonUp);
        }
        
        #endregion

        #region -- 수계타입 목록 읽어오기 --
        private void GetWKList()
        {
            string uri = WKUri;

            client = new WebClient();
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(GetWKListCompleted);
            client.OpenReadAsync(new Uri(uri, UriKind.Relative));
        }

        void GetWKListCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null) return;
            Stream str = e.Result;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(PostModel<DamBoModel>));
            PostModel<DamBoModel> postmodel = (PostModel<DamBoModel>)ser.ReadObject(str);

            WKCombo.DisplayMemberPath = "WKNM";
            WKCombo.ItemsSource = postmodel.Data;
            WKCombo.SelectedIndex = 0;

            WKCombo.SelectionChanged += new EventHandler(DamTpCombo_SelectionChanged);
        }
        #endregion
        
        #region -- 댐타입 콤보 --
        private void GetDamTypeList()
        {
            string uri = DamTypeUri;

            client = new WebClient();
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(GetDamTypeListCompleted);
            client.OpenReadAsync(new Uri(uri, UriKind.Relative));
        }

        void GetDamTypeListCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null) return;
            Stream str = e.Result;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IList<DamType>));
            IList<DamType> list = (IList<DamType>)ser.ReadObject(str);

            damTpCombo.DisplayMemberPath = "DAMTPNM";
            damTpCombo.ItemsSource = list;

            empdata.DamTpCombo = damTpCombo;
            if (firstLoad == true)
            {
                //MessageBox.Show("DAMtp:" + empdata.DEFAULT_DAMTYPE + " INDEX:" + empdata.INDEX_OF_DEFAULT_DAMTYPE);
                damTpCombo.SelectedIndex = empdata.INDEX_OF_DEFAULT_DAMTYPE;
                if (IsSearchReady == true) firstLoad = false;
            }
            else
            {
                damTpCombo.SelectedIndex = 0;
            }
            //첫번째 로딩이 끝난후에 댐코드 정보를 읽어오도록 한다.!! 속도
            damTpCombo.SelectionChanged += new EventHandler(DamTpCombo_SelectionChanged);
        }
        private void DamTpCombo_SelectionChanged(object sender, EventArgs e)
        {
            GetDamCodeList();
        }
        #endregion

        #region == 댐코드 콤보 ==
        private void GetDamCodeList()
        {
            string sDamTp = "";
            string sWkCd = "";
            if (firstLoad == true)
            {
                sDamTp = empdata.DEFAULT_DAMTYPE;
            }
            else
            {
                DamType oDamTp = (DamType)damTpCombo.SelectedItem;
                if (oDamTp != null) sDamTp = oDamTp.DAMTYPE;
                DamBoModel oDambo = (DamBoModel)WKCombo.SelectedItem;
                if (oDambo != null) sWkCd = oDambo.WKCD;
            }

            string uri = DamCodeUri;

            uri += "&damtp=" + sDamTp + "&wkcd=" + sWkCd;
            
            client = new WebClient();
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(GetDamCodeListCompleted);
            client.OpenReadAsync(new Uri(uri, UriKind.Relative));
        }
        void GetDamCodeListCompleted(object sender, OpenReadCompletedEventArgs e)
        {

            if (e.Error != null) return;
            Stream str = e.Result;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JsonModel<DamCode>));
            JsonModel<DamCode> ret = (JsonModel<DamCode>)ser.ReadObject(str);
            IList<DamCode> list = ret.Data;
            damCdCombo.DisplayMemberPath = "DAMNM";
            damCdCombo.SelectionChanged -= new EventHandler(damCdCombo_SelectionChanged);
            damCdCombo.ItemsSource = list;

            damCdCombo.SelectionChanged += new EventHandler(damCdCombo_SelectionChanged);
            empdata.DamCdCombo = damCdCombo;
            if (firstLoad == true)
            {
                //MessageBox.Show("DAMCD:" + empdata.DEFAULT_DAMCD + " INDEX:" + empdata.INDEX_OF_DEFAULT_DAMCD);

                damCdCombo.SelectedIndex = empdata.INDEX_OF_DEFAULT_DAMCD;
                if (IsSearchReady == true) firstLoad = false;
            }
            else
            {
                damCdCombo.SelectedIndex = 0;
            }
            //GetObsCodeList();
        }
        private void damCdCombo_SelectionChanged(object sender, EventArgs e)
        {
            CurObsCodeList = new List<ObsCode>();
            
            string dataTp = ((KeyValue<string>)dataTpCombo.SelectedItem).Key;
            if ("1".Equals(dataTp) == true)
            {
                GetObsCodeList();
            }
        }
        #endregion

        #region == 관측국코드 리스트 ==
        private void GetObsCodeList()
        {
            if (damCdCombo.SelectedItem == null)
            {
                damCdCombo.SelectedIndex = 0;
            }
            DamCode dd = (DamCode)damCdCombo.SelectedItem;
            
            string damCd = dd.DAMCD;
            
            if (damCd != null)
            {
                string uri = ObsCodeUri + "&DamCode=" + damCd;

                client = new WebClient();
                client.OpenReadCompleted += new OpenReadCompletedEventHandler(GetObsCodeListCompleted);
                client.OpenReadAsync(new Uri(uri, UriKind.Relative));
            }
        }
        void GetObsCodeListCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null) return;
            Stream str = e.Result;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JsonModel<ObsCode>));
            JsonModel<ObsCode> ret = (JsonModel<ObsCode>)ser.ReadObject(str);
            CurObsCodeList = ret.Data;
            string dataTp = ((KeyValue<string>)dataTpCombo.SelectedItem).Key;
            if (dataTp.Equals("1"))
            {
                CreateGridColumns();
            }
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
            dataTpCombo.SelectedIndex = 1;
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
            if(dataTp.Equals("1") == true) CurXAxis.ItemsSource = CurChartData1M.ToDataSource();
            else CurXAxis.ItemsSource = CurChartData.ToDataSource();

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
                        ss.ItemsSource = CurChartData1M.ToDataSource();
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

            if (CurDamDataList == null || CurDamDataList.Count == 0) //1분 데이터일 경우
            {
                _series.ItemsSource = CurChartData1M.ToDataSource();
            }
            else
            {
                _series.ItemsSource = CurChartData.ToDataSource();// CurChartData.ToDataSource();
            }
            

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

            Style HeaderCellStyle = this.Resources["DataGridColumnHeaderStyle"] as Style;
            Style DefaultStyle = this.Resources["DataGridColumnDefaultStyle"] as Style;
            Style DefaultCellStyle = this.Resources["DataGridColumnCellStyle"] as Style;
            Style HeaderReadOnlyCellStyle = this.Resources["DataGridColumnHeaderReadonlyStyle"] as Style;
            Style ReadOnlyCellStyle = this.Resources["DataGridColumnCellReadonlyStyle"] as Style;

            //string strDataTemplate = @"<DataTemplate xmlns=""http://schemas.microsoft.com/client/2007"">"
            //        + @"<Border Background=""{Binding Path=EXCOLOR }"" Width=""70"" BorderThickness=""1"" BorderBrush=""#FFB5BCC7"" CornerRadius=""5"" >"
            //        + @"<TextBlock Text=""{Binding Path=EXCD }"" HorizontalAlignment=""Stretch"" VerticalAlignment=""Stretch"" TextAlignment=""Center"" />"
            //        + @"</Border>"
            //        + @"</DataTemplate>";

            string strDataTemplate = @"<DataTemplate "
            + @"xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" "
            + @"xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"" "
            + @"xmlns:local=""clr-namespace:HDIMSAPP;assembly=HDIMSAPP"" "
            + @">"
                        + @"<Border Background=""{Binding {1} }"" Width=""{2}"" BorderThickness=""1"" BorderBrush=""#FFB5BCC7"" CornerRadius=""5"" >"
                        + @"<TextBlock Text=""{Binding {0} }"" HorizontalAlignment=""Stretch"" VerticalAlignment=""Stretch"" TextAlignment=""Center"" />"
                        + @"</Border>"
                        + @"</DataTemplate>";

            string dataTp = ((KeyValue<string>)dataTpCombo.SelectedItem).Key;
            string searchTp = ((KeyValue<string>)searchTpCombo.SelectedItem).Key;
            string colorTp = ((KeyValue<string>)selectColorCombo.SelectedItem).Key;

            if (dataTp.Equals("1"))
            {
                DataGridTextColumn _uc = new DataGridTextColumn()
                {
                    Binding = new Binding("P_OBSDT"),
                    Header = "측정일시",
                    HeaderStyle = HeaderCellStyle,
                    CellStyle = DefaultStyle,
                    Width = new DataGridLength(120)
                };
                damGrid.Columns.Add(_uc);
                if (CurObsCodeList != null)
                {
                    foreach (ObsCode dc in CurObsCodeList)
                    {
                        _uc = new DataGridTextColumn()
                        {
                            Binding = new Binding("OBSCD_" + dc.KEY),
                            Header = dc.VALUE,
                            HeaderStyle = HeaderCellStyle,
                            CellStyle = DefaultCellStyle,
                            MinWidth = 60
                        };
                        damGrid.Columns.Add(_uc);
                    }
                }
                damGrid.FrozenColumnCount = 1;
            }
            else
            {
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
                    DataGridTextColumn _tc = new DataGridTextColumn()
                    {
                        Binding = new Binding(keys[j]),
                        Header = names[j],
                        HeaderStyle = HeaderCellStyle,
                        CellStyle = DefaultStyle,
                        Width = new DataGridLength(widths[j])
                    };

                    damGrid.Columns.Add(_tc);
                }
                
                for (var i = 0; i < timelen; i++)
                {
                    string cd = firstTime.AddMinutes((i + 1) * addTime).ToString("HH:mm");
                    if (cd.Equals("00:00")) cd = "24:00";
                    string idnm = searchTp + "_" + i;
                    string selectedColor = idnm.Replace("WL", "CELL");
                    string width = "60";
                    Style _headStyle = HeaderCellStyle;
                    Style _cellStyle = DefaultCellStyle;
                    if ((dataTp.Equals("10") || dataTp.Equals("30")) && cd.Substring(3, 2).Equals("00"))
                    {
                        _headStyle = HeaderReadOnlyCellStyle;
                        _cellStyle = ReadOnlyCellStyle;
                    }
                    Binding b = new Binding(idnm);
                    b.Mode = BindingMode.TwoWay;    //수정도 가능하게 함??????????????
                    //DataGridColumn __uc = new DataGridColumn();
                    
                    DataGridTemplateColumn _uc = new DataGridTemplateColumn()
                    {
                        CellTemplate = (DataTemplate)System.Windows.Markup.XamlReader.Load(strDataTemplate.Replace("{0}", idnm).Replace("{1}", selectedColor).Replace("{2}", width)),
                        //CellEditingTemplate = (DataTemplate)System.Windows.Markup.XamlReader.Load(strDataTemplate.Replace("{0}", idnm).Replace("{1}", selected_i).Replace("{2}", "60")),
                        Header = cd,
                        HeaderStyle = _headStyle,
                        CellStyle = _cellStyle,
                        SortMemberPath = idnm,
                        Width = new DataGridLength(60)
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

            var rowQuery = from gridRow in elements
                           where gridRow
                               is DataGridRow
                           select gridRow as DataGridRow;
            DataGridRow dataGridRow = rowQuery.FirstOrDefault();
            if (dataGridRow == null)
            {
                return;
            }

            object dataGridObject = dataGridRow.DataContext;

            var cellQuery = from gridCell in elements
                            where gridCell is DataGridCell
                            select gridCell as DataGridCell;
            DataGridCell dataGridCell = cellQuery.FirstOrDefault();
            DataGridColumn dataGridColumn;

            if (dataGridCell != null)
            {
                dataGridCell.Focus();
                dataGridColumn = DataGridColumn.GetColumnContainingElement(dataGridCell);

                string damCd, obsCd, obsDt, trmdv;
                WaterLevelSearchData _row = CurDamDataList[dataGridRow.GetIndex()];
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
        private void damGrid_CellDoubleClicked(object sender, Controls.DataGridCellClickedArgs e)
        {
            DataGridRow _rw = e.DataGridRow;
            DataGridTextColumn _tc = e.DataGridColumn as DataGridTextColumn;
            int _index = _rw.GetIndex();
            if (_rw != null && _index >= 0 && CurDamDataList != null && CurDamDataList.Count > _index)
            {
                string damTp, damCd, obsCd, obsDt;
                WaterLevelSearchData _row = CurDamDataList[_index];
                if (_row.TRMDV != null && _row.TRMDV.Equals("10"))
                {
                    damCd = _row.DAMCD.ToString(); //_row["DAMCD"].ToString();
                    obsCd = _row.OBSCD.ToString();
                    obsDt = _row.OBSDT.ToString();

                    //damTp = ((DamType)damTpCombo.SelectedItem).DAMTYPE;
                    damTp = "";
                    WaterLevelVerify dialog = new WaterLevelVerify(_row);
                    dialog.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    dialog.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    dialog.Height = 700;
                    dialog.Width = 1140;
                    dialog.Show();
                }
            }
        }

        private void damGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            //string colorTp = ((KeyValue<string>)selectColorCombo.SelectedItem).Key;
            //object _rowVal = e.Row.DataContext;
            //if (_rowVal != null)
            //{

            //    object _trmdv = CommonUtil.GetValue(_rowVal, "TRMDV");
            //    object _obsdt = CommonUtil.GetValue(_rowVal, "OBSDT");


            //    if (_trmdv != null)
            //    {
            //        for (int i = 0; i < damGrid.Columns.Count; i++)
            //        {
            //            DataGridTemplateColumn _col = this.damGrid.Columns[i] as DataGridTemplateColumn;
            //            FrameworkElement fe = _col.GetCellContent(e.Row);
            //            FrameworkElement result = SilverlightUtil.GetParent(fe, typeof(DataGridCell));
            //            if (result != null)
            //            {
            //                DataGridCell _cell = (DataGridCell)result;
            //                //1분데이터일 경우 날짜별 background 설정
            //                if (_trmdv != null && _obsdt != null && _trmdv.ToString().Equals("1"))
            //                {
            //                    _cell.Background = CommonUtil.GetDateTimeColorBrush(_trmdv.ToString(), _obsdt.ToString());
            //                }
            //                //검정색상및 보정강조(bold) 설정
            //                if (_col.Binding != null && _col.Binding.Path != null)
            //                {

            //                    string _path = _col.Binding.Path.Path;
            //                    object _edexlvl = null;
            //                    object _excolor = null;
            //                    if (_path.StartsWith("WL_"))
            //                    {
            //                        _edexlvl = CommonUtil.GetValue(_rowVal, _path.Replace("WL_", "EDEXLVL_"));
            //                        _excolor = CommonUtil.GetValue(_rowVal, _path.Replace("WL_", "EXCOLOR_"));
            //                    }
            //                    if (_edexlvl != null && _edexlvl.ToString().Length > 0)
            //                    {
            //                        _cell.FontWeight = FontWeights.Bold;
            //                    }
            //                    if (_excolor != null && _excolor.ToString().Length == 6 && colorTp.Equals("Y"))
            //                    {
            //                        _cell.Background = new SolidColorBrush(Constants.GetColorFromString(_excolor.ToString()));
            //                    }
            //                }

            //            }
            //        }
            //    }
            //}
        }
        #endregion

        #region == 조회버튼 ==
        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            string damCd = ((DamCode)damCdCombo.SelectedItem).DAMCD;
            string dataTp = ((KeyValue<string>)dataTpCombo.SelectedItem).Key;
            if (dataTp.Equals("1") && damCd.Length < 7)
            {
                MessageBox.Show("1분자료는 조회하실 댐을 선택하셔야 합니다.");
                return;
            }
            if (dataTp.Equals("1") && (CurObsCodeList==null || CurObsCodeList.Count==0))
            {
                MessageBox.Show("선택하신 댐의 관측국이 존재하지 않습니다.");
                return;
            }
            SetupButtonStatus(false);
            ToggleSaveEnable();
            GetDamDataList();

            //댐명이 전체일 경우 그래프 레전드쪽 전체선택, 전체해제 버튼 비활성화.
            if (damCd == "")
            {
                AllLegendSelectBtn.IsEnabled = false;
                AllLegendDeselectBtn.IsEnabled = false;
            }
            else
            {
                AllLegendSelectBtn.IsEnabled = true;
                AllLegendDeselectBtn.IsEnabled = true;
            }
        }
        private string GetUrlParam()
        {
            string uri = "/DataSearch/GetWaterlevelVerifySearchList/?";

            DamType selDamTp = (DamType)damTpCombo.SelectedItem;
            DamCode selDamCd = (DamCode)damCdCombo.SelectedItem;
            DateTime selStartDt = selectDtCal.SelectedDate.Value;
            KeyValue<string> selDataTp = (KeyValue<string>)dataTpCombo.SelectedItem;
            KeyValue<string> selSearchTp = (KeyValue<string>)searchTpCombo.SelectedItem;

            string damTp = selDamTp.DAMTYPE;
            string damCd = selDamCd.DAMCD;
            string selectDt = selStartDt.ToString("yyyyMMdd");
            string dataTp = selDataTp.Key;
            string searchTp = selSearchTp.Key;
            string wkcd = "";

            DamBoModel oDambo = (DamBoModel)WKCombo.SelectedItem;
            if (oDambo != null) wkcd = oDambo.WKCD;

            uri += "damType=" + damTp + "&damCd=" + damCd + "&selectDay=" + selectDt + "&dataTp=" + dataTp + "&SearchType=" + searchTp + "&Wkcd=" + wkcd;

            return uri;
        }

        private void GetDamDataList()
        {
            ShowLoadingBar(LoadingType.SEARCH);
            GetObsCodeList();

            damGrid.ItemsSource = null;
            CurDamDataList = new List<WaterLevelSearchData>();
            CurDamDataList1M = new List<IDictionary<string, string>>();
            damDataChart.Axes.Clear();
            damDataChart.Series.Clear();

            string url = GetUrlParam();
            client = new WebClient();
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(GetDamDataListCompleted);
            client.OpenReadAsync(new Uri(GetUrlParam(), UriKind.Relative));
        }

        private void ConvertChartData()
        {
            string dataTp = ((KeyValue<string>)dataTpCombo.SelectedItem).Key;
            string searchTp = ((KeyValue<string>)searchTpCombo.SelectedItem).Key;

            CurChartData.Clear();
            CurChartData1M.Clear();

            if (dataTp.Equals("1"))
            {
                for (var i = CurDamDataList1M.Count - 1; i >= 0; i--)
                {
                    IDictionary<string, string> _dc = CurDamDataList1M[i];
                    IDictionary _row = new Dictionary<string, string>();
                    _row.Add("OBSDT", _dc["OBSDT"]);
                    _row.Add("P_OBSDT", DateUtil.formatDate(_dc["OBSDT"]));
                    foreach (ObsCode _cdc in CurObsCodeList)
                    {
                        string _key = "OBSCD_" + _cdc.KEY;

                        if (_dc.ContainsKey(_key))
                        {
                            _row.Add(_key, _dc[_key]);
                        }
                        else
                        {
                            _row.Add(_key, null);
                        }
                    }
                    CurChartData1M.Add(_row);
                }
            }
            else
            {
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
        }

        private void GetDamDataListCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null) return;
            string dataTp = ((KeyValue<string>)dataTpCombo.SelectedItem).Key;
            string strJson = "";
            Stream stream = e.Result;
            using (StreamReader reader = new StreamReader(stream))
            {
                strJson = reader.ReadToEnd();
            }

            if (dataTp.Equals("1") == true)
            {
                CurDamDataList1M = JsonConvert.DeserializeObject<IList<IDictionary<string,string>>>(strJson);

                IList<IDictionary> _tmp = new List<IDictionary>();
                foreach (Dictionary<string, string> _dc in CurDamDataList1M)
                {
                    IDictionary _item = _dc;
                    _item.Add("P_OBSDT", DateUtil.formatDate(_dc["OBSDT"]));
                    _item.Add("TRMDV", dataTp);
                    _tmp.Add(_item);
                }
                damGrid.ItemsSource = _tmp.ToDataSource();

                ConvertChartData();
                IList obj = (IList)CurChartData1M;
                mmc = new MinMaxCalculator(obj);
                SetupDamChart();
            }
            else
            {
                CurDamDataList = JsonConvert.DeserializeObject<IList<WaterLevelSearchData>>(strJson);
                damGrid.ItemsSource = CurDamDataList;

                ConvertChartData();
                IList obj = (IList)CurDamDataList;
                mmc = new MinMaxCalculator(obj);
                SetupDamChart();
            }
            

            SetupButtonStatus(true);
            ToggleSaveEnable();
            HideLoadingBar();
        }
        #endregion

        #region == 고장보고 ==
        private void cimsBtn_Click(object sender, RoutedEventArgs e)
        {

            IList<WaterLevelSearchData> _itemSource = damGrid.ItemsSource.OfType<WaterLevelSearchData>().ToList<WaterLevelSearchData>();
            int totalcnt = 0;
            for (int i = 0; i < 144; i++)
            {
                string columnName = string.Format("CELL_{0}_SELECTEDCOLOR", i);
                object value = CommonUtil.GetValue(_itemSource[0], columnName);
                if (value != null) totalcnt++;
            }

            MessageBox.Show(totalcnt.ToString());



            ////damcd, startDt
            //DamCode damCdCmb = (DamCode)damCdCombo.SelectedItem;
            //DateTime selStartDt = selectDtCal.SelectedDate.Value;

            //string damCd = damCdCmb.DAMCD;
            //string startDt = selStartDt.ToString("yyyyMMdd");
            //HtmlPage.Window.Invoke("linkToCims", damCd, startDt);
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
                    IList<object> _itemSource = damGrid.ItemsSource.OfType<object>().ToList<object>();
                    for (int _rowIndex = 0; _rowIndex < _itemSource.Count; _rowIndex++)
                    {
                        _worksheetRow = _worksheet.Rows[_rowIndex + 1]; //Excel Next Row Number 1 start
                        _rowItem = _itemSource[_rowIndex];
                        _trmdv = CommonUtil.GetValue(_rowItem, "TRMDV");
                        if (_trmdv != null && _trmdv.ToString().Equals("1")) _fixIDx = 1;
                        else _fixIDx = 3;
                        for (int _colIdx = 0; _colIdx < this.damGrid.Columns.Count; _colIdx++)
                        {
                            DataGridTextColumn _column = damGrid.Columns[_colIdx] as DataGridTextColumn;
                            _worksheetCell = _worksheetRow.Cells[_colIdx];
                            _worksheetCell.Value = CommonUtil.GetValue(_rowItem, _column.Binding.Path.Path); //_gridRow.Cells[_colIdx].Value;
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
                    HideLoadingBar();
                }
            }
           
        }

        #endregion

        #region -- 버튼 활성화 여부 체크 --
        private void SetupButtonStatus(bool enable)
        {
            if (enable)
            {
                if (damGrid.ItemsSource != null && damGrid.ItemsSource.OfType<object>().Count() > 0)
                {
                    chartBtn.IsEnabled = true;
                    excelBtn.IsEnabled = true;
                }
                else
                {
                    chartBtn.IsEnabled = false;
                    excelBtn.IsEnabled = false;
                }
            }
            else
            {
                chartBtn.IsEnabled = false;
                excelBtn.IsEnabled = false;
            }
        }

        #region 붙여넣기 및 저장 기능 설정
        private void ToggleSaveEnable()
        {
            string dataTp = ((KeyValue<string>)dataTpCombo.SelectedItem).Key;
            string searchTp = ((KeyValue<string>)searchTpCombo.SelectedItem).Key;
            string colorTp = ((KeyValue<string>)selectColorCombo.SelectedItem).Key;

            if(damGrid.ItemsSource != null && dataTp.Equals("10") == true && searchTp.Equals("WL") == true) 
            {
                damGrid.IsPasteEnable = true;
                saveBtn.IsEnabled = true;
            }
            else 
            {
                damGrid.IsPasteEnable = false;
                saveBtn.IsEnabled = false;
            }

        }
        #endregion
        #endregion

        #region --- 기타 --

        private void ShowLoadingBar(LoadingType type)
        {
            switch (type)
            {
                case LoadingType.EXCEL:
                    LoadingBar.BusyContent = "엑셀파일을 저장중입니다...";
                    break;
                default:
                    LoadingBar.BusyContent = "수위자료를 조회중입니다...";
                    break;
            }
            LoadingBar.IsBusy = true;
        }

        private void HideLoadingBar()
        {
            LoadingBar.IsBusy = false;
        }
        // 사용자가 이 페이지를 탐색할 때 실행됩니다.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private void legendBtn_Click(object sender, RoutedEventArgs e)
        {
            HtmlPage.Window.Invoke("showLegend", "W");
        }
        #endregion

        #region == 저장 ==
        private IList<WaterLevelData> getDirtyDatas() 
        {
            IList<WaterLevelData> list = new List<WaterLevelData>();

            IEnumerable<DataGridCell> cells = damGrid.getDirtyCells();

            foreach (DataGridCell cell in cells)
            {
                DataGridRow _rw = DataGridRow.GetRowContainingElement(cell);
                DataGridTextColumn _tc = DataGridColumn.GetColumnContainingElement(cell) as DataGridTextColumn;
                int _index = _rw.GetIndex();
                if (_rw != null && _index >= 0 && CurDamDataList != null && CurDamDataList.Count > _index
                    && _tc.Header.ToString().Length == 5 && _tc.Header.ToString().Contains(':') == true)
                {
                    
                    WaterLevelSearchData _row = CurDamDataList[_index];

                    WaterLevelData data = new WaterLevelData();
                    data.OBSDT = _row.OBSDT.ToString() + _tc.Header.ToString().Replace(":","");  //날짜 + 시간
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
            string uri = "/Verify/GetWaterLevelVerifyList";
            //MessageBox.Show("Save triggered");
            //EmpData e = new EmpData(); 

            //보정자, 보정일시 CGEMPNO, CGEMPNM, CGDT
            string cgempno = empdata.EMPNO;
            string cgempnm = empdata.EMPNM;
            string cgdt = DateTime.Now.ToString("yyyyMMddHHmmss");

            //MessageBox.Show(cgdt);

            IList<WaterLevelData> DirtyDatas = getDirtyDatas();

            if (DirtyDatas.Count == 0)
            {
                MessageBox.Show("변경된 데이터가 없습니다.");
                return;
            }

            //MessageBox.Show(" 변경자! : " + cgempnm);

            // 사유, 변경자 등을 적용.
            foreach (WaterLevelData d in DirtyDatas)
            {
                d.EDEXWAY = edExWay;
                d.EDEXLVL = edExLvl;
                d.EXRSN = cnRsn;     //증상
                d.CNRSN = cnrsnText; //사유는 넣지 않음.
                d.CNDS = cndsText;
                d.CGDT = cgdt;
                d.CGEMPNM = cgempnm;
                d.CGEMPNO = cgempno;

                //MessageBox.Show(" 변경된 행! : " + d.P_OBSDT);
            }

            ////////////////////////////////////////////////////////////////////////
            ///////////////// 144일 단위로 나누어서 서버에 보내긔
            //            int PostCnt = 144;
            //            WaterLevelData[] data144 = DirtyDatas.ToArray<WaterLevelData>();
            ////////////////////////////////////////////////////////////////////////

            PostModel<WaterLevelData> p = new PostModel<WaterLevelData>();
            p.Data = DirtyDatas;

            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(PostModel<WaterLevelData>));

            //JavaScriptSerializer serializer = new JavaScriptSerializer(); 
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, p);

            string josnserdata = System.Text.Encoding.UTF8.GetString(ms.ToArray(), 0, (int)ms.Length);
            client = new WebClient();
            client.UploadStringCompleted += new UploadStringCompletedEventHandler(VerifyDamDataCompleted);
            client.Headers["Content-type"] = "application/json";
            client.Encoding = System.Text.Encoding.UTF8;
            client.UploadStringAsync(new Uri(uri, UriKind.Relative), "POST", josnserdata);


            //MessageBox.Show(josnserdata);

            LoadingBar.BusyContent = "수위자료 저장중...";
            LoadingBar.IsBusy = true;

        }
        private void VerifyDamDataCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            if (e.Error != null) return;

            //MessageBox.Show("보정에 성공하였습니다.");

            LoadingBar.IsBusy = false;

            GetDamDataList();
        }
        #endregion 



    }
}
