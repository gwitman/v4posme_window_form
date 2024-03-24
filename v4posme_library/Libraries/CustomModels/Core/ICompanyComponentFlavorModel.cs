using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface ICompanyComponentFlavorModel
{
    TbCompanyComponentFlavor GetRowByCompanyAndComponentAndComponentItemId(int companyId,int componentId,int componentItemId);
}