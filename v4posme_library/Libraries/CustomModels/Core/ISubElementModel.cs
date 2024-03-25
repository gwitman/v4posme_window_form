using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface ISubElementModel
{
    TbSubelement GetRowByNameAndElementId(int elementId,string name);
}