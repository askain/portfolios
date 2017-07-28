using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HDIMSAPP.Utils
{
    public class MinMaxCalculator
    {
        private IList Data;
        public MinMaxCalculator(IList obj)
        {
            this.Data = obj;
        }

        #region == Min & BottomOffset ==
        public bool MinLegendsChanged = true;
        public bool BottomOffsetChanged = true;
        public double BottomOffsetMultipler = 0.05;
        private IDictionary<string, double> MinLegends = new Dictionary<string, double>();
        private double _BottomOffset = 0;
        public double BottomOffset
        {
            get
            {
                if (double.IsInfinity(Max) == true || double.IsInfinity(Min) == true) return 0;

                if (Max == Min) return 0.01;

                if (BottomOffsetChanged == true || _BottomOffset == 0)
                {
                    _BottomOffset = (Max - Min) * BottomOffsetMultipler;
                }
                BottomOffsetChanged = false;
                return _BottomOffset;
            }
        }
        private double _Min = double.NegativeInfinity;
        public double Min
        {
            get
            {
                try
                {
                    if (MinLegendsChanged == true || _Min == double.NegativeInfinity)
                    {
                        _Min = MinLegends.Min<KeyValuePair<string, double>>(m => m.Value);
                    }
                    MinLegendsChanged = false;
                    return _Min;
                }
                catch (Exception ex)
                {

                }
                return double.NegativeInfinity;
            }
        }
        #endregion

        #region == Max & TopOffest ==
        public bool MaxLegendsChanged = true;
        public bool TopOffsetChanged = true;
        public double TopOffsetMultipler = 0.05;
        private IDictionary<string, Double> MaxLegends = new Dictionary<string, double>();
        private double _TopOffset = 0;
        public double TopOffset
        {
            get
            {
                if (double.IsInfinity(Max) == true || double.IsInfinity(Min) == true) return 0;

                if (Max == Min) return 0.05;

                if (TopOffsetChanged == true || _TopOffset == 0)
                {
                    _TopOffset = (Max - Min) * TopOffsetMultipler;
                }
                TopOffsetChanged = false;
                return _TopOffset;
            }
        }
        private double _Max = double.PositiveInfinity;
        public double Max
        {
            get
            {
                try
                {
                    if (MaxLegendsChanged == true || _Max == double.PositiveInfinity)
                    {
                        _Max = MaxLegends.Max<KeyValuePair<string, double>>(m => m.Value);
                    }
                    MaxLegendsChanged = false;
                    return _Max;
                }
                catch (Exception ex)
                {

                }
                return double.PositiveInfinity;
            }
        }
        #endregion

        #region == difference between Top and Bottom ==
        public string DifferenceTandB
        {
            get
            {
                return (Max - Min).ToString("0.##");
            }
        }

        public string Unit
        {
            get
            {
                if (MinLegends.Count == 0) return "";

                return MinLegends.Keys.FirstOrDefault();
            }
        }

        #endregion

        #region == functions ==

        public void AddLegend(string LegendName)
        {
            //MessageBox.Show(LegendName);

            IList<double> Legend = new List<double>();

            //MessageBox.Show(LegendName);
            foreach (object t in Data)
            {
                //Dictionary 타입인 경우
                IDictionary d = t as IDictionary;
                if (d != null)
                {
                    
                    if (d[LegendName] != null && string.IsNullOrEmpty(d[LegendName].ToString()) == false )
                    {
                        //
                        double dbl_p;
                        if (double.TryParse(d[LegendName].ToString(), out dbl_p) == true)
                        {
                            Legend.Add(dbl_p);
                        }
                    }
                    
                }
                else
                {
                    //일반 모델인경우
                    object value = CommonUtil.GetValue(t, LegendName);

                    if(value != null) {
                        string str_p = value.ToString();
                        double dbl_p ;
                        if (double.TryParse(str_p, out dbl_p) == true)
                        {
                            Legend.Add(dbl_p);
                        }
                    }
                }
            }

            if(Legend.Count != 0 ) {
                MinLegends.Add(LegendName, Legend.Min());
                MaxLegends.Add(LegendName, Legend.Max());
                MinLegendsChanged = true;
                MaxLegendsChanged = true;
                TopOffsetChanged = true;
                BottomOffsetChanged = true;
            }
        }

        public void RemoveLegend(string LegendName)
        {
            try
            {
                MinLegends.Remove(LegendName);
                MaxLegends.Remove(LegendName);
            }
            catch (Exception ex)
            {
                //
            }
            MinLegendsChanged = true;
            MaxLegendsChanged = true;
            TopOffsetChanged = true;
            BottomOffsetChanged = true;

        }

        public void UpdateLegend()
        {
            try
            {
                string[] keys = new string[MinLegends.Count];
                MinLegends.Keys.CopyTo(keys, 0);

                foreach (string key in keys)
                {
                    RemoveLegend(key);
                    AddLegend(key);
                }

            }
            catch (Exception ex)
            {
                //
            }
        }
        #endregion

    }
}
