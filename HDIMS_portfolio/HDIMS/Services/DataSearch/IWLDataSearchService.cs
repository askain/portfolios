using System.Collections;
using System.Collections.Generic;
using HDIMS.Models.Domain.DataSearch;

namespace HDIMS.Services.DataSearch
{
    public interface IWLDataSearchService
    {
        IList<WLDataSearchModel> getList(Hashtable param);
        IList<WLDataSearchModel> getSearchList(Hashtable param);
        IList<WLDataSearchModel> getChartList(Hashtable param);
        int getWlDataTotalCount(Hashtable param);
        IList<WLDataHistoryModel> getHistory(Hashtable param);
    }
}