using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface ICompanyDataViewModel
{
    TbCompanyDataview GetRowByCompanyIdDataViewId(int companyId, int dataViewId, int callerId, int componentId);

    TbCompanyDataview GetRowByCompanyIdDataViewIdAndFlavor(int companyId, int dataViewId,
        int callerId, int componentId, int flavorId);
}