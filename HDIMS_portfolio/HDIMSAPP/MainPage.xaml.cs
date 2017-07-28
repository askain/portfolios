using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using HDIMSAPP.Common;
using HDIMSAPP.Models;
using HDIMSAPP.Models.Common;
using Infragistics.Controls.Menus;

namespace HDIMSAPP
{
    public partial class MainPage : UserControl
    {
        private BackgroundWorker bw = new BackgroundWorker();

        private readonly string MenuUri = "/Common/MenuList/?useFlag=Y";
        private readonly string RequestAuthUri = "/Common/RequestAuth";
        private readonly string InsertUpdateHomeUri = "/ManAuthMng/InsertUpdateHome/?";
        private WebClient client;

        public MainPage()
        {
            InitializeComponent();

            // 무동작 자동 로그아웃
            UserInteractionMonitor.Current.RegisterIdleNotification(TimeSpan.FromHours(1), OnUserIdle);
        }

        #region == 자료수정권한 요청 ==
        private void RequestAuthBtn_Click(object sender, RoutedEventArgs e)
        {
            string uri = RequestAuthUri;

            client = new WebClient();
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(RequestAuth);
            client.OpenReadAsync(new Uri(uri, UriKind.Relative));
        }

        private void RequestAuth(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null) return;
        
            Stream stream = e.Result;
            string str = string.Empty;
            
            using (StreamReader reader = new StreamReader(stream))
            {
                str = reader.ReadToEnd();
            }
            
            if(str.Equals("0") == true)
            {
                MessageBox.Show("권한 수정이 담당자에게 요청되었습니다.");    
            }
            else
            {
                MessageBox.Show("권한 수정 요청중 에러가 발생하였습니다.");
            }            
        }
        #endregion

