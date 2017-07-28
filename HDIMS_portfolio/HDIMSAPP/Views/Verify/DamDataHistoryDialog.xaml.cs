using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Controls;
using HDIMSAPP.Common.Converter;
using HDIMSAPP.Models.Verify;
using Infragistics.Controls.Grids;

namespace HDIMSAPP.Views.Verify
{
    public partial class DamDataHistoryDialog : ChildWindow
    {
        private readonly string DamDataHistoryUri = "/DataSearch/GetDamDataHistoryList/?"; //damcd, obsdt, trmdv
        private readonly string WaterLevelDataHistoryUri = "/DataSearch/GetWaterLevelHistoryList/?"; // obscd, obsdt, trmdv
        private readonly string RainFallDataHistoryUri = "/DataSearch/GetRainFallHistoryList/?"; // obscd, obsdt, trmdv
        private WebClient client;
        private string DialogTp;
        public string damcd { get; set; }
        public string obsdt { get; set; }
        public string trmdv { get; set; }
        public string obscd { get; set; }
        public ColumnBaseCollection ParentColumns { get; set; }
        private ObsDtConverter obsdtConv = new ObsDtConverter();



        public DamDataHistoryDialog(string DialogTp, string damcd, string obscd, string obsdt, string trmdv)
        {
            this.DialogTp = DialogTp;
            this.damcd = damcd;
            this.obscd = obscd;
            this.obsdt = obsdt;
            this.trmdv = trmdv;
            
            InitializeComponent();
            initSearchPanel();
        }

        /// <summary>
        /// 댐운영자료용
        /// </summary>
        /// <param name="DialogTp"></param>
        /// <param name="damcd"></param>
        /// <param name="obscd"></param>
        /// <param name="obsdt"></param>
        /// <param name="trmdv"></param>
        /// <param name="parentColumns"></param>
        public DamDataHistoryDialog(string DialogTp, string damcd, string obscd, string obsdt, string trmdv, ColumnBaseCollection parentColumns)
        {
            this.DialogTp = DialogTp;
            this.damcd = damcd;
            this.obscd = obscd;
            this.obsdt = obsdt;
            this.trmdv = trmdv;
            this.ParentColumns = parentColumns;

            InitializeComponent();
            initSearchPanel();
        }

        #region == initializing ==

        private void initSearchPanel()
        {
            switch (this.DialogTp)
            {
                case "DAM":
                    makeGridColumnsForDamData();
                    GetDataHistoryList();
                    break;
                case "WL":
                    makeGridColumnsForWaterLevelData();
                    GetDataHistoryList();
                    break;
                case "RF":
                    makeGridColumnsForRainFallData();
                    GetDataHistoryList();
                    break;
                default :
                    MessageBox.Show("허가되지 않은 조회");
                    this.DialogResult = false;
                    break;
            }
        }
        #endregion

        #region == custom functions ==

