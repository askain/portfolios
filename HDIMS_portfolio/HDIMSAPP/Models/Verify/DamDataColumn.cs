using System.Collections.Generic;
using System.Windows;
using Infragistics.Controls.Grids;

namespace HDIMSAPP.Models.Verify
{
    public class DamDataColumnCollection
    {
        private IDictionary<string, DamDataColumn> _datas = new Dictionary<string, DamDataColumn>();

        public DamDataColumnCollection(bool isDay=false)
        {
            InitColumnsByCommon();
            if (!isDay) InitColumns();
            else InitColumnsByDay();
        }

        #region -- 공통컬럼 생성 --
        private void InitColumnsByCommon()
        {
            _datas.Add("DAMNM", new DamDataColumn()
            {
                Key = "DAMNM",
                Text = "댐명",
                Digit = -1,
                Readonly = true,
                Width = 80,
                IsFixed = FixedState.Left,
                IsChart = false,
                ApplyStyle = false, 
                DataType = GridDataType.STRING,
                Alignment = HorizontalAlignment.Center
            });
            _datas.Add("DAMCD", new DamDataColumn()
            {
                Key = "DAMCD",
                Text = "댐코드",
                Digit = -1,
                Readonly = true,
                Width = 60,
                IsFixed = FixedState.Left,
                IsChart = false,
                ApplyStyle = false, DataType = GridDataType.STRING,
                Alignment = HorizontalAlignment.Center
            });
            _datas.Add("OBSDT", new DamDataColumn()
            {
                Key = "OBSDT",
                Text = "측정일시",
                Digit = -1,
                Readonly = true,
                Width = 120,
                IsFixed = FixedState.Left,
                IsChart = false,
                ApplyStyle = false, DataType = GridDataType.DATE,
                Alignment = HorizontalAlignment.Center
            });
        }
        #endregion 
        #region -- 컬럼 자료 생성 --
        private void InitColumns()
        {
            _datas.Add("RWL", new DamDataColumn()
            {
                Key = "RWL",
                Text = "저수위",
                Digit = 2,
                Readonly = false,
                Width = 70,
                IsFixed = FixedState.NotFixed,
                IsChart = true, DataType=GridDataType.NUMBER,
                ApplyStyle = true,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("OSPILWL", new DamDataColumn()
            {
                Key = "OSPILWL",
                Text = "방수로 수위",
                Digit = 2,
                Readonly = false,
                Width = 70,
                IsFixed = FixedState.NotFixed,
                IsChart = true,
                DataType = GridDataType.NUMBER,
                ApplyStyle = true,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("RSQTY", new DamDataColumn()
            {
                Key = "RSQTY",
                Text = "저수량",
                Digit = 3,
                Readonly = true,
                Width = 70,
                IsFixed = FixedState.NotFixed,
                IsChart = true, DataType=GridDataType.NUMBER,
                ApplyStyle = true,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("RSRT", new DamDataColumn()
            {
                Key = "RSRT",
                Text = "저수율",
                Digit = 1,
                Readonly = true,
                Width = 70,
                IsFixed = FixedState.NotFixed,
                IsChart = true, DataType=GridDataType.NUMBER,
                ApplyStyle = true,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("IQTY", new DamDataColumn()
            {
                Key = "IQTY",
                Text = "유입량",
                Digit = 3,
                Readonly = true,
                Width = 70,
                IsFixed = FixedState.NotFixed,
                IsChart = true, DataType=GridDataType.NUMBER, ApplyStyle = true,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("ETCIQTY1", new DamDataColumn()
            {
                Key = "ETCIQTY1",
                Text = "기타 유입량1",
                Digit = 3,
                Readonly = true,
                Width = 70,
                IsFixed = FixedState.NotFixed,
                IsChart = true, DataType=GridDataType.NUMBER, ApplyStyle = true,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("ETCIQTY2", new DamDataColumn()
            {
                Key = "ETCIQTY2",
                Text = "기타 유입량2",
                Digit = 2,
                Readonly = false,
                Width = 70,
                IsFixed = FixedState.NotFixed,
                IsChart = true, DataType=GridDataType.NUMBER, ApplyStyle = true,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("ETQTY", new DamDataColumn()
            {
                Key = "ETQTY",
                Text = "공용량",
                Digit = 3,
                Readonly = true,
                Width = 70,
                IsFixed = FixedState.NotFixed,
                IsChart = true, DataType=GridDataType.NUMBER, ApplyStyle = true,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("TDQTY", new DamDataColumn()
            {
                Key = "TDQTY",
                Text = "총방류량",
                Digit = 2,
                Readonly = true,
                Width = 70,
                IsFixed = FixedState.NotFixed,
                IsChart = true, DataType=GridDataType.NUMBER, ApplyStyle = true,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("EDQTY", new DamDataColumn()
            {
                Key = "EDQTY",
                Text = "발전 방류량",
                Digit = 2,
                Readonly = false,
                Width = 70,
                IsFixed = FixedState.NotFixed,
                IsChart = true, DataType=GridDataType.NUMBER, ApplyStyle = true,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("ETCEDQTY", new DamDataColumn()
            {
                Key = "ETCEDQTY",
                Text = "기타발전 방류량",
                Digit = 2,
                Readonly = false,
                Width = 70,
                IsFixed = FixedState.NotFixed,
                IsChart = true, DataType=GridDataType.NUMBER, ApplyStyle = true,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("SPDQTY", new DamDataColumn()
            {
                Key = "SPDQTY",
                Text = "수문 방류량",
                Digit = 2,
                Readonly = false,
                Width = 70,
                IsFixed = FixedState.NotFixed,
                IsChart = true, DataType=GridDataType.NUMBER, ApplyStyle = true,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("ETCDQTY1", new DamDataColumn()
            {
                Key = "ETCDQTY1",
                Text = "기타 방류량1",
                Digit = 2,
                Readonly = false,
                Width = 70,
                IsFixed = FixedState.NotFixed,
                IsChart = true, DataType=GridDataType.NUMBER, ApplyStyle = true,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("ETCDQTY2", new DamDataColumn()
            {
                Key = "ETCDQTY2",
                Text = "기타 방류량2",
                Digit = 2,
                Readonly = false,
                Width = 70,
                IsFixed = FixedState.NotFixed,
                IsChart = true, DataType=GridDataType.NUMBER, ApplyStyle = true,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("ETCDQTY3", new DamDataColumn()
            {
                Key = "ETCDQTY3",
                Text = "기타 방류량3",
                Digit = 2,
                Readonly = false,
                Width = 70,
                IsFixed = FixedState.NotFixed,
                IsChart = true, DataType=GridDataType.NUMBER, ApplyStyle = true,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("OTLTDQTY", new DamDataColumn()
            {
                Key = "OTLTDQTY",
                Text = "아울렛 방류량",
                Digit = 2,
                Readonly = false,
                Width = 70,
                IsFixed = FixedState.NotFixed,
                IsChart = true, DataType=GridDataType.NUMBER, ApplyStyle = true,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("ITQTY1", new DamDataColumn()
            {
                Key = "ITQTY1",
                Text = "취수1",
                Digit = 2,
                Readonly = false,
                Width = 70,
                IsFixed = FixedState.NotFixed,
                IsChart = true, DataType=GridDataType.NUMBER, ApplyStyle = true,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("ITQTY2", new DamDataColumn()
            {
                Key = "ITQTY2",
                Text = "취수2",
                Digit = 2,
                Readonly = false,
                Width = 70,
                IsFixed = FixedState.NotFixed,
                IsChart = true, DataType=GridDataType.NUMBER, ApplyStyle = true,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("ITQTY3", new DamDataColumn()
            {
                Key = "ITQTY3",
                Text = "취수3",
                Digit = 2,
                Readonly = false,
                Width = 70,
                IsFixed = FixedState.NotFixed,
                IsChart = true, DataType=GridDataType.NUMBER, ApplyStyle = true,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("DAMBSARF", new DamDataColumn()
            {
                Key = "DAMBSARF",
                Text = "평균우량",
                Digit = 2,
                Readonly = true,
                Width = 70,
                IsFixed = FixedState.NotFixed,
                IsChart = true, DataType=GridDataType.NUMBER, ApplyStyle = true,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("CGEMPNM", new DamDataColumn()
            {
                Key = "CGEMPNM",
                Text = "보정자",
                Digit = -1,
                Readonly = true,
                Width = 65,
                IsFixed = FixedState.NotFixed,
                IsChart = false,
                ApplyStyle = false, DataType = GridDataType.STRING,
                Alignment = HorizontalAlignment.Center
            });
            _datas.Add("CGDT", new DamDataColumn()
            {
                Key = "CGDT",
                Text = "보정일시",
                Digit = -1,
                Readonly = true,
                Width = 120,
                IsFixed = FixedState.NotFixed,
                IsChart = false,
                ApplyStyle = false, DataType = GridDataType.DATE,
                Alignment = HorizontalAlignment.Center
            });
            _datas.Add("CHKEMPNM", new DamDataColumn()
            {
                Key = "CHKEMPNM",
                Text = "확인자",
                Digit = -1,
                Readonly = true,
                Width = 65,
                IsFixed = FixedState.NotFixed,
                IsChart = false,
                ApplyStyle = false, DataType = GridDataType.STRING,
                Alignment = HorizontalAlignment.Center
            });
            _datas.Add("CHKDT", new DamDataColumn()
            {
                Key = "CHKDT",
                Text = "확인일시",
                Digit = -1,
                Readonly = true,
                Width = 120,
                IsFixed = FixedState.NotFixed,
                IsChart = false,
                ApplyStyle = false,
                DataType = GridDataType.DATE,
                Alignment = HorizontalAlignment.Center
            });
        }
        #endregion


        #region -- 일자료 컬럼 생성 --
        private void InitColumnsByDay()
        {
            
            _datas.Add("RWL", new DamDataColumn()
            {
                Key = "RWL",
                Text = "저수위",
                Digit = -1,
                Readonly = true,
                Width = 70,
                IsFixed = FixedState.NotFixed,
                IsChart = true, DataType=GridDataType.NUMBER, ApplyStyle = true,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("VYRWL", new DamDataColumn()
            {
                Key = "VYRWL",
                Text = "전년 저수위",
                Digit = -1,
                Readonly = true,
                Width = 70,
                IsChart = true, DataType=GridDataType.NUMBER,
                ApplyStyle = true,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("OYRWL", new DamDataColumn()
            {
                Key = "OYRWL",
                Text = "예년 저수위",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("MXRWL", new DamDataColumn()
            {
                Key = "MXRWL",
                Text = "최고 저수위",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("MXRWLDH", new DamDataColumn()
            {
                Key = "MXRWLDH",
                Text = "최고 저수위일자",
                Digit = -1,
                Readonly = true,
                Width = 110,
                IsChart = false,
                DataType = GridDataType.DATE,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Center
            });
            _datas.Add("MNRWL", new DamDataColumn()
            {
                Key = "MNRWL",
                Text = "최저 저수위",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("MNRWLDH", new DamDataColumn()
            {
                Key = "MNRWLDH",
                Text = "최저 저수위일자",
                Digit = -1,
                Readonly = true,
                Width = 110,
                IsChart = false,
                IsFixed = FixedState.NotFixed,
                DataType = GridDataType.DATE,
                Alignment = HorizontalAlignment.Center
            });
            _datas.Add("RSQTY", new DamDataColumn()
            {
                Key = "RSQTY",
                Text = "저수량",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("VYRSQTY", new DamDataColumn()
            {
                Key = "VYRSQTY",
                Text = "전년 저수량",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("OYRSQTY", new DamDataColumn()
            {
                Key = "OYRSQTY",
                Text = "예년 저수량",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("IQTY", new DamDataColumn()
            {
                Key = "IQTY",
                Text = "유입량",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("VYIQTY", new DamDataColumn()
            {
                Key = "VYIQTY",
                Text = "전년 유입량",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("OYIQTY", new DamDataColumn()
            {
                Key = "OYIQTY",
                Text = "예년 유입량",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("MXIQTY", new DamDataColumn()
            {
                Key = "MXIQTY",
                Text = "최대 유입량",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("MXIQTYDH", new DamDataColumn()
            {
                Key = "MXIQTYDH",
                Text = "최대 유입량일자",
                Digit = -1,
                Readonly = true,
                Width = 110,
                IsChart = false,
                DataType = GridDataType.DATE,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Center
            });
            _datas.Add("ETCIQTY1", new DamDataColumn()
            {
                Key = "ETCIQTY1",
                Text = "기타 유입량1",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("OYETCIQTY1", new DamDataColumn()
            {
                Key = "OYETCIQTY1",
                Text = "예년기타 유입량1",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("ETCIQTY2", new DamDataColumn()
            {
                Key = "ETCIQTY2",
                Text = "기타 유입량2",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("OYETCIQTY2", new DamDataColumn()
            {
                Key = "OYETCIQTY2",
                Text = "예년기타 유입량2",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("TDQTY", new DamDataColumn()
            {
                Key = "TDQTY",
                Text = "총방류량",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("VYTDQTY", new DamDataColumn()
            {
                Key = "VYTDQTY",
                Text = "전년 총방류량",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("OYTDQTY", new DamDataColumn()
            {
                Key = "OYTDQTY",
                Text = "예년 총방류량",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("MXDQTY", new DamDataColumn()
            {
                Key = "MXDQTY",
                Text = "최대 방류량",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("MXDQTYDH", new DamDataColumn()
            {
                Key = "MXDQTYDH",
                Text = "최대 방류량일자",
                Digit = -1,
                Readonly = true,
                Width = 110,
                IsChart = false,
                IsFixed = FixedState.NotFixed,
                DataType = GridDataType.DATE,
                Alignment = HorizontalAlignment.Center
            });
            _datas.Add("EDQTY", new DamDataColumn()
            {
                Key = "EDQTY",
                Text = "발전 방류량",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("ETCEDQTY", new DamDataColumn()
            {
                Key = "ETCEDQTY",
                Text = "기타발전 방류량",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("SPDQTY", new DamDataColumn()
            {
                Key = "SPDQTY",
                Text = "여수로 방류량",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("ETCDQTY1", new DamDataColumn()
            {
                Key = "ETCDQTY1",
                Text = "기타 방류량1",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("ETCDQTY2", new DamDataColumn()
            {
                Key = "ETCDQTY2",
                Text = "기타 방류량2",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("ETCDQTY3", new DamDataColumn()
            {
                Key = "ETCDQTY3",
                Text = "기타 방류량3",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("OTLTDQTY", new DamDataColumn()
            {
                Key = "OTLTDQTY",
                Text = "아울렛 방류량",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("ITQTY1", new DamDataColumn()
            {
                Key = "ITQTY1",
                Text = "취수량1",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("ITQTY2", new DamDataColumn()
            {
                Key = "ITQTY2",
                Text = "취수량2",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("ITQTY3", new DamDataColumn()
            {
                Key = "ITQTY3",
                Text = "취수량3",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("RF", new DamDataColumn()
            {
                Key = "RF",
                Text = "강수량",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("VYRF", new DamDataColumn()
            {
                Key = "VYRF",
                Text = "전년 강수량",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("OYRF", new DamDataColumn()
            {
                Key = "OYRF",
                Text = "예년 강수량",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("PYACURF", new DamDataColumn()
            {
                Key = "PYACURF",
                Text = "금년 년누계",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("VYACURF", new DamDataColumn()
            {
                Key = "VYACURF",
                Text = "전년 년누계",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("OYAACURF", new DamDataColumn()
            {
                Key = "OYAACURF",
                Text = "예년 년누계",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("MXRF", new DamDataColumn()
            {
                Key = "MXRF",
                Text = "최대 강우량",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("MXRFDH", new DamDataColumn()
            {
                Key = "MXRFDH",
                Text = "최대 강우일시",
                Digit = -1,
                Readonly = true,
                Width = 110,
                IsChart = false,
                DataType = GridDataType.DATE,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Center
            });
            _datas.Add("EDEXLVL", new DamDataColumn()
            {
                Key = "EDEXLVL",
                Text = "검보정레벨",
                Digit = -1,
                Readonly = true,
                Width = 75,
                IsChart = false,
                DataType = GridDataType.STRING,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Center
            });
            _datas.Add("RSRT", new DamDataColumn()
            {
                Key = "RSRT",
                Text = "저수율",
                Digit = -1,
                Readonly = true,
                Width = 60, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("ETQTY", new DamDataColumn()
            {
                Key = "ETQTY",
                Text = "공용량",
                Digit = -1,
                Readonly = true,
                Width = 60, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("HEDQTY", new DamDataColumn()
            {
                Key = "HEDQTY",
                Text = "HDAPS 발전방류량",
                Digit = -1,
                Readonly = true,
                Width = 80,
                IsChart = true, DataType=GridDataType.NUMBER,
                ApplyStyle = true,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("GEDQTY", new DamDataColumn()
            {
                Key = "GEDQTY",
                Text = "발전정보 발전방류량",
                Digit = -1,
                Readonly = true,
                Width = 80,
                IsChart = true, DataType=GridDataType.NUMBER,
                ApplyStyle = true,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("RF07", new DamDataColumn()
            {
                Key = "RF07",
                Text = "07시 금일강수량",
                Digit = -1,
                Readonly = true,
                Width = 80,
                IsChart = true, DataType=GridDataType.NUMBER,
                ApplyStyle = true,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("VYEDQTY", new DamDataColumn()
            {
                Key = "VYEDQTY",
                Text = "전년발전 방류량",
                Digit = -1,
                Readonly = true,
                Width = 80,
                IsChart = true, DataType=GridDataType.NUMBER,
                ApplyStyle = true,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("VYETCEDQTY", new DamDataColumn()
            {
                Key = "VYETCEDQTY",
                Text = "전년기타 발전방류량",
                Digit = -1,
                Readonly = true,
                Width = 90, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("VYSPDQTY", new DamDataColumn()
            {
                Key = "VYSPDQTY",
                Text = "전년여수로 방류량",
                Digit = -1,
                Readonly = true,
                Width = 80,
                IsChart = true, DataType=GridDataType.NUMBER,
                ApplyStyle = true,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("VYETCDQTY1", new DamDataColumn()
            {
                Key = "VYETCDQTY1",
                Text = "전년기타 방류량1",
                Digit = -1,
                Readonly = true,
                Width = 80,
                IsChart = true, DataType=GridDataType.NUMBER,
                ApplyStyle = true,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("VYETCDQTY2", new DamDataColumn()
            {
                Key = "VYETCDQTY2",
                Text = "전년기타 방류량2",
                Digit = -1,
                Readonly = true,
                Width = 80,
                IsChart = true, DataType=GridDataType.NUMBER,
                ApplyStyle = true,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("VYETCDQTY3", new DamDataColumn()
            {
                Key = "VYETCDQTY3",
                Text = "전년기타 방류량3",
                Digit = -1,
                Readonly = true,
                Width = 80,
                IsChart = true, DataType=GridDataType.NUMBER,
                ApplyStyle = true,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("VYOTLTDQTY1", new DamDataColumn()
            {
                Key = "VYOTLTDQTY1",
                Text = "전년OUTLET 방류량",
                Digit = -1,
                Readonly = true,
                Width = 85,
                IsChart = true, DataType=GridDataType.NUMBER,
                ApplyStyle = true,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("VYITQTY1", new DamDataColumn()
            {
                Key = "VYITQTY1",
                Text = "전년 취수량1",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("VYITQTY2", new DamDataColumn()
            {
                Key = "VYITQTY2",
                Text = "전년 취수량2",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("VYITQTY3", new DamDataColumn()
            {
                Key = "VYITQTY3",
                Text = "전년 취수량3",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("OYEDQTY", new DamDataColumn()
            {
                Key = "OYEDQTY",
                Text = "예년발전 방류량",
                Digit = -1,
                Readonly = true,
                Width = 80,
                IsChart = true, DataType=GridDataType.NUMBER,
                ApplyStyle = true,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("OYETCEDQTY", new DamDataColumn()
            {
                Key = "OYETCEDQTY",
                Text = "예년기타 발전방류량",
                Digit = -1,
                Readonly = true,
                Width = 90, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("OYSPDQTY", new DamDataColumn()
            {
                Key = "OYSPDQTY",
                Text = "예년여수로 방류량",
                Digit = -1,
                Readonly = true,
                Width = 80,
                IsChart = true, DataType=GridDataType.NUMBER,
                ApplyStyle = true,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("OYETCDQTY1", new DamDataColumn()
            {
                Key = "OYETCDQTY1",
                Text = "예년기타 방류량1",
                Digit = -1,
                Readonly = true,
                Width = 80,
                IsChart = true, DataType=GridDataType.NUMBER,
                ApplyStyle = true,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("OYETCDQTY2", new DamDataColumn()
            {
                Key = "OYETCDQTY2",
                Text = "예년기타 방류량2",
                Digit = -1,
                Readonly = true,
                Width = 80,
                IsChart = true, DataType=GridDataType.NUMBER,
                ApplyStyle = true,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("OYETCDQTY3", new DamDataColumn()
            {
                Key = "OYETCDQTY3",
                Text = "예년기타 방류량3",
                Digit = -1,
                Readonly = true,
                Width = 80,
                IsChart = true, DataType=GridDataType.NUMBER,
                ApplyStyle = true,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("OYOTLTDQTY1", new DamDataColumn()
            {
                Key = "OYOTLTDQTY1",
                Text = "예년OUTLET 방류량",
                Digit = -1,
                Readonly = true,
                Width = 90, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("OYITQTY1", new DamDataColumn()
            {
                Key = "OYITQTY1",
                Text = "예년 취수량1",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("OYITQTY2", new DamDataColumn()
            {
                Key = "OYITQTY2",
                Text = "예년 취수량2",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });
            _datas.Add("OYITQTY3", new DamDataColumn()
            {
                Key = "OYITQTY3",
                Text = "예년 취수량3",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });

            _datas.Add("OSPILWL", new DamDataColumn()
            {
                Key = "OSPILWL",
                Text = "방수로 수위",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });

            _datas.Add("ESSSQTY", new DamDataColumn()
            {
                Key = "ESSSQTY",
                Text = "계획 방류량",
                Digit = -1,
                Readonly = true,
                Width = 70, IsChart=true, ApplyStyle=true, DataType=GridDataType.NUMBER,
                IsFixed = FixedState.NotFixed,
                Alignment = HorizontalAlignment.Right
            });

        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets/Sets value for the item by that index
        /// </summary>
        public DamDataColumn this[string key]
        {
            get
            {
                return (DamDataColumn)this._datas[key];
            }
            set
            {
                this._datas[key] = value;
            }
        }
        #endregion

        #region Public Methods
        public void Clear()
        {
            this._datas.Clear();
            
        }
        
        public bool Contains(DamDataColumn item) {
            if (this._datas.ContainsKey(item.Key))
                return true;
            return false;
        }

        public bool ContainsKey(string key)
        {
            return this._datas.ContainsKey(key);   
        }

        public ICollection<DamDataColumn> Values()
        {
            return this._datas.Values;
        }

        public ICollection<string> Keys()
        {
            return this._datas.Keys;
        }

        public IDictionary<string, DamDataColumn> ToCollections()
        {
            return _datas;
        }
        #endregion
    }

    public class DamDataColumn
    {
        public string Key { get; set; }
        public string Text { get; set; }
        public int Digit { get; set; }
        public bool Readonly { get; set; }
        public int Width { get; set; }
        public HorizontalAlignment Alignment { get; set; }
        public FixedState IsFixed { get; set; }
        public bool IsChart { get; set; }
        public bool ApplyStyle { get; set; }
        public GridDataType DataType { get; set; } 
    }
}
