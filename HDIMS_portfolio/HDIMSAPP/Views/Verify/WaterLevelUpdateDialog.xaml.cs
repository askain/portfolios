using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using HDIMSAPP.Common.Converter;
using HDIMSAPP.Models;
using HDIMSAPP.Models.Verify;
using HDIMSAPP.Utils;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Grids;

namespace HDIMSAPP.Views.Verify
{
    public partial class WaterLevelUpdateDialog : ChildWindow
    {
        private readonly string linearUri = "/Verify/GetWLLinearInterpolationResult/?";

        private WebClient client;
        private DateTime currDate = DateTime.Now;
        private DateTime prevDate = DateTime.Now.AddDays(-1);

        private string[] columnKeys = { "OBSDT", "WL", "EXVL", "EXVL2" };
        private string[] columnNames = {"측정일시","변경전수위","변경후선형","변경후지수함수곡선" };
        private double[] columnWidths = {120,100,100,100,100};

        private string[] applyTargetOptionKeys = { "MANUAL", "EXVL", "EXVL2" };
        private string[] applyTargetOptionValues = { "직접수정", "선형보간적용", "지수함수곡선적용" };
        private WaterLevelVerify parent { get; set; }
        private string TargetColumnName = "WL";
        private IList<WaterLevelDataForVerifying> CurrentData;
        private IEnumerable<WaterLevelData> _TargetDatas;
        public IEnumerable<WaterLevelData> TargetDatas
        {
            get
            {
                if (_TargetDatas == null )
                {
                    _TargetDatas = from cell in parent.damGrid.SelectionSettings.SelectedCells
                                   orderby ((WaterLevelData)cell.Row.Data).OBSDT descending
                                   where TargetColumnName.Equals(cell.Column.Key)
                                   select (WaterLevelData)cell.Row.Data;
                }

                return _TargetDatas;
            }
        }
        private IEnumerable<WaterLevelDataForVerifying> _GraphDatas;
        public IEnumerable<WaterLevelDataForVerifying> GraphDatas
        {
            get
            {
                if (_GraphDatas == null)
                {
                    WaterLevelDataForVerifying[] a = new WaterLevelDataForVerifying[CurrentData.Count];
                    CurrentData.CopyTo(a, 0);

                    IList <WaterLevelDataForVerifying> b = a.ToList();

                    String targetObsdt1 = DateUtil.AddDateTime(b.Last().OBSDT, -10, "mm"); //10분전
                    String targetObsdt2 = DateUtil.AddDateTime(b.Last().OBSDT, -20, "mm"); //20분전

                    if (targetObsdt1.EndsWith("2400"))  // 10분전이 00시00분인경우
                    {
                        // 10분전
                        WaterLevelDataForVerifying temp = new WaterLevelDataForVerifying();
                        temp.OBSDT = targetObsdt1;
                        temp.WL = b.Last().WL != " " ? b.Last().WL : b.Average((x) => { return double.Parse(x.WL); }).ToString();
                        b.Insert(b.Count, temp);

                        //20분전
                        temp = new WaterLevelDataForVerifying();
                        temp.OBSDT = targetObsdt2;
                        temp.WL = b.Last().WL;
                        b.Insert(b.Count, temp);
                    }
                    else if (targetObsdt2.EndsWith("2400"))
                    {
                        // 10분전
                        IEnumerable<WaterLevelData> prev = from row in parent.damGrid.Rows
                                                           where ((WaterLevelData)row.Data).OBSDT == targetObsdt1
                                                           select (WaterLevelData)row.Data;
                        WaterLevelDataForVerifying temp = new WaterLevelDataForVerifying();
                        temp.OBSDT = prev.First().OBSDT;
                        temp.WL = prev.First().WL;
                        b.Insert(b.Count, temp);

                        //20분전
                        temp = new WaterLevelDataForVerifying();
                        temp.OBSDT = targetObsdt2;
                        temp.WL = b.Last().WL;
                        b.Insert(b.Count, temp);

                    }
                    else
                    {
                        IEnumerable<WaterLevelData> prev = from row in parent.damGrid.Rows
                                                           orderby ((WaterLevelData)row.Data).OBSDT descending
                                                           where ((WaterLevelData)row.Data).OBSDT == targetObsdt1 || ((WaterLevelData)row.Data).OBSDT == targetObsdt2
                                                           select (WaterLevelData)row.Data;

                        foreach (WaterLevelData p in prev)
                        {
                            WaterLevelDataForVerifying temp = new WaterLevelDataForVerifying();
                            temp.OBSDT = p.OBSDT;
                            temp.WL = p.WL;
                            b.Insert(b.Count, temp);
                        }
                    }

                    String targetObsdt3 = DateUtil.AddDateTimeHH24(DateUtil.convToHH24(b.First().OBSDT), 10, "mm"); //10분후
                    String targetObsdt4 = DateUtil.AddDateTimeHH24(DateUtil.convToHH24(b.First().OBSDT), 20, "mm"); //20분후

                    if (targetObsdt3.EndsWith("0010"))
                    {
                        // 10분전
                        WaterLevelDataForVerifying temp = new WaterLevelDataForVerifying();
                        temp.OBSDT = targetObsdt3;
                        temp.WL = b.First().WL != " " ? b.First().WL : b.Average((x) => { return double.Parse(x.WL); }).ToString();
                        b.Insert(0, temp);

                        //20분전
                        temp = new WaterLevelDataForVerifying();
                        temp.OBSDT = targetObsdt4;
                        temp.WL = b.First().WL;
                        b.Insert(0, temp);

                    }
                    else if (targetObsdt4.EndsWith("0010"))
                    {
                        // 10분전
                        IEnumerable<WaterLevelData> next = from row in parent.damGrid.Rows
                                                           where ((WaterLevelData)row.Data).OBSDT == targetObsdt3
                                                           select (WaterLevelData)row.Data;
                        WaterLevelDataForVerifying temp = new WaterLevelDataForVerifying();
                        temp.OBSDT = next.First().OBSDT;
                        temp.WL = next.First().WL;
                        b.Insert(0, temp);

                        //20분전
                        temp = new WaterLevelDataForVerifying();
                        temp.OBSDT = targetObsdt4;
                        temp.WL = b.First().WL;
                        b.Insert(0, temp);
                    }
                    else
                    {
                        IEnumerable<WaterLevelData> next = from row in parent.damGrid.Rows
                                                           orderby ((WaterLevelData)row.Data).OBSDT ascending
                                                           where ((WaterLevelData)row.Data).OBSDT == targetObsdt3 || ((WaterLevelData)row.Data).OBSDT == targetObsdt4
                                                           select (WaterLevelData)row.Data;

                        foreach (WaterLevelData p in next)
                        {
                            WaterLevelDataForVerifying temp = new WaterLevelDataForVerifying();
                            temp.OBSDT = p.OBSDT;
                            temp.WL = p.WL;
                            b.Insert(0, temp);
                        }
                    }

                    _GraphDatas = b;
                }

                return _GraphDatas;
            }
        }

