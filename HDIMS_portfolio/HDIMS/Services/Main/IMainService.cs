using System.Collections;
using System.Collections.Generic;
using HDIMS.Models.Domain.Main;

namespace HDIMS.Services.Main
{
    public interface IMainService
    {
        IList<Hashtable> GetRadarTimes(Hashtable param);

        IList<Hashtable> GetPopBalloonDate(Hashtable param);

        IList<Hashtable> GetGoogleEarthDate(Hashtable param);


        int getabnormalListCount(Hashtable param);
        IList<MainAbnormalModel> getabnormalList(Hashtable param);
        void updateabnormalData(Hashtable param);

        int getabnormdamListCount(Hashtable param);
        IList<MainAbnormdamModel> getabnormdamList(Hashtable param);
        void updateabnormdamData(Hashtable param);

        int getdamoperListCount(Hashtable param);
        IList<MainDamoperModel> getdamoperList(Hashtable param);
        void updatedamoperData(Hashtable param);

        int getequipListCount(Hashtable param);
        IList<MainEquipModel> getequipList(Hashtable param);
        void updateequipData(Hashtable param);

        IList<ObsCode> getObsCodeList(Hashtable param);
        void saveMain(MainModel param);
        MainModel getMain(MainModel param);
        void updateMain(MainModel param);
        void deleteMain(MainModel param);
    }
}
