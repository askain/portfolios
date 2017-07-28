using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Browser;
using System.IO;
using HDIMSMAIN.Models.Main;
using Newtonsoft.Json;
using HDIMSMAIN.Models;
using System.Runtime.Serialization.Json;
using HDIMSMAIN.Models.Common;

namespace HDIMSMAIN
{
    public partial class MainPage : UserControl
    {
        private readonly string AbnormDamReadUrl = "/Main/GetabnormdamList/?";
        private readonly string AbnormDamUpdateUrl = "/Main/Updateabnormdam";

        private readonly string AbnormDataReadUrl = "/Main/GetabnormalList/?";
        private readonly string AbnormDataUpdateUrl = "/Main/Updateabnormal";

        private readonly string AbnormOperReadUrl = "/Main/GetdamoperList/?";
        private readonly string AbnormOperUpdateUrl = "/Main/Updatedamoper";

        private readonly string AbnormEquipReadUrl = "/Main/GetequipList/?";
        private readonly string AbnormEquipUpdateUrl = "/Main/Updateequip";

        private readonly string GetBoardContentsUri = "/Board/GetBoardContent/?";

        private WebClient client;

        private string MGTDAM = "MAIN";
        private string MGTOBS = "";
        private string P_DAMCD = "";    //바로 전에 검색한 댐코드(들)
        private string P_OBSCD = "";    //바로 전에 검색한 댐코드(들)

        private EmpData empData = new EmpData();

        public MainPage()
        {
            InitializeComponent();
            MGTDAM = empData.MGTDAMCD;
            GetAbnormDamList(MGTDAM);
            GetAbnormOperList(MGTDAM);
            GetAbnormDataList(MGTDAM,"");
            GetAbnormEquipList(MGTDAM,"");
            GetNotifications();
            xamWindow.Close();
        }

        //자바스크립트과 통신하는 메소드.
        [ScriptableMember()] //javascript에서 호출됨
        public void LoadFromMap(string damCd)
        {
            if (string.IsNullOrEmpty(damCd)) damCd = "MAIN";
            GetAbnormDamList(damCd);
            GetAbnormOperList(damCd);
            GetAbnormDataList(damCd, "");
            GetAbnormEquipList(damCd, "");
            
        }
        [ScriptableMember()] //javascript에서 호출됨
        public void LoadFromObs(string damCd, string obsCd)
        {
                GetAbnormDamList(damCd);
                GetAbnormOperList(damCd);
                GetAbnormDataList(damCd, obsCd);
                GetAbnormEquipList(damCd, obsCd);
        }


        #region -- 댐이상자료 처리 --
        
        public void GetAbnormDamList(string damCd = null)
        {
            if (damCd != null)
            {
                P_DAMCD = damCd;
            }

            AbnormDamSaveBtn.IsEnabled = false;
            AbnormDamLoadingBar.BusyContent = "댐운영자료를 조회중입니다.";
            AbnormDamLoadingBar.IsBusy = true;
            string uri = AbnormDamReadUrl + "DAMCD=" + P_DAMCD;
            client = new WebClient();
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(GetAbnormDamListCompleted);
            client.OpenReadAsync(new Uri(uri, UriKind.Relative));
            
        }

