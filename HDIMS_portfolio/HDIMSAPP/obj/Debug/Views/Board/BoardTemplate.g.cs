﻿#pragma checksum "D:\hdims\HDIMS\HDIMSAPP\Views\Board\BoardTemplate.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "57C4EC0E36ACC69F4F3FBDE8A3256168"
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


namespace HDIMSAPP.Views.Board {
    
    
    public partial class BoardTemplate : System.Windows.Controls.Page {
        
        internal HDIMSAPP.Common.Converter.DateTimeConverter DateTimeConv;
        
        internal System.Windows.Media.SolidColorBrush BackgroundColor;
        
        internal System.Windows.Style CustomPager1;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.TextBlock txtBorderName;
        
        internal Infragistics.Controls.Editors.XamComboEditor CategoryCombo;
        
        internal Infragistics.Controls.Editors.XamComboEditor ConditionCombo;
        
        internal System.Windows.Controls.TextBox ConditionTbox;
        
        internal System.Windows.Controls.Button reloadBtn;
        
        internal System.Windows.Controls.Button writeBtn;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/HDIMSAPP;component/Views/Board/BoardTemplate.xaml", System.UriKind.Relative));
            this.DateTimeConv = ((HDIMSAPP.Common.Converter.DateTimeConverter)(this.FindName("DateTimeConv")));
            this.BackgroundColor = ((System.Windows.Media.SolidColorBrush)(this.FindName("BackgroundColor")));
            this.CustomPager1 = ((System.Windows.Style)(this.FindName("CustomPager1")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.txtBorderName = ((System.Windows.Controls.TextBlock)(this.FindName("txtBorderName")));
            this.CategoryCombo = ((Infragistics.Controls.Editors.XamComboEditor)(this.FindName("CategoryCombo")));
            this.ConditionCombo = ((Infragistics.Controls.Editors.XamComboEditor)(this.FindName("ConditionCombo")));
            this.ConditionTbox = ((System.Windows.Controls.TextBox)(this.FindName("ConditionTbox")));
            this.reloadBtn = ((System.Windows.Controls.Button)(this.FindName("reloadBtn")));
            this.writeBtn = ((System.Windows.Controls.Button)(this.FindName("writeBtn")));
            this.damGrid = ((Infragistics.Controls.Grids.XamGrid)(this.FindName("damGrid")));
            this.LoadingBar = ((System.Windows.Controls.BusyIndicator)(this.FindName("LoadingBar")));
        }
    }
}

