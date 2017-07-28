using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Windows;
using System.Windows.Media;
using Newtonsoft.Json;

namespace HDIMSAPP.Utils
{
    public class CommonUtil
    {
        public static SolidColorBrush GetDateTimeColorBrush(string _trmdv, string _obsdt)
        {
            SolidColorBrush _midColor = new SolidColorBrush(new Color() { A = 255, R = 233, G = 212, B = 255 });
            SolidColorBrush _hourColor = new SolidColorBrush(new Color() { A = 255, R = 224, G = 255, B = 224 });

            if (_trmdv == null || _obsdt == null) return null;

            if (_trmdv.Equals("60") || _trmdv.Equals("30"))
            {
                if (_obsdt.Substring(8, 2).Equals("24"))
                {
                    return _midColor;
                }
            }
            else if (_trmdv.Equals("DAY"))
            {
                if (_obsdt.Substring(6, 2).Equals("01"))
                {
                    return _midColor;
                }
            }
            else if (_trmdv.Equals("1") || _trmdv.Equals("10"))
            {
                if (_obsdt.Substring(8, 4).Equals("2400"))
                {
                    return _midColor;
                }
                else if (_obsdt.Substring(10, 2).Equals("00"))
                {
                    return _hourColor;
                }
            }
            return null;
        }

        public static void SetValue(object TargetObject, string ColumnName, object Value)
        {
            PropertyInfo pi = TargetObject.GetType().GetProperty(ColumnName);
            pi.SetValue(TargetObject, Value, null);
        }

        public static object GetValue(object TargetObject, string ColumnName)
        {
            if (TargetObject.GetType().GetProperty(ColumnName) != null)
            {
                return TargetObject.GetType().GetProperty(ColumnName).GetValue(TargetObject, null);
            }
            return null;
        }

        public static string GetNumberFormat(int digit)
        {
            StringBuilder form = new StringBuilder();//.000}
            form.Append("{0:###,###,###,###,##0");
            if (digit > 0)
            {
                form.Append(".");
                for (var i = 0; i < digit; i++)
                {
                    form.Append("0");
                }
            }
            return form.Append("}").ToString();
        }

        #region 객체 복사 1
        /// <summary>
        /// 객체를 복사하는 메소드
        /// 단, 제네릭 리스트 형식일 경우 내용은 복사가 안됌 ㅜㅜ
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T Clone<T>(T source)
        {
            T cloned = (T)Activator.CreateInstance(source.GetType());
            foreach (PropertyInfo curPropInfo in source.GetType().GetProperties())
            {
                if (curPropInfo.GetGetMethod() != null && (curPropInfo.GetSetMethod() != null))
                {
                    // Handle Non-indexer properties
                    if (curPropInfo.Name != "Item")
                    {
                        // get property from source
                        object getValue = curPropInfo.GetGetMethod().Invoke(source, new object[] { });

                        // clone if needed
                        if (getValue != null && getValue is DependencyObject)
                            getValue = Clone((DependencyObject)getValue);

                        // set property on cloned
                        curPropInfo.GetSetMethod().Invoke(cloned, new object[] { getValue });
                    }
                    // handle indexer
                    else
                    {
                        // get count for indexer
                        int numberofItemInColleciton = (int)curPropInfo.ReflectedType.GetProperty("Count").GetGetMethod().Invoke(source, new object[] { });

                        // run on indexer
                        for (int i = 0; i < numberofItemInColleciton; i++)
                        {
                            // get item through Indexer
                            object getValue = curPropInfo.GetGetMethod().Invoke(source, new object[] { i });

                            // clone if needed
                            if (getValue != null && getValue is DependencyObject)
                                getValue = Clone((DependencyObject)getValue);

                            // add item to collection
                            curPropInfo.ReflectedType.GetMethod("Add").Invoke(cloned, new object[] { getValue });
                        }
                    }   //end if
                }   //end if
            }   // end foreach
            return cloned;
        }


        #endregion


