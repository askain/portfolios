﻿#pragma checksum "C:\Projects\HDIMS\HDIMSAPP\Views\Statistics\DamDataVerifyStat.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "5963D085179A381BB54C83E3AE39DC2F"
//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.261
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
    
    
    public partial class DamDataVerifyStat : System.Windows.Controls.Page {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid SubLayoutRoot;
        
        internal System.Windows.Controls.Border border1;
        
        internal System.Windows.Controls.StackPanel stackPanel1;
        
        internal System.Windows.Controls.Image image1;
        
        internal System.Windows.Controls.TextBlock titleTextBlock;
        
        internal System.Windows.Controls.StackPanel SearchPanel;
        
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
        
        internal System.Windows.Controls.Button searchBtn;
        
        internal System.Windows.Controls.Button AbnormDamLegendBtn;
        
        internal System.Windows.Controls.Button excelBtn;
        
        internal System.Windows.Controls.Button cimsBtn;
        
        internal Infragistics.Controls.Grids.XamGrid damGrid;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/HDIMSAPP;component/Views/Statistics/DamDataVerifyStat.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.SubLayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("SubLayoutRoot")));
            this.border1 = ((System.Windows.Controls.Border)(this.FindName("border1")));
            this.stackPanel1 = ((System.Windows.Controls.StackPanel)(this.FindName("stackPanel1")));
            this.image1 = ((System.Windows.Controls.Image)(this.FindName("image1")));
            this.titleTextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("titleTextBlock")));
            this.SearchPanel = ((System.Windows.Controls.StackPanel)(this.FindName("SearchPanel")));
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
            this.searchBtn = ((System.Windows.Controls.Button)(this.FindName("searchBtn")));
            this.AbnormDamLegendBtn = ((System.Windows.Controls.Button)(this.FindName("AbnormDamLegendBtn")));
            this.excelBtn = ((System.Windows.Controls.Button)(this.FindName("excelBtn")));
            this.cimsBtn = ((System.Windows.Controls.Button)(this.FindName("cimsBtn")));
            this.damGrid = ((Infragistics.Controls.Grids.XamGrid)(this.FindName("damGrid")));
            this.LoadingBar = ((System.Windows.Controls.BusyIndicator)(this.FindName("LoadingBar")));
        }
    }
}