        #region == 댐운영자료 변경이력 ==
        private void makeGridColumnsForDamData()
        {
            //컬럼수 25개
            historyGrid.Columns.Clear();
            DamDataConverter damDataConverter = new DamDataConverter();
            Style DefaultCellStyle = this.Resources["defaultCellStyle"] as Style;
            string[] columnKeys = {"OBSDT","CGDT","CGEMPNM","RWL","RSQTY","RSRT","OSPILWL","IQTY","ETCIQTY1","ETCIQTY2","ETQTY","TDQTY","EDQTY","ETCEDQTY","SPDQTY"
                                      ,"ETCDQTY1","ETCDQTY2","ETCDQTY3","OTLTDQTY","ITQTY1","ITQTY2","ITQTY3","DAMBSARF","CNRSN","CNDS"};
            string[] columnNames = {"측정일시","보정일시","보정자","저수위","저수량","저수율","방수로\n수위","유입량","기타\n유입량1","기타\n유입량2","공용량","총방류량","발전\n방류량"
                                       ,"기타발전\n방류량","수문\n방류량","기타\n방류량1","기타\n방류량2","기타\n방류량3","아울렛\n방류량","취수1","취수2","취수3","평균\n우량"
                                      ,"사유","내역"};
            double[] columnWidths = {120,120,80,70,70,70,70,70,70,70,70,70,70,70,70,70,70,70,70,70,70,70,70,200,200};
            bool[] columnReadOnlys = { true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true, true };

            for (var i = 0; i < columnKeys.Length; i++)
            {
                TextColumn tc = new TextColumn();
                tc.Key = columnKeys[i];
                if (ParentColumns[columnKeys[i]] != null)
                {
                    TextColumn parentTc = ParentColumns[columnKeys[i]] as TextColumn;
                    tc.HeaderText = parentTc.HeaderText;
                    tc.ValueConverter = parentTc.ValueConverter;
                    tc.ValueConverterParameter = parentTc.ValueConverterParameter;
                }
                else
                {
                    tc.HeaderText = columnNames[i];
                }
                tc.TextWrapping = TextWrapping.Wrap;
                tc.Width = new ColumnWidth(columnWidths[i], false);
                tc.HeaderTextHorizontalAlignment = HorizontalAlignment.Center;
                tc.IsReadOnly = columnReadOnlys[i];
                tc.CellStyle = DefaultCellStyle;

                if ("OBSDT".Equals(columnKeys[i]) || "CGDT".Equals(columnKeys[i]))
                {
                    tc.IsFixed = FixedState.Left;
                    tc.HorizontalContentAlignment = HorizontalAlignment.Center;
                    tc.ValueConverter = obsdtConv;
                }
                else if (columnKeys[i] == "CNRSN" || columnKeys[i] == "CNDS")
                {
                    tc.HorizontalContentAlignment = HorizontalAlignment.Left;
                }
                else
                {
                    tc.HorizontalContentAlignment = HorizontalAlignment.Right;
                }
                historyGrid.Columns.Add(tc);
            }
        }

        private string GetUrlParamForDamData()
        {
            string uri = DamDataHistoryUri;
            uri += "damcd=" + this.damcd + "&obsdt=" + this.obsdt + "&trmdv=" + this.trmdv;
            return uri;
        }

        //private void GetDamDataHistoryList()
        //{
        //    LoadingBar.IsBusy = true;
        //    historyGrid.ItemsSource = null;
        //    client = new WebClient();
        //    client.OpenReadCompleted += new OpenReadCompletedEventHandler(GetHistoryCompleted);
        //    client.OpenReadAsync(new Uri(GetUrlParamForDamData(), UriKind.Relative));
        //}

        #endregion

        #region == 수위자료 변경이력 ==
        private void makeGridColumnsForWaterLevelData()
        {
            historyGrid.Columns.Clear();
            Style DefaultCellStyle = this.Resources["defaultCellStyle"] as Style;
            string[] columnKeys = {"OBSDT","CGDT","CGEMPNM","WL","FLW","EDEXLVLCONT","EDEXWAYCONT","CNRSN","CNDS"};
            string[] columnNames = { "측정일시", "보정일시", "보정자", "변경전" + Environment.NewLine + "수위", "변경전" + Environment.NewLine + "유량", "보정등급", "보정방법", "증상", "내역" };
            double[] columnWidths = { 120, 120, 80, 70, 70, 100, 100, 200, 200 };

            for (var i = 0; i < columnKeys.Length; i++)
            {
                TextColumn tc = new TextColumn();
                tc.Key = columnKeys[i];
                tc.HeaderText = columnNames[i];
                tc.TextWrapping = TextWrapping.Wrap;
                tc.Width = new ColumnWidth(columnWidths[i], false);
                tc.HeaderTextHorizontalAlignment = HorizontalAlignment.Center;
                tc.IsReadOnly = true;
                tc.CellStyle = DefaultCellStyle;
                //tc.ValueConverter = new DamDataConverter();
                //tc.ValueConverterParameter = damConst;

                if ("OBSDT".Equals(columnKeys[i]) || "CGDT".Equals(columnKeys[i]))
                {
                    tc.IsFixed = FixedState.Left;
                    tc.HorizontalContentAlignment = HorizontalAlignment.Center;
                    tc.ValueConverter = obsdtConv;
                }
                else if ("WL".Equals(columnKeys[i]) || "FLW".Equals(columnKeys[i]) )
                {
                    tc.HorizontalContentAlignment = HorizontalAlignment.Right; 
                }
                else
                {
                    tc.HorizontalContentAlignment = HorizontalAlignment.Left;
                }
                historyGrid.Columns.Add(tc);
            }
        }

