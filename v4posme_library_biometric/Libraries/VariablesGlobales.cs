using Unity;
using v4posme_library_biometric.Libraries.CustomModels;

namespace v4posme_library_biometric.Libraries;

public class VariablesGlobales
{
    private readonly IUnityContainer _unityContainer= new UnityContainer();
    
    public static VariablesGlobales Instance { get; } = new();
    
    public IUnityContainer UnityContainer => _unityContainer;

    private VariablesGlobales()
    {
        #region CDI_MODELS

        _unityContainer.RegisterType<IBiometricUserModel, BiometricUserModel>();
        _unityContainer.RegisterType<ITempFingerprintModel, TempFingerprintModel>();
        _unityContainer.RegisterType<IFingerprintsModel, FingerprintsModel>();

        #endregion
    }
}