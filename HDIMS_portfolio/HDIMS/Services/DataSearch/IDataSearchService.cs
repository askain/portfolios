using System.Collections;
using System.Collections.Generic;
using HDIMS.Models.Domain.DataSearch;

namespace HDIMS.Services.DataSearch
{
    public interface IDataSearchService
    {
        IList<DataSearchModel> getList(Hashtable param);
        int getListCount(Hashtable param);
        IList<Hashtable> GetNetworkSearchList(Hashtable param);
        IList<Hashtable> GetWaterQualitySearchList(Hashtable param);
    }
}