        #region == 기본페이지로 저장 ==
        private void SaveAsHomeBtn_Click(object sender, RoutedEventArgs e)
        {
            string uri = string.Format("{0}home={1}", InsertUpdateHomeUri, ContentFrame.Source.OriginalString);

            client = new WebClient();
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(InsertUpdateHomeCompleted);
            client.OpenReadAsync(new Uri(uri, UriKind.Relative));
        }
        private void InsertUpdateHomeCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null) return;

            Stream stream = e.Result;
            string str = string.Empty;

            using (StreamReader reader = new StreamReader(stream))
            {
                str = reader.ReadToEnd();
            }

            if (str.Equals("1") == true)
            {
                MessageBox.Show("기본페이지로 저장되었습니다.");
            }
            else
            {
                MessageBox.Show("기본페이지로 저장하지 못 했습니다.");
            }
        }
        #endregion

        #region == 무동작 로그아웃 ==
        private void OnUserIdle(object sender, IdleEventArgs ex)
        {
            if(ex.IsIdle == true) {
                Logout();
                MessageBox.Show("1시간 동안 조작이 없었기 때문에 로그인 페이지로 이동합니다.");
                //MessageBox.Show("ex.IsIdle:" + ex.IsIdle.ToString() + " / ex.IdleTime:" + ex.IdleTime.ToString());
            }
            
        }
        #endregion

        //private void LayoutRoot_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.F12 && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
        //    {
        //        ToggleFullScreen(true);
        //    }
        //    if (e.Key == Key.Escape)
        //    {
        //        ToggleFullScreen(false);
        //    }
        //}

        void SubLayoutRoot_LayoutUpdated(object sender, System.EventArgs e)
        {
            LeftOutlookBar.SelectedAreaMinHeight = SubLayoutRoot.ActualHeight-30;
            //MessageBox.Show(LayoutRoot.ActualHeight + ":" + LayoutRoot.Height + ":" + SubLayoutRoot.Height + ":" + SubLayoutRoot.ActualHeight);
            //if (SubLayoutRoot != null && SubLayoutRoot.ActualWidth > 24)
            //{
            //    SubLayoutRoot.ColumnDefinitions[0].MaxWidth = SubLayoutRoot.ActualWidth - this.splitter.ActualWidth - SubLayoutRoot.ColumnDefinitions[2].ActualWidth;
            //    SubLayoutRoot.ColumnDefinitions[2].Width = new GridLength(SubLayoutRoot.ActualWidth - this.splitter.ActualWidth - SubLayoutRoot.ColumnDefinitions[0].ActualWidth);
            //}
        }

        #region == 메뉴 리스트 가져오기 ==
        private void GetMenuList()
        {
            string _uri = MenuUri + "&authcode=00";
            client = new WebClient();
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(GetMenuListCompleted);
            client.OpenReadAsync(new Uri(_uri, UriKind.Relative));
        }

        void GetMenuListCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null) return;
            Stream str = e.Result;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IList<MenuModel>));
            IList<MenuModel> _menuList = (IList<MenuModel>)ser.ReadObject(str);
            //TopMenu
            CreateTopMenuList(_menuList);

            ////LeftMenu
            OutlookBarGroup _menuBar = LeftOutlookBar.Groups["hdimsMenuBar"];
            XamDataTree _menuTree = _menuBar.FindName("LeftMenuTree") as XamDataTree;
            _menuTree.Loaded += new RoutedEventHandler(_menuTree_Loaded);
            _menuTree.ItemsSource = _menuList;

            #region testcode
            //if (_menuTree == null)
            //{
            //    MessageBox.Show("_menuTree is null");
            //}

            //if (LeftMenuTree == null)
            //{
            //    MessageBox.Show("LeftMenuTree is null");
            //}

            //if (this.FindName("LeftMenuTree") == null)
            //{
            //    MessageBox.Show("this.FindName(LeftMenuTree) is Null");
            //}

            ////((Infragistics.Controls.Menus.XamDataTree)(this.FindName("LeftMenuTree"))).ItemsSource = _menuList;
            #endregion

        }
        #endregion

        #region == 상당 메뉴 리스트 만들기 ==
        private void CreateTopMenuList(IList<MenuModel> _menuList)
        {
            IList<string> exceptIds = new List<string>();
            exceptIds.Add("18"); //시스템정보
            exceptIds.Add("73"); //내정보.
            foreach (MenuModel _parent in _menuList)
            {
                if (exceptIds.Contains(_parent.id)) continue;

                XamMenuItem _pItem = new XamMenuItem();
                string _pLink = ConvertLinkUri(_parent.link);
                _pItem.Style = this.Resources["MenuHeaderStyling"] as Style;
                _pItem.Header = _parent.text;
                _pItem.Cursor = Cursors.Hand;
                _pItem.Margin = new Thickness() { Left = 5, Top = 5, Right = 5, Bottom = 5 };

                if (_pLink != null && _pLink.Length > 0)
                {
                    _pItem.NavigationOnClick = true;
                    _pItem.NavigationElement = ContentFrame;
                    _pItem.NavigationUri = new Uri(_pLink, UriKind.Relative);
                }
                int _transformXPos = -120;
                if (!_parent.id.Equals("1"))
                {
                    int _subWidth = 0;
                    foreach (MenuModel _child in _parent.children)
                    {
                        //link 재조정 
                        string _cLink = ConvertLinkUri(_child.link);
                        XamMenuItem _cItem = new XamMenuItem();
                        _cItem.Header = _child.text;
                        _cItem.Style = this.Resources["ChildrenStyling"] as Style;
                        _cItem.NavigationOnClick = true;
                        _cItem.NavigationUri = new Uri(_cLink, UriKind.Relative);
                        _cItem.NavigationElement = ContentFrame;
                        _cItem.Cursor = Cursors.Hand;
                        _cItem.HorizontalContentAlignment = HorizontalAlignment.Center;
                        _cItem.Margin = new Thickness() { Left = 3, Top = 3, Right = 3, Bottom = 3 };
                        _pItem.Items.Add(_cItem);
                        _subWidth += _child.text.Length * 10 + 30;
                    }
                    _transformXPos = _subWidth / 2 - 20;
                }

                TopMenu.Items.Add(_pItem);
            }
        }
        #endregion

        public string ConvertLinkUri(string _link)
        {
            string _ret = "";
            if (_link.StartsWith("/R/#"))
            {
                _ret = _link.Replace("/R/#", "");
            }
            else
            {
                _ret = "/Home/" + HttpUtility.UrlEncode(_link);
            }
            return _ret;
        }

        #region == 우측하단 시계 ==
        private void startWorker()
        {
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.RunWorkerAsync();
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            int i = 0;
            while (true)
            {
                worker.ReportProgress(i);
                i = (i == 0) ? 1 : 0;
                System.Threading.Thread.Sleep(10000);
            }
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            int _pos = e.ProgressPercentage;
            CurrentDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
        }
        #endregion



        // 프레임에서 탐색한 후 현재 페이지를 나타내는 HyperlinkButton이 선택되었는지 확인합니다.
        private void ContentFrame_Navigated(object sender, NavigationEventArgs e)
        {
            
            //MessageBox.Show(e.Uri.OriginalString);
            /*
            foreach (UIElement child in LinksStackPanel.Children)
            {
                HyperlinkButton hb = child as HyperlinkButton;
                if (hb != null && hb.NavigateUri != null)
                {
                    if (hb.NavigateUri.ToString().Equals(e.Uri.ToString()))
                    {
                        VisualStateManager.GoToState(hb, "ActiveLink", true);
                    }
                    else
                    {
                        VisualStateManager.GoToState(hb, "InactiveLink", true);
                    }
                }
            }
             */
        }

        // 탐색하는 동안 오류가 발생하면 오류 창을 표시합니다.
        private void ContentFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            e.Handled = true;
            ChildWindow errorWin = new ErrorWindow(e.Uri);
            errorWin.Show();
        }

        #region == 좌측 메뉴 확장 & 축소 ==
        private void LeftOutlookBar_NavigationPaneMinimized(object sender, EventArgs e)
        {
            SubLayoutRoot.ColumnDefinitions[0].Width = new GridLength(LeftOutlookBar.MinimizedWidth);
        }

        private void LeftOutlookBar_NavigationPaneExpanded(object sender, EventArgs e)
        {
            SubLayoutRoot.ColumnDefinitions[0].Width = new GridLength(230);
        }
        #endregion


        #region == 사이트맵 ==
        private void SchedulerExecutorBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ContentFrame.Source = new Uri("/SchedulerExecutor", UriKind.Relative);
        }
        #endregion

        #region == 사이트맵 ==
        private void ViewSiteMapBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ContentFrame.Source = new Uri("/SiteMap", UriKind.Relative);
        }
        #endregion

        #region == 구굴어스 ==
        private void ViewRadarBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HtmlPage.Window.Invoke("popupRadar");
        }
        #endregion

        #region == 현 접속자 현황 ==
        private void CurrentUserBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HtmlPage.Window.Invoke("popupCurrentUsers");
        }
        #endregion

        #region == 로그아웃 ==
        private void Logout()
        {
            HtmlPage.Window.Invoke("logOut");
        }
        private void LogOutBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Logout();
        }

        private void LogOutBtn_Click(object sender, RoutedEventArgs e)
        {
            Logout();
        }
        #endregion


        #region == 좌측 메뉴 ==
        /// <summary>
        /// 매뉴 노드를 렌더링 할때 메뉴에 기본적으로 첫번째 노드를 선택하게 함.(페이지 이동은 없음)
        /// 사유 : 아무런 노드도 선택되어 있지 않을때 메뉴에 빈공간을 클릭하면 무조건 첫번째 노드를 선택한 것으로 되어서 페이지 이동 해버림. 그래서 노드 선택 ㄱㄱ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _menuTree_Loaded(object sender, RoutedEventArgs e)
        {
            XamDataTree _menuTree = sender as XamDataTree;
            _menuTree.ActiveNodeChanged -= new EventHandler<ActiveNodeChangedEventArgs>(LeftMenuTree_ActiveNodeChanged);
            //MessageBox.Show(((MenuModel)_menuTree.Nodes[0].Data).id);
            _menuTree.ActiveNode = _menuTree.Nodes[0];
            _menuTree.ActiveNodeChanged += new EventHandler<ActiveNodeChangedEventArgs>(LeftMenuTree_ActiveNodeChanged);
        }

        private void LeftMenuTree_ActiveNodeChanged(object sender, ActiveNodeChangedEventArgs e)
        {
            XamDataTreeNode _selNode = e.NewActiveTreeNode;
            
            if (_selNode != null && _selNode.Data != null)
            {
                MenuModel _selMenu = e.NewActiveTreeNode.Data as MenuModel;
                if (_selMenu.children != null)
                {

                    if (_selMenu.link != null && _selMenu.link.Length > 0)
                    {
                        string _selLink = ConvertLinkUri(_selMenu.link);
                        ContentFrame.Source = new Uri(_selLink, UriKind.Relative);
                    }

                    //if (_selNode.IsExpanded)
                    //{
                    //    MessageBox.Show("_selNode.IsExpanded == true");
                    //    _selNode.IsExpanded = false;
                    //}
                    //else
                    //{
                    //    MessageBox.Show("_selNode.IsExpanded == false");
                    //    _selNode.IsExpanded = true;
                    //    if (_selMenu.link != null && _selMenu.link.Length > 0)
                    //    {
                    //        string _selLink = ConvertLinkUri(_selMenu.link);
                    //        ContentFrame.Source = new Uri(_selLink, UriKind.Relative);
                    //    }
                    //}

                }
                else
                {
                    if (_selMenu.link != null && _selMenu.link.Length > 0)
                    {
                        string _selLink = ConvertLinkUri(_selMenu.link);
                        ContentFrame.Source = new Uri(_selLink, UriKind.Relative);
                    }
                }
            }
        }

        //private void FullScreenBtn_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    ToggleFullScreen(!Application.Current.Host.Content.IsFullScreen);
        //}

        //private void ToggleFullScreen(bool _tryFull)
        //{
        //    if (_tryFull)
        //    {
        //        Application.Current.Host.Content.IsFullScreen = true;
        //        FullScreenBtn.Text = "전체화면취소";
        //    }
        //    else
        //    {
        //        Application.Current.Host.Content.IsFullScreen = false;
        //        FullScreenBtn.Text = "전체화면보기";
        //    }
        //}

        private void LeftOutlookBar_SelectedGroupPopupOpening(object sender, Infragistics.CancellableEventArgs e)
        {
            e.Cancel = true;
            LeftOutlookBar.IsMinimized = false;
        }
        #endregion

        private void MainUC_Loaded(object sender, RoutedEventArgs e)
        {
            GetMenuList();
            //LoadLogoImage();
            ContactText.Text = "정광일님 접속중";
            CurrentDateTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            startWorker();
            SubLayoutRoot.LayoutUpdated += new System.EventHandler(SubLayoutRoot_LayoutUpdated);
            //LayoutRoot.KeyUp += new KeyEventHandler(LayoutRoot_KeyUp);
        }



    }
}