        private string GetUrlParamForWaterLevelData()
        {
            string uri = WaterLevelDataHistoryUri;
            uri += "obscd=" + this.obscd + "&obsdt=" + this.obsdt + "&trmdv=" + this.trmdv;
            return uri;
        }

        #endregion
        
        #region == 우량자료 변경이력 ==
        
        private void makeGridColumnsForRainFallData()
        {
            historyGrid.Columns.Clear();
            Style DefaultCellStyle = this.Resources["defaultCellStyle"] as Style;
            string[] columnKeys = {"OBSDT", "CGDT","CGEMPNM","PYACURF","RF","EDEXLVLCONT","EDEXWAYCONT","CNRSN","CNDS"};
            string[] columnNames = { "측정일시", "보정일시", "보정자", "변경전" + Environment.NewLine + "계측우량", "변경전" + Environment.NewLine + "시간우량", "보정등급", "보정방법", "증상", "내역" };
            double[] columnWidths = { 120, 120, 80, 70, 70, 100, 100, 200, 200 };

            for (var i = 0; i < columnKeys.Length; i++)
            {
                TextColumn tc = new TextColumn();
                tc.Key = columnKeys[i];
                tc.HeaderText = columnNames[i];
                tc.TextWrapping = TextWrapping.Wrap;
                tc.Width = new ColumnWidth(columnWidths[i], false);
                tc.HeaderTextHorizontalAlignment = HorizontalAlignment.Center;
                tc.IsReadOnly = true;
                tc.CellStyle = DefaultCellStyle;
                //tc.ValueConverter = new DamDataConverter();
                //tc.ValueConverterParameter = damConst;

                if ("OBSDT".Equals(columnKeys[i]) || "CGDT".Equals(columnKeys[i]))
                {
                    tc.IsFixed = FixedState.Left;
                    tc.HorizontalContentAlignment = HorizontalAlignment.Center;
                    tc.ValueConverter = obsdtConv;
                }
                else if ("PYACURF".Equals(columnKeys[i]) || "RF".Equals(columnKeys[i]))
                {
                    tc.HorizontalContentAlignment = HorizontalAlignment.Right; 
                }
                else
                {
                    tc.HorizontalContentAlignment = HorizontalAlignment.Left;
                }
                historyGrid.Columns.Add(tc);
            }
        }

        private string GetUrlParamForRainFallData()
        {
            string uri = RainFallDataHistoryUri;
            uri += "obscd=" + this.obscd + "&obsdt=" + this.obsdt + "&trmdv=" + this.trmdv;
            return uri;
        }
        #endregion
        

        #endregion

        #region == Events ==
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
        private void GetDataHistoryList()
        {
            string uri = "";
            switch (this.DialogTp)
            {
                case "DAM":
                    uri = GetUrlParamForDamData();
                    break;
                case "WL":
                    uri = GetUrlParamForWaterLevelData();
                    break;
                case "RF":
                    uri = GetUrlParamForRainFallData();
                    break;
                default:
                    break;
            }
            
            LoadingBar.IsBusy = true;
            historyGrid.ItemsSource = null;
            client = new WebClient();
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(GetHistoryCompleted);
            client.OpenReadAsync(new Uri(uri, UriKind.Relative));
        }
        void GetHistoryCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null) return;

            Stream str = e.Result;

            StreamReader reader = new StreamReader(str); 
            string text = reader.ReadToEnd();

            if ("DAM".Equals(this.DialogTp))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IList<DamData>));

                //MessageBox.Show("DamData");
                IList<DamData> list = (IList<DamData>)ser.ReadObject(str);
                historyGrid.ItemsSource = list;

            } else if("WL".Equals(this.DialogTp)) 
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IList<WaterLevelData>));

                //MessageBox.Show("WaterLevelData");
                IList<WaterLevelData> list = (IList<WaterLevelData>)ser.ReadObject(str);
                historyGrid.ItemsSource = list;

            }
            else if ("RF".Equals(this.DialogTp))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IList<RainFallData>));

                IList<RainFallData> list = (IList<RainFallData>)ser.ReadObject(str);
                historyGrid.ItemsSource = list;
            }
            else
            {
                MessageBox.Show("Else");
            }
            
            str.Close();
            LoadingBar.IsBusy = false;
        }
        #endregion
    }
}

