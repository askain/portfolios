﻿#pragma checksum "D:\hdims\HDIMS\HDIMSAPP\Views\Statistics\DamDataStat.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1BAA7EE1FD7F5032B7F90EDC0E28CC46"
//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.18331
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace HDIMSAPP.Views.Statistics {
    
    
    public partial class DamDataStat : System.Windows.Controls.Page {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid SubLayoutRoot;
        
        internal System.Windows.Controls.Border border1;
        
        internal System.Windows.Controls.StackPanel stackPanel1;
        
        internal System.Windows.Controls.TextBlock titleTextBlock;
        
        internal System.Windows.Controls.StackPanel SearchPanel1;
        
        internal System.Windows.Controls.TextBlock textBlockWKCD;
        
        internal Infragistics.Controls.Editors.XamComboEditor WKCombo;
        
        internal System.Windows.Controls.TextBlock textBlock1;
        
        internal Infragistics.Controls.Editors.XamComboEditor damTpCombo;
        
        internal System.Windows.Controls.TextBlock textBlock2;
        
        internal Infragistics.Controls.Editors.XamComboEditor damCdCombo;
        
        internal System.Windows.Controls.TextBlock textBlock3;
        
        internal System.Windows.Controls.DatePicker startDtCal;
        
        internal System.Windows.Controls.TextBlock hipentextBlock;
        
        internal System.Windows.Controls.DatePicker endDtCal;
        
        internal System.Windows.Controls.TextBlock textBlock8;
        
        internal Infragistics.Controls.Editors.XamComboEditor exCdCombo;
        
        internal System.Windows.Controls.TextBlock textBlock9;
        
        internal Infragistics.Controls.Editors.XamComboEditor dispTpCombo;
        
        internal System.Windows.Controls.TextBlock textBlock10;
        
        internal Infragistics.Controls.Editors.XamComboEditor statTpCombo;
        
        internal System.Windows.Controls.Button searchBtn;
        
        internal System.Windows.Controls.Button excelBtn;
        
        internal System.Windows.Controls.Button reportBtn;
        
        internal System.Windows.Controls.Button chartBtn;
        
        internal System.Windows.Controls.Grid charGridLayout;
        
        internal Infragistics.Controls.Charts.XamPieChart excdPieChart;
        
        internal System.Windows.Controls.ItemsControl pieLegendItemControl;
        
        internal System.Windows.Controls.Border EmptyPieChart;
        
        internal System.Windows.Controls.GridSplitter SplitterBetweenPieAndColumn;
        
        internal Infragistics.Controls.Charts.XamDataChart dispTpChart;
        
        internal Infragistics.Controls.Charts.CategoryXAxis dispTpChartXAxis1;
        
        internal Infragistics.Controls.Charts.NumericYAxis dispTpChartYAxis1;
        
        internal Infragistics.Controls.Charts.NumericYAxis dispTpChartYAxis2;
        
        internal Infragistics.Controls.Charts.ColumnSeries dispTpChartSer1;
        
        internal Infragistics.Controls.Charts.LineSeries dispTpChartSer2;
        
        internal Infragistics.Controls.Charts.Legend dispTpChartLegend;
        
        internal Infragistics.Controls.Charts.XamDataChart statTpChart;
        
        internal Infragistics.Controls.Charts.CategoryXAxis statTpChartXAxis1;
        
        internal Infragistics.Controls.Charts.NumericYAxis statTpChartYAxis1;
        
        internal Infragistics.Controls.Charts.NumericYAxis statTpChartYAxis2;
        
        internal Infragistics.Controls.Charts.ColumnSeries statTpChartSer1;
        
        internal Infragistics.Controls.Charts.LineSeries statTpChartSer2;
        
        internal Infragistics.Controls.Charts.Legend statTpChartLegend;
        
        internal Infragistics.Controls.Grids.XamGrid dataGrid;
        
        internal System.Windows.Controls.BusyIndicator LoadingBar;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/HDIMSAPP;component/Views/Statistics/DamDataStat.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.SubLayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("SubLayoutRoot")));
            this.border1 = ((System.Windows.Controls.Border)(this.FindName("border1")));
            this.stackPanel1 = ((System.Windows.Controls.StackPanel)(this.FindName("stackPanel1")));
            this.titleTextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("titleTextBlock")));
            this.SearchPanel1 = ((System.Windows.Controls.StackPanel)(this.FindName("SearchPanel1")));
            this.textBlockWKCD = ((System.Windows.Controls.TextBlock)(this.FindName("textBlockWKCD")));
            this.WKCombo = ((Infragistics.Controls.Editors.XamComboEditor)(this.FindName("WKCombo")));
            this.textBlock1 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock1")));
            this.damTpCombo = ((Infragistics.Controls.Editors.XamComboEditor)(this.FindName("damTpCombo")));
            this.textBlock2 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock2")));
            this.damCdCombo = ((Infragistics.Controls.Editors.XamComboEditor)(this.FindName("damCdCombo")));
            this.textBlock3 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock3")));
            this.startDtCal = ((System.Windows.Controls.DatePicker)(this.FindName("startDtCal")));
            this.hipentextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("hipentextBlock")));
            this.endDtCal = ((System.Windows.Controls.DatePicker)(this.FindName("endDtCal")));
            this.textBlock8 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock8")));
            this.exCdCombo = ((Infragistics.Controls.Editors.XamComboEditor)(this.FindName("exCdCombo")));
            this.textBlock9 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock9")));
            this.dispTpCombo = ((Infragistics.Controls.Editors.XamComboEditor)(this.FindName("dispTpCombo")));
            this.textBlock10 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock10")));
            this.statTpCombo = ((Infragistics.Controls.Editors.XamComboEditor)(this.FindName("statTpCombo")));
            this.searchBtn = ((System.Windows.Controls.Button)(this.FindName("searchBtn")));
            this.excelBtn = ((System.Windows.Controls.Button)(this.FindName("excelBtn")));
            this.reportBtn = ((System.Windows.Controls.Button)(this.FindName("reportBtn")));
            this.chartBtn = ((System.Windows.Controls.Button)(this.FindName("chartBtn")));
            this.charGridLayout = ((System.Windows.Controls.Grid)(this.FindName("charGridLayout")));
            this.excdPieChart = ((Infragistics.Controls.Charts.XamPieChart)(this.FindName("excdPieChart")));
            this.pieLegendItemControl = ((System.Windows.Controls.ItemsControl)(this.FindName("pieLegendItemControl")));
            this.EmptyPieChart = ((System.Windows.Controls.Border)(this.FindName("EmptyPieChart")));
            this.SplitterBetweenPieAndColumn = ((System.Windows.Controls.GridSplitter)(this.FindName("SplitterBetweenPieAndColumn")));
            this.dispTpChart = ((Infragistics.Controls.Charts.XamDataChart)(this.FindName("dispTpChart")));
            this.dispTpChartXAxis1 = ((Infragistics.Controls.Charts.CategoryXAxis)(this.FindName("dispTpChartXAxis1")));
            this.dispTpChartYAxis1 = ((Infragistics.Controls.Charts.NumericYAxis)(this.FindName("dispTpChartYAxis1")));
            this.dispTpChartYAxis2 = ((Infragistics.Controls.Charts.NumericYAxis)(this.FindName("dispTpChartYAxis2")));
            this.dispTpChartSer1 = ((Infragistics.Controls.Charts.ColumnSeries)(this.FindName("dispTpChartSer1")));
            this.dispTpChartSer2 = ((Infragistics.Controls.Charts.LineSeries)(this.FindName("dispTpChartSer2")));
            this.dispTpChartLegend = ((Infragistics.Controls.Charts.Legend)(this.FindName("dispTpChartLegend")));
            this.statTpChart = ((Infragistics.Controls.Charts.XamDataChart)(this.FindName("statTpChart")));
            this.statTpChartXAxis1 = ((Infragistics.Controls.Charts.CategoryXAxis)(this.FindName("statTpChartXAxis1")));
            this.statTpChartYAxis1 = ((Infragistics.Controls.Charts.NumericYAxis)(this.FindName("statTpChartYAxis1")));
            this.statTpChartYAxis2 = ((Infragistics.Controls.Charts.NumericYAxis)(this.FindName("statTpChartYAxis2")));
            this.statTpChartSer1 = ((Infragistics.Controls.Charts.ColumnSeries)(this.FindName("statTpChartSer1")));
            this.statTpChartSer2 = ((Infragistics.Controls.Charts.LineSeries)(this.FindName("statTpChartSer2")));
            this.statTpChartLegend = ((Infragistics.Controls.Charts.Legend)(this.FindName("statTpChartLegend")));
            this.dataGrid = ((Infragistics.Controls.Grids.XamGrid)(this.FindName("dataGrid")));
            this.LoadingBar = ((System.Windows.Controls.BusyIndicator)(this.FindName("LoadingBar")));
        }
    }
}

