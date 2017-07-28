using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Windows;
using System.Windows.Controls;
using HDIMSAPP.Models;
using HDIMSAPP.Models.Common;

namespace HDIMSAPP.Views.Verify
{
    public partial class DamDataSaveDialog : ChildWindow
    {
        /*
         * 
//보정등급
var gl_edexlvl = [
{ text: '전체', value: '' },
{ text: '원시수문 자료', value: '0' },
{ text: 'S/W,RTU및분기', value: '1' },
{ text: '년간,대외 자료', value: '2' }
];

var gl_edexlvl2 = [
{ text: '선택하세요', value: '' },
{ text: '원시수문 자료', value: '0' },
{ text: 'S/W,RTU및분기', value: '1' },
{ text: '년간,대외 자료', value: '2' }
];

var gl_edexway_WL2 = [
{ text: '선택하세요', value: '' },
{ text: '자동계산', value: '1' },
{ text: '목측법', value: '2' },
{ text: '이동평균', value: '3' },
{ text: '예년자료', value: '4' },
{ text: '과거자료', value: '5' },
{ text: '기타방법', value: '6' }
];

var gl_edexway_RF2 = [
{ text: '선택하세요', value: '' },
{ text: '자동보정', value: '1' },
{ text: '목측보정', value: '2' },
{ text: 'RDS법', value: '3' },
{ text: '보간법', value: '4' },
{ text: '신뢰도분석', value: '5' },
{ text: '인근지자체', value: '6' },
{ text: '기상청AWS', value: '7' },
{ text: '회귀분석', value: '8' },
{ text: '기타방법', value: '9' }
];

var gl_edexway_DD2 = [
{ text: '선택하세요', value: '' },
{ text: '자동계산', value: '1' },
{ text: '목측법', value: '2' },
{ text: '이동평균', value: '3' },
{ text: '예년자료', value: '4' },
{ text: '과거자료', value: '5' },
{ text: '기타방법', value: '6' }
];
         */
        private WebClient client;

        private readonly string EdExWayUri = "/Common/GetEdExWayList/?edyn=Y";  //edtp, edyn
        private readonly string EdExLvlUri = "/Common/GetEdExLvlList/?edyn=Y";  //edyn, firstvalue
        private readonly string ExRsnUri = "/Common/GetExCodeList/?"; //extp, firstvalue

        private WaterLevelVerify WlParent;
        private WaterLevelSearch WlSearchParent;
        private WaterLevelSearch_Telerik WlSearchTelerikParent;
        private Page PageParent;

        #region constructors
        public DamDataSaveDialog(Page caller)
        {
            throw new InvalidCastException("지정된 페이지에서만 보정 할 수 있습니다.");
        }
        public DamDataSaveDialog(WaterLevelVerify caller)
        {
            this.WlParent = caller;
            InitializeComponent();
            initSearchPanel();
        }
        public DamDataSaveDialog(WaterLevelSearch caller)
        {
            this.WlSearchParent = caller;
            InitializeComponent();
            initSearchPanel();
        }
        public DamDataSaveDialog(WaterLevelSearch_Telerik caller)
        {
            this.WlSearchTelerikParent  = caller;
            InitializeComponent();
            initSearchPanel();
        }
        #endregion

        #region == initialize ==
        private void initSearchPanel()
        {
            initExLvlCombo();
            initExWayCombo();
            initExRsnCombo();

            // 댐운영자료: 사유 ,  수위&우량: 증상
            if (WlParent != null || WlSearchParent != null || WlSearchTelerikParent != null)
            {
                CnRsnStack.Height = 0;
                ExRsnStack.Height = 22;
                cnrsnText.IsEnabled = false;
            }
        }

