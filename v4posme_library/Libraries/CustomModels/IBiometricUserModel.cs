using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels
{
    public interface IBiometricUserModel
    {
        void DeleteAppPosme(int userId);

        int InsertAppPosme(TbUser? data);
    }
}
