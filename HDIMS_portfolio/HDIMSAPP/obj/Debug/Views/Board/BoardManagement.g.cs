﻿#pragma checksum "D:\hdims\HDIMS\HDIMSAPP\Views\Board\BoardManagement.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "334C9C1AC005D7A4DC7C2A86CD859621"
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


namespace HDIMSAPP.Views.Board {
    
    
    public partial class BoardManagement : System.Windows.Controls.Page {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid SubLayoutRoot;
        
        internal System.Windows.Controls.Border border1;
        
        internal System.Windows.Controls.StackPanel stackPanel1;
        
        internal System.Windows.Controls.Image image1;
        
        internal System.Windows.Controls.TextBlock titleTextBlock;
        
        internal System.Windows.Controls.StackPanel SearchPanel;
        
        internal System.Windows.Controls.Button editAllBtn;
        
        internal System.Windows.Controls.Button addBtn;
        
        internal System.Windows.Controls.Button deleteBtn;
        
        internal System.Windows.Controls.Button saveBtn;
        
        internal System.Windows.Controls.Button searchBtn;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/HDIMSAPP;component/Views/Board/BoardManagement.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.SubLayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("SubLayoutRoot")));
            this.border1 = ((System.Windows.Controls.Border)(this.FindName("border1")));
            this.stackPanel1 = ((System.Windows.Controls.StackPanel)(this.FindName("stackPanel1")));
            this.image1 = ((System.Windows.Controls.Image)(this.FindName("image1")));
            this.titleTextBlock = ((System.Windows.Controls.TextBlock)(this.FindName("titleTextBlock")));
            this.SearchPanel = ((System.Windows.Controls.StackPanel)(this.FindName("SearchPanel")));
            this.editAllBtn = ((System.Windows.Controls.Button)(this.FindName("editAllBtn")));
            this.addBtn = ((System.Windows.Controls.Button)(this.FindName("addBtn")));
            this.deleteBtn = ((System.Windows.Controls.Button)(this.FindName("deleteBtn")));
            this.saveBtn = ((System.Windows.Controls.Button)(this.FindName("saveBtn")));
            this.searchBtn = ((System.Windows.Controls.Button)(this.FindName("searchBtn")));
            this.damGrid = ((Infragistics.Controls.Grids.XamGrid)(this.FindName("damGrid")));
            this.LoadingBar = ((System.Windows.Controls.BusyIndicator)(this.FindName("LoadingBar")));
        }
    }
}

