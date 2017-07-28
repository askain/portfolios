using System.Collections.Generic;

namespace HDIMSAPP.Common
{
    // Under contruction
    public class UnitFinder
    {
        private static IDictionary<string, string> UnitsForExact = new Dictionary<string, string>();
        private static IDictionary<string, string> UnitsForLike = new Dictionary<string, string>();

        private static void LoadUnitsForExact()
        {
            #region == Exact==
            UnitsForExact.Add("강우량", "mm");
            UnitsForExact.Add("저수위", "EL.m");
            UnitsForExact.Add("총유입량", "m³/sec");
            UnitsForExact.Add("총방류량", "m³/sec");
            UnitsForExact.Add("자체유입량", "m³/sec");
            UnitsForExact.Add("외부유입량", "m³/sec");
            UnitsForExact.Add("계획방류량", "m³/sec");
            UnitsForExact.Add("발전방류량", "m³/sec");
            UnitsForExact.Add("여수로방류량", "m³/sec");
            UnitsForExact.Add("Outlet방류량", "m³/sec");

            UnitsForExact.Add("기타방류량1", "m³/sec");
            UnitsForExact.Add("기타방류량2", "m³/sec");
            UnitsForExact.Add("기타방류량3", "m³/sec");
            UnitsForExact.Add("취수량1", "m³/sec");
            UnitsForExact.Add("취수량2", "m³/sec");
            UnitsForExact.Add("취수량3", "m³/sec");
//저수량 
//저수율 
//방수로 수위
//유입량
//기타유입량
            //공용량
            #endregion
        }
        private static void LoadUnitsForLike()
        {
            #region == Like ==
            UnitsForLike.Add("우량", "mm");
            UnitsForLike.Add("수위", "EL.m");
            UnitsForLike.Add("유입량", "m³/sec");
            UnitsForLike.Add("방류량", "m³/sec");
            UnitsForLike.Add("취수량", "m³/sec");
            UnitsForLike.Add("발전량", "CMS");

            #endregion
        }

        public static string GetUnit(string PropertyName)
        {
            if (UnitsForExact.Count == 0) LoadUnitsForExact();
            if (UnitsForLike.Count == 0) LoadUnitsForLike();

            if (UnitsForExact.ContainsKey(PropertyName) == true)
            {
                return UnitsForExact[PropertyName];
            }
            else
            {
                foreach (string key in UnitsForLike.Keys)
                {
                    if (PropertyName.Contains(key))
                    {
                        return UnitsForLike[key];
                    }
                }
            }
            return string.Empty;
        }
    }
}
