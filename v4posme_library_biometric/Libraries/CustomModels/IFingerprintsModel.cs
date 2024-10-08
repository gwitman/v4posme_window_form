using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Cms;
using v4posme_library_biometric.Models;

namespace v4posme_library_biometric.Libraries.CustomModels;

public interface IFingerprintsModel
{
    void DeleteAppPosme(int companyId);

    void UpdateAppPosme(int fingerId, Fingerprint data);

    int InsertAppPosme(Fingerprint data);

    int GetCount();

    Fingerprint? GetFingerprintId(int id);
}