using System;

namespace HDIMS.Utils
{
    public class ChartUtil
    {
        #region SetRangeValue - 범례 최대,최소값 설정 double
        /// <summary>
        /// SetRangeValue - 범례 최대,최소값 설정 double
        /// </summary>
        public static double[] SetRangeValue(double dobMax, double dobMin)
        {
            double[] iRet = new double[2];
            if (dobMax == 0 && dobMin == 0)
            {
                iRet[0] = 1.5;
                iRet[1] = -1.2;
            }
            else if (dobMax == dobMin)
            {
                if (Math.Abs(dobMax) < 0.1)
                {
                    iRet[0] = dobMax + 0.01;
                    iRet[1] = dobMin - 0.04;
                }
                else if (Math.Abs(dobMax) < 1)
                {
                    iRet[0] = dobMax + 0.07;
                    iRet[1] = dobMin - 0.09;
                }
                else if (Math.Abs(dobMax) < 10)
                {
                    iRet[0] = dobMax + 0.9;
                    iRet[1] = dobMin - 1.5;
                }
                else if (Math.Abs(dobMax) < 100)
                {
                    iRet[0] = dobMax + 12.5;
                    iRet[1] = dobMin - 16.5;
                }
                else
                {
                    iRet[0] = dobMax + 80.5;
                    iRet[1] = dobMin - 90.5;
                }
            }
            else
            {
                if (Math.Abs(dobMax - dobMin) < 0.1)
                {
                    iRet[0] = dobMax + 0.01;
                    iRet[1] = dobMin - 0.04;
                }
                else if (Math.Abs(dobMax - dobMin) < 1)
                {
                    iRet[0] = dobMax + 0.07;
                    iRet[1] = dobMin - 0.09;
                }
                else if (Math.Abs(dobMax - dobMin) < 10)
                {
                    iRet[0] = dobMax + 0.9;
                    iRet[1] = dobMin - 1.5;
                }
                else if (Math.Abs(dobMax - dobMin) < 50)
                {
                    iRet[0] = dobMax + 12.5;
                    iRet[1] = dobMin - 16.5;
                }
                else if (Math.Abs(dobMax - dobMin) < 100)
                {
                    iRet[0] = dobMax + 20.5;
                    iRet[1] = dobMin - 30.5;
                }
                else if (Math.Abs(dobMax - dobMin) < 500)
                {
                    iRet[0] = dobMax + 80.5;
                    iRet[1] = dobMin - 90.5;
                }
                else if (Math.Abs(dobMax - dobMin) < 1000)
                {
                    iRet[0] = dobMax + 200.5;
                    iRet[1] = dobMin - 300.5;
                }
                else
                {
                    iRet[0] = dobMax + 500.5;
                    iRet[1] = dobMin - 700.5;
                }

            }

            return iRet;
        }
        #endregion

        public static string GetLegendColor(int idx)
        {
            string[] colors =new string[] {"A5BC4E", "0000FF", "00FF00", "FF0000", "DAA520", "4169E1", "FF6347", "00BFFF", "006400", "800000", "DEB887", "4B0082", "8B4513", "1E90FF", "008B8B", "FFD700", "800080", "A52A2A", "87CEEB", "2F4F4F", "8A2BE2", "FF7F50", "40E0D0", "BC8F8F", "B8860B", "20B2AA", "556B2F", "C0C0C0", "C71585", "CD853F", "DC143C", "000080", "228B22", "FF00FF", "E9967A", "6B8E23", "FF1493", "F08080", "7CFC00", "FF69B4", "F4A460", "BDB76B", "483D8B", "FA8072", "9ACD32", "6A5ACD", "FF4500", "90EE90", "9370D8", "D2B48C", "A0522D", "8FBC8F", "808080", "FFA07A", "808000", "BA55D3", "CD5C5C", "EE82EE", "32CD32", "000000"};

            if (colors.Length > idx)
            {
                return "#"+colors[idx];
            }
            return "#FFFFFF";
        }
    }
}