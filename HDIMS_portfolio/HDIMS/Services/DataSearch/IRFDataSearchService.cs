using System.Collections;
using System.Collections.Generic;
using HDIMS.Models.Domain.DataSearch;

namespace HDIMS.Services.DataSearch
{
    public interface IRFDataSearchService  
    {
        IList<RFDataSearchModel> getList(Hashtable param);
        IList<RFDataSearchModel> getSearchList(Hashtable param);
        IList<RFDataSearchModel> getChartList(Hashtable param);
        int getRfDataTotalCount(Hashtable param);
        IList<RFDataHistoryModel> getHistory(Hashtable param);
    }
}