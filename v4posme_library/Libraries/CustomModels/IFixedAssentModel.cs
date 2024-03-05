using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IFixedAssentModel
{
    void UpdateAppPosme(int companyId, int branchId, int fixedAssentId, TbFixedAssent data);

    void DeleteAppPosme(int companyId, int branchId, int fixedAssentId);
    
    int InsertAppPosme(TbFixedAssent data);

    TbFixedAssent GetRowByPk(int companyId, int branchId, int fixedAssentId);
}