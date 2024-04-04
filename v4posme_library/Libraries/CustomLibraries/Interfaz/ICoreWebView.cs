using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Interfaz;

public interface ICoreWebView
{
    void GetViewByName(TbUser user, int componentId, string name, int callerId, int? permission = null,
        Dictionary<string, string>? parameter = null);

    void GetViewByDataViewId(TbUser user, int componentId, int dataviewId, int callerId, int? permission = null,
        Dictionary<string, string>? parameter = null);

    void GetView(TbUser user, int? componentId = null, int? callerId = null, int? permission = null,
        Dictionary<string, string>? parameter = null);

    void GetViewDefault(TbUser user, int? componentId = null, int? callerId = null, int? targetComponentId = null,
        int? permission = null, Dictionary<string, string>? parameter = null);
}