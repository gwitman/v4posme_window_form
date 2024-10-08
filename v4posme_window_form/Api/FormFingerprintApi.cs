using Unity;
using v4posme_library_biometric.Libraries;
using v4posme_library_biometric.Libraries.CustomModels;
using v4posme_library_biometric.Models;

namespace v4posme_window.Api;

public class FormFingerprintApi
{
    private readonly ITempFingerprintModel _tempFingerprintModel=VariablesGlobales.Instance.UnityContainer.Resolve<ITempFingerprintModel>();

    public bool WebActiveSensorEnroll(int entityId)
    {
        const string tockenPc = "llRnk81687411555823";
        const string fingerName = "Pulgar_Derecho";
        _tempFingerprintModel.DeleteAppPosme(tockenPc);

        var tempFingerprint = new TempFingerprint
        {
            Id = DateTime.Now.ToShortTimeString(),
            FingerName = fingerName,
            TokenPc = tockenPc,
            Option = "enroll",
            Text = "El sensor de huella dactilar esta activado",
            UserId = entityId,
        };
       return _tempFingerprintModel.InsertAppPosme(tempFingerprint);
    }
}