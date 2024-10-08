using v4posme_library_biometric.Models;

namespace v4posme_library_biometric.Libraries.CustomModels;

public interface ITempFingerprintModel
{
    void DeleteAppPosme(string tocenPc);

    void UpdateAppPosme(string tockenPc, TempFingerprint data);

    bool InsertAppPosme(TempFingerprint data);
}