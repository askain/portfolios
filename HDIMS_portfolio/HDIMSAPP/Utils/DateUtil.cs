using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace HDIMSAPP.Utils
{
    public class DateUtil
    {
        #region == 타 입 변 환 ==
        /// <summary>
        /// 문자열에따라 숫자를 부여함
        /// D -> 8
        /// M -> 6
        /// Y -> 4
        /// </summary>
        /// <param name="dtype">문자</param>
        /// <returns>숫자</returns>
        public static int GetDataLen(string dtype)
        {
            int dlen = 8;
            if (dtype.Equals("M")) dlen = 6;
            else if (dtype.Equals("Y")) dlen = 4;
            return dlen;
        }

        /// <summary>
        /// 문자열을 날짜형식문자열로 으로 변경
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static string formatDate(string v)
        {
            var cText = "";
            if (v != string.Empty && v != null)
            {
                if (v.Length >= 4)
                {
                    cText = v.Substring(0, 4);
                }
                if (v.Length >= 6)
                {
                    cText += "-" + v.Substring(4, 2);
                }
                if (v.Length >= 8)
                {
                    cText += "-" + v.Substring(6, 2);
                }
                if (v.Length >= 10)
                {
                    cText += " " + v.Substring(8, 2);
                }
                if (v.Length >= 12)
                {
                    cText += ":" + v.Substring(10, 2);
                }
                //if (v.Length >= 14)
                //{
                //    cText += ":" + v.Substring(12, 2);
                //}
            }
            return cText;
        }

        /// <summary>
        /// 24:00형식을 00:00형식으로 변경
        /// </summary>
        /// <param name="p_date"></param>
        /// <returns></returns>
        public static string convToHH24(string p_date)
        {
            string v_ret = p_date;
            string v_dhm = "000000";
            if (p_date.Length == 14)
            {
                v_dhm = "000000";
            }
            else if (p_date.Length == 12)
            {
                v_dhm = "0000";
            }
            else
            {
                v_dhm = "00";
            }

            if (p_date.Substring(8, 2).Equals("24"))
            {
                v_ret = DateTime.ParseExact(p_date.Substring(0, 8), "yyyyMMdd", null).AddDays(1).ToString("yyyyMMdd") + v_dhm;
            }


            return v_ret;
        }

        public static string convTo24DT(string p_date)
        {
            string v_dhm = "240000";
            string v_ret = p_date;

            if (p_date.Length == 14)
            {
                v_dhm = "240000";
            }
            else if (p_date.Length == 12)
            {
                v_dhm = "2400";
            }
            else
            {
                v_dhm = "24";
            }

            if (p_date.Length > 10)
            {
                if (p_date.Substring(8, 4).Equals("0000"))
                {
                    v_ret = DateTime.ParseExact(p_date.Substring(0, 8), "yyyyMMdd", null).AddDays(-1).ToString("yyyyMMdd") + v_dhm;
                }
                else
                {
                    v_ret = p_date;
                }
            }
            else
            {
                if (p_date.Substring(8, 2).Equals("00"))
                {
                    v_ret = DateTime.ParseExact(p_date.Substring(0, 8), "yyyyMMdd", null).AddDays(-1).ToString("yyyyMMdd") + v_dhm;
                }
                else
                {
                    v_ret = p_date;
                }
            }

            return v_ret;
        }

        public static string AddDateTime(string p_date, double add, string type)
        {
            string v_ret = p_date;
            string c_date = convToHH24(p_date);
            string pattern = "yyyyMMdd";
            if (p_date.Length == 14) pattern = "yyyyMMddHHmmss";
            else if (p_date.Length == 12) pattern = "yyyyMMddHHmm";
            else if (p_date.Length == 10) pattern = "yyyyMMddHH";
            else if (p_date.Length == 8) pattern = "yyyyMMdd";
            if (type.Equals("mm"))
            {
                v_ret = convTo24DT(DateTime.ParseExact(c_date, pattern, null).AddMinutes(add).ToString(pattern));
            }
            else if (type.Equals("HH"))
            {
                v_ret = convTo24DT(DateTime.ParseExact(c_date, pattern, null).AddHours(add).ToString(pattern));
            }
            else if (type.Equals("dd"))
            {
                v_ret = convTo24DT(DateTime.ParseExact(c_date, pattern, null).AddDays(add).ToString(pattern));
            }
            else if (type.Equals("MM"))
            {
                v_ret = convTo24DT(DateTime.ParseExact(c_date, pattern, null).AddMonths((int)add).ToString(pattern));
            }

            return v_ret;
        }

        public static string AddDateTimeHH24(string p_date, double add, string type)
        {
            string v_ret = p_date;
            string c_date = p_date;
            string pattern = "yyyyMMdd";
            if (p_date.Length == 14) pattern = "yyyyMMddHHmmss";
            else if (p_date.Length == 12) pattern = "yyyyMMddHHmm";
            else if (p_date.Length == 10) pattern = "yyyyMMddHH";
            else if (p_date.Length == 8) pattern = "yyyyMMdd";
            if (type.Equals("mm"))
            {
                v_ret = convTo24DT(DateTime.ParseExact(c_date, pattern, null).AddMinutes(add).ToString(pattern));
            }
            else if (type.Equals("HH"))
            {
                v_ret = convTo24DT(DateTime.ParseExact(c_date, pattern, null).AddHours(add).ToString(pattern));
            }
            else if (type.Equals("dd"))
            {
                v_ret = convTo24DT(DateTime.ParseExact(c_date, pattern, null).AddDays(add).ToString(pattern));
            }
            else if (type.Equals("MM"))
            {
                v_ret = convTo24DT(DateTime.ParseExact(c_date, pattern, null).AddMonths((int)add).ToString(pattern));
            }

            return v_ret;
        }

        public static DateTime convertToDateTime(string d, string format)
        {
            return DateTime.ParseExact(DateUtil.convToHH24(d), format, System.Globalization.CultureInfo.InvariantCulture);
        }
        #endregion

        #region == 중간날짜 구하기(복수의 날짜 리스트 구하기) ==
        /// <summary>
        /// 시작날짜와 마지막날짜 사이의 날짜를 리스트화 시킴.
        /// 단, 날짜 타입에 따라서 동적으로 일 월 년도로 바꾸어 저장.
        /// </summary>
        /// <param name="dtype">날짜 타입(GetDataLen참조)</param>
        /// <param name="sdate">시작날짜</param>
        /// <param name="edate">끝날짜</param>
        /// <param name="prefix">prefix</param>
        /// <returns></returns>
        public static IList<string> GetDateList(string dtype, string sdate, string edate, string prefix)
        {
            //IList<string> list = new List<string>();
            //int int_dtype = GetDataLen(dtype);
            //int sdate_int = int.Parse(sdate.Substring(0, int_dtype));
            //int edate_int = int.Parse(edate.Substring(0, int_dtype));
            //string str_dtype = "yyyyMMdd";
            //if (GetDataLen(dtype) == 4)
            //{
            //    str_dtype = "yyyy";
            //}
            //else if (GetDataLen(dtype) == 6)
            //{
            //    str_dtype = "yyyyMM";
            //}
            //DateTime sdate = 

            //while (sdate_int <= edate_int)
            //{
            //    if (int_dtype == 8)
            //    {

            //    }
            //    else if (int_dtype == 6)
            //    {

            //    }
            //    else
            //    {
            //        list.Add(prefix + sdate_int.ToString());
            //        sdate_int++;
            //    }
            //}
            IList<string> DateList = new List<string>();
            DateTimeFormatInfo koFT = System.Globalization.DateTimeFormatInfo.InvariantInfo;

            string sameTp = "D";        //
            if (dtype.Equals("D"))
            {
                if (!sdate.Substring(0, 4).Equals(edate.Substring(0, 4))) sameTp = "Y";
                else if (!sdate.Substring(0, 6).Equals(edate.Substring(0, 6))) sameTp = "M";
                else sameTp = "D";
            }
            else
            {
                if (!sdate.Substring(0, 4).Equals(edate.Substring(0, 4))) sameTp = "Y";
                else sameTp = "M";
            }

            DateTime StartDate = new DateTime(Int32.Parse(sdate.Substring(0,4)), Int32.Parse(sdate.Substring(4,2)), Int32.Parse(sdate.Substring(6,2)));
            DateTime EndDate = new DateTime(Int32.Parse(edate.Substring(0, 4)), Int32.Parse(edate.Substring(4, 2)), Int32.Parse(edate.Substring(6, 2)));
            if (dtype.Equals("M"))
            {
                //월일 경우 날짜비교상 종료일보다 적은 일이 있을 수 있으므로 시작날짜는 1로 잡는다. 
                StartDate = new DateTime(Int32.Parse(sdate.Substring(0, 4)), Int32.Parse(sdate.Substring(4, 2)), 1);
            }
            string datePattern = "yyyyMM";
            if (dtype.Equals("D"))
            {
                if (sameTp.Equals("D"))
                {
                    datePattern = "dd";
                }
                else if (sameTp.Equals("M"))
                {
                    datePattern = "MMdd";
                }
                else
                {
                    datePattern = "yyyyMMdd";
                }
                DateList.Add(prefix + StartDate.ToString(datePattern, koFT));
                while (StartDate.AddDays(1) <= EndDate)
                {
                    StartDate = StartDate.AddDays(1);
                    DateList.Add(prefix + StartDate.ToString(datePattern, koFT));
                } 
            }
            else
            {
                if (sameTp.Equals("M"))
                {
                    datePattern = "MM";
                }
                else
                {
                    datePattern = "yyyyMM";
                }
                DateList.Add(prefix + StartDate.ToString(datePattern, koFT));
                while (StartDate.AddMonths(1) <= EndDate)
                {
                    StartDate = StartDate.AddMonths(1);
                    DateList.Add(prefix + StartDate.ToString(datePattern, koFT));
                } 
            }

            return DateList;
        }

        public static IList<string> GetDateList(string dtype, string sdate, string edate, string prefix, Func<string, string, string, string> myfunction)
        {
            //IList<string> list = new List<string>();
            //int int_dtype = GetDataLen(dtype);
            //int sdate_int = int.Parse(sdate.Substring(0, int_dtype));
            //int edate_int = int.Parse(edate.Substring(0, int_dtype));
            //string str_dtype = "yyyyMMdd";
            //if (GetDataLen(dtype) == 4)
            //{
            //    str_dtype = "yyyy";
            //}
            //else if (GetDataLen(dtype) == 6)
            //{
            //    str_dtype = "yyyyMM";
            //}
            //DateTime sdate = 

            //while (sdate_int <= edate_int)
            //{
            //    if (int_dtype == 8)
            //    {

            //    }
            //    else if (int_dtype == 6)
            //    {

            //    }
            //    else
            //    {
            //        list.Add(prefix + sdate_int.ToString());
            //        sdate_int++;
            //    }
            //}
            IList<string> DateList = new List<string>();
            DateTimeFormatInfo koFT = System.Globalization.DateTimeFormatInfo.InvariantInfo;

            string sameTp = "D";        //
            if (dtype.Equals("D"))
            {
                if (!sdate.Substring(0, 4).Equals(edate.Substring(0, 4))) sameTp = "Y";
                else if (!sdate.Substring(0, 6).Equals(edate.Substring(0, 6))) sameTp = "M";
                else sameTp = "D";
            }
            else
            {
                if (!sdate.Substring(0, 4).Equals(edate.Substring(0, 4))) sameTp = "Y";
                else sameTp = "M";
            }

            DateTime StartDate = new DateTime(Int32.Parse(sdate.Substring(0, 4)), Int32.Parse(sdate.Substring(4, 2)), Int32.Parse(sdate.Substring(6, 2)));
            DateTime EndDate = new DateTime(Int32.Parse(edate.Substring(0, 4)), Int32.Parse(edate.Substring(4, 2)), Int32.Parse(edate.Substring(6, 2)));
            if (dtype.Equals("M"))
            {
                //월일 경우 날짜비교상 종료일보다 적은 일이 있을 수 있으므로 시작날짜는 1로 잡는다. 
                StartDate = new DateTime(Int32.Parse(sdate.Substring(0, 4)), Int32.Parse(sdate.Substring(4, 2)), 1);
            }
            string datePattern = "yyyyMM";
            string toDatePattern = "yyyy년 MM월";
            if (dtype.Equals("D"))
            {
                if (sameTp.Equals("D"))
                {
                    datePattern = "dd";
                    toDatePattern = "dd일";
                }
                else if (sameTp.Equals("M"))
                {
                    datePattern = "MMdd";
                    toDatePattern = "MM월 dd일";
                }
                else
                {
                    datePattern = "yyyyMMdd";
                    toDatePattern = "yyyy년 MM월dd일";
                }
                DateList.Add(prefix + myfunction(StartDate.ToString(datePattern, koFT), datePattern, toDatePattern));
                while (StartDate.AddDays(1) <= EndDate)
                {
                    StartDate = StartDate.AddDays(1);
                    DateList.Add(prefix + myfunction(StartDate.ToString(datePattern, koFT), datePattern, toDatePattern));
                }
            }
            else
            {
                if (sameTp.Equals("M"))
                {
                    datePattern = "MM";
                    toDatePattern = "MM월";
                }
                else
                {
                    datePattern = "yyyyMM";
                    toDatePattern = "yyyy년 MM월";
                }
                DateList.Add(prefix + myfunction(StartDate.ToString(datePattern, koFT), datePattern, toDatePattern));
                while (StartDate.AddMonths(1) <= EndDate)
                {
                    StartDate = StartDate.AddMonths(1);
                    DateList.Add(prefix + myfunction(StartDate.ToString(datePattern, koFT), datePattern, toDatePattern));
                }
            }

            return DateList;
        }
        #endregion

        #region == 날짜계산 ==

        #region == Month ==
        public static DateTime GetFirstDateOfMonth(DateTime d)
        {
            return new DateTime(d.Year, d.Month, 1);
        }

        public static DateTime GetLastDateOfMonth(DateTime d)
        {
            d = d.AddMonths(1);
            return (new DateTime(d.Year, d.Month, 1)).AddDays(-1);
        }
        #endregion

        #region == Quarter ==
        public static DateTime GetFirstDateOfQuarter(DateTime d)
        {
            int year = d.Year;
            int month = 0;
            int day = 1;
            switch (d.Month)
            {
                case 1:
                case 2:
                case 3:
                    month = 1;
                    break;
                case 4:
                case 5:
                case 6:
                    month = 4;
                    break;
                case 7:
                case 8:
                case 9:
                    month = 7;
                    break;
                case 10:
                case 11:
                case 12:
                    month = 10;
                    break;
                default:
                    break;
            }
            return new DateTime(year, month, day);
        }

        public static DateTime GetLastDateOfQuarter(DateTime d)
        {
            int year = d.Year;
            int month = 0;
            int day = 0;
            switch (d.Month)
            {
                case 1:
                case 2:
                case 3:
                    month = 3;
                    day = 31;
                    break;
                case 4:
                case 5:
                case 6:
                    month = 4;
                    day = 30;
                    break;
                case 7:
                case 8:
                case 9:
                    month = 9;
                    day = 30;
                    break;
                case 10:
                case 11:
                case 12:
                    month = 12;
                    day = 31;
                    break;
                default:
                    break;
            }
            return new DateTime(year, month, day);
        }
        #endregion

        #region == Year ==
        public static DateTime GetFirstDateOfYear(DateTime d)
        {
            return new DateTime(d.Year, 1, 1);
        }

        public static DateTime GetLastDateOfYear(DateTime d)
        {
            return new DateTime(d.Year, 12, 31);
        }
        #endregion

        #endregion

        public static string convStrToDate(string date, bool isSecond=false)
        {
            StringBuilder ret = new StringBuilder();

            if (date.Length >= 6)
            {
                ret.Append(date.Substring(0, 4) + "-" + date.Substring(4, 2));
            }

            if (date.Length >= 8)
            {
                ret.Append("-" + date.Substring(6, 2));
            }

            if (date.Length >= 10)
            {
                ret.Append(" " + date.Substring(8, 2));
            }

            if (date.Length >= 12)
            {
                ret.Append(":" + date.Substring(10, 2));
            }

            if (date.Length >= 14 && isSecond)
            {
                ret.Append(":" + date.Substring(12, 2));
            }

            return ret.ToString();
        }

        public static string convStrToDateKor(string date, string statTp = "M", bool isSecond = false)
        {
            StringBuilder ret = new StringBuilder();
            if (date.Length == 2)
            {
                ret.Append(date);
                if ("M".Equals(statTp)) ret.Append("월");
                else if ("Q".Equals(statTp)) ret.Append("분기");
                else ret.Append("일");
                return ret.ToString();
            }
            else if (date.Length == 4)
            {
                if (int.Parse(date) > 1900)
                {
                    ret.Append(date);
                    ret.Append("년");
                }
                else
                {
                    ret.Append(date.Substring(0,2));
                    ret.Append("년");
                    if ("Q".Equals(statTp))
                    {
                        ret.Append(" ");
                        ret.Append(date.Substring(2, 2));
                        ret.Append("분기");
                    } 
                    else 
                    {
                        ret.Append(" ");
                        ret.Append(date.Substring(2, 2));
                        ret.Append("월");
                    }
                }
                return ret.ToString();
            }
            else if (date.Length == 5)
            {
                if ("Q".Equals(statTp))
                {
                    ret.Append(date.Substring(0, 4));
                    ret.Append("년");
                    ret.Append(" ");
                    ret.Append(date.Substring(4, 1));
                    ret.Append("분기");
                }
                else
                {

                }
                return ret.ToString();
            }


            if (date.Length >= 6)
            {
                ret.Append(date.Substring(0, 4));
                ret.Append("년 ");
                if ("Q".Equals(statTp))
                {
                    ret.Append(date.Substring(4, 2));
                    ret.Append("분기");
                }
                else
                {
                    ret.Append(date.Substring(4, 2));
                    ret.Append("월");
                }
            }

            if (date.Length >= 8)
            {
                ret.Append(" ");
                ret.Append(date.Substring(6, 2));
                ret.Append("일");
            }

            if (date.Length >= 10)
            {
                ret.Append(" ");
                ret.Append(date.Substring(8, 2));
                ret.Append("시");
            }

            if (date.Length >= 12)
            {
                ret.Append(" ");
                ret.Append(date.Substring(10, 2));
                ret.Append("분");
            }

            if (date.Length >= 14 && isSecond)
            {
                ret.Append(" ");
                ret.Append(date.Substring(12, 2));
            }

            return ret.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"> ex) "201203091245"</param>
        /// <param name="isSecond"></param>
        /// <param name="InuputDateType"> ex)  "yyyyMMddhhmm" </param>
        /// <returns> 2012년 03시 09분 12시 45분</returns>
        public static string convStrToDateKor(string date, string InuputDateType, string OutputDateType)
        {
            int index_yyyy = InuputDateType.IndexOf("yyyy");
            int index_yy = -1;
            if (index_yyyy == -1) index_yy = InuputDateType.IndexOf("yy");
            int index_MM = InuputDateType.IndexOf("MM");
            int index_dd = InuputDateType.IndexOf("dd");
            int index_hh = InuputDateType.IndexOf("hh");
            int index_mm = InuputDateType.IndexOf("mm");

            string ret = OutputDateType.Trim();

            if (index_yyyy >= 0)
            {
                ret = ret.Replace("yyyy", date.Substring(index_yyyy, 4));
            }

            if (index_yy >= 0)
            {
                ret = ret.Replace("yy", date.Substring(index_yy, 2));
            }

            if (index_MM >= 0)
            {
                ret = ret.Replace("MM", date.Substring(index_MM, 2));
            }

            if (index_dd >= 0)
            {
                ret = ret.Replace("dd", date.Substring(index_dd, 2));
            }

            if (index_hh >= 0)
            {
                ret = ret.Replace("hh", date.Substring(index_hh, 2));
            }

            if (index_mm >= 0)
            {
                ret = ret.Replace("mm", date.Substring(index_mm, 2));
            }

            return ret;
        }
    }
}