        #region 객체 복사2
        public static T CloneByJson<T>(T Orig)
            where T : class
        {
            // 다음 과정을 거쳐서 객체 복사
            // Object ----> Json String ----> new Object
            T result = null;
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                ser.WriteObject(ms, Orig);

                string josnserdata = System.Text.Encoding.UTF8.GetString(ms.ToArray(), 0, (int)ms.Length);
                result = JsonConvert.DeserializeObject<T>(josnserdata);
            }

            return result;
        }
        #endregion

        #region 객체복사 샘플3
        //IList<BoardModel> clone = new List<BoardModel>();

        //foreach (BoardModel b in items)
        //{
        //    clone.Add(Utils.CommonUtil.Clone<BoardModel>(b));
        //}
        //clone[0].BOARDCD = "99";

        //MessageBox.Show(clone[0].BOARDCD + " / " + items[0].BOARDCD);
        #endregion

        // 오리지날 오브젝트와 변경된 오브젝트를 비교해서 변경된 점이 있으면 리턴
        // 단, 추가/삭제된 것은 찾아내지 못함. ModelComparer 를 이용하세요.
        public static T2 CompareTwoClass_ReturnDifferences<T1, T2>(T1 Orig, T2 Dest)
            where T1 : class
            where T2 : class
        {

            // Loop through each property in the destination   
            foreach (var DestProp in Dest.GetType().GetProperties())
            {
                // Find the matching property in the Orig class and compare 
                foreach (var OrigProp in Orig.GetType().GetProperties())
                {

                    if (OrigProp.Name != DestProp.Name || OrigProp.PropertyType != DestProp.PropertyType) continue;

                    // 둘다 값이 null 이면 그냥 비교 안함 동일한 걸로 치지 뭐...
                    if (OrigProp.GetValue(Orig, null) == null && DestProp.GetValue(Dest, null) == null) continue;

                    // 어느 한쪽이 null인데 다른 쪽이 null 이 아니면 다른거야.
                    if (OrigProp.GetValue(Orig, null) == null && DestProp.GetValue(Dest, null) != null) 
                        return Dest;
                    if (OrigProp.GetValue(Orig, null) != null && DestProp.GetValue(Dest, null) == null)
                        return Dest;

                    if (OrigProp.GetValue(Orig, null).ToString() != DestProp.GetValue(Dest, null).ToString())
                        return Dest;
                    //Differences = Differences == CoreFormat.StringNoCharacters
                    //    ? string.Format("{0}: {1} -> {2}", OrigProp.Name,
                    //                                       OrigProp.GetValue(Orig, null),
                    //                                       DestProp.GetValue(Dest, null))
                    //    : string.Format("{0} {1}{2}: {3} -> {4}", Differences,
                    //                                              Environment.NewLine,
                    //                                              OrigProp.Name,
                    //                                              OrigProp.GetValue(Orig, null),
                    //                                              DestProp.GetValue(Dest, null));
                }
            }
            return null;
        }

        // 리스트도 복사할 수 있게...
        public static object Clone(object Orig, Type type)
        {
            #region 객체복사 샘플
            //IList<BoardModel> clone = new List<BoardModel>();

            //foreach (BoardModel b in items)
            //{
            //    clone.Add(Utils.CommonUtil.Clone<BoardModel>(b));
            //}
            //clone[0].BOARDCD = "99";

            //MessageBox.Show(clone[0].BOARDCD + " / " + items[0].BOARDCD);
            #endregion

            #region 객체 복사2
            //// 다음 과정을 거쳐서 객체 복사
            //// Object ----> Json String ----> new Object
            //DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(IList<BoardModel>));
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    ser.WriteObject(ms, items);

            //    string josnserdata = System.Text.Encoding.UTF8.GetString(ms.ToArray(), 0, (int)ms.Length);
            //    _originalData = JsonConvert.DeserializeObject<IList<BoardModel>>(strJson);
            //}
            ////_originalData[0].BOARDCD = "99";
            #endregion

            return null;
        }

    }
}
