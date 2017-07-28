using System.Collections;
using System.Collections.Generic;
using HDIMS.Models.Domain.DataSearch;

namespace HDIMS.Models.Dao.DataSearch
{
    public interface IWLDataSearchDao
    {
        IList<WLDataSearchModel> getList(Hashtable param);
    }
}