        void GetAbnormDamListCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            AbnormDamSaveBtn.IsEnabled = true;
            AbnormDamLoadingBar.IsBusy = false;
            if (e.Error != null) return;
            Stream str = e.Result;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JsonModel<AbnormDamModel>));
            JsonModel<AbnormDamModel> ret = (JsonModel<AbnormDamModel>)ser.ReadObject(str);
            int totalCount = ret.totalCount;
            IList<AbnormDamModel> list = ret.Data;
            AbnormDamCountTxt.Text = "갯수 : " + GetNumberFormat(totalCount);
            AbnormDamGrid.ItemsSource = list;
        }


        private void AbnormDamSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            IList<AbnormDamModel> _checkList = AbnormDamGrid.ItemsSource.OfType<AbnormDamModel>().Where<AbnormDamModel>(c => c.CHKYN == true).ToList<AbnormDamModel>();
            if (_checkList.Count < 1)
            {
                MessageBox.Show("체크된 자료가 없습니다.\n확인항목을 체크하세요");
                return;
            }
            AbnormDamSaveBtn.IsEnabled = false;
            AbnormDamLoadingBar.BusyContent = "댐운영자료를 저장중입니다.";
            AbnormDamLoadingBar.IsBusy = true;
            JsonModel<AbnormDamModel> _postData = new JsonModel<AbnormDamModel>() { Data = _checkList };
            string param = JsonConvert.SerializeObject(_postData);

            WebClient client = new WebClient();
            client.Headers["Content-Type"] = "application/json";
            client.UploadStringCompleted += new UploadStringCompletedEventHandler(SaveAbnormDamCompleted);
            client.UploadStringAsync(new Uri(AbnormDamUpdateUrl, UriKind.Relative), "POST", param);
        }

        private void SaveAbnormDamCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            AbnormDamSaveBtn.IsEnabled = true;
            AbnormDamLoadingBar.IsBusy = false;
            if (e.Error != null) return;
            GetAbnormDamList();
        }

        private void AbnormDamGrid_Checked(object sender, RoutedEventArgs e)
        {
            AbnormDamGrid.ItemsSource.OfType<AbnormDamModel>().ToList<AbnormDamModel>().ForEach(c => c.CHKYN = true);
        }

        private void AbnormDamGrid_UnChecked(object sender, RoutedEventArgs e)
        {
            AbnormDamGrid.ItemsSource.OfType<AbnormDamModel>().ToList<AbnormDamModel>().ForEach(c => c.CHKYN = false);
        }


        private void AbnormDamLegendBtn_Click(object sender, RoutedEventArgs e)
        {
            HtmlPage.Window.Invoke("showDamLegend");
        }

        #endregion

        #region -- 알람현황 처리 --
        public void GetAbnormOperList(string damCd = null)
        {
            if (damCd != null)
            {
                P_DAMCD = damCd;
            }
            AbnormOperSaveBtn.IsEnabled = false;
            AbnormOperLoadingBar.BusyContent = "알람현황자료를 조회중입니다.";
            AbnormOperLoadingBar.IsBusy = true;
            string uri = AbnormOperReadUrl + "DAMCD=" + P_DAMCD;
            client = new WebClient();
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(GetAbnormOperListCompleted);
            client.OpenReadAsync(new Uri(uri, UriKind.Relative));
        }
        void GetAbnormOperListCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            AbnormOperSaveBtn.IsEnabled = true;
            AbnormOperLoadingBar.IsBusy = false;
            if (e.Error != null) return;
            Stream str = e.Result;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JsonModel<AbnormOperModel>));
            JsonModel<AbnormOperModel> ret = (JsonModel<AbnormOperModel>)ser.ReadObject(str);
            int totalCount = ret.totalCount;
            IList<AbnormOperModel> list = ret.Data;
            AbnormOperCountTxt.Text = "갯수 : " + GetNumberFormat(totalCount);
            
            AbnormOperGrid.ItemsSource = list;
        }


        private void AbnormOperSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            IList<AbnormOperModel> _checkList = AbnormOperGrid.ItemsSource.OfType<AbnormOperModel>().Where<AbnormOperModel>(c => c.CHKYN == true).ToList<AbnormOperModel>();
            if (_checkList.Count < 1)
            {
                MessageBox.Show("체크된 자료가 없습니다.\n확인항목을 체크하세요");
                return;
            }
            AbnormOperSaveBtn.IsEnabled = false;
            AbnormOperLoadingBar.BusyContent = "알람현황자료를 저장중입니다.";
            AbnormOperLoadingBar.IsBusy = true;
            JsonModel<AbnormOperModel> _postData = new JsonModel<AbnormOperModel>() { Data = _checkList };
            string param = JsonConvert.SerializeObject(_postData);

            WebClient client = new WebClient();
            client.Headers["Content-Type"] = "application/json";
            client.UploadStringCompleted += new UploadStringCompletedEventHandler(SaveAbnormOperCompleted);
            client.UploadStringAsync(new Uri(AbnormOperUpdateUrl, UriKind.Relative), "POST", param);
        }

        private void SaveAbnormOperCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            AbnormOperSaveBtn.IsEnabled = true;
            AbnormOperLoadingBar.IsBusy = false;
            if (e.Error != null) return;
            GetAbnormOperList();
        }

        private void AbnormOperGrid_Checked(object sender, RoutedEventArgs e)
        {
            AbnormOperGrid.ItemsSource.OfType<AbnormOperModel>().ToList<AbnormOperModel>().ForEach(c => c.CHKYN = true);
        }

        private void AbnormOperGrid_UnChecked(object sender, RoutedEventArgs e)
        {
            AbnormOperGrid.ItemsSource.OfType<AbnormOperModel>().ToList<AbnormOperModel>().ForEach(c => c.CHKYN = false);
        }

        private void AlarmLegendBtn_Click(object sender, RoutedEventArgs e)
        {
            HtmlPage.Window.Invoke("showAlarmLegend");
        }
        #endregion

        #region -- 수위/우량 이상자료 처리 --
        public void GetAbnormDataList(string damCd = null, string obsCd = null)
        {
            if (damCd != null)
            {
                P_DAMCD = damCd;
            }
            if (obsCd != null)
            {
                P_OBSCD = obsCd;
            }

            AbnormDataSaveBtn.IsEnabled = false;
            AbnormDataLoadingBar.BusyContent = "수위/우량자료를 조회중입니다.";
            AbnormDataLoadingBar.IsBusy = true;
            string uri = AbnormDataReadUrl + "DAMCD=" + P_DAMCD + "&OBSCD=" + P_OBSCD;
            client = new WebClient();
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(GetAbnormDataListCompleted);
            client.OpenReadAsync(new Uri(uri, UriKind.Relative));
        }
        void GetAbnormDataListCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            AbnormDataSaveBtn.IsEnabled = true;
            AbnormDataLoadingBar.IsBusy = false;
            if (e.Error != null) return;
            Stream str = e.Result;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JsonModel<AbnormDataModel>));
            JsonModel<AbnormDataModel> ret = (JsonModel<AbnormDataModel>)ser.ReadObject(str);
            int totalCount = ret.totalCount;
            IList<AbnormDataModel> list = ret.Data;
            AbnormDataCountTxt.Text = "갯수 : " + GetNumberFormat(totalCount);
            AbnormDataGrid.ItemsSource = list;
        }


        private void AbnormDataSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            IList<AbnormDataModel> _checkList = AbnormDataGrid.ItemsSource.OfType<AbnormDataModel>().Where<AbnormDataModel>(c => c.CHKYN == true).ToList<AbnormDataModel>();
            if (_checkList.Count < 1)
            {
                MessageBox.Show("체크된 자료가 없습니다.\n확인항목을 체크하세요");
                return;
            }
            AbnormDataSaveBtn.IsEnabled = false;
            AbnormDataLoadingBar.BusyContent = "수위/우량자료를 저장중입니다.";
            AbnormDataLoadingBar.IsBusy = true;
            JsonModel<AbnormDataModel> _postData = new JsonModel<AbnormDataModel>() { Data = _checkList };
            string param = JsonConvert.SerializeObject(_postData);

            WebClient client = new WebClient();
            client.Headers["Content-Type"] = "application/json";
            client.UploadStringCompleted += new UploadStringCompletedEventHandler(SaveAbnormDataCompleted);
            client.UploadStringAsync(new Uri(AbnormDataUpdateUrl, UriKind.Relative), "POST", param);
        }

        private void SaveAbnormDataCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            AbnormDataSaveBtn.IsEnabled = true;
            AbnormDataLoadingBar.IsBusy = false;
            if (e.Error != null) return;
            GetAbnormDataList();
        }
        private void AbnormDataLegendBtn_Click(object sender, RoutedEventArgs e)
        {
            HtmlPage.Window.Invoke("showLegend", "");
        }

        private void AbnormDataGrid_Checked(object sender, RoutedEventArgs e)
        {
            AbnormDataGrid.ItemsSource.OfType<AbnormDataModel>().ToList<AbnormDataModel>().ForEach(c => c.CHKYN = true);
        }

        private void AbnormDataGrid_UnChecked(object sender, RoutedEventArgs e)
        {
            AbnormDataGrid.ItemsSource.OfType<AbnormDataModel>().ToList<AbnormDataModel>().ForEach(c => c.CHKYN = false);
        }
        #endregion

        #region -- 설비상태 이상자료 처리 --
        public void GetAbnormEquipList(string damCd = null, string obsCd = null)
        {
            if (damCd != null)
            {
                P_DAMCD = damCd;
            }
            if (obsCd != null)
            {
                P_OBSCD = obsCd;
            }

            AbnormEquipSaveBtn.IsEnabled = false;
            AbnormEquipLoadingBar.BusyContent = "설비상태자료를 조회중입니다.";
            AbnormEquipLoadingBar.IsBusy = true;
            string uri = AbnormEquipReadUrl + "DAMCD=" + P_DAMCD + "&OBSCD=" + P_OBSCD;
            client = new WebClient();
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(GetAbnormEquipListCompleted);
            client.OpenReadAsync(new Uri(uri, UriKind.Relative));
        }
        void GetAbnormEquipListCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            AbnormEquipSaveBtn.IsEnabled = true;
            AbnormEquipLoadingBar.IsBusy = false;
            if (e.Error != null) return;
            Stream str = e.Result;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JsonModel<AbnormEquipModel>));
            JsonModel<AbnormEquipModel> ret = (JsonModel<AbnormEquipModel>)ser.ReadObject(str);
            int totalCount = ret.totalCount;
            IList<AbnormEquipModel> list = ret.Data;
            AbnormEquipCountTxt.Text = "갯수 : " + GetNumberFormat(totalCount);
            AbnormEquipGrid.ItemsSource = list;
        }


        private void AbnormEquipSaveBtn_Click(object sender, RoutedEventArgs e)
        {
            IList<AbnormEquipModel> _checkList = AbnormEquipGrid.ItemsSource.OfType<AbnormEquipModel>().Where<AbnormEquipModel>(c => c.CHKYN == true).ToList<AbnormEquipModel>();
            if (_checkList.Count < 1)
            {
                MessageBox.Show("체크된 자료가 없습니다.\n확인항목을 체크하세요");
                return;
            }
            AbnormEquipSaveBtn.IsEnabled = false;
            AbnormEquipLoadingBar.BusyContent = "설비상태자료를 저장중입니다.";
            AbnormEquipLoadingBar.IsBusy = true;
            JsonModel<AbnormEquipModel> _postData = new JsonModel<AbnormEquipModel>() { Data = _checkList };
            string param = JsonConvert.SerializeObject(_postData);

            WebClient client = new WebClient();
            client.Headers["Content-Type"] = "application/json";
            client.UploadStringCompleted += new UploadStringCompletedEventHandler(SaveAbnormEquipCompleted);
            client.UploadStringAsync(new Uri(AbnormEquipUpdateUrl, UriKind.Relative), "POST", param);
        }

        private void SaveAbnormEquipCompleted(object sender, UploadStringCompletedEventArgs e)
        {
            AbnormEquipSaveBtn.IsEnabled = true;
            AbnormEquipLoadingBar.IsBusy = false;
            if (e.Error != null) return;
            GetAbnormEquipList();
        }

        private void AbnormEquipGrid_Checked(object sender, RoutedEventArgs e)
        {
            AbnormEquipGrid.ItemsSource.OfType<AbnormEquipModel>().ToList<AbnormEquipModel>().ForEach(c => c.CHKYN = true);
        }

        private void AbnormEquipGrid_UnChecked(object sender, RoutedEventArgs e)
        {
            AbnormEquipGrid.ItemsSource.OfType<AbnormEquipModel>().ToList<AbnormEquipModel>().ForEach(c => c.CHKYN = false);
        }
        #endregion

        #region == 공지 사항 ==
        private void GetNotifications()
        {
            string uri = string.Format("{0}limit={1}&homeyn={2}", GetBoardContentsUri, "999", "Y");
            client = new WebClient();

            client.OpenReadCompleted += new OpenReadCompletedEventHandler(GetNotificationsCompleted);
            client.OpenReadAsync(new Uri(uri, UriKind.Relative));
        }
        private void GetNotificationsCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null) return;
            Stream stream = e.Result;
            string strJson;
            using (StreamReader reader = new StreamReader(stream))
            {
                strJson = reader.ReadToEnd();
            }

            IList<BoardContentModel> Notifications = JsonConvert.DeserializeObject<IList<BoardContentModel>>(strJson);

            NotificationGrid.ItemsSource = Notifications;

            //txtTitle.Text += " 등록된 글수 :" + AllCnt.ToString();
            //MessageBox.Show(AllCnt.ToString());
        }
        #endregion

        #region --ETC
        private string GetNumberFormat(int num)
        {
            return String.Format("{0:###,###,###,###,##0}", num);
        }
        #endregion

        #region == 접기 펴기 ==
        //private void Notify_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    TextBlock t = sender as TextBlock;

        //    foreach (BoardContentModel b in NotificationGrid.ItemsSource)
        //    {
        //        if (t.Text.Equals(b.TITLE))
        //        {
        //            xamWindow.Header = b.TITLE;
        //            xamWindow.Content = b.CONTENT;
        //            xamWindow.Show();
        //        }
        //    }
        //}
        private void DamOper_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            StackPanel s = sender as StackPanel;
            StackPanel targetStackPanel;
            switch((string)s.Tag) {
                case "DamOper":
                    targetStackPanel = DamOperStackPanel;
                    break;
                case "Alarm":
                    targetStackPanel = AlarmStackPanel;
                    break;
                case "WR":
                    targetStackPanel = WRStackPanel;
                    break;
                case "Equip":
                    targetStackPanel = EquipStackPanel;
                    break;
                default:
                    return;
            }

            if (targetStackPanel.Height < 200)
            {
                //targetStackPanel.VerticalAlignment = VerticalAlignment.Top;
                //targetStackPanel.HorizontalAlignment = HorizontalAlignment.Center;
                //targetStackPanel.Width = 600;
                //targetStackPanel.Height = 298;   //=-> 270
                targetStackPanel.Height = targetStackPanel == EquipStackPanel ? 248 : 358;   //=-> 270
            }
            else
            {
                //targetStackPanel.VerticalAlignment = VerticalAlignment.Top;
                //targetStackPanel.HorizontalAlignment = HorizontalAlignment.Left;
                //targetStackPanel.Width = 200;
                targetStackPanel.Height = 52;
            }

        }
        
        #endregion

        private void ShowEquipBtn_Click(object sender, RoutedEventArgs e)
        {
            EquipStackPanel.Visibility = Visibility.Visible;
            NotifyStackPanel.Visibility = Visibility.Collapsed;
        }

        private void ShowNotifyBtn_Click(object sender, RoutedEventArgs e)
        {
            EquipStackPanel.Visibility = Visibility.Collapsed;
            NotifyStackPanel.Visibility = Visibility.Visible;
        }

        private void NotificationGrid_CellClicked(object sender, Infragistics.Controls.Grids.CellClickedEventArgs e)
        {
            BoardContentModel b = e.Cell.Row.Data as BoardContentModel;
            xamWindow.Header = b.TITLE;
            xamWindow.Content = b.CONTENT;
            xamWindow.Show();
        }
    }
}