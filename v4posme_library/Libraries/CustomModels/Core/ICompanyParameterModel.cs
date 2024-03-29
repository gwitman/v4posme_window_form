using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface ICompanyParameterModel
{
    void UpdateAppPosme(int companyId,int parameterId,TbCompanyParameter data);
    
    TbCompanyParameter?  GetRowByParameterIdCompanyId(int companyId,int parameterId);
}