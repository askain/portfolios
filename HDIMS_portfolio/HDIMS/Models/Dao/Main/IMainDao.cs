using System.Collections.Generic;
using HDIMS.Models.Domain.Main;

namespace HDIMS.Models.Dao.Main
{
    public interface IMainDao
    {
        IList<MainTimeModel> gettimeList();
        
        void insertMain(MainModel param);
        MainModel getMain(MainModel param);
        void updateMain(MainModel param);
        void deleteMain(MainModel param);
    }
}
