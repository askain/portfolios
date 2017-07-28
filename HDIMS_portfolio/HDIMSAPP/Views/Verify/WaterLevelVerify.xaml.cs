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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using HDIMSAPP.Common;
using HDIMSAPP.Common.ConditionalFormatRule;
using HDIMSAPP.Common.Converter;
using HDIMSAPP.Models;
using HDIMSAPP.Models.Common;
using HDIMSAPP.Models.DamBoObsMng;
using HDIMSAPP.Models.Verify;
using HDIMSAPP.Utils;
using ImageTools;
using ImageTools.IO.Png;
using Infragistics;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Grids;
using Infragistics.Documents.Excel;

namespace HDIMSAPP.Views.Verify
{
    public partial class WaterLevelVerify : ChildWindow
    {
        #region == 전역 변수 ==
        #region == URI ==
        private readonly string WaterLevelDataUri = "/Verify/GetWaterLevelVerifyList/?";
        private readonly string WaterLevelAbnomDataUri = "/Statistics/GetWlModifiedDataList/?";
        private readonly string DamCodeUri = "/Common/DamCodeList/?"; //DamType, firstvalue, damcd, obscd
        private readonly string ObsCodeUri = "/Common/ObsCodeList/?"; //DamCode, string ObsTp, string firstvalue, string Tp)
        private readonly string DamTypeUri = "/Common/GetDamTypeList/?allyn=Y";
        private readonly string EdExLvlUri = "/Common/GetEdExLvlList/?edyn=Y&firstvalue=전체";
        private readonly string EmpUri = "/ManAuthMng/GetManList/?authCode=03&start=0&limit=10000";
        private readonly string WKUri = "/DamBoObsMng/GetWK/?allyn=Y";
        #endregion

        private WebClient client;
        EmpData empdata = new EmpData();
        private readonly string ObsTp = "WL";

        #region == 차트관련 전역 변수 ==
        private bool isChartView = true;
        private IList<WaterLevelData> CurDataList = new List<WaterLevelData>();
        private CategoryXAxis CurXAxis = new CategoryXAxis();
        private NumericYAxis CurYAxis = new NumericYAxis();
        private MinMaxCalculator mmc;
        #endregion

        #region == 검색용 콤보박스 ==
        //private string[] dateTpKeys = { "obsdt", "chdt", "vrdt" };
        //private string[] dateTpValues = { "측정일시", "확인일시", "보정일시" };
        private string[] dataTpKeys = { "10", "30", "60" };
        private string[] dataTpValues = { "10분", "30분", "60분" };
        private string[] ChartLegendKeys = { "WL", "EXVL", "FLW" };
        private string[] ChartLegendTitles = { "원시수위", "추정수위", "원시유량" };
        private DateTime currDate = DateTime.Now;
        private DateTime prevDate = DateTime.Now.AddDays(-1);
        #endregion

        #region == 동기화를 위한 변수 ==
        private bool firstLoad = true;
        private bool IsSearchTriggerOn = false;     // true 일 경우 검색을 버튼을 눌렀으니 검색조건 데이터바인딩이 끝나자마자 수위데이터를 로드한다.
        private bool IsSearchReady { get { if (damTpCombo.ItemsSource == null || damCdCombo.ItemsSource == null || obsCdCombo.ItemsSource == null || exEmpNoCombo.ItemsSource == null) { return false; } return true; } }   //검색조건 데이터가 전부 바인딩 되었는가
        private bool IsSaveReady
        {
            get
            {
                IEnumerable<Row> dirtyDatas = from row in damGrid.Rows
                                              where ((WaterLevelData)row.Data).IsDirty == true
                                              select row;
                if (dirtyDatas.Count() == 0) return false;
                return true;

            }
        }

        private WaterLevelSearchData Data;

        #endregion

        private enum LoadingType { SEARCH, EXCEL, SAVE };
        #endregion

        #region == 생성자 ==
        public WaterLevelVerify(WaterLevelSearchData Data)
        {
            this.Data = Data;
            InitializeComponent();
            InitSearchPanel();
        }
        #endregion

        #region == 초기화 ==
        #region == 초기화 ==
        private void InitSearchPanel()
        {
            GetWKList();
            GetDamTypeList();
            GetDamCodeList();
            GetObsCodeList(); //이벤트에 의해 관측국로드
            InitDataTpCombo();
            
            GetEdExLvlList();
            GetEmpList();
            InitSearchDate();
            
            CreateGridColumns();
            SetupDamChart();
            searchBtn_Click(null, null);
            updateBtn.IsEnabled = false;
            damGrid.SelectedCellsCollectionChanged += new EventHandler<SelectionCollectionChangedEventArgs<SelectedCellsCollection>>(damGrid_SelectedCellsCollectionChanged);

            TrySearch(); //검색시도
        }
        #endregion

        #region == 그리드 선택 변경 이벤트 ==
        private void damGrid_SelectedCellsCollectionChanged(object sender, SelectionCollectionChangedEventArgs<SelectedCellsCollection> e)
        {
            if (e.NewSelectedItems.Count > 0)
            {
                IList<Cell> _list = e.NewSelectedItems.OfType<Cell>().ToList<Cell>();
                int selCnt = 0;
                foreach (Cell _cell in _list)
                {
                    if (_cell.Column is TextColumn)
                    {
                        TextColumn _tc = _cell.Column as TextColumn;
                        if (!_tc.IsReadOnly) selCnt++;
                    }
                }
                if (selCnt > 1)
                    updateBtn.IsEnabled = true;
                else
                    updateBtn.IsEnabled = false;
            }
            else
            {
                updateBtn.IsEnabled = false;
            }
        }
        #endregion

