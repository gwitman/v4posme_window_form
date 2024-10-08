using v4posme_library_biometric.Models;

namespace v4posme_library_biometric.Libraries.CustomModels
{
    public interface IBiometricUserModel
    {
        void DeleteAppPosme(int userId);

        int InsertAppPosme(User? data);
    }
}