        #region == 보정등급 콤보박스 ==
        private void initExLvlCombo()
        {
            string uri = EdExLvlUri;

            client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(GetEdExLvlListCompleted);
            client.OpenReadAsync(new Uri(uri, UriKind.Relative));
        }
        void GetEdExLvlListCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null) return;
            Stream str = e.Result;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JsonModel<Code>));
            JsonModel<Code> ret = (JsonModel<Code>)ser.ReadObject(str);
            IList<Code> list = ret.Data;

            edExLvlCombo.DisplayMemberPath = "VALUE";
            edExLvlCombo.ItemsSource = list;
            edExLvlCombo.SelectedIndex = 0;
        }
        #endregion

        #region == 보정방법 콤보박스 ==
        private void initExWayCombo()
        {
            string uri = EdExWayUri;

            if (WlParent != null || WlSearchParent != null || WlSearchTelerikParent != null)  
            {
                uri += "&edtp=WL";
            }

            client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(GetEdExWayListCompleted);
            client.OpenReadAsync(new Uri(uri, UriKind.Relative));
        }
        void GetEdExWayListCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null) return;
            Stream str = e.Result;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JsonModel<Code>));
            JsonModel<Code> ret = (JsonModel<Code>)ser.ReadObject(str);
            IList<Code> list = ret.Data;

            edExWayCombo.DisplayMemberPath = "VALUE";
            edExWayCombo.ItemsSource = list;
            edExWayCombo.SelectedIndex = 0;
        }
        #endregion

        #region == 사유 콤보박스 ==
        private void initExRsnCombo()
        {
            string uri = ExRsnUri;

            if (WlParent != null || WlSearchParent != null || WlSearchTelerikParent != null)
            {
                uri += "&extp=W";
            }

            client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            client.OpenReadCompleted += new OpenReadCompletedEventHandler(GetExRsnListCompleted);
            client.OpenReadAsync(new Uri(uri, UriKind.Relative));
        }
        void GetExRsnListCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            if (e.Error != null) return;
            Stream str = e.Result;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(JsonModel<ExCode>));
            JsonModel<ExCode> ret = (JsonModel<ExCode>)ser.ReadObject(str);
            IList<ExCode> list = ret.Data;

            exRsnCombo.DisplayMemberPath = "EXNOTE";
            exRsnCombo.ItemsSource = list;
            exRsnCombo.SelectedIndex = 0;
        }
        #endregion
        
        #endregion

        #region == Events ==
        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Saving is not available.");
            return;

            Code selEdExLvl = (Code)edExLvlCombo.SelectedItem;
            Code selEdExWay = (Code)edExWayCombo.SelectedItem;

            if (selEdExLvl.KEY == "")
            {
                MessageBox.Show("보정등급을 선택하세요.");
                return;
            }
            if (selEdExWay.KEY == "")
            {
                MessageBox.Show("보정방법을 선택하세요.");
                return;
            }

            //선택된 셀에 두가지의 정보를 입력한다.
            //보정등급, 보정방법, 사유, 내역 입력

            //$(document.body).mask("변경된 데이터를 저장중입니다...");
            //pWin.saveData(selEdExLvl.Key, selEdExWay.Key, cnrsnText.Text, cndsText.Text, window);

            //MessageBox.Show("lvl:" + selEdExLvl.KEY + "  selEdExWay:" + selEdExLvl.KEY + " exRsn:" + selExRsn.EXCD + " exRsnText:" + selExRsn.EXNOTE + " cnrsnText:" + cnrsnText.Text + " cndsText" + cndsText.Text);

            //if(DamParent != null) {
            //    MessageBox.Show(DamParent.ToString());
            //    return;
            //}


            if (WlParent != null)
            {
                ExCode selExRsn = (ExCode)exRsnCombo.SelectedItem;
                WlParent.saveData(selEdExWay.KEY, selEdExLvl.KEY, selExRsn.EXCD, selExRsn.EXNOTE, cndsText.Text);
            }
            else if (WlSearchParent != null)
            {
                ExCode selExRsn = (ExCode)exRsnCombo.SelectedItem;
                WlSearchParent.saveData(selEdExWay.KEY, selEdExLvl.KEY, selExRsn.EXCD, selExRsn.EXNOTE, cndsText.Text);
            }
            else if (WlSearchTelerikParent != null)
            {
                ExCode selExRsn = (ExCode)exRsnCombo.SelectedItem;
                WlSearchTelerikParent.saveData(selEdExWay.KEY, selEdExLvl.KEY, selExRsn.EXCD, selExRsn.EXNOTE, cndsText.Text);
            }

            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
        #endregion
    }
}

