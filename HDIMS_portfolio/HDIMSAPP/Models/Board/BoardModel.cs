
using System.Collections.Generic;
using System.Text;
using HDIMSAPP.Models.Common;
using HDIMSAPP.Models.ManAuthMng;

namespace HDIMSAPP.Models.Board
{
    public class BoardModel : ObservableModel
    {
        public BoardModel()
        {
            
            //this.Operation = "I";   //I : Insert, U : Update, D : Delete
            this.USEYN = "Y";
        }

        public string CRUD { get; set; }

        #region == 원래 변수와 찌질이들 ==
        private string _BOARDCD;
        public string BOARDCD { get { return _BOARDCD; } set { _BOARDCD = value; OnPropertyChanged("BOARDCD"); } }
        private string _BOARDNM;
        public string BOARDNM { get { return _BOARDNM; } set {
            //if (value != null && value.Length > 100) throw new Exception("100자 이상 입력할 수 없습니다.");
            _BOARDNM = value; OnPropertyChanged("BOARDNM"); } }
        private string _BOARDNOTE;
        public string BOARDNOTE { get { return _BOARDNOTE; } set { _BOARDNOTE = value; OnPropertyChanged("BOARDNOTE"); } }
        private string _FILEYN;
        public string FILEYN { get { return _FILEYN; } set { _FILEYN = value; OnPropertyChanged("FILEYN"); } }
        private string _FILEEXTENTION;
        public string FILEEXTENSION { get { return _FILEEXTENTION; } set { _FILEEXTENTION = value; OnPropertyChanged("FILEEXTENTION"); } }
        private string _MAILYN;
        public string MAILYN { get { return _MAILYN; } set { _MAILYN = value; OnPropertyChanged("MAILYN"); } }
        private string _MAILADDRESS;
        public string MAILADDRESS { get { return _MAILADDRESS; } set { _MAILADDRESS = value; OnPropertyChanged("MAILADDRESS"); } }
        private string _READAUTHCODE;
        public string READAUTHCODE { get { return _READAUTHCODE; } set { _READAUTHCODE = value; OnPropertyChanged("READAUTHCODE"); } }
        private string _WRITEAUTHCODE;
        public string WRITEAUTHCODE { get { return _WRITEAUTHCODE; } set { _WRITEAUTHCODE = value; OnPropertyChanged("WRITEAUTHCODE"); } }
        private int _ORD;
        public int ORD { get { return _ORD; } set { _ORD = value; OnPropertyChanged("ORD"); } }
        private string _USEYN;
        public string USEYN { get { return _USEYN; } set { _USEYN = value; OnPropertyChanged("USEYN"); } }
        private string _HOMEYN;
        public string HOMEYN { get { return _HOMEYN; } set { _HOMEYN = value; OnPropertyChanged("HOMEYN"); } }
        private int _MAXFILESIZE;
        public int MAXFILESIZE { get { return _MAXFILESIZE; } set { _MAXFILESIZE = value; OnPropertyChanged("MAXFILESIZE"); } }
        private string _OASISYN;
        public string OASISYN { get { return _OASISYN; } set { _OASISYN = value; OnPropertyChanged("OASISYN"); } }
        

        #endregion

        #region == 화면 표출용 ==
        #region == FILEYN & FILEEXTENTION 관련 ==
        public bool FILEYN_BOOL {
            get
            {
                if (FILEYN == null) return false;
                else if ("N".Equals(FILEYN)) return false;

                return true;
            }
            set
            {
                if (value == true)
                {
                    FILEYN = "Y";
                }
                else
                {
                    FILEYN = "N";
                }
                OnPropertyChanged("FILEYN_BOOL");
            }
        }

        #endregion

        #region == MAILYN & MAILADDRESS 관련 ==
        public bool MAILYN_BOOL
        {
            get
            {
                if (MAILYN == null) return false;
                else if ("N".Equals(MAILYN)) return false;

                return true;
            }
            set
            {
                if (value == true)
                {
                    MAILYN = "Y";
                }
                else
                {
                    MAILYN = "N";
                }
                OnPropertyChanged("MAILYN_BOOL");
            }
        }

        #endregion

        #region == USEYN 관련 ==
        public bool USEYN_BOOL
        {
            get
            {
                if (USEYN == null) return false;
                else if ("N".Equals(USEYN)) return false;

                return true;
            }
            set
            {
                if (value == true)
                {
                    USEYN = "Y";
                }
                else
                {
                    USEYN = "N";
                }
                OnPropertyChanged("USEYN_BOOL");
            }
        }
        #endregion

