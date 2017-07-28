﻿#pragma checksum "D:\hdims\HDIMS\HDIMSAPP\Views\DataSearch\NetworkSearch.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "BD9E4616A979354E8B66305824785B8E"
//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.18331
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

using HDIMSAPP.Common.Converter;
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


namespace HDIMSAPP.Views.DataSearch {
    
    
    public partial class NetworkSearch : System.Windows.Controls.Page {
        
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
        
        internal System.Windows.Controls.TextBlock textBlock13;
        
        internal Infragistics.Controls.Editors.XamComboEditor obsCdCombo;
        
        internal System.Windows.Controls.TextBlock textBlock5;
        
        internal Infragistics.Controls.Editors.XamComboEditor dataTpCombo;
        
        internal System.Windows.Controls.TextBlock textBlock3;
        
        internal System.Windows.Controls.DatePicker startDtCal;
        
        internal Infragistics.Controls.Editors.XamComboEditor startHrCombo;
        
        internal System.Windows.Controls.TextBlock hipentextBlock;
        
        internal System.Windows.Controls.DatePicker endDtCal;
        
        internal Infragistics.Controls.Editors.XamComboEditor endHrCombo;
        
        internal System.Windows.Controls.Button searchBtn;
        
        internal System.Windows.Controls.Button excelBtn;
        
        internal System.Windows.Controls.Button chartBtn;
        
        internal Infragistics.Controls.Grids.XamGrid dataGrid;
        
        internal System.Windows.Controls.GridSplitter chartGridSplitter;
        
        internal System.Windows.Controls.Grid damChartGrid;
        
        internal System.Windows.Controls.TextBlock DifferenceTextBlock;
        
        internal System.Windows.Controls.Button exportImageGridChart;
        
        internal System.Windows.Controls.Button viewAllGridChart;
        
        internal System.Windows.Controls.Button closeGridChart;
        
        internal Infragistics.Controls.Charts.XamDataChart gridChart;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/HDIMSAPP;component/Views/DataSearch/NetworkSearch.xaml", System.UriKind.Relative));
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
            this.textBlock13 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock13")));
            this.obsCdCombo = ((Infragistics.Controls.Editors.XamComboEditor)(this.FindName("obsCdCombo")));
            this.textBlock5 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock5")));
            this.dataTpCombo = ((Infragistics.Controls.Editors.XamComboEditor)(this.FindName("dataTpCombo")));
            this.textBlock3 = ((System.Windows.Controls.TextBlock)(this.FindName("textBlock3")));
            this.startDtCal = ((System.Windows.Controls.DatePicker)(this.FindName("startDtCal")));
            this.startHrCombo = ((Infragistics.Controls.Editors.XamComboEditor)(this.FindName("startHrCombo")));
            this.hipentextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("hipentextBlock")));
            this.endDtCal = ((System.Windows.Controls.DatePicker)(this.FindName("endDtCal")));
            this.endHrCombo = ((Infragistics.Controls.Editors.XamComboEditor)(this.FindName("endHrCombo")));
            this.searchBtn = ((System.Windows.Controls.Button)(this.FindName("searchBtn")));
            this.excelBtn = ((System.Windows.Controls.Button)(this.FindName("excelBtn")));
            this.chartBtn = ((System.Windows.Controls.Button)(this.FindName("chartBtn")));
            this.dataGrid = ((Infragistics.Controls.Grids.XamGrid)(this.FindName("dataGrid")));
            this.chartGridSplitter = ((System.Windows.Controls.GridSplitter)(this.FindName("chartGridSplitter")));
            this.damChartGrid = ((System.Windows.Controls.Grid)(this.FindName("damChartGrid")));
            this.DifferenceTextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("DifferenceTextBlock")));
            this.exportImageGridChart = ((System.Windows.Controls.Button)(this.FindName("exportImageGridChart")));
            this.viewAllGridChart = ((System.Windows.Controls.Button)(this.FindName("viewAllGridChart")));
            this.closeGridChart = ((System.Windows.Controls.Button)(this.FindName("closeGridChart")));
            this.gridChart = ((Infragistics.Controls.Charts.XamDataChart)(this.FindName("gridChart")));
            this.gridChartLegend = ((Infragistics.Controls.Charts.Legend)(this.FindName("gridChartLegend")));
            this.LoadingBar = ((System.Windows.Controls.BusyIndicator)(this.FindName("LoadingBar")));
        }
    }
}

