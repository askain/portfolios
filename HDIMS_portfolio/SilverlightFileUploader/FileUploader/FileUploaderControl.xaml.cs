/*
Copyright 2003-2009 Virtual Chemistry, Inc. (VCI)
http://www.virtualchemistry.com
Using .Net, Silverlight, SharePoint and more to solve your tough problems in web-based data management.

Author: Peter Coley
*/

using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Linq;
using System.IO;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Net;
using Newtonsoft.Json;

namespace Vci.Silverlight.FileUploader
{
    [ScriptableType]
    public partial class FileUploaderControl : UserControl
    {
        private WebClient client;

        private bool _multiSelect = true;
        protected FileCollection _files;

        public FileCollection Files { get { return _files; } }

        /// <summary>
        /// File filter -- see documentation for .net OpenFileDialog for details.
        /// </summary>
        public string FileFilter { get; set; }

        /// <summary>
        /// Path to the upload handler.
        /// </summary>
        public string UploadHandlerPath { get; set; }

        /// <summary>
        /// Maximum file size of an uploaded file in KB.  Defaults to no limit.
        /// </summary>
        public long MaxFileSizeKB { get; set; }

        /// <summary>
        /// Chunk size in megabytes.  Defaults to 3 MB.
        /// </summary>
        public long ChunkSizeMB { get; set; }

        /// <summary>
        /// True if multiple files can be selected.  Defaults to true.
        /// </summary>
        public bool MultiSelect
        {
            get { return _multiSelect; }
            set
            {
                _multiSelect = value;
            }
        }

        public string BoardCd { get; set; }
        private string _ContentCd { get; set; }
        public string ContentCd
        {
            get { return _ContentCd; }
            set
            {
                _ContentCd = value;
                txtEmptyMessage.Text = "파일을 업로드 하시려면 [파일선택] 버튼을 눌러주세요";
            }
        }

        private bool _CanSelectFiles = true;
        public bool CanSelectFiles
        {
            set
            {
                _CanSelectFiles = value;
                btnChoose.IsEnabled = _CanSelectFiles;
            }
        }

        private bool _CanDeleteFiles = true;
        public bool CanDeleteFiles
        {
            set
            {
                _CanDeleteFiles = value;
                foreach (UserFile ic in icFiles.Items)
                {
                    ic.CanDelete = _CanDeleteFiles;
                    //FileListItemControl control = (FileListItemControl)VisualTreeHelper.GetChild((ContentPresenter)icFiles.ItemContainerGenerator.ContainerFromItem(ic),0)
                    //control.btnRemove.IsEnabled = _CanDeleteFiles;
                }
            }
        }

        public bool IsFinished {
            get {
                return _files.FirstOrDefault(f => !f.IsCompleted) == null;
            }
        }


        /// <summary>
        /// Access to the scroll viewer -- used by silverlight app that needs to host this in windowless mode
        /// and fix mouse capture bugs.
        /// </summary>
        public ScrollViewer ScrollViewer { get { return svFiles; } }

        /// <summary>
        /// This event is raised when the user selects 1 or more files and begins uploading.
        /// </summary>
        [ScriptableMember]
        public event EventHandler UploadStarted;

        /// <summary>
        /// All File Uploaded 
        /// </summary>
        [ScriptableMember]
        public event EventHandler UploadEnded;

        public FileUploaderControl()
        {
            // Required to initialize variables
            InitializeComponent();

            MaxFileSizeKB = -1;
            ChunkSizeMB = 3;

            MultiSelect = true;

            _files = new FileCollection { MaxUploads = 2 };

            icFiles.ItemsSource = _files;
            progressPercent.DataContext = _files;
            txtUploadedBytes.DataContext = _files;
            txtPercent.DataContext = _files;

            this.Loaded += new RoutedEventHandler(FileUploaderControl_Loaded);

            _files.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(_files_CollectionChanged);
            _files.PercentageChanged += new EventHandler(_files_PercentageChanged);
            _files.AllFilesFinished += new EventHandler(_files_AllFilesFinished);
            _files.ErrorOccurred += new EventHandler<UploadErrorOccurredEventArgs>(_files_ErrorOccurred);
        }

        void _files_ErrorOccurred(object sender, UploadErrorOccurredEventArgs e)
        {
            // not sure why this is necessary sometimes... the progress bar will get stuck after an error sometimes
            _files_PercentageChanged(sender, null);
        }

