using System.Collections.Generic;
using System.Windows;
using Infragistics.Controls.Grids;

namespace HDIMSAPP.Models.DataSearch
{
    public class EquipColumnCollection
    {
        private IList<EquipColumn> _datas = new List<EquipColumn>();

        public EquipColumnCollection()
        {
            _datas.Add(new EquipColumn("측정일시", "OBSDT", 120, HorizontalAlignment.Center, FixedState.Left, false));
            _datas.Add(new EquipColumn("댐명", "DAMNM", 80, HorizontalAlignment.Center, FixedState.Left, false));
            _datas.Add(new EquipColumn("관측국", "OBSNM", 150, HorizontalAlignment.Center, FixedState.Left, false));
            _datas.Add(new EquipColumn("배터리 전압", "BATTVOLT", 60, HorizontalAlignment.Center, FixedState.NotFixed, true));
            _datas.Add(new EquipColumn("배터리 상태", "BATTSTS", 60, HorizontalAlignment.Center, FixedState.NotFixed, false));
            _datas.Add(new EquipColumn("DOOR 상태", "DOORSTS", 60, HorizontalAlignment.Center, FixedState.NotFixed, false));
            _datas.Add(new EquipColumn("수위 센서", "WL_SEN", 60, HorizontalAlignment.Center, FixedState.NotFixed, false));
            _datas.Add(new EquipColumn("우량 센서", "RF_SEN", 60, HorizontalAlignment.Center, FixedState.NotFixed, false));
            _datas.Add(new EquipColumn("수질 센서", "WQ_SEN", 60, HorizontalAlignment.Center, FixedState.NotFixed, false));
            _datas.Add(new EquipColumn("기타 센서", "ETC_SEN", 60, HorizontalAlignment.Center, FixedState.NotFixed, false));
            _datas.Add(new EquipColumn("RTU 메모리", "RTU_MEMORY", 60, HorizontalAlignment.Center, FixedState.NotFixed, false));
            _datas.Add(new EquipColumn("RTU 리셋", "RTU_RESET", 60, HorizontalAlignment.Center, FixedState.NotFixed, false));
            _datas.Add(new EquipColumn("LAN 포트", "LAN_PORT", 60, HorizontalAlignment.Center, FixedState.NotFixed, false));
            _datas.Add(new EquipColumn("CDMA 모뎀", "CDMA_MODEM", 60, HorizontalAlignment.Center, FixedState.NotFixed, false));
            _datas.Add(new EquipColumn("VSAT 포트", "VSAT_PORT", 60, HorizontalAlignment.Center, FixedState.NotFixed, false));
            _datas.Add(new EquipColumn("CDMA 포트", "CDMA_PORT", 60, HorizontalAlignment.Center, FixedState.NotFixed, false));
            _datas.Add(new EquipColumn("유선 포트", "WIRE_PORT", 60, HorizontalAlignment.Center, FixedState.NotFixed, false));
            _datas.Add(new EquipColumn("멀티 케스트", "MULTICAST_SOCKET", 60, HorizontalAlignment.Center, FixedState.NotFixed, false));
            _datas.Add(new EquipColumn("VSAT 이벤트", "VSAT_EVENT", 60, HorizontalAlignment.Center, FixedState.NotFixed, false));
            _datas.Add(new EquipColumn("CDMA 이벤트", "CDMA_EVENT", 60, HorizontalAlignment.Center, FixedState.NotFixed, false));
            _datas.Add(new EquipColumn("유선 이벤트", "WIRE_EVENT", 60, HorizontalAlignment.Center, FixedState.NotFixed, false));
            _datas.Add(new EquipColumn("UDP 이벤트", "UDP_EVENT", 60, HorizontalAlignment.Center, FixedState.NotFixed, false));
            _datas.Add(new EquipColumn("IDU PING", "IDU_PING", 60, HorizontalAlignment.Center, FixedState.NotFixed, false));
            _datas.Add(new EquipColumn("SNR", "SNR", 60, HorizontalAlignment.Center, FixedState.NotFixed, true));
            _datas.Add(new EquipColumn("주망 상태", "PRIMARY_DATA", 60, HorizontalAlignment.Center, FixedState.NotFixed, false));
            _datas.Add(new EquipColumn("보조망 호출", "SECONDARY_CALL", 60, HorizontalAlignment.Center, FixedState.NotFixed, false));
            _datas.Add(new EquipColumn("데이터 결측", "DATA_STATUS", 60, HorizontalAlignment.Center, FixedState.NotFixed, false));
        }

        public IList<EquipColumn> VALUES { get { return _datas; } }
    }


    public class EquipColumn
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public int Width { get; set; }
        public HorizontalAlignment Alignment { get; set; }
        public FixedState IsFixed { get; set; }
        public bool IsChart { get; set; }

        public EquipColumn() { }

        public EquipColumn(string _name, string _key, int _width, HorizontalAlignment _align, FixedState _isFixed, bool _isChart)
        {
            this.Key = _key;
            this.Name = _name;
            this.Width = _width;
            this.Alignment = _align;
            this.IsFixed = _isFixed;
            this.IsChart = _isChart;
        }
    }
}
