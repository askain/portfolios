using System.Collections.Generic;
using System.Windows;
using Infragistics.Controls.Grids;

namespace HDIMSAPP.Models.Codes
{
    public class ExamManagementCollection
    {
        private IList<ExamManagementModel> _datas = new List<ExamManagementModel>();
        
        public ExamManagementCollection()
        {
            _datas.Add(new ExamManagementModel("검정코드", "EXCD", 100, HorizontalAlignment.Center, FixedState.NotFixed));
            _datas.Add(new ExamManagementModel("구분", "EXTP_SHOW", 80, HorizontalAlignment.Center, FixedState.NotFixed));
            _datas.Add(new ExamManagementModel("우선순위", "EXORD", 80, HorizontalAlignment.Center, FixedState.NotFixed));
            _datas.Add(new ExamManagementModel("설명", "EXCONT", 500, HorizontalAlignment.Left, FixedState.NotFixed));
            _datas.Add(new ExamManagementModel("요약", "EXNOTE", 120, HorizontalAlignment.Center, FixedState.NotFixed));
            _datas.Add(new ExamManagementModel("수행주기(10분)", "PROCINT", 100, HorizontalAlignment.Center, FixedState.NotFixed));
            _datas.Add(new ExamManagementModel("사용여부", "EXYN_SHOW", 100, HorizontalAlignment.Center, FixedState.NotFixed));
            _datas.Add(new ExamManagementModel("색상", "EXCOLOR", 90, HorizontalAlignment.Center, FixedState.NotFixed));
            _datas.Add(new ExamManagementModel("최종수행시간", "PROCDT", 120, HorizontalAlignment.Center, FixedState.NotFixed));
        }

        public IList<ExamManagementModel> VALUES { get { return _datas; } }
    }

    
    public class ExamManagementModel
    {
        
        public string EXCD { get; set; }
        public string EXTP { get; set; }
        public string EXTP_SHOW
        {
            get
            {
                if (EXTP == "W")
                {
                    //EXTP.Replace("W", "수위");
                    return EXTP.Replace("W", "수위");
                }
                else if ("R".Equals(EXTP.ToUpper()))
                {
                    return "우량";
                }
                else 
                {
                    return "공통";
                }
            }
        }      
        public int EXORD { get; set; }
        public string EXCONT { get; set; }
        public string EXNOTE { get; set; }
        public string EXYN { get; set; }
        public string EXYN_SHOW
        {
            get
            {
                if ("Y".Equals(EXYN.ToUpper()))
                {
                    return "사용중";
                }
                else
                {
                    return "사용안함";
                }
            }
        }
        public string EXCOLOR { get; set; }
        public string EXCOLUMN { get; set; }
        public string EXCOLUMNNM { get; set; }
        public string ID { get; set; }
        public string PROCDT { get; set; }
        public int PROCINT { get; set; }

        public ExamManagementModel() { }

        public string Key { get; set; }
        public string Name { get; set; }
        public int Width { get; set; }
        public HorizontalAlignment Alignment { get; set; }
        public FixedState IsFixed { get; set; }

        public ExamManagementModel(string _name, string _key, int _width, HorizontalAlignment _align, FixedState _isFixed)
        {
            this.Key = _key;
            this.Name = _name;
            this.Width = _width;
            this.Alignment = _align;
            this.IsFixed = _isFixed;
        }

        public string value { get; set; }

    }
}