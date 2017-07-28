using System.Collections;
using System.Collections.Generic;
using HDIMS.Models.Domain.Login;

namespace HDIMS.Services.Login
{
    public interface ILoginService 
    {
        IList<LoginModel> GetEmployee(Hashtable param);

        void InsertLoginInfo(Hashtable param);
        void UpdateLoginInfo(Hashtable param);
    }
}