﻿#pragma checksum "C:\Projects\HDIMS\HDIMSAPP\Views\Verify\사본 - WaterLevelSearch.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E809DD6E22561C0ABF7F0132551BDB18"
//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.269
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

using HDIMSAPP.Common.Converter;
using HDIMSAPP.Controls;
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


namespace HDIMSAPP.Views.Verify {
    
    
    public partial class WaterLevelSearch : System.Windows.Controls.Page {
        
        internal HDIMSAPP.Common.Converter.VisibilityConverter VisibilityConverter;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid SubLayoutRoot;
        
        internal System.Windows.Controls.Border border1;
        
        internal System.Windows.Controls.StackPanel stackPanel1;
        
        internal System.Windows.Controls.TextBlock titleTextBlock;
        
        internal System.Windows.Controls.StackPanel SearchPanel;
        
        internal System.Windows.Controls.TextBlock textBlock0;
        
        internal Infragistics.Controls.Editors.XamComboEditor WKCombo;
        
        internal System.Windows.Controls.TextBlock textBlock1;
        
        internal Infragistics.Controls.Editors.XamComboEditor damTpCombo;
        
        internal System.Windows.Controls.TextBlock textBlock2;
        
        internal Infragistics.Controls.Editors.XamComboEditor damCdCombo;
        
        internal System.Windows.Controls.TextBlock textBlock3;
        
        internal System.Windows.Controls.DatePicker selectDtCal;
        
        internal System.Windows.Controls.TextBlock textBlock4;
        
        internal Infragistics.Controls.Editors.XamComboEditor dataTpCombo;
        
        internal System.Windows.Controls.TextBlock textBlock5;
        
        internal Infragistics.Controls.Editors.XamComboEditor searchTpCombo;
        
        internal System.Windows.Controls.TextBlock textBlock6;
        
        internal Infragistics.Controls.Editors.XamComboEditor selectColorCombo;
        
        internal System.Windows.Controls.Button searchBtn;
        
        internal System.Windows.Controls.Button legendBtn;
        
        internal System.Windows.Controls.Button excelBtn;
        
        internal System.Windows.Controls.Button chartBtn;
        
        internal System.Windows.Controls.Button cimsBtn;
        
        internal System.Windows.Controls.Button saveBtn;
        
        internal HDIMSAPP.Controls.DoubleClickDataGrid damGrid;
        
        internal System.Windows.Controls.GridSplitter chartGridSplitter;
        
        internal System.Windows.Controls.Grid damChartGrid;
        
        internal System.Windows.Controls.TextBlock DifferenceTextBlock;
        
        internal System.Windows.Controls.Button exportImageGridChart;
        
        internal System.Windows.Controls.Button closeGridChart;
        
        internal Infragistics.Controls.Charts.XamDataChart damDataChart;
        
        internal System.Windows.Controls.Button AllLegendSelectBtn;
        
        internal System.Windows.Controls.TextBlock AllSelectTxt;
        
        internal System.Windows.Controls.Button AllLegendDeselectBtn;
        
        internal System.Windows.Controls.TextBlock AllDeselectTxt;
        
        internal Infragistics.Controls.Charts.Legend gridChartLegend;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/HDIMSAPP;component/Views/Verify/%EC%82%AC%EB%B3%B8%20-%20WaterLevelSearch.xaml", System.UriKind.Relative));
            this.VisibilityConverter = ((HDIMSAPP.Common.Converter.VisibilityConverter)(this.FindName("VisibilityConverter")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.SubLayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("SubLayoutRoot")));
            this.border1 = ((System.Windows.Controls.Border)(this.FindName("border1")));
            this.stackPanel1 = ((System.Windows.Controls.StackPanel)(this.FindName("stackPanel1")));
            this.titleTextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("titleTextBlock")));
            this.SearchPanel = ((System.Windows.Controls.StackPanel)(this.FindName("SearchPanel")));
            this.textBlock0 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock0")));
            this.WKCombo = ((Infragistics.Controls.Editors.XamComboEditor)(this.FindName("WKCombo")));
            this.textBlock1 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock1")));
            this.damTpCombo = ((Infragistics.Controls.Editors.XamComboEditor)(this.FindName("damTpCombo")));
            this.textBlock2 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock2")));
            this.damCdCombo = ((Infragistics.Controls.Editors.XamComboEditor)(this.FindName("damCdCombo")));
            this.textBlock3 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock3")));
            this.selectDtCal = ((System.Windows.Controls.DatePicker)(this.FindName("selectDtCal")));
            this.textBlock4 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock4")));
            this.dataTpCombo = ((Infragistics.Controls.Editors.XamComboEditor)(this.FindName("dataTpCombo")));
            this.textBlock5 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock5")));
            this.searchTpCombo = ((Infragistics.Controls.Editors.XamComboEditor)(this.FindName("searchTpCombo")));
            this.textBlock6 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock6")));
            this.selectColorCombo = ((Infragistics.Controls.Editors.XamComboEditor)(this.FindName("selectColorCombo")));
            this.searchBtn = ((System.Windows.Controls.Button)(this.FindName("searchBtn")));
            this.legendBtn = ((System.Windows.Controls.Button)(this.FindName("legendBtn")));
            this.excelBtn = ((System.Windows.Controls.Button)(this.FindName("excelBtn")));
            this.chartBtn = ((System.Windows.Controls.Button)(this.FindName("chartBtn")));
            this.cimsBtn = ((System.Windows.Controls.Button)(this.FindName("cimsBtn")));
            this.saveBtn = ((System.Windows.Controls.Button)(this.FindName("saveBtn")));
            this.damGrid = ((HDIMSAPP.Controls.DoubleClickDataGrid)(this.FindName("damGrid")));
            this.chartGridSplitter = ((System.Windows.Controls.GridSplitter)(this.FindName("chartGridSplitter")));
            this.damChartGrid = ((System.Windows.Controls.Grid)(this.FindName("damChartGrid")));
            this.DifferenceTextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("DifferenceTextBlock")));
            this.exportImageGridChart = ((System.Windows.Controls.Button)(this.FindName("exportImageGridChart")));
            this.closeGridChart = ((System.Windows.Controls.Button)(this.FindName("closeGridChart")));
            this.damDataChart = ((Infragistics.Controls.Charts.XamDataChart)(this.FindName("damDataChart")));
            this.AllLegendSelectBtn = ((System.Windows.Controls.Button)(this.FindName("AllLegendSelectBtn")));
            this.AllSelectTxt = ((System.Windows.Controls.TextBlock)(this.FindName("AllSelectTxt")));
            this.AllLegendDeselectBtn = ((System.Windows.Controls.Button)(this.FindName("AllLegendDeselectBtn")));
            this.AllDeselectTxt = ((System.Windows.Controls.TextBlock)(this.FindName("AllDeselectTxt")));
            this.gridChartLegend = ((Infragistics.Controls.Charts.Legend)(this.FindName("gridChartLegend")));
            this.LoadingBar = ((System.Windows.Controls.BusyIndicator)(this.FindName("LoadingBar")));
        }
    }
}

