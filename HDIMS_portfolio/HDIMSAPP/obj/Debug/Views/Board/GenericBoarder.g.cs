﻿#pragma checksum "C:\Projects\HDIMS\HDIMSAPP\Views\Board\GenericBoarder.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "16ECE7DC0827F07D9D8AB739E88CB3A2"
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


namespace HDIMSAPP.Views.Board {
    
    
    public partial class GenericBoard : System.Windows.Controls.Page {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Grid SubLayoutRoot;
        
        internal System.Windows.Controls.Border BoarderTop;
        
        internal System.Windows.Controls.TextBlock txtTitle;
        
        internal Infragistics.Controls.Menus.XamMenu TopMenu;
        
        internal System.Windows.Controls.Border ContentFrame;
        
        internal System.Windows.Controls.TextBlock txtBorderName;
        
        internal Infragistics.Controls.Grids.XamGrid damGrid;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/HDIMSAPP;component/Views/Board/GenericBoarder.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.SubLayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("SubLayoutRoot")));
            this.BoarderTop = ((System.Windows.Controls.Border)(this.FindName("BoarderTop")));
            this.txtTitle = ((System.Windows.Controls.TextBlock)(this.FindName("txtTitle")));
            this.TopMenu = ((Infragistics.Controls.Menus.XamMenu)(this.FindName("TopMenu")));
            this.ContentFrame = ((System.Windows.Controls.Border)(this.FindName("ContentFrame")));
            this.txtBorderName = ((System.Windows.Controls.TextBlock)(this.FindName("txtBorderName")));
            this.damGrid = ((Infragistics.Controls.Grids.XamGrid)(this.FindName("damGrid")));
        }
    }
}

