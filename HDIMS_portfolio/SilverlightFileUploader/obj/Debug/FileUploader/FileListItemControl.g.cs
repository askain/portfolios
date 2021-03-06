﻿#pragma checksum "D:\hdims\HDIMS\SilverlightFileUploader\FileUploader\FileListItemControl.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E235EDECD19201C4427F1CBAFFE4EC58"
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
    
    
    public partial class FileListItemControl : System.Windows.Controls.UserControl {
        
        internal System.Windows.Media.Animation.Storyboard sbProgress;
        
        internal System.Windows.Media.Animation.SplineDoubleKeyFrame sbProgressFrame;
        
        internal System.Windows.Controls.Border LayoutRoot;
        
        internal System.Windows.VisualStateGroup UploadStates;
        
        internal System.Windows.VisualState Pending;
        
        internal System.Windows.VisualState Uploading;
        
        internal System.Windows.VisualState Canceling;
        
        internal System.Windows.VisualState Canceled;
        
        internal System.Windows.VisualState Error;
        
        internal System.Windows.VisualState Finished;
        
        internal System.Windows.VisualState Processing;
        
        internal System.Windows.Controls.Grid panelFile;
        
        internal System.Windows.Controls.TextBlock txtName;
        
        internal System.Windows.Controls.TextBlock txtState;
        
        internal System.Windows.Controls.TextBlock txtSize;
        
        internal System.Windows.Controls.ProgressBar progressPercent;
        
        internal System.Windows.Controls.Button btnRemove;
        
        internal System.Windows.Controls.Button btnCancel;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/SilverlightFileUploader;component/FileUploader/FileListItemControl.xaml", System.UriKind.Relative));
            this.sbProgress = ((System.Windows.Media.Animation.Storyboard)(this.FindName("sbProgress")));
            this.sbProgressFrame = ((System.Windows.Media.Animation.SplineDoubleKeyFrame)(this.FindName("sbProgressFrame")));
            this.LayoutRoot = ((System.Windows.Controls.Border)(this.FindName("LayoutRoot")));
            this.UploadStates = ((System.Windows.VisualStateGroup)(this.FindName("UploadStates")));
            this.Pending = ((System.Windows.VisualState)(this.FindName("Pending")));
            this.Uploading = ((System.Windows.VisualState)(this.FindName("Uploading")));
            this.Canceling = ((System.Windows.VisualState)(this.FindName("Canceling")));
            this.Canceled = ((System.Windows.VisualState)(this.FindName("Canceled")));
            this.Error = ((System.Windows.VisualState)(this.FindName("Error")));
            this.Finished = ((System.Windows.VisualState)(this.FindName("Finished")));
            this.Processing = ((System.Windows.VisualState)(this.FindName("Processing")));
            this.panelFile = ((System.Windows.Controls.Grid)(this.FindName("panelFile")));
            this.txtName = ((System.Windows.Controls.TextBlock)(this.FindName("txtName")));
            this.txtState = ((System.Windows.Controls.TextBlock)(this.FindName("txtState")));
            this.txtSize = ((System.Windows.Controls.TextBlock)(this.FindName("txtSize")));
            this.progressPercent = ((System.Windows.Controls.ProgressBar)(this.FindName("progressPercent")));
            this.btnRemove = ((System.Windows.Controls.Button)(this.FindName("btnRemove")));
            this.btnCancel = ((System.Windows.Controls.Button)(this.FindName("btnCancel")));
        }
    }
}

