﻿#pragma checksum "D:\hdims\HDIMS\SilverlightFileUploader\FileUploader\FileUploaderControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A664AC03B18BC2A247F196F4F9A3CE8A"
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


namespace Vci.Silverlight.FileUploader {
    
    
    public partial class FileUploaderControl : System.Windows.Controls.UserControl {
        
        internal System.Windows.Media.Animation.Storyboard sbProgress;
        
        internal System.Windows.Media.Animation.SplineDoubleKeyFrame sbProgressFrame;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.VisualStateGroup UploadingStates;
        
        internal System.Windows.VisualState Empty;
        
        internal System.Windows.VisualState Uploading;
        
        internal System.Windows.VisualState Finished;
        
        internal System.Windows.Controls.ScrollViewer svFiles;
        
        internal System.Windows.Controls.ItemsControl icFiles;
        
        internal System.Windows.Controls.Button btnChoose;
        
        internal System.Windows.Controls.Button btnUpload;
        
        internal System.Windows.Controls.Button btnClear;
        
        internal System.Windows.Controls.ProgressBar progressPercent;
        
        internal System.Windows.Controls.TextBlock txtUploadedBytes;
        
        internal System.Windows.Controls.TextBlock txtEmptyMessage;
        
        internal System.Windows.Controls.TextBlock txtPercent;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/SilverlightFileUploader;component/FileUploader/FileUploaderControl.xaml", System.UriKind.Relative));
            this.sbProgress = ((System.Windows.Media.Animation.Storyboard)(this.FindName("sbProgress")));
            this.sbProgressFrame = ((System.Windows.Media.Animation.SplineDoubleKeyFrame)(this.FindName("sbProgressFrame")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.UploadingStates = ((System.Windows.VisualStateGroup)(this.FindName("UploadingStates")));
            this.Empty = ((System.Windows.VisualState)(this.FindName("Empty")));
            this.Uploading = ((System.Windows.VisualState)(this.FindName("Uploading")));
            this.Finished = ((System.Windows.VisualState)(this.FindName("Finished")));
            this.svFiles = ((System.Windows.Controls.ScrollViewer)(this.FindName("svFiles")));
            this.icFiles = ((System.Windows.Controls.ItemsControl)(this.FindName("icFiles")));
            this.btnChoose = ((System.Windows.Controls.Button)(this.FindName("btnChoose")));
            this.btnUpload = ((System.Windows.Controls.Button)(this.FindName("btnUpload")));
            this.btnClear = ((System.Windows.Controls.Button)(this.FindName("btnClear")));
            this.progressPercent = ((System.Windows.Controls.ProgressBar)(this.FindName("progressPercent")));
            this.txtUploadedBytes = ((System.Windows.Controls.TextBlock)(this.FindName("txtUploadedBytes")));
            this.txtEmptyMessage = ((System.Windows.Controls.TextBlock)(this.FindName("txtEmptyMessage")));
            this.txtPercent = ((System.Windows.Controls.TextBlock)(this.FindName("txtPercent")));
        }
    }
}