        #region == HOMEYN 관련 ==
        public bool HOMEYN_BOOL
        {
            get
            {
                if (HOMEYN == null) return false;
                else if ("N".Equals(HOMEYN)) return false;

                return true;
            }
            set
            {
                if (value == true)
                {
                    HOMEYN = "Y";
                }
                else
                {
                    HOMEYN = "N";
                }
                OnPropertyChanged("HOMEYN_BOOL");
            }
        }
        #endregion

        #region == OASISYN 관련 ==
        public bool OASISYN_BOOL
        {
            get
            {
                if (OASISYN == null) return false;
                else if ("N".Equals(OASISYN)) return false;

                return true;
            }
            set
            {
                if (value == true)
                {
                    OASISYN = "Y";
                }
                else
                {
                    OASISYN = "N";
                }
                OnPropertyChanged("OASISYN_BOOL");
            }
        }
        #endregion



        #region == READAUTHCODE 관련 ==
        public string READAUTHNAMES
        {
            get
            {
                if (READAUTHCODE == null) return null;

                return this.AuthCode2Name(READAUTHCODE);
            }
        }

        public IList<AuthModel> READAUTHOBJECTS
        {
            get
            {
                IList<AuthModel> SelectedItems = String2Collection(READAUTHCODE);
                return SelectedItems;
            }
            set
            {
                READAUTHCODE = Collection2String(value);
                OnPropertyChanged("READAUTHNAMES");
            }
        }
        #endregion

        #region == WRITEAUTHCODE 관련 ==
        public string WRITEAUTHNAMES
        {
            get
            {
                if (WRITEAUTHCODE == null) return null;

                return this.AuthCode2Name(WRITEAUTHCODE);
            }
        }

        public IList<AuthModel> WRITEAUTHOBJECTS
        {
            get
            {
                IList<AuthModel> SelectedItems = String2Collection(WRITEAUTHCODE);
                return SelectedItems;
            }
            set
            {
                WRITEAUTHCODE = Collection2String(value);
                OnPropertyChanged("WRITEAUTHNAMES");
            }
        }
        #endregion
        
        #endregion

        #region == 카테고리 관련 ==
        public IList<string> GetCategories()
        {
            IList<string> result = new List<string>();
            if (_BOARDNOTE != null)
            {
                string[] categoryArray = _BOARDNOTE.Split(',');

                if (categoryArray != null)
                {
                    foreach (string m in categoryArray)
                    {
                        if (string.IsNullOrEmpty(m) == false)
                        {
                            result.Add(m);
                        }
                    }
                }
            }
            return result;
        }
        #endregion

        #region == functions  ==
        private string AuthCode2Name(string codes)
        {
            if(codes == null) return null;
            if (AUTHLIST == null) return null;

            StringBuilder sb = new StringBuilder();

            foreach (AuthModel m in AUTHLIST)
            {
                if (codes.Contains(m.AUTHCODE) == true)
                {
                    if (sb.Length != 0) sb.Append(",");
                    sb.Append(m.AUTHNAME);
                }
            }
            return sb.ToString();
        }

        private IList<AuthModel> String2Collection(string codes)
        {
            if (codes == null) return null;
            if (AUTHLIST == null) return null;

            if (READAUTHCODE == null) return null;
            string[] codesArray = codes.Split(',');

            IList<AuthModel> result = new List<AuthModel>();
            foreach (AuthModel m in AUTHLIST)
            {
                //MessageBox.Show(m.AUTHNAME);
                foreach (string c in codesArray)
                {
                    //MessageBox.Show(c);
                    if (m.AUTHCODE.Equals(c))
                    {
                        //MessageBox.Show("Matched!");
                        result.Add(m);
                        continue;
                    }
                }
            }

            return result;
        }

        private string Collection2String(object value)
        {
            if (value == null) return null;

            IList<AuthModel> result = value as IList<AuthModel>;

            StringBuilder sb = new StringBuilder();

            foreach (AuthModel model in result)
            {
                if (sb.Length != 0) sb.Append(",");

                sb.Append(model.AUTHCODE);
            }
            return sb.ToString();
        }
        #endregion

        

        public IList<AuthModel> AUTHLIST { get; set; }
    }
}