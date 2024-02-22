using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface ICompanyParameterModel
{
    TbCompanyParameter? GetRowByParameterIdCompanyId(int companyId, int parameterId);
    void UpdateAppPosme(int companyId, int parameterId, TbCompanyParameter data);
}
