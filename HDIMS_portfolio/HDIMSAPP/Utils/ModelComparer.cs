using System;
using System.Collections.Generic;
using System.Linq;

namespace HDIMSAPP.Utils
{
    public class ModelComparer<T>
        where T : class, new()
    {
        IEnumerable<T> Orig;
        IEnumerable<T> Dest;

        public ModelComparer(IEnumerable<T> Orig, IEnumerable<T> Dest)
        {
            this.Orig = Orig;
            this.Dest = Dest;
        }

        public int getIndexOfPrimaryKey(string PrimaryKey)
        {
            int indexOfPrimaryKey = -1;
            bool IsExist = false;
            foreach (var property in (new T()).GetType().GetProperties())
            {
                indexOfPrimaryKey++;
                if (property.Name.Equals(PrimaryKey))
                {
                    IsExist = true;
                    break;
                }
            }

            if(IsExist == false) {
                throw new IndexOutOfRangeException("지정한 변수명이 모델에 존재하지 않습니다. 변수명:" + PrimaryKey);
            }

            return indexOfPrimaryKey;
        }

        public object GetValue(T Model, int indexOfPrimaryKey)
        {
            object Value = Model.GetType().GetProperties()[indexOfPrimaryKey].GetValue(Model, null);
            return Value;
        }

        public IEnumerable<T> GetAddedItems(string PrimaryKey)
        {
            if(Orig == null || Dest == null) {
                throw new InvalidOperationException("모델을 비교하기 위해서는 먼저 원본과 사본 인덱서를 바인딩하여야 합니다.");
            }
            int indexOfPrimaryKey = getIndexOfPrimaryKey(PrimaryKey);

            // C#의 버그를 찾아내었다. 씨발
            //IEnumerable<T> DifferencesList = this.Orig.Where(x => !Dest.Any(x1 => x1.GetType().GetProperties()[indexOfPrimaryKey] == x.GetType().GetProperties()[indexOfPrimaryKey]));

            IList<T> AddedList = new List<T>(); 
            foreach (T DestModel in Dest)
            {
                bool IsAdded = true;

                object DestValue = null;
                DestValue = GetValue(DestModel, indexOfPrimaryKey);
                
                foreach (T OrigModel in Orig)
                {
                    object OrigValue = null;
                    OrigValue = GetValue(OrigModel, indexOfPrimaryKey); 

                    if (DestValue.ToString() == OrigValue.ToString())
                    {
                        IsAdded = false;
                        continue;
                    }
                }
                if (IsAdded == true)
                {
                    AddedList.Add(DestModel);
                }
            }
            return AddedList;



            //System.Reflection.PropertyInfo[] info = Dest.GetType().GetProperties();
            //info[PrimaryKey]
            //Dest.GetType().GetProperties()[PrimaryKey]

            //return DifferencesList;

        }

        public IEnumerable<T> GetRemovedItems(string PrimaryKey)
        {
            if (Orig == null || Dest == null)
            {
                throw new InvalidOperationException("모델을 비교하기 위해서는 먼저 원본과 사본 인덱서를 바인딩하여야 합니다.");
            }

            int indexOfPrimaryKey = getIndexOfPrimaryKey(PrimaryKey);

            IList<T> RemovedList = new List<T>();
            foreach (T OrigModel in Orig)
            {
                bool IsDeleted = true;
                object OrigValue = GetValue(OrigModel,indexOfPrimaryKey);

                foreach (T DestModel in Dest)
                {
                    object DestValue = GetValue(DestModel, indexOfPrimaryKey);

                    if (DestValue.ToString() == OrigValue.ToString())
                    {
                        IsDeleted = false;
                        continue;
                    }
                }
                if (IsDeleted == true)
                {
                    RemovedList.Add(OrigModel);
                }
            }
            return RemovedList;
        }

        public IEnumerable<T> GetEditedItems(string PrimaryKey)
        {
            if (Orig == null || Dest == null)
            {
                throw new InvalidOperationException("모델을 비교하기 위해서는 먼저 원본과 사본 인덱서를 바인딩하여야 합니다.");
            }

            int indexOfPrimaryKey = getIndexOfPrimaryKey(PrimaryKey);

            IList<T> EditedList = new List<T>();
            foreach (T DestModel in Dest)
            {
                object DestValue = GetValue(DestModel, indexOfPrimaryKey);

                foreach (T OrigModel in Orig)
                {
                    object OrigValue = GetValue(OrigModel,indexOfPrimaryKey);

                    //Primary가 같은것을 확인
                    if (DestValue.ToString() == OrigValue.ToString())
                    {
                        //세부 값이 같은지 확인한다.
                        if (CompareTwoClass_ReturnDifferences(OrigModel, DestModel) == true)
                        {
                            EditedList.Add(DestModel);
                        }
                    }
                }
            }
            return EditedList;
        }


        public IEnumerable<T> GetAllChanges(string PrimaryKey)
        {
            if (Orig == null || Dest == null)
            {
                throw new InvalidOperationException("모델을 비교하기 위해서는 먼저 원본과 사본 인덱서를 바인딩하여야 합니다.");
            }

            IEnumerable<T> ret = GetAddedItems(PrimaryKey).Union<T>(GetRemovedItems(PrimaryKey)).Union<T>(GetEditedItems(PrimaryKey));

            return ret;
        }

        // under construction
        // 오리지날 오브젝트와 변경된 오브젝트를 비교해서 변경된 점이 있으면 리턴
        public static bool CompareTwoClass_ReturnDifferences(T OrigModel, T DestModel)
        {
            // Loop through each property in the destination   
            foreach (var DestProp in DestModel.GetType().GetProperties())
            {
                // Find the matching property in the Orig class and compare 
                foreach (var OrigProp in OrigModel.GetType().GetProperties())
                {
                    if (OrigProp.Name != DestProp.Name || OrigProp.PropertyType != DestProp.PropertyType) continue;

                    //Item이라는 indexer Property가 나올 때가 있어서... 배열 형식이라 비교할 필요가 있지만... 일단 1 Depth Level 까지만 비교하기로 한다.
                    if ("Item".Equals(OrigProp.Name) || "Item".Equals(DestProp.Name)) continue;

                    object OrigValue = OrigProp.GetValue(OrigModel, null);
                    object DestValue = DestProp.GetValue(DestModel, null);
                    

                    // 둘다 값이 null 이면 그냥 비교 안함 변동되지 않은 값으로 인정
                    if (OrigValue == null && DestValue == null) continue;

                    // 어느 한쪽이 null인데 다른 쪽이 null 이 아니면 다른거야.
                    if (OrigValue == null && DestValue != null)
                    {
                        //MessageBox.Show(OrigProp.Name + ">" + DestProp.Name);
                        return true;
                    }
                    if (OrigValue != null && DestValue == null)
                    {
                        //MessageBox.Show("<");
                        return true;
                    }

                    if (OrigValue.ToString() != DestValue.ToString())
                    {
                        //MessageBox.Show("!=");
                        return true;
                    }
                }
            }
            return false;
        } 
        

        //// under construction
        //// 오리지날 오브젝트와 변경된 오브젝트를 비교해서 변경된 점이 있으면 리턴
        //public static T2 CompareTwoClass_ReturnDifferences<T1, T2>(T1 Orig, T2 Dest)
        //    where T1 : class
        //    where T2 : class
        //{
        //    // Loop through each property in the destination   
        //    foreach (var DestProp in Dest.GetType().GetProperties())
        //    {
        //        // Find the matching property in the Orig class and compare 
        //        foreach (var OrigProp in Orig.GetType().GetProperties())
        //        {

        //            if (OrigProp.Name != DestProp.Name || OrigProp.PropertyType != DestProp.PropertyType) continue;
        //            if (OrigProp.GetValue(Orig, null).ToString() != DestProp.GetValue(Dest, null).ToString())
        //                return Dest;
        //                //Differences = Differences == CoreFormat.StringNoCharacters
        //                //    ? string.Format("{0}: {1} -> {2}", OrigProp.Name,
        //                //                                       OrigProp.GetValue(Orig, null),
        //                //                                       DestProp.GetValue(Dest, null))
        //                //    : string.Format("{0} {1}{2}: {3} -> {4}", Differences,
        //                //                                              Environment.NewLine,
        //                //                                              OrigProp.Name,
        //                //                                              OrigProp.GetValue(Orig, null),
        //                //                                              DestProp.GetValue(Dest, null));
        //        }
        //    }
        //    return null;
        //} 
        

    }
}
