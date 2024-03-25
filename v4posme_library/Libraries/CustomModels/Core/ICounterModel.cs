using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface ICounterModel
{
    TbCounter GetRowByPk(int companyId, int branchId, int componentId, int componentItemId);

    void UpdateAppPosme(int companyId, int branchId, int componentId, int componentItemId, TbCounter data);
}