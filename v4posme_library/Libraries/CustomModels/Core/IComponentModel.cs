using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface IComponentModel
{
    TbComponent? GetRowByName(string name);
}