        void _files_PercentageChanged(object sender, EventArgs e)
        {
            // if the percentage is decreasing, don't use an animation
            if (_files.Percentage < progressPercent.Value)
                progressPercent.Value = _files.Percentage;
            else
            {
                sbProgressFrame.Value = _files.Percentage;
                sbProgress.Begin();
            }
        }

        void _files_AllFilesFinished(object sender, EventArgs e)
        {
            VisualStateManager.GoToState(this, "Finished", true);
            OnUploadEnded();
        }

        void _files_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (_files.Count == 0)
            {
                VisualStateManager.GoToState(this, "Empty", true);
                btnChoose.IsEnabled = _CanSelectFiles;
                btnUpload.IsEnabled = false;
            }
            else
            {
                // disable the upload button if only a single file can be uploaded
                btnChoose.IsEnabled = _CanSelectFiles && MultiSelect;

                if (_files.FirstOrDefault(f => !f.IsCompleted) != null)
                {
                    VisualStateManager.GoToState(this, "Uploading", true);
                    if(ContentCd != null) btnUpload.IsEnabled = true;
                    else btnUpload.IsEnabled = false;
                }
                else
                {
                    VisualStateManager.GoToState(this, "Finished", true);
                    btnUpload.IsEnabled = false;
                }
            }
        }

        private void FileUploaderControl_Loaded(object sender, RoutedEventArgs e)
        {
            VisualStateManager.GoToState(this, "Empty", false);
        }

        /// <summary>
        /// Initialize this control from an InitParams collection provided to the silverlight plugin.  Also sets this control up
        /// to be accessed via javascript.
        /// </summary>
        /// <param name="InitParams"></param>
        public void InitializeFromInitParams(IDictionary<string, string> initParams)
        {
            HtmlPage.RegisterScriptableObject("UploaderControl", this);
            HtmlPage.RegisterScriptableObject("Files", _files);

            if (initParams.ContainsKey("UploadedFileProcessorType") && !string.IsNullOrEmpty(initParams["UploadedFileProcessorType"]))
                _files.UploadedFileProcessorType = initParams["UploadedFileProcessorType"];

            if (initParams.ContainsKey("MaxUploads") && !string.IsNullOrEmpty(initParams["MaxUploads"]))
                _files.MaxUploads = int.Parse(initParams["MaxUploads"]);

            if (initParams.ContainsKey("MaxFileSizeKB") && !string.IsNullOrEmpty(initParams["MaxFileSizeKB"]))
                MaxFileSizeKB = long.Parse(initParams["MaxFileSizeKB"]);

            if (initParams.ContainsKey("ChunkSizeMB") && !string.IsNullOrEmpty(initParams["ChunkSizeMB"]))
                ChunkSizeMB = long.Parse(initParams["ChunkSizeMB"]);

            if (initParams.ContainsKey("FileFilter") && !string.IsNullOrEmpty(initParams["FileFilter"]))
                FileFilter = initParams["FileFilter"];

            if (initParams.ContainsKey("UploadHandlerPath") && !string.IsNullOrEmpty(initParams["UploadHandlerPath"]))
                UploadHandlerPath = initParams["UploadHandlerPath"];

            if (initParams.ContainsKey("MultiSelect") && !string.IsNullOrEmpty(initParams["MultiSelect"]))
                MultiSelect = Convert.ToBoolean(initParams["MultiSelect"].ToLower());

            if (initParams.ContainsKey("BoardCd") && !string.IsNullOrEmpty(initParams["BoardCd"]))
                BoardCd = initParams["BoardCd"];

            if (initParams.ContainsKey("ContentCd") && !string.IsNullOrEmpty(initParams["ContentCd"]))
                ContentCd = initParams["ContentCd"];

            if(BoardCd != null && ContentCd != null)
                FetchFiles();
        }

        private void btnChoose_Click(object sender, RoutedEventArgs e)
        {
            SelectUserFiles();
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            ClearFileList();
        }