        #region -- 수계타입 콤보박스 --
        private void GetWKList()
        {
            IList<DamBoModel> Data = new List<DamBoModel>();
            Data.Add(new DamBoModel() { WKNM = "All" });

            wkTpCombo.DisplayMemberPath = "WKNM";
            wkTpCombo.ItemsSource = Data;
            wkTpCombo.SelectedIndex = 0;
        }
        #endregion

        #region == 댐타입 콤보박스 ==
        private void GetDamTypeList()
        {
            IList<DamType> Data = new List<DamType>();
            Data.Add(new DamType() { DAMTPNM = "All" });

            damTpCombo.DisplayMemberPath = "DAMTPNM";
            damTpCombo.ItemsSource = Data;
            damTpCombo.SelectedIndex = 0;
        }
        #endregion

        #region == 댐코드 콤보박스 ==
        private void GetDamCodeList()
        {
            IList<DamCode> Data = new List<DamCode>();
            Data.Add(new DamCode() { DAMNM = this.Data.DAMNM, DAMCD = this.Data.DAMCD });

            damCdCombo.DisplayMemberPath = "DAMNM";
            damCdCombo.ItemsSource = Data;
            damCdCombo.SelectedIndex = 0;
        }
        #endregion

        #region == 관측국 콤보박스  ==
        private void GetObsCodeList()
        {
            IList<Code> Data = new List<Code>();
            Data.Add(new Code() { VALUE = this.Data.OBSNM });

            obsCdCombo.DisplayMemberPath = "VALUE";
            obsCdCombo.ItemsSource = Data;
            obsCdCombo.SelectedIndex = 0;
        }
        #endregion

        //#region == 일시구분 콤보박스 ==
        //private void InitDateTpCombo()
        //{
        //    dateTpCombo.DisplayMemberPath = "Value";
        //    dateTpCombo.ItemsSource = GetDateTpList();
        //    dateTpCombo.SelectedIndex = 0;
        //}
        //private IList<KeyValue<string>> GetDateTpList()
        //{
        //    IList<KeyValue<string>> dateTpOptions = new List<KeyValue<string>>();
        //    for (var i = 0; i < dateTpKeys.Length; i++)
        //    {
        //        KeyValue<string> item = new KeyValue<string>();
        //        item.Key = dateTpKeys[i];
        //        item.Value = dateTpValues[i];
        //        dateTpOptions.Add(item);
        //    }

        //    return dateTpOptions;
        //}

        //#endregion

        #region == 일시 콤보박스 ==
        private void InitSearchDate()
        {
            if (firstLoad == true)
            {
                DateTime selDate = DateTime.ParseExact(this.Data.OBSDT, "yyyyMMdd", null);
                DateTime selDateOfYesterday = selDate.AddDays(-1);

                startDtCal.SelectedDate = selDateOfYesterday;
                //startDtCal.DisplayDateEnd = selDate;
                endDtCal.SelectedDate = selDate;
                //endDtCal.DisplayDateStart = selDateOfYesterday;
            }
            else
            {
                startDtCal.SelectedDate = prevDate;
                //startDtCal.DisplayDateEnd = currDate;
                endDtCal.SelectedDate = currDate;
                //endDtCal.DisplayDateStart = prevDate;
            }

            startHrCombo.DisplayMemberPath = "Value";
            endHrCombo.DisplayMemberPath = "Value";
            startHrCombo.ItemsSource = Constants.GetHourList();
            endHrCombo.ItemsSource = Constants.GetHourList();
            startHrCombo.SelectedIndex = 0;
            endHrCombo.SelectedIndex = 24;
        }
        #endregion

        #region == 보정등급 콤보박스 ==
        private void GetEdExLvlList()
        {
            IList<Code> Data = new List<Code>();
            Data.Add(new Code() { VALUE = "All" });

            edExLvlCombo.DisplayMemberPath = "VALUE";
            edExLvlCombo.ItemsSource = Data;
            edExLvlCombo.SelectedIndex = 0;
        }
        #endregion

