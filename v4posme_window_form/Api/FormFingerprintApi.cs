using Unity;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library_biometric.Libraries;
using v4posme_library_biometric.Libraries.CustomModels;
using v4posme_library_biometric.Models;

namespace v4posme_window.Api;

public class FormFingerprintApi
{
    private readonly ICoreWebTools coreWebTools=v4posme_library.Libraries.VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebTools>();
    private readonly ITempFingerprintModel _tempFingerprintModel=VariablesGlobales.Instance.UnityContainer.Resolve<ITempFingerprintModel>();
    private readonly ICoreWebParameter coreWebParameter = v4posme_library.Libraries.VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebParameter>();

    public bool WebActiveSensorEnroll(int entityId)
    {
        var tockenPc = v4posme_library.Libraries.VariablesGlobales.ConfigurationBuilder["TOKEN_PC"];
        var fingerName = v4posme_library.Libraries.VariablesGlobales.ConfigurationBuilder["Pulgar_Derecho"];
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

    public bool WebActiveSensorRead()
    {
        try
        {
            var tockenPc = v4posme_library.Libraries.VariablesGlobales.ConfigurationBuilder["TOKEN_PC"];
            _tempFingerprintModel.DeleteAppPosme(tockenPc);

            var tempFingerprint = new TempFingerprint
            {
                Id = DateTime.Now.ToShortTimeString(),
                Option = "read",
                TokenPc = tockenPc,
                CreatedAt = DateTime.Now
            };
            _tempFingerprintModel.InsertAppPosme(tempFingerprint);
            var appCompany = v4posme_library.Libraries.VariablesGlobales.ConfigurationBuilder["APP_COMPANY"];
            var executeProgramFingerPint = coreWebParameter.GetParameterValue("OPEN_FINGERPRINT_EXECUTE", Convert.ToInt32(appCompany));
            var executeProgramFingerPintPath = coreWebParameter.GetParameterValue("OPEN_FINGERPRINT_EXECUTE_PATH", Convert.ToInt32(appCompany));
            return true;
        }
        catch (Exception e)
        {
            coreWebTools.Log($"Se produjo el siguiente error WebActiveSensorRead: {e.Source} {e.Message}");
            return false;
        }
    }

    public TempFingerprint? WebSsejs()
    {
        try
        {
            var tockenPc = v4posme_library.Libraries.VariablesGlobales.ConfigurationBuilder["TOKEN_PC"];
            var fingerName = v4posme_library.Libraries.VariablesGlobales.ConfigurationBuilder["Pulgar_Derecho"];
            var resultado = _tempFingerprintModel.GetSsejs(tockenPc);

            if (resultado.Image is not null)
            {
                var data = resultado;
                data.Image = null;
                _tempFingerprintModel.UpdateAppPosme(tockenPc, data);
            }
            return resultado;
        }
        catch (Exception e)
        {
            coreWebTools.Log($"Se produjo el siguiente error WebSsejs: {e.Source} {e.Message}");
            return null;
        }
    }
}