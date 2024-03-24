using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface ICompanyComponentModel
{
    TbCompanyComponent GetRowByPk(int companyId,int componentId);
}