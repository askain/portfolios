using System.Collections;
using System.Collections.Generic;
using HDIMS.Models.Domain.DataSearch;

namespace HDIMS.Models.Dao.DataSearch
{
    public interface IRFDataSearchDao
    {
        IList<RFDataSearchModel> getList(Hashtable param);
    }
}