using System.Collections.Generic;
using System.Windows;
using Infragistics.Controls.Grids;

namespace HDIMSAPP.Models.DataSearch
{
    public class QualityColumnCollection
    {
        private IList<QualityColumn> _datas = new List<QualityColumn>();

        public QualityColumnCollection()
        {
            _datas.Add(new QualityColumn("댐명", "DAMNM", 100, HorizontalAlignment.Center, FixedState.Left));
            _datas.Add(new QualityColumn("댐코드", "DAMCD", 60, HorizontalAlignment.Center, FixedState.Left));
            _datas.Add(new QualityColumn("관측국", "OBSNM", 120, HorizontalAlignment.Center, FixedState.Left));
            _datas.Add(new QualityColumn("관측국코드", "OBSCD", 85, HorizontalAlignment.Center, FixedState.Left));
            _datas.Add(new QualityColumn("측정일시", "OBSDT", 120, HorizontalAlignment.Center, FixedState.Left));
            _datas.Add(new QualityColumn("해발수심", "OBSELM", 80, HorizontalAlignment.Center, FixedState.NotFixed));
            _datas.Add(new QualityColumn("계측시간", "ACTOBSDT", 120, HorizontalAlignment.Center, FixedState.NotFixed));
            _datas.Add(new QualityColumn("측정수심", "SRDWATITG", 80, HorizontalAlignment.Center, FixedState.NotFixed));
            _datas.Add(new QualityColumn("측정수심", "SRDWATFLT", 80, HorizontalAlignment.Center, FixedState.NotFixed));
            _datas.Add(new QualityColumn("수위", "RWL", 60, HorizontalAlignment.Center, FixedState.NotFixed));
            _datas.Add(new QualityColumn("탁도", "TBDT", 60, HorizontalAlignment.Center, FixedState.NotFixed));
            _datas.Add(new QualityColumn("수소이온농도", "PH", 100, HorizontalAlignment.Center, FixedState.NotFixed));
            _datas.Add(new QualityColumn("수온", "WTRTMP", 60, HorizontalAlignment.Center, FixedState.NotFixed));
            _datas.Add(new QualityColumn("전기전도", "ELCCND", 80, HorizontalAlignment.Center, FixedState.NotFixed));
            _datas.Add(new QualityColumn("용존산소", "DO", 80, HorizontalAlignment.Center, FixedState.NotFixed));
            _datas.Add(new QualityColumn("조류색소", "CHL", 80, HorizontalAlignment.Center, FixedState.NotFixed));
            _datas.Add(new QualityColumn("염분도", "SAL", 70, HorizontalAlignment.Center, FixedState.NotFixed));
            _datas.Add(new QualityColumn("검보정상태", "EDEXLVL", 80, HorizontalAlignment.Center, FixedState.NotFixed));
        }

        public IList<QualityColumn> VALUES { get { return _datas; } }
    }


    public class QualityColumn
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public int Width { get; set; }
        public HorizontalAlignment Alignment { get; set; }
        public FixedState IsFixed { get; set; }

        public QualityColumn() { }

        public QualityColumn(string _name, string _key, int _width, HorizontalAlignment _align, FixedState _isFixed)
        {
            this.Key = _key;
            this.Name = _name;
            this.Width = _width;
            this.Alignment = _align;
            this.IsFixed = _isFixed;
        }
    }
}
