using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HDIMSAPP.Utils
{
    public class StatUtil
    {

        //public static IEnumerable GroupBy<T>(IEnumerable<T> Target, string GroupBy, string[] Select)
        //    where T : class, new()
        //{
        //    IList<T> ret = new List<T>();

        //    foreach (T t in Target)
        //    {
        //        //기존 값이 있는지 확인
        //        T ExistObject = GetExist<T>(ret, t, GroupBy);

        //        if (ExistObject == null)
        //        {
        //            //기존 값이 없으면 새로 객체를 생성해서 넣음
        //            ret.Add(t);
        //        }
        //        else
        //        {
        //            //기존 값이 있으면 기존 값에 새 값을 더함.
        //            foreach (string s in Select)
        //            {
        //                object objA = CommonUtil.GetValue(ExistObject, s);
        //                object objB = CommonUtil.GetValue(t, s);

        //                if (objA != null && objB != null)
        //                {
        //                    int A = 0;
        //                    int B = 0;
        //                    int AB = 0;
        //                    if (int.TryParse(objA.ToString(), out A) && int.TryParse(objA.ToString(), out B))
        //                    {
        //                        AB = A + B;
        //                        CommonUtil.SetValue(ExistObject, s, AB.ToString());
        //                    }
        //                }
        //            }

        //        }
        //    }
        //    return ret;
        //}

        //public static T GetExist<T>(IEnumerable<T> list, T WannaFindThis, string GroupBy)
        //    where T : class
        //{
        //    string primaryKey = CommonUtil.GetValue(WannaFindThis, GroupBy).ToString();

        //    foreach (T t in list)
        //    {
        //        string thisKey = CommonUtil.GetValue(t, GroupBy).ToString();

        //        if (primaryKey.Equals(thisKey))
        //            return t;
        //    }

        //    return null;
        //}

        /// <summary>
        /// x축이 없는 데이터 sum 
        /// 예) Pie 차트
        /// </summary>
        /// <param name="Target"></param>
        /// <param name="GroupBy"></param>
        /// <param name="Select"></param>
        /// <returns></returns>
        public static IDictionary<string, string> GroupBy(IEnumerable<IDictionary<string, string>> Target, string[] Select)
        {
            if (Target == null) return null;
            if (Target.Count() == 0) return new Dictionary<string, string>();

            foreach(string column in Select) {
                if (Target.First().ContainsKey(column) == false)
                {
                    throw new ArgumentOutOfRangeException("지정된 Column을 찾을 수 없습니다.: " + column);
                }
            }

            IDictionary<string, string> ret = new Dictionary<string, string>();

            //0으로 초기화
            foreach(string column in Select) {
                ret.Add(column, "0");
            }

            foreach (IDictionary<string, string> data in Target)
            {
                foreach (string column in Select)
                {
                    try 
                    {
                        ret[column] = (double.Parse(ret[column]) + double.Parse(data[column])).ToString();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message + " column :" + column, ex);
                    }
                }
            }

            return ret;
        }

        public static IList<IDictionary<string, string>> GroupBy(IEnumerable<IDictionary<string, string>> Target, string GroupByColumn, string[] Select)
        {
            if (Target == null) return null;
            if (Target.Count() == 0) return new List<IDictionary<string, string>>();

            IList<IDictionary<string, string>> ret = new List<IDictionary<string, string>>();

            foreach (IDictionary<string, string> t in Target)
            {
                //기존 값이 있는지 확인
                IDictionary<string, string> ExistObject = GetExist(ret, t, GroupByColumn); ;

                if (ExistObject == null)
                {
                    //기존 값이 없으면 새로 객체를 생성해서 넣음
                    IDictionary<string, string> newData = new Dictionary<string, string>();
                    newData.Add(GroupByColumn, t[GroupByColumn]);
                    foreach (string s in Select)    
                    {
                        newData.Add(s, "0");
                    }
                    ret.Add(newData);
                    ExistObject = newData;
                }


                //기존 값에 새 값을 더함.
                foreach (string s in Select)
                {
                    try
                    {
                        double A = double.Parse(ExistObject[s]);
                        double B = double.Parse(t[s]);

                        double AB = A + B;
                        ExistObject[s] = AB.ToString();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message + " s :" + s, ex);
                    }
                }

            }
            return ret;
        }

        public static IDictionary<string, string> GetExist(IEnumerable<IDictionary<string, string>> list, IDictionary<string, string> WannaFindThis, string GroupBy)
        {
            string primaryKey = string.Empty;
            try
            {
                primaryKey = WannaFindThis[GroupBy].ToString();

                foreach (IDictionary<string, string> t in list)
                {
                    string thisKey = t[GroupBy] as string;

                    if (primaryKey.Equals(thisKey))
                        return t;
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message + " primaryKey :" + primaryKey + ", GroupBy :" + GroupBy);
                //MessageBox.Show("WannaFindThis[GroupBy] : " + WannaFindThis[GroupBy]);
                //foreach (string key in WannaFindThis.Keys)
                //{
                //    MessageBox.Show(key);
                //}
                throw new Exception(ex.Message + " primaryKey :" + primaryKey + ", GroupBy :" + GroupBy, ex);
            }

            return null;
        }
    }
}
