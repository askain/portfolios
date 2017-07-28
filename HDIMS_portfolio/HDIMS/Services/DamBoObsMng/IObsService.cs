using System.Collections;
using System.Collections.Generic;
using HDIMS.Models.Domain.DamBoObsMng;

namespace HDIMS.Services.DamBoObsMng
{
    public interface IObsService
    {
        IList<ObsModel> Select(Hashtable param);
        IList<ObsModel> SelectObsType();
    }
}