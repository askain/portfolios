using System.Collections;
using System.Collections.Generic;

using HDIMS.Models.Domain.Common;

namespace HDIMS.Services.Common
{
    public interface ICommonService
    {
        IList<HDIMS.Models.Domain.Common.Code> GetDamCodeList(Hashtable param);
        IList<HDIMS.Models.Domain.Common.Code> GetObsCodeList(Hashtable param);
        IList<HDIMS.Models.Domain.Common.Code> GetRfObsCodeList(Hashtable param);
        IList<Hashtable> GetMgtCodeList(Hashtable param);
        IList<ExCode> GetExCodeList(Hashtable param);
        IList<EtcCode> GetEtcCodeList(Hashtable param);
        IList<UserInfo> GetUserInfoList(Hashtable param);
        IList<MenuModel> GetMenuList(Hashtable param);
        IList<MenuModel> GetAdminMenuList(Hashtable param);
        IList<PairValue> GetMenuTitleList(Hashtable param);
        IList<HDIMS.Models.Domain.Common.Code> GetEdExWayList(Hashtable param);
        IList<HDIMS.Models.Domain.Common.Code> GetEdExLvlList(Hashtable param);
        IList<string> GetStaticObsCodeList(Hashtable param);
    }
}
