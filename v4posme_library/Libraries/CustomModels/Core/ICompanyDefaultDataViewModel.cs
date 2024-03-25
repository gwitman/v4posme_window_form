using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface ICompanyDefaultDataViewModel
{
    TbCompanyDefaultDataview GetRowByCcct(int companyId,int componentId,int callerId,int targetComponentId);
}