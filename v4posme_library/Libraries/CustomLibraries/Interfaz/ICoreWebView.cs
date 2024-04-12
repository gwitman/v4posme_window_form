using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomLibraries.Interfaz;

public interface ICoreWebView
{
    TableCompanyDataViewDto? GetViewByName(TbUser user, int componentId, string name, int callerId, int? permission = null,
        Dictionary<string, string>? parameter = null);

    TableCompanyDataViewDto GetViewByDataViewId(TbUser user, int componentId, int dataviewId, int callerId, int? permission = null,
        Dictionary<string, string>? parameter = null);

    TableCompanyDataViewDto GetView(TbUser user, int? componentId = null, int? callerId = null, int? permission = null,
        Dictionary<string, string>? parameter = null);

    TableCompanyDataViewDto? GetViewDefault(TbUser user, int? componentId = null, int? callerId = null, int? targetComponentId = null,
        int? permission = null, Dictionary<string, string>? parameter = null);
}