        #region == 사원 콤보박스 ==
        private void GetEmpList()
        {
            string uri = EmpUri;

            client = new WebClient();
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(GetEmpListCompleted);
            client.OpenReadAsync(new Uri(uri, UriKind.Relative));
        }
        void GetEmpListCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null) return;
            Stream str = e.Result;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JsonModel<Code>));
            JsonModel<Code> ret = (JsonModel<Code>)ser.ReadObject(str);
            IList<Code> temp = ret.Data;

            IList<Code> list = new List<Code>();
            list.Add(new Code());
            list[0].EMPNO = "";
            list[0].KORNAME = "전체";

            foreach (Code c in temp)
            {
                list.Add(c);
            }
            
            exEmpNoCombo.DisplayMemberPath = "KORNAME";
            exEmpNoCombo.ItemsSource = list;
            exEmpNoCombo.SelectedIndex = 0;

        }
        #endregion

        #region == 구분 콤보박스 ==
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
        #endregion

        #region == 그리드 ==

        private void CreateGridColumns()
        {
            damGrid.Columns.Clear();

            //ControlTemplate template = (ControlTemplate)XamlReader.Load("<ControlTemplate xmlns:ig=\"http://schemas.infragistics.com/xaml\" xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" TargetType=\"ig:EventSpan\"><Grid Margin=\"0,2,0,2\"><Rectangle Margin=\"0 " + currentMargin.ToString() + " 0 0\" RadiusX=\"5\" RadiusY=\"5\" Fill=\"{TemplateBinding Fill}\" Height=\"10\" VerticalAlignment=\"Top\" /></Grid></ControlTemplate>");

            //newStyle.Setters.Add(new Setter()
            //{
            //    Property = EventSpan.TemplateProperty,
            //    Value = template,
            //});

            string strDataTemplate = @"<DataTemplate xmlns=""http://schemas.microsoft.com/client/2007"">"
                    + @"<Border Background=""{Binding Path=EXCOLOR }"" Width=""70"" BorderThickness=""1"" BorderBrush=""#FFB5BCC7"" CornerRadius=""5"" >"
                    + @"<TextBlock Text=""{Binding Path=EXCD }"" HorizontalAlignment=""Stretch"" VerticalAlignment=""Stretch"" TextAlignment=""Center"" />"
                    + @"</Border>"
                    + @"</DataTemplate>";

            Style DefaultCellStyle = this.Resources["defaultCellStyle"] as Style;

            ObsDtConverter obsdtConv = new ObsDtConverter();
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

            damGrid.ConditionalFormattingSettings.AllowConditionalFormatting = true;

            #region 측정일시, 관측국명
            TextColumn tc = new TextColumn();
            tc.Key = "OBSDT";
            tc.HeaderText = "측정일시";
            //tc.TextWrapping = TextWrapping.Wrap;
            tc.Width = new ColumnWidth(120, false);
            tc.HeaderStyle = this.Resources["headerCellStyle"] as Style;
            tc.CellStyle = DefaultCellStyle;
            tc.HeaderTextHorizontalAlignment = HorizontalAlignment.Center;
            tc.HorizontalContentAlignment = HorizontalAlignment.Center;
            tc.IsReadOnly = true;
            tc.IsFixed = FixedState.Left;
            tc.ConditionalFormatCollection.Add(new DateTimeContionalFormatRule());
            tc.ValueConverter = obsdtConv;
            damGrid.Columns.Add(tc);

            //tc.TextBlockStyle = NoneEditableColumnStyle;

            tc = new TextColumn();
            tc.Key = "OBSNM";
            tc.HeaderText = "관측국명";
            //tc.TextWrapping = TextWrapping.Wrap;
            tc.Width = new ColumnWidth(120, false);
            tc.HeaderStyle = this.Resources["headerCellStyle"] as Style;
            tc.CellStyle = DefaultCellStyle;
            tc.HeaderTextHorizontalAlignment = HorizontalAlignment.Center;
            tc.HorizontalContentAlignment = HorizontalAlignment.Center;
            tc.IsReadOnly = true;
            tc.IsFixed = FixedState.Left;
            tc.ConditionalFormatCollection.Add(new DateTimeContionalFormatRule());
            damGrid.Columns.Add(tc);
            #endregion

            #region 원시, 추정 수위
            GroupColumn gc1 = new GroupColumn();
            gc1.Key = "ORI";
            gc1.HeaderText = "원시";
            gc1.HeaderStyle = this.Resources["headerCellStyle"] as Style;
            gc1.HeaderTextHorizontalAlignment = HorizontalAlignment.Center;
            damGrid.Columns.Add(gc1);

            tc = new TextColumn();
            tc.Key = "WL";
            tc.HeaderText = "수위";
            //tc.TextWrapping = TextWrapping.Wrap;
            tc.Width = new ColumnWidth(80, false);
            tc.HeaderStyle = this.Resources["headerCellStyle"] as Style;
            tc.HeaderTextHorizontalAlignment = HorizontalAlignment.Center;
            tc.HorizontalContentAlignment = HorizontalAlignment.Right;
            tc.ConditionalFormatCollection.Add(new EditableColumnConditionalFormatRule());
            tc.ConditionalFormatCollection.Add(new DateTimeContionalFormatRule());
            tc.ValueConverter = damDataConverter;
            tc.ValueConverterParameter = ddc;
            tc.IsReadOnly = false;
            gc1.Columns.Add(tc);

            tc = new TextColumn();
            tc.Key = "FLW";
            tc.HeaderText = "유량";
            //tc.TextWrapping = TextWrapping.Wrap;
            tc.Width = new ColumnWidth(80, false);
            tc.HeaderStyle = this.Resources["headerCellStyle"] as Style;
            tc.HeaderTextHorizontalAlignment = HorizontalAlignment.Center;
            tc.HorizontalContentAlignment = HorizontalAlignment.Right;
            tc.IsReadOnly = true;
            tc.ConditionalFormatCollection.Add(new DateTimeContionalFormatRule());
            gc1.Columns.Add(tc);

            tc = new TextColumn();
            tc.Key = "EDEXWAYCONT";
            tc.HeaderText = "보정방법";
            //tc.TextWrapping = TextWrapping.Wrap;
            tc.Width = new ColumnWidth(80, false);
            tc.HeaderStyle = this.Resources["headerCellStyle"] as Style;
            tc.HeaderTextHorizontalAlignment = HorizontalAlignment.Center;
            tc.HorizontalContentAlignment = HorizontalAlignment.Center;
            tc.IsReadOnly = true;
            tc.ConditionalFormatCollection.Add(new DateTimeContionalFormatRule());
            gc1.Columns.Add(tc);

            GroupColumn gc2 = new GroupColumn();
            gc2.Key = "EX";
            gc2.HeaderText = "추정";
            gc2.HeaderStyle = this.Resources["headerCellStyle"] as Style;
            gc2.HeaderTextHorizontalAlignment = HorizontalAlignment.Center;
            damGrid.Columns.Add(gc2);

            tc = new TextColumn();
            tc.Key = "EXVL";
            tc.HeaderText = "수위";
            //tc.TextWrapping = TextWrapping.Wrap;
            tc.Width = new ColumnWidth(80, false);
            tc.HeaderStyle = this.Resources["headerCellStyle"] as Style;
            tc.HeaderTextHorizontalAlignment = HorizontalAlignment.Center;
            tc.HorizontalContentAlignment = HorizontalAlignment.Right;
            tc.IsReadOnly = true;
            tc.ConditionalFormatCollection.Add(new DateTimeContionalFormatRule());
            tc.ValueConverter = damDataConverter;
            tc.ValueConverterParameter = ddc;
            gc2.Columns.Add(tc);

            TemplateColumn tes = new TemplateColumn();
            tes.Key = "EXCD";
            tes.HeaderText = "검정방법";
            tes.Width = new ColumnWidth(80, false);
            tes.HeaderStyle = this.Resources["headerCellStyle"] as Style;
            tes.HeaderTextHorizontalAlignment = HorizontalAlignment.Center;
            tes.HorizontalContentAlignment = HorizontalAlignment.Center;
            tes.IsReadOnly = true;
            DataTemplate dt = (DataTemplate)XamlReader.Load(strDataTemplate);
            tes.ItemTemplate = dt;
            tes.ConditionalFormatCollection.Add(new DateTimeContionalFormatRule());
            gc2.Columns.Add(tes);
            
            //tc = new TextColumn();
            //tc.Key = "EXCD";
            //tc.HeaderText = "검정방법";
            ////tc.TextWrapping = TextWrapping.Wrap;
            //tc.Width = new ColumnWidth(80, false);
            //tc.HeaderTextHorizontalAlignment = HorizontalAlignment.Center;
            //tc.HorizontalContentAlignment = HorizontalAlignment.Center;
            //tc.IsReadOnly = true;
            ////tc.ConditionalFormatCollection.Add();
            //tc.ConditionalFormatCollection.Add(new ExCodeConditionalFormatRule());
            //gc2.Columns.Add(tc);


            #endregion

            #region 보정자, 확인자, 일시
            tc = new TextColumn();
            tc.Key = "CGEMPNM";
            tc.HeaderText = "보정자";
            //tc.TextWrapping = TextWrapping.Wrap;
            tc.Width = new ColumnWidth(120, false);
            tc.HeaderStyle = this.Resources["headerCellStyle"] as Style;
            tc.HeaderTextHorizontalAlignment = HorizontalAlignment.Center;
            tc.HorizontalContentAlignment = HorizontalAlignment.Center;
            tc.IsReadOnly = true;
            tc.ConditionalFormatCollection.Add(new DateTimeContionalFormatRule());
            damGrid.Columns.Add(tc);

            tc = new TextColumn();
            tc.Key = "CGDT";
            tc.HeaderText = "보정일시";
            //tc.TextWrapping = TextWrapping.Wrap;
            tc.Width = new ColumnWidth(120, false);
            tc.HeaderStyle = this.Resources["headerCellStyle"] as Style;
            tc.HeaderTextHorizontalAlignment = HorizontalAlignment.Center;
            tc.HorizontalContentAlignment = HorizontalAlignment.Center;
            tc.IsReadOnly = true;
            tc.ConditionalFormatCollection.Add(new DateTimeContionalFormatRule());
            tc.ValueConverter = obsdtConv;
            damGrid.Columns.Add(tc);

            tc = new TextColumn();
            tc.Key = "CHKEMPNM";
            tc.HeaderText = "확인자";
            //tc.TextWrapping = TextWrapping.Wrap;
            tc.Width = new ColumnWidth(120, false);
            tc.HeaderStyle = this.Resources["headerCellStyle"] as Style;
            tc.HeaderTextHorizontalAlignment = HorizontalAlignment.Center;
            tc.HorizontalContentAlignment = HorizontalAlignment.Center;
            tc.IsReadOnly = true;
            tc.ConditionalFormatCollection.Add(new DateTimeContionalFormatRule());
            damGrid.Columns.Add(tc);

            tc = new TextColumn();
            tc.Key = "CHKDT";
            tc.HeaderText = "확인일시";
            //tc.TextWrapping = TextWrapping.Wrap;
            tc.Width = new ColumnWidth(120, false);
            tc.HeaderStyle = this.Resources["headerCellStyle"] as Style;
            tc.HeaderTextHorizontalAlignment = HorizontalAlignment.Center;
            tc.HorizontalContentAlignment = HorizontalAlignment.Center;
            tc.IsReadOnly = true;
            tc.ConditionalFormatCollection.Add(new DateTimeContionalFormatRule());
            tc.ValueConverter = obsdtConv;
            damGrid.Columns.Add(tc);
            #endregion

        }

        private void ToggleEditable()
        {
            
            Style ReadOnlyHeaderStyle = this.Resources["headerReadonlyCellStyle"] as Style;
            Style ReadOnlyCellStyle = this.Resources["readonlyCellStyle"] as Style;
            Style DefaultHeaderStyle = this.Resources["headerCellStyle"] as Style;
            Style DefaultCellStyle = this.Resources["defaultCellStyle"] as Style;
            
            ColumnBaseCollection cbc = damGrid.Columns;

            string dataTp;
            string damCd;
            try
            {
                dataTp = ((KeyValue<string>)dataTpCombo.SelectedItem).Key;
                damCd = ((DamCode)damCdCombo.SelectedItem).DAMCD;
            }
            catch (NullReferenceException ex)
            {
                return;
            }

            if ("10".Equals(dataTp) && showModifiedChk.IsChecked == false)
            {
                //수정불가
                //1.보정,취소버튼 비활성화
                //2.전부흰색
                //3.수정불가 처리
                //updateBtn.IsEnabled = false;
                cancelBtn.IsEnabled = false;

                ((TextColumn)((GroupColumn)cbc[2]).Columns[0]).IsReadOnly = false;   //수위
                ((TextColumn)((GroupColumn)cbc[2]).Columns[0]).CellStyle = DefaultCellStyle;    //수위

                ((TextColumn)((GroupColumn)cbc[2]).Columns[1]).HeaderStyle = DefaultHeaderStyle;    //유량
                ((TextColumn)((GroupColumn)cbc[2]).Columns[1]).CellStyle = DefaultCellStyle;    //유량

                ((TextColumn)((GroupColumn)cbc[2]).Columns[2]).HeaderStyle = DefaultHeaderStyle;    //보정방법
                ((TextColumn)((GroupColumn)cbc[2]).Columns[2]).CellStyle = DefaultCellStyle;    //보정방법

                ((GroupColumn)cbc[3]).HeaderStyle = this.Resources["headerCellStyle"] as Style; //추정

                ((TextColumn)((GroupColumn)cbc[3]).Columns[0]).HeaderStyle = DefaultHeaderStyle;    //수위(추정)
                ((TextColumn)((GroupColumn)cbc[3]).Columns[0]).CellStyle = DefaultCellStyle;    //수위(추정)

                ((TemplateColumn)((GroupColumn)cbc[3]).Columns[1]).HeaderStyle = DefaultHeaderStyle;    //검정방법
                ((TemplateColumn)((GroupColumn)cbc[3]).Columns[1]).CellStyle = DefaultCellStyle;    //검정방법

                ((TextColumn)cbc[4]).HeaderStyle = DefaultHeaderStyle;
                ((TextColumn)cbc[4]).CellStyle = DefaultCellStyle;
                ((TextColumn)cbc[5]).HeaderStyle = DefaultHeaderStyle;
                ((TextColumn)cbc[5]).CellStyle = DefaultCellStyle;
                ((TextColumn)cbc[6]).HeaderStyle = DefaultHeaderStyle;
                ((TextColumn)cbc[6]).CellStyle = DefaultCellStyle;
                ((TextColumn)cbc[7]).HeaderStyle = DefaultHeaderStyle;
                ((TextColumn)cbc[7]).CellStyle = DefaultCellStyle;

            }

        }

        #endregion

        #region == 일시 캘린더 ==
        //private void startDtCal_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    startDtCal.DisplayDateEnd = startDtCal.SelectedDate;
        //    endDtCal.DisplayDateStart = startDtCal.SelectedDate;
        //}

        //private void endDtCal_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    startDtCal.DisplayDateEnd = endDtCal.SelectedDate;
        //    endDtCal.DisplayDateStart = endDtCal.SelectedDate;
        //}
        #endregion

        #endregion

        #region == 조회 검색 ==
        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            IsSearchTriggerOn = true;
            TrySearch();
        }
        private void TrySearch() 
        {
            GetDamDataList();
            ToggleEditable();
        }

        private void GetDamDataList()
        {
            LoadingBar.IsBusy = true;
            damGrid.ItemsSource = null;
            SetupDamChart();

            CurDataList = new List<WaterLevelData>();
            WaterLevelData temp = null;
            for (int u = 143 ; u >= 0 ; u--) {

                temp = new WaterLevelData();
                String ObsTime;
                if (u == 143)
                {
                    ObsTime = this.Data.OBSDT + "2400";
                }
                else
                {
                    DateTime temp2 = DateTime.ParseExact(this.Data.OBSDT + "0000", "yyyyMMddhhmm", null);
                    temp2 = temp2.AddMinutes((u + 1) * 10);
                    ObsTime = temp2.ToString("yyyyMMddHHmm");
                }
                temp.OBSDT = ObsTime;
                temp.OBSNM = this.Data.OBSNM;
                //temp.ORI
                temp.WL = (String)CommonUtil.GetValue(this.Data, "WL_" + u);
                temp.FLW = (String)CommonUtil.GetValue(this.Data, "FLW_" + u);
                temp.EDEXWAYCONT = "";
                temp.EXCOLOR = (String)CommonUtil.GetValue(this.Data, "EXCOLOR_" + u);
                temp.EXVL = (String)CommonUtil.GetValue(this.Data, "EDEXLVL_" + u);
                if (temp.EXVL != null)
                {
                    temp.EXCD = "Auto-Input";
                    temp.CGDT = ObsTime;
                    temp.CGEMPNM = "Daemon";
                }
                
                CurDataList.Add(temp);
            }

            InitIsDirty();
            #region 차트 MIN & MAX 구하기
            IList obj = (IList)CurDataList;
            mmc = new MinMaxCalculator(obj);
            #endregion

            damGrid.ItemsSource = CurDataList;
            CurXAxis.ItemsSource = CurDataList;
 
            var i = 0;
            foreach (LineSeries ss in damDataChart.Series)
            {
                ss.XAxis = CurXAxis;
                ss.YAxis = CurYAxis;
                if (i == 0)
                {
                    ss.Visibility = Visibility.Visible;
                    ss.ItemsSource = CurDataList;
                }
                else
                {
                    ss.Visibility = Visibility.Collapsed;
                }

                i++;
            }

            firstLoad = false;
            IsSearchTriggerOn = false;
            toggleSavable(false.ToString());
            LoadingBar.IsBusy = false;
            
        }
        private void InitIsDirty()
        {
            foreach (WaterLevelData wl in CurDataList)
            {
                wl.IsDirty = false;
            }
        }
        #endregion

        #region == 이력조회 기능 ==
        private void damGrid_CellDoubleClicked(object sender, CellClickedEventArgs e)
        {
            Cell c = (Cell)e.Cell;

            if (c.Column.Key == "CGEMPNM" || c.Column.Key == "CGDT" || c.Column.Key == "CHKEMPNM" || c.Column.Key == "CHKDT")
            {
                WaterLevelData d = (WaterLevelData)c.Row.Data;

                DamDataHistoryDialog diag = new DamDataHistoryDialog("WL", null, d.OBSCD, d.OBSDT, d.TRMDV);
                diag.Show();
            }
        }
        #endregion
        
        #region == 차트 기능 ==
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

        
        private void SetupDamChart()
        {
            damDataChart.Axes.Clear();
            damDataChart.Series.Clear();

            CurXAxis = new CategoryXAxis();
            AxisLabelSettings xAxisLabelSettings = new AxisLabelSettings();
            CurXAxis.MajorStroke = new SolidColorBrush(Colors.Gray);
            CurXAxis.MajorStrokeThickness = 0.5;
            CurXAxis.Label = "{P_OBSDT}";
            CurXAxis.IsInverted = true;
            CurXAxis.LabelSettings = xAxisLabelSettings;

            CurYAxis = new NumericYAxis();
            CurYAxis.MajorStroke = new SolidColorBrush(Colors.Gray);
            CurYAxis.MajorStrokeThickness = 0.5;
            CurYAxis.Label = "{:F2}";

            damDataChart.Axes.Add(CurXAxis);
            damDataChart.Axes.Add(CurYAxis);

            //시리즈 추가
            string _key, _title;
            Brush _brush;
            for (var i = 0; i < ChartLegendKeys.Length; i++)
            {
                _key = ChartLegendKeys[i];
                _title = ChartLegendTitles[i];
                _brush = new SolidColorBrush(Constants.GetColorFromString(Constants.ChartColors[i]));
                LineSeries ss = CreateSeries(_key, _title, _brush);
                ss.Visibility = Visibility.Collapsed;
                damDataChart.Series.Add(ss);
            }
        }


        private LineSeries CreateSeries(string key, string text, Brush brush)
        {
            string name = key + "_SERIES";
            LineSeries ser = new LineSeries();
            ser.Name = name;
            ser.ValueMemberPath = key;
            ser.Title = text;
            ser.XAxis = CurXAxis;
            ser.YAxis = CurYAxis;
            ser.MarkerType = MarkerType.None;
            ser.Brush = brush;
            ser.MarkerBrush = brush;
            ser.Thickness = 1.5;
            ser.LegendItemTemplate = this.Resources["CheckBoxLegendItem"] as DataTemplate;
            ser.Tag = key; //멀티 시리즈일 경우 해당 컬럼값을 찾기 위해서
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
            resizeGridChart();
            toggleChartPanel();
        }

        private WaterLevelData GetDamDataFromChart(double x)
        {
            int totalCnt = CurDataList.Count - 1;
            int sIndex = 0;
            if (x > 0.5) sIndex = (int)Math.Ceiling(x * totalCnt);
            else sIndex = (int)Math.Floor(x * totalCnt);
            if (sIndex < 0) sIndex = 0;
            if (sIndex > totalCnt) sIndex = totalCnt;
            return CurDataList[sIndex];
        }

        private void damChartGrid_WindowRectChanged(object sender, Infragistics.RectChangedEventArgs e)
        {
            Rect rect = e.NewRect;
            double leftPos = rect.Left; //min =0
            double rightPos = rect.Right; //max = 1
            //좌측 인덱스와 우측 인덱스를 가져오기
            if (damGrid.ItemsSource != null && CurDataList != null)
            {
                int totalCnt = CurDataList.Count - 1;
                int startIndex = 0;
                int endIndex = totalCnt;
                startIndex = (int)Math.Floor(leftPos * totalCnt);
                endIndex = (int)Math.Ceiling(rightPos * totalCnt);
                startIndex = (startIndex < 0) ? 0 : startIndex;
                endIndex = (endIndex > totalCnt) ? totalCnt : endIndex;
                string startDt = CurDataList[totalCnt - startIndex].OBSDT; //역순이므로 전체갯수에서 해당위치 빼기
                string endDt = CurDataList[totalCnt - endIndex].OBSDT;
                damGrid.FilteringSettings.RowFiltersCollection.Clear();
                Column col = damGrid.Columns.DataColumns["OBSDT"];
                RowsFilter rf = new RowsFilter(typeof(DamData), col);
                //MessageBox.Show(startDt + ":" + endDt);
                rf.Conditions.Add(new ComparisonCondition() { Operator = ComparisonOperator.GreaterThanOrEqual, FilterValue = startDt });
                rf.Conditions.Add(new ComparisonCondition() { Operator = ComparisonOperator.LessThanOrEqual, FilterValue = endDt });
                damGrid.FilteringSettings.RowFiltersCollection.Add(rf);
                if (startIndex == 0 && endIndex == totalCnt)
                {
                    viewAllGridChart.IsEnabled = false;
                }
                else
                {
                    viewAllGridChart.IsEnabled = true;
                }

            }

        }

        private void LegendCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox _cb = e.OriginalSource as CheckBox;
            string _serName = _cb.Tag.ToString(); //Series 명
            LineSeries _series = damDataChart.Series[_serName] as LineSeries;
            _series.ItemsSource = CurDataList;

            #region 차트 MIN & MAX 구하기
            //MessageBox.Show(mmc.ToString());
            mmc.AddLegend(_series.ValueMemberPath);
            _series.YAxis.MaximumValue = mmc.Max + mmc.TopOffset;
            _series.YAxis.MinimumValue = mmc.Min - mmc.BottomOffset;
            DifferenceTextBlock.Text = "최대 최소차:" + mmc.DifferenceTandB + "EL.m";
            #endregion
        }

        private void LegendCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox _cb = e.OriginalSource as CheckBox;
            string _serName = _cb.Tag.ToString(); //Series 명
            LineSeries _series = damDataChart.Series[_serName] as LineSeries;
            _series.ItemsSource = null;

            #region 차트 MIN & MAX 구하기
            mmc.RemoveLegend(_series.ValueMemberPath);
            _series.YAxis.MaximumValue = mmc.Max + mmc.TopOffset;
            _series.YAxis.MinimumValue = mmc.Min - mmc.BottomOffset;
            DifferenceTextBlock.Text = "최대 최소차:" + mmc.DifferenceTandB + "EL.m";
            #endregion
        }

        #region 차트 MIN & MAX 구하기
        public void UpdateMinMaxChart(string _serName)
        {
            LineSeries _series = damDataChart.Series[_serName] as LineSeries;
            if (_series.Visibility == Visibility.Visible)
            {
                mmc.UpdateLegend();
                _series.YAxis.MaximumValue = mmc.Max + mmc.TopOffset;
                _series.YAxis.MinimumValue = mmc.Min - mmc.BottomOffset;
            }
        }
        #endregion
        #endregion

        #region == CIMS 고장보고서 ==
        private void cimsBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Reporting is not available.");
        }
        #endregion

        //--------------------------------------------------------------
        #region == 보정 기능 ==
        private void damGrid_CellExitedEditMode(object sender, CellExitedEditingEventArgs e)
        {
            toggleSavable(true.ToString());

            #region 차트 MIN & MAX 구하기
            string _serName = e.Cell.Column.Key + "_SERIES";
            UpdateMinMaxChart(_serName);
            #endregion
        }
        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            WaterLevelUpdateDialog diag = new WaterLevelUpdateDialog(this);
            if (diag.TargetDatas.Count() == 0)
            {
                MessageBox.Show("데이터를 선택해 주십시오.");
                return;
            }
            diag.VerticalAlignment = System.Windows.VerticalAlignment.Center;
            diag.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;            
            ////diag.Width = 900;
            ////diag.Height = 500;
            diag.Show();
        }
        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            DamDataSaveDialog diag = new DamDataSaveDialog(this);
            ////diag.Width = 500;
            ////diag.Height = 300;

            diag.Show();
        }

        private IList<WaterLevelData> getDirtyDatas()
        {
            IList<WaterLevelData> DirtyDatas = new List<WaterLevelData>();

            foreach (WaterLevelData d in CurDataList)
            {
                if (d.IsDirty == true)
                {
                    DirtyDatas.Add(d);
                }
            }
            return DirtyDatas;
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
        public void toggleSavable(string strFlag)
        {
            if (strFlag != null)
            {
                saveBtn.IsEnabled = bool.Parse(strFlag);
                return;
            }

            if (IsSaveReady == true)
            {
                saveBtn.IsEnabled = true;
            }
            else
            {
                saveBtn.IsEnabled = false;
            }
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
                WorksheetMergedCellsRegion _mergeRegion;
                try
                {
                    _stream = saveDialog.OpenFile();

                    //Freeze Cell
                    _worksheet.DisplayOptions.PanesAreFrozen = true;
                    _worksheet.DisplayOptions.FrozenPaneSettings.FrozenRows = 2;
                    _worksheet.DisplayOptions.FrozenPaneSettings.FrozenColumns = 2;
                    //Header Row
                    int _nextColIdx = 0;
                    for (int ix = 0; ix < this.damGrid.Columns.Count; ix++)
                    {
                        if (this.damGrid.Columns[ix] is GroupColumn)
                        {
                            GroupColumn _gc = this.damGrid.Columns[ix] as GroupColumn;
                            _mergeRegion = _worksheet.MergedCellsRegions.Add(0, _nextColIdx, 0, (_nextColIdx + _gc.Columns.Count - 1));
                            _mergeRegion.Value = _gc.HeaderText;
                            ExcelUtil.SetMergeRegionCellFormat(_mergeRegion, RowType.HeaderRow);

                            for (int jx = 0; jx < _gc.Columns.Count; jx++)
                            {
                                _worksheetRow = _worksheet.Rows[1];
                                _worksheetCell = _worksheetRow.Cells[_nextColIdx];
                                _worksheetCell.Value = _gc.Columns[jx].HeaderText;
                                ExcelUtil.SetWorksheetCellFormat(_worksheetCell, RowType.HeaderRow);
                                _worksheetCell.CellFormat.FillPatternForegroundColor = Colors.White;
                                _nextColIdx++;
                            }
                        }
                        else
                        {
                            Column _cc = this.damGrid.Columns[ix] as Column;
                            _mergeRegion = _worksheet.MergedCellsRegions.Add(0, _nextColIdx, 1, _nextColIdx);
                            _mergeRegion.Value = _cc.HeaderText;
                            ExcelUtil.SetMergeRegionCellFormat(_mergeRegion, RowType.HeaderRow);
                            _nextColIdx++;
                        }

                    }
                    //Data Row
                    foreach (Row _gridRow in this.damGrid.Rows)
                    {
                        _worksheetRow = _worksheet.Rows[_gridRow.Index + 2]; //Excel Next Row Number 1 start
                        int _rowColIdx = 0;
                        for (int _cc = 0; _cc < _gridRow.Cells.Count; _cc++)
                        {
                            if (_gridRow.Cells[_cc].Column is GroupColumn)
                            {
                                for (int _gg = 0; _gg < _gridRow.Cells[_cc].Column.AllColumns.Count; _gg++)
                                {
                                    _worksheetCell = _worksheetRow.Cells[_rowColIdx];
                                    _worksheetCell.Value = _gridRow.Cells[_gridRow.Cells[_cc].Column.AllColumns[_gg]].Value;
                                    if (_worksheetCell.Value != null && (_rowColIdx == 2 || _rowColIdx == 3 || _rowColIdx == 5 || _rowColIdx == 6))
                                    {
                                        double _val;
                                        bool _res = Double.TryParse(_worksheetCell.Value.ToString(), out _val);
                                        if (_res) _worksheetCell.Value = _val;
                                    }
                                    ExcelUtil.SetWorksheetCellFormat(_worksheetCell, RowType.DataRow);
                                    _rowColIdx++;
                                }
                            }
                            else
                            {
                                _worksheetCell = _worksheetRow.Cells[_rowColIdx];
                                _worksheetCell.Value = _gridRow.Cells[_cc].Value;
                                if (_worksheetCell.Value != null && (_rowColIdx == 2 || _rowColIdx == 3 || _rowColIdx == 5 || _rowColIdx == 6))
                                {
                                    double _val;
                                    bool _res = Double.TryParse(_worksheetCell.Value.ToString(), out _val);
                                    if (_res) _worksheetCell.Value = _val;
                                }
                                ExcelUtil.SetWorksheetCellFormat(_worksheetCell, RowType.DataRow);
                                _rowColIdx++;
                            }
                        }
                        //MessageBox.Show("row : " + _gridRow.Index + ", col : " + _rowColIdx);
                    }
                    _workbook.Save(_stream);
                }
                catch (Exception exc)
                {
                    MessageBox.Show(exc.Message);
                }
                finally
                {
                    if (_stream != null) _stream.Close();
                    HideLoadingBar();
                }
            }

        }
        #endregion

        #region == 클립보드 카피페 기능 ==
        private void XwGridClipboardPasting(object sender, ClipboardPastingEventArgs e)
        {
            XamGrid xamGrid = sender as XamGrid;

            if (xamGrid != null)
            {
                xamGrid.PasteData(e.Values);
                toggleSavable(null);
            }
        }

        private void XwGridClipboardCopying(object sender, ClipboardCopyingEventArgs e)
        {
            XamGrid xamGrid = sender as XamGrid;
            if (xamGrid != null && !xamGrid.IsSelectionValid())
            {
                MessageBox.Show("Only rectangular single band regions are allowed.");
                e.Cancel = true;
            }
        }
        #endregion


        #region --- 기타 --
        private void ShowLoadingBar(LoadingType type)
        {
            switch (type)
            {
                case LoadingType.EXCEL:
                    LoadingBar.BusyContent = "엑셀파일을 저장중입니다...";
                    break;
                case LoadingType.SAVE:
                    LoadingBar.BusyContent = "보정된 수위자료를 저장중입니다...";
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

        private void legendBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Legend is not available.");
        }
        #endregion

        



    }
}