        #region == 차트관련 전역 변수 ==
        private CategoryXAxis CurXAxis = new CategoryXAxis();
        private NumericYAxis CurYAxis = new NumericYAxis();
        private MinMaxCalculator mmc;
        #endregion


        public WaterLevelUpdateDialog(WaterLevelVerify parent)
        {
            if (parent != null)
            {
                this.parent = parent;
            }
            InitializeComponent();
            SetupDamChart();
            initializeCustomObjects();

        }

        #region == initialize ==
        private void initializeCustomObjects()
        {
            InitApplyTargetCombo(); //적용대상 콤보박스에 itemsource 변경
            try
            {
                if (IsValid() == true)
                {
                    makeGridColumns();
                    LoadGridData();

                    logBaseText.LostFocus += new RoutedEventHandler(logBaseText_LostFocus);
                    logBaseText.KeyDown += new KeyEventHandler(logBaseText_KeyDown); 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void initLogBase()
        {

            if (logBase.Minimum == 0.0)
            {
                try
                {
                    //string previous_obsdt = parent.damGrid.SelectionSettings.SelectedCells.First().Row.Cells["OBSDT"].ToString();
                    //string next_obsdt = parent.damGrid.SelectionSettings.SelectedCells.Last().Row.Cells["OBSDT"].ToString();

                    double max = CurrentData.Max(x => double.Parse(x.EXVL));//Math.Round(double.Parse(CurrentData.First().EXVL), 3);
                    double min = CurrentData.Min(x => double.Parse(x.EXVL)); //Math.Round(double.Parse(CurrentData.Last().EXVL), 3);
                    double avg = (max + min) / 2;

                    logBase.Maximum = max + 0.01;
                    logBase.Minimum = min - 0.01;
                    logBaseText.Text = avg.ToString();
                    //logBase.Maximum = TargetDatas.Max(x => double.Parse(x.WL == null || string.IsNullOrEmpty(x.WL.Trim()) ? "-100" : x.WL));
                    //logBase.Minimum = TargetDatas.Min(x => double.Parse(x.WL == null || string.IsNullOrEmpty(x.WL.Trim()) ? "100" : x.WL));

                    //logBaseText.Text = TargetDatas.Average(x => double.Parse(x.WL == null || string.IsNullOrEmpty(x.WL.Trim()) ? "0" : x.WL)).ToString("0.##");
                }
                catch (Exception ex)
                {

                }
            }
        }

        #region == 적용대상 콤보박스 ==
        private void InitApplyTargetCombo()
        {
            //damTpCombo.SelectionChanged += new EventHandler(DamTpCombo_SelectionChanged);
            applyTargetCombo.SelectionChanged += new EventHandler(applyTargetCombo_SelectionChanged);

            applyTargetCombo.DisplayMemberPath = "Value";
            applyTargetCombo.ItemsSource = GetDataTpList();
            applyTargetCombo.SelectedIndex = 0;
            //applyTargetCombo_SelectionChanged(null, null);
        }
        private IList<KeyValue<string>> GetDataTpList()
        {
            IList<KeyValue<string>> dataTpOptions = new List<KeyValue<string>>();
            for (var i = 0; i < applyTargetOptionKeys.Length; i++)
            {
                KeyValue<string> item = new KeyValue<string>();
                item.Key = applyTargetOptionKeys[i];
                item.Value = applyTargetOptionValues[i];
                dataTpOptions.Add(item);
            }

            return dataTpOptions;
        }
        private void applyTargetCombo_SelectionChanged(object sender, EventArgs e)
        {
            Style DefaultCellStyle = this.Resources["defaultCellStyle"] as Style;
            Style HighlightCellStyle = this.Resources["highlightCellStyle"] as Style;
            //Style s = new Style(typeof(CellControl));
            //s.Setters.Add(new Setter(CellControl.BackgroundProperty, new SolidColorBrush(Colors.Red)));
            //s.Setters.Add(new Setter(CellControl.ForegroundProperty, new SolidColorBrush(Colors.Yellow)));
            //e.Cell.Style = s;

            KeyValue<string> selApplyTarget = (KeyValue<string>)applyTargetCombo.SelectedItem;

            //MessageBox.Show(selApplyTarget.Key);

            if ("EXVL2".Equals(selApplyTarget.Key))
            {
                SliderStack.Height = 30;
                try
                {
                    VerifyGrid.Columns["EXVL2"].CellStyle = HighlightCellStyle;
                    VerifyGrid.Columns["EXVL"].CellStyle = DefaultCellStyle;//(Style)(this.Resources["noCellStyle"]);
                }
                catch (NullReferenceException ex) { }
                numericTxtBox.Style = null;
                numericTxtBox.IsEnabled = false;

            }
            else if ("EXVL".Equals(selApplyTarget.Key))
            {
                
                SliderStack.Height = 0;
                try
                {
                    VerifyGrid.Columns["EXVL2"].CellStyle = DefaultCellStyle;//(Style)(this.Resources["noCellStyle"]);
                    VerifyGrid.Columns["EXVL"].CellStyle = HighlightCellStyle;
                }
                catch (NullReferenceException ex) { }
                numericTxtBox.Style = null;
                numericTxtBox.IsEnabled = false;

                LoadGridData();
            }
            else if ("MANUAL".Equals(selApplyTarget.Key))
            {
                SliderStack.Height = 0;
                try {
                    VerifyGrid.Columns["EXVL2"].CellStyle = DefaultCellStyle;// (Style)(this.Resources["noCellStyle"]);
                    VerifyGrid.Columns["EXVL"].CellStyle = DefaultCellStyle;//(Style)(this.Resources["noCellStyle"]);
                } catch (NullReferenceException ex) { }
                numericTxtBox.Style = (Style)(this.Resources["highlightXamNumericInputStyle"]); 
                numericTxtBox.IsEnabled = true;
            }
        }

        #endregion

        #region == 그리드 ==
        private void makeGridColumns()
        {
            VerifyGrid.Columns.Clear();
            Style DefaultCellStyle = this.Resources["defaultCellStyle"] as Style;

            for (var i = 0; i < columnKeys.Length; i++)
            {
                if ("ACT".Equals(columnKeys[i]) == false)
                {
                    ObsDtConverter ObsDtConv = new ObsDtConverter();

                    TextColumn tc = new TextColumn();
                    tc.Key = columnKeys[i];
                    tc.HeaderText = columnNames[i];
                    tc.TextWrapping = TextWrapping.Wrap;
                    tc.Width = new ColumnWidth(columnWidths[i], false);
                    tc.HeaderTextHorizontalAlignment = HorizontalAlignment.Center;
                    tc.HorizontalContentAlignment = HorizontalAlignment.Center;
                    tc.IsReadOnly = true;
                    tc.CellStyle = DefaultCellStyle;

                    if ("OBSDT".Equals(columnKeys[i]))        //"항목" 이외에는 전부 센터정렬
                    {
                        tc.ValueConverter = ObsDtConv;
                    }

                    VerifyGrid.Columns.Add(tc);
                }               
            }
        }
        #endregion

        #region == 차트 ==
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
            for (var i = 1; i < columnKeys.Length; i++)
            {
                _key = columnKeys[i];
                _title = columnNames[i];
                _brush = new SolidColorBrush(Constants.GetColorFromString(Constants.ChartColors[i - 1]));
                LineSeries ss = CreateSeries(_key, _title, _brush);
                ss.Visibility = Visibility.Visible;
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
        #endregion

        #endregion

        #region == custom functions ==
        //연속된 데이터 인지 확인
        private bool IsValid()
        {
            IEnumerable<Row> sortedRows = from cell in parent.damGrid.SelectionSettings.SelectedCells
                                          orderby ((WaterLevelData)cell.Row.Data).OBSDT ascending
                                          where cell.Column.Key == "WL"
                                          select (Row)cell.Row;

            int Index1 = parent.damGrid.Rows.IndexOf(sortedRows.First());
            int Index2 = parent.damGrid.Rows.IndexOf(sortedRows.Last());

            if (Math.Abs(Index1 - Index2) + 1 != sortedRows.Count())
            {
                //MessageBox.Show("Index1 - Index2 + 1:" + (Index1 - Index2 + 1).ToString() + " RowsCollection.Count:" + RowsCollection.Count);
                throw new Exception("선형보간, 지수함수곡선 적용은 연속 데이터만 가능합니다.");
            }

            return true;
        }

        // 보간
        private void Interpolation()
        {
            
            KeyValue<string> selApplyTarget = (KeyValue<string>)applyTargetCombo.SelectedItem;
            IEnumerable<WaterLevelData> ToCollection = TargetDatas;
            
            if ("EXVL2".Equals(selApplyTarget.Key) || "SPLINE".Equals(selApplyTarget.Key))
            {
                MessageBox.Show("Spline Interpolation Server is not ready.");
            }
            else if ("EXVL".Equals(selApplyTarget.Key))
            {
                List<WaterLevelDataForVerifying> g = GraphDatas.ToList();

                g[0].EXVL = g[0].WL;
                g[1].EXVL = g[1].WL;
                g[g.Count - 2].EXVL = g[g.Count - 2].WL;
                g[g.Count - 1].EXVL = g[g.Count - 1].WL;


                TimeSpan s = DateUtil.convertToDateTime(g[1].OBSDT, "yyyyMMddHHmm") - DateUtil.convertToDateTime(g[g.Count - 2].OBSDT, "yyyyMMddHHmm");
                int ss = (int) s.TotalMinutes / 10;
                double exvl_start = double.Parse(g[1].EXVL);
                double exvl_end = double.Parse(g[g.Count - 2].EXVL);
                double exvl_diff = exvl_end - exvl_start; 
                for (int i = 0; i < ss; i++)
                {
                    double exvl2 = exvl_diff / ss * i + exvl_start;
                    g[1 + i].EXVL = exvl2.ToString();
                }
            }
            else if ("MANUAL".Equals(selApplyTarget.Key))
            {
                string fromValue = numericTxtBox.Text;

                foreach (WaterLevelData toData in ToCollection)
                {
                    toData.WL = fromValue;
                }
            }



            
            //MessageBox.Show(selApplyTarget.Key);

            //PointCollection ps = new PointCollection();

            //foreach(WaterLevelDataForVerifying d in GraphDatas) {
            //    Point p = new Point();

            //    p.X = int.Parse(d.OBSDT.Substring(8, 4)) / 10;
            //    p.Y = double.Parse(d.WL);

            //    ps.Add(p);
            //}


            
            //PointCollection pc = new PointCollection();
            

            //Point p1 = new Point();
            //p1.X = DateUtil.convertToDateTime(g[0].OBSDT, "yyyyMMddHHmm").Ticks;
            //p1.Y = double.Parse(g[0].WL);
            //ps.Add(p1);
            ////g[0].EXVL = g[0].WL;
            ////g[0].EXVL2 = g[0].WL;
            
            //Point p2 = new Point();
            //p2.X = DateUtil.convertToDateTime(g[1].OBSDT, "yyyyMMddHHmm").Ticks;
            //p2.Y = double.Parse(g[1].WL);
            //ps.Add(p2);
            ////g[1].EXVL = g[1].WL;
            ////g[1].EXVL2 = g[1].WL;

            //Point p3 = new Point();
            //p3.X = DateUtil.convertToDateTime(g[g.Count - 2].OBSDT, "yyyyMMddHHmm").Ticks;
            //p3.Y = double.Parse(g[g.Count - 2].WL);
            //ps.Add(p3);
            ////g[g.Count - 2].EXVL = g[g.Count - 2].WL;
            ////g[g.Count - 2].EXVL2 = g[g.Count - 2].WL;

            //Point p4 = new Point();
            //p4.X = DateUtil.convertToDateTime(g.Last().OBSDT, "yyyyMMddHHmm").Ticks;
            //p4.Y = double.Parse(g.Last().WL);
            //ps.Add(p4);
            ////g[g.Count - 1].EXVL = g[g.Count - 1].WL;
            ////g[g.Count - 1].EXVL2 = g[g.Count - 1].WL;

            //pc.Add(p1);
            //pc.Add(p2);
            //pc.Add(p3);
            //pc.Add(p4);
            //pc = InterpolationUtil.CatmullRomSpline(pc, Math.Abs(100 / CurrentData.Count) / 100.0);
            ////pc = InterpolationUtil.LargrangianInterpolation(pc, Math.Abs(100 / CurrentData.Count) / 100.0); //무한대값 나옴

            ////Double[] KN = new Double[] { 0, 0, 0, 0, 1,1,1,1};
            ////pc = InterpolationUtil.BSpline(pc, 3, KN, 0.01);    // 값이 낙하함

            //for (int i = 0; i < pc.Count && i < CurrentData.Count(); i++)
            //{
            //    CurrentData[i].EXVL2 = (Math.Abs(pc[i].Y * 100) / 100).ToString();
            //    //MessageBox.Show("(x,y)=(" + pc[i].X + "," + pc[i].Y + ")");
            //}



            //if ("EXVL2".Equals(selApplyTarget.Key) || "EXVL".Equals(selApplyTarget.Key))
            //{
            //    #region 선형보간
            //    IList<double> knownPoint = new List<double>();
            //    IList<double> knownValue = new List<double>();

            //    GraphDatas.First();

            //    knownPoint.Add(editStartIndex + 1);    //10분전 큰숫자
            //    knownValue.Add(double.Parse(data[editStartIndex + 1].ORIGINAL));

            //    knownPoint.Add(editEndIndex - 1);    //10분후 작은숫자
            //    knownValue.Add(double.Parse(data[editEndIndex - 1].ORIGINAL));

            //    MathNet.Numerics.Interpolation.IInterpolationMethod Itp = MathNet.Numerics.Interpolation.Interpolation.CreateLinearSpline(knownPoint, knownValue);

            //    for (int i = editEndIndex; i <= editStartIndex; i++)
            //    {

            //        data[i].BOGAN = Itp.Interpolate(double.Parse(i.ToString())).ToString("0.#######");
            //        WEBSOLTOOL.Util.DataUtil.SetValue(data[i], targetColumn, temp3);
            //        log.Debug(data[i].OBSDT + " = " + data[i].SPLINE);
            //    }
            //    #endregion
            //}
            //else if ("SPLINE".Equals(selApplyTarget.Key))
            //{
            //    #region SPLINE 보간
            //    IList<double> knownPoint = new List<double>();
            //    IList<double> knownValue = new List<double>();

            //    log.Info("//===== 수정데이터 이전의 데이터와 이후데이터. 그리고 정가운데의 유저입력치를 받아서 기준좌표를 작성 =====");
            //    knownPoint = new List<double>();
            //    knownValue = new List<double>();
            //    for (int i = 0; i < editEndIndex; i++)
            //    {
            //        temp1 = WEBSOLTOOL.Util.DataUtil.GetValue(data[i], targetColumn);
            //        if (string.IsNullOrEmpty(data[i].ORIGINAL) == false)
            //        {
            //            knownPoint.Add(i);
            //            knownValue.Add(double.Parse(data[i].ORIGINAL.ToString()));
            //        }
            //    }
            //    knownValue.Add(LogBase);
            //    knownPoint.Add(data.Count * 0.5);
            //    for (int i = editStartIndex; i < data.Count; i++)
            //    {
            //        temp1 = WEBSOLTOOL.Util.DataUtil.GetValue(data[i], targetColumn);
            //        if (string.IsNullOrEmpty(data[i].ORIGINAL) == false)
            //        {
            //            knownPoint.Add(i);
            //            knownValue.Add(double.Parse(data[i].ORIGINAL));
            //        }
            //    }
            //    log.Info("//==================================================================================================");

            //    MathNet.Numerics.Interpolation.IInterpolationMethod Itp = MathNet.Numerics.Interpolation.Interpolation.CreateAkimaCubicSpline(knownPoint, knownValue);

            //    for (int i = editEndIndex; i <= editStartIndex; i++)
            //    {
            //        data[i].BOGAN = Itp.Interpolate(double.Parse(i.ToString())).ToString("0.#######");
            //    }
            //    #endregion
            //}


        }


        private void LoadGridData()
        {
            LoadingBar.IsBusy = true;
            
            if (CurrentData == null || CurrentData.Count() == 0)
            {
                CurrentData = new List<WaterLevelDataForVerifying>();
                foreach (WaterLevelData wld in TargetDatas)
                {
                    WaterLevelDataForVerifying temp = new WaterLevelDataForVerifying();
                    temp.OBSDT = wld.OBSDT;
                    temp.WL = wld.WL;
                    //temp.EXVL = wld.EXVL;
                    //temp.EXVL2 = "0";

                    CurrentData.Add(temp);
                }
            }
            else
            {
                Interpolation();
            }

            #region 차트 MIN & MAX 구하기
            IList obj = (IList)GraphDatas;
            mmc = new MinMaxCalculator(obj);
            #region 차트 MIN & MAX 구하기
            foreach (string col in columnKeys)
            {
                if (col.Equals("OBSDT") == true) continue;

                mmc.AddLegend(col);
            }
            //damDataChart.FlowDirection = System.Windows.FlowDirection.LeftToRight;
            foreach(LineSeries ls in damDataChart.Series) {
                ls.YAxis.MaximumValue = mmc.Max + mmc.TopOffset;
                ls.YAxis.MinimumValue = mmc.Min - mmc.BottomOffset;
            }

            #endregion
            #endregion

            CurXAxis.ItemsSource = null;
            CurXAxis.ItemsSource = GraphDatas;
            foreach (LineSeries ss in damDataChart.Series)
            {
                ss.XAxis = CurXAxis;
                ss.YAxis = CurYAxis;
                ss.Visibility = Visibility.Visible;
                ss.ItemsSource = null;
                ss.ItemsSource = GraphDatas;
            }
            VerifyGrid.ItemsSource = null;
            VerifyGrid.ItemsSource = CurrentData;
            
            ToggleTargetItems();

            initLogBase();

            LoadingBar.IsBusy = false;
        }
        #endregion

        private void ToggleTargetItems()
        {
            applyTargetCombo.IsEnabled = true;
            VerifyGrid.Columns["EXVL"].Visibility = Visibility.Visible;
            VerifyGrid.Columns["EXVL2"].Visibility = Visibility.Visible;

        }
        //void SplineInterpolation()
        //{
        //    IList<double> knownPoint = new List<double>();
        //    IList<double> knownValue = new List<double>();

        //    knownPoint.Add(0.0);
        //    knownPoint.Add((CurrentData.Count - 1)/2);
        //    knownPoint.Add(CurrentData.Count-1);

        //    knownValue.Add(double.Parse(CurrentData.First().WL));
        //    knownValue.Add(logBase.Value);
        //    knownValue.Add(double.Parse(CurrentData.Last().WL));


        //    LinearSplineInterpolation Itp = new LinearSplineInterpolation(knownPoint, knownValue);


        //    foreach(WaterLevelDataForVerifying d in CurrentData) {
        //        d.SPLINE = Itp.Interpolate(CurrentData.IndexOf(d)).ToString("0.##");
        //    }
        //}

        #region == correction functions ==

        private void applyUpdateAll()
        {
            KeyValue<string> selApplyTarget = (KeyValue<string>)applyTargetCombo.SelectedItem;
            IEnumerable<WaterLevelData> ToCollection = TargetDatas;

            if ("EXVL2".Equals(selApplyTarget.Key) || "EXVL".Equals(selApplyTarget.Key) || "SPLINE".Equals(selApplyTarget.Key)) 
            {
                RowCollection fromCollection = VerifyGrid.Rows;

                foreach (Row fromRow in fromCollection)
                {
                    WaterLevelDataForVerifying fromData = (WaterLevelDataForVerifying)fromRow.Data;
                    foreach (WaterLevelData toData in ToCollection) 
                    {
                        if(toData.OBSDT.Equals(fromData.OBSDT)) 
                        {
                            toData.WL = fromRow.Cells[selApplyTarget.Key].Value.ToString();
                            break;
                        }
                        
                    }
                }
                

            } 
            else if("MANUAL".Equals(selApplyTarget.Key)) 
            {
                string fromValue = numericTxtBox.Text;

                foreach (WaterLevelData toData in ToCollection)
                {
                    toData.WL = fromValue;
                }
            }

        }


        #endregion

        #region == 그외 Event ==
        

        private void applyBtn_Click(object sender, RoutedEventArgs e)
        {
            applyUpdateAll();
            parent.toggleSavable(null);
            parent.UpdateMinMaxChart(TargetColumnName + "_SERIES");
            this.DialogResult = true;
        }

        private void cancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        void logBaseText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                logBase_MouseLeftButtonUp(sender, null);
            }
        }

        void logBaseText_LostFocus(object sender, RoutedEventArgs e)
        {
            logBase_MouseLeftButtonUp(sender, null);
        }

        private void logBase_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (IsValid() == true)
                {
                    LoadGridData();
                }
            }
            catch (Exception ex)
            {
                //
            }
        }
        #endregion

    }
}

