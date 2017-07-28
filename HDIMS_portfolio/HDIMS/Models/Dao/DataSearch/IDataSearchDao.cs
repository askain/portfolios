using System.Collections;
using System.Collections.Generic;
using HDIMS.Models.Domain.DataSearch;

namespace HDIMS.Models.Dao.DataSearch
{
    public interface IDataSearchDao
    {
        IList<DataSearchModel> getList(Hashtable param);
    }
}