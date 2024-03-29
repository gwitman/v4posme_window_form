using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Interfaz;

public interface ICoreWebTools
{
    string FormatMessageError(object message);
    Dictionary<string, object> FormatParameter(string filter);

    TbComponent? GetComponentIdByComponentName(string componentName);
}