        #region == 기존 파일 가져오기 ==
        private void FetchFiles()
        {
            string url = string.Format("/FileUploader/GetFileList?BoardCd={0}&ContentCd={1}", BoardCd, ContentCd);
            client = new WebClient();
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(FetchFilesCompleted);
            client.OpenReadAsync(new Uri(url, UriKind.Relative));
        }
        private void FetchFilesCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null) return;
            string strJson = "";
            Stream stream = e.Result;
            using (StreamReader reader = new StreamReader(stream))
            {
                strJson = reader.ReadToEnd();
            }
            IList<IDictionary<string, string>> FileInfoList = JsonConvert.DeserializeObject<IList<IDictionary<string, string>>>(strJson);
            foreach (IDictionary<string,string> userFile in FileInfoList)
            {
                try
                {
                    UserFile u = new UserFile();
                    //u.FileName = userFile["FileName"];
                    u.Guid = userFile["Guid"];
                    u.FileSize = long.Parse(userFile["FileSize"]);
                    u.BytesUploaded = u.FileSize;
                    //u.FileStream = file.OpenRead();
                    u.UIDispatcher = this.Dispatcher;
                    //u.HttpUploader = true;
                    u.UploadHandlerName = UploadHandlerPath;
                    u.ChunkSizeMB = ChunkSizeMB;
                    u.State = Constants.FileStates.Finished;
                    u.BoardCd = BoardCd;
                    u.ContentCd = ContentCd;
                    _files.Add(u);
                }
                catch (Exception ex)
                {
                    // 파일을 가져오는 중 에러가 발생하면 그 파일은 무시함.
                }
            }
            _files.BytesUploaded = 100;

            
        }
        #endregion

        /// <summary>
        /// Open the select file dialog, and begin uploading files.
        /// </summary>
        private void SelectUserFiles()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = MultiSelect;

            try
            {
                // Check the file filter (filter is used to filter file extensions to select, for example only .jpg files)
                if (!string.IsNullOrEmpty(FileFilter))
                    ofd.Filter = FileFilter;
            }
            catch (ArgumentException ex)
            {
                // User supplied a wrong configuration filter
                MessageBox.Show("허용가능한 파일 형태가 잘못 설정되어있습니다. 현재설정된 파일 필터" + Environment.NewLine + FileFilter, "경고", MessageBoxButton.OK);
                //throw new Exception("허용가능한 파일 형태가 잘못 설정되어있습니다.", ex);
            }

            if (ofd.ShowDialog() == true)
            {
                foreach (FileInfo file in ofd.Files)
                {
                    // create an object that represents a file being uploaded
                    UserFile userFile = new UserFile();
                    userFile.FileName = file.Name;
                    userFile.FileStream = file.OpenRead();
                    userFile.UIDispatcher = this.Dispatcher;
                    userFile.HttpUploader = true;
                    userFile.UploadHandlerName = UploadHandlerPath;
                    userFile.ChunkSizeMB = ChunkSizeMB;
                    userFile.BoardCd = BoardCd;
                    userFile.ContentCd = ContentCd;

                    userFile.CanDelete = _CanDeleteFiles;

                    // check the file size limit
                    if (MaxFileSizeKB == -1 || userFile.FileSize <= MaxFileSizeKB * 1024)
                        _files.Add(userFile);
                    else
                        HtmlPage.Window.Alert(userFile.FileName + " 는 파일용량 제한을 초과하였습니다. " + (MaxFileSizeKB).ToString() + "KB.");
                }
            }

            //// start uploading the selected files
            //if (_files.Count > 0)
            //{
            //    OnUploadStarted();
            //    _files.UploadFiles();
            //}
        }

        /// <summary>
        /// Clear the file list
        /// </summary>
        [ScriptableMember]
        public void ClearFileList()
        {
            // clear all files that are completed (canceled, finished, error), or pending
            _files.Where(f => f.IsCompleted || f.State == Constants.FileStates.Pending).ToList().ForEach(f => _files.Remove(f));
        }

        private void OnUploadStarted()
        {
            EventHandler handler = UploadStarted;
            if (handler != null)
                handler(this, null);
        }

        private void OnUploadEnded()
        {
            EventHandler handler = UploadEnded;
            if (handler != null)
                handler(this, null);
        }

        public void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            //ContentCd
            foreach (UserFile file in _files)
            {
                file.ContentCd = this.ContentCd;
            }

            OnUploadStarted();
            _files.UploadFiles();
        }

    }

    public class PercentConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string percent = "0%";

            if (value != null)
                percent = (int)value + "%";

            return percent;
        }

        // only use one-way binding for percentages
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}