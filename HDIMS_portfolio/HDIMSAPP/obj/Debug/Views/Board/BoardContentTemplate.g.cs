﻿#pragma checksum "D:\hdims\HDIMS\HDIMSAPP\Views\Board\BoardContentTemplate.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "579447438CA68893E943BACE8F4C4043"
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
using Vci.Silverlight.FileUploader;


namespace HDIMSAPP.Views.Board {
    
    
    public partial class BoardContentTemplate : System.Windows.Controls.ChildWindow {
        
        internal System.Windows.Media.SolidColorBrush ForgroundColor;
        
        internal System.Windows.Media.SolidColorBrush BackgroundColor;
        
        internal System.Windows.Media.SolidColorBrush LineColor;
        
        internal HDIMSAPP.Common.Converter.DateTimeConverter DateTimeConv;
        
        internal HDIMSAPP.Common.Converter.ColorConverter ColorConv;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.Border TopBoard;
        
        internal System.Windows.Controls.Button editBtn;
        
        internal System.Windows.Controls.Button saveBtn;
        
        internal System.Windows.Controls.Button oasisBtn;
        
        internal System.Windows.Controls.Button openOasisDocBtn;
        
        internal System.Windows.Controls.Button deleteBtn;
        
        internal System.Windows.Controls.Button masterBtn;
        
        internal System.Windows.Controls.Button closeBtn;
        
        internal System.Windows.Controls.ScrollViewer ContentScrollVeiwer;
        
        internal System.Windows.Controls.TextBox textBoxTitle;
        
        internal System.Windows.Controls.Border OptionBorder;
        
        internal System.Windows.Controls.CheckBox ChkSendMail;
        
        internal System.Windows.Controls.Border CategoryBorder;
        
        internal Infragistics.Controls.Editors.XamComboEditor CategoryCombo;
        
        internal System.Windows.Controls.Border FilUploaderBorder;
        
        internal Vci.Silverlight.FileUploader.FileUploaderControl FileUploader;
        
        internal System.Windows.Controls.TextBox textBoxContent;
        
        internal System.Windows.Controls.StackPanel WholeReplyPanel;
        
        internal System.Windows.Controls.StackPanel ReadReplyPanel;
        
        internal System.Windows.Controls.Image OpenReplyImg;
        
        internal System.Windows.Controls.Image CloseReplyImg;
        
        internal System.Windows.Controls.ItemsControl ReplyItemsControl;
        
        internal System.Windows.Controls.StackPanel WriteReplyPanel;
        
        internal System.Windows.Controls.TextBox textBoxReply;
        
        internal System.Windows.Controls.Button replyBtn;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/HDIMSAPP;component/Views/Board/BoardContentTemplate.xaml", System.UriKind.Relative));
            this.ForgroundColor = ((System.Windows.Media.SolidColorBrush)(this.FindName("ForgroundColor")));
            this.BackgroundColor = ((System.Windows.Media.SolidColorBrush)(this.FindName("BackgroundColor")));
            this.LineColor = ((System.Windows.Media.SolidColorBrush)(this.FindName("LineColor")));
            this.DateTimeConv = ((HDIMSAPP.Common.Converter.DateTimeConverter)(this.FindName("DateTimeConv")));
            this.ColorConv = ((HDIMSAPP.Common.Converter.ColorConverter)(this.FindName("ColorConv")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TopBoard = ((System.Windows.Controls.Border)(this.FindName("TopBoard")));
            this.editBtn = ((System.Windows.Controls.Button)(this.FindName("editBtn")));
            this.saveBtn = ((System.Windows.Controls.Button)(this.FindName("saveBtn")));
            this.oasisBtn = ((System.Windows.Controls.Button)(this.FindName("oasisBtn")));
            this.openOasisDocBtn = ((System.Windows.Controls.Button)(this.FindName("openOasisDocBtn")));
            this.deleteBtn = ((System.Windows.Controls.Button)(this.FindName("deleteBtn")));
            this.masterBtn = ((System.Windows.Controls.Button)(this.FindName("masterBtn")));
            this.closeBtn = ((System.Windows.Controls.Button)(this.FindName("closeBtn")));
            this.ContentScrollVeiwer = ((System.Windows.Controls.ScrollViewer)(this.FindName("ContentScrollVeiwer")));
            this.textBoxTitle = ((System.Windows.Controls.TextBox)(this.FindName("textBoxTitle")));
            this.OptionBorder = ((System.Windows.Controls.Border)(this.FindName("OptionBorder")));
            this.ChkSendMail = ((System.Windows.Controls.CheckBox)(this.FindName("ChkSendMail")));
            this.CategoryBorder = ((System.Windows.Controls.Border)(this.FindName("CategoryBorder")));
            this.CategoryCombo = ((Infragistics.Controls.Editors.XamComboEditor)(this.FindName("CategoryCombo")));
            this.FilUploaderBorder = ((System.Windows.Controls.Border)(this.FindName("FilUploaderBorder")));
            this.FileUploader = ((Vci.Silverlight.FileUploader.FileUploaderControl)(this.FindName("FileUploader")));
            this.textBoxContent = ((System.Windows.Controls.TextBox)(this.FindName("textBoxContent")));
            this.WholeReplyPanel = ((System.Windows.Controls.StackPanel)(this.FindName("WholeReplyPanel")));
            this.ReadReplyPanel = ((System.Windows.Controls.StackPanel)(this.FindName("ReadReplyPanel")));
            this.OpenReplyImg = ((System.Windows.Controls.Image)(this.FindName("OpenReplyImg")));
            this.CloseReplyImg = ((System.Windows.Controls.Image)(this.FindName("CloseReplyImg")));
            this.ReplyItemsControl = ((System.Windows.Controls.ItemsControl)(this.FindName("ReplyItemsControl")));
            this.WriteReplyPanel = ((System.Windows.Controls.StackPanel)(this.FindName("WriteReplyPanel")));
            this.textBoxReply = ((System.Windows.Controls.TextBox)(this.FindName("textBoxReply")));
            this.replyBtn = ((System.Windows.Controls.Button)(this.FindName("replyBtn")));
            this.LoadingBar = ((System.Windows.Controls.BusyIndicator)(this.FindName("LoadingBar")));
        }
    }
}

