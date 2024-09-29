using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public interface IEmployeeModel
{
    void UpdateAppPosme(int companyId,int branchId,int entityId,TbEmployee data);
    
    void DeleteAppPosme(int companyId,int branchId,int entityId);
    
    int InsertAppPosme(TbEmployee data);

    List<TbEmployeeDto> GetRowByBranchIdAndType(int companyId, int branchId, int typeEmployer);
    
    List<TbEmployeeDto> GetRowByBranchId(int companyId, int branchId);
    
    TbEmployeeDto GetRowByPk(int companyId, int branchId, int entityId);

    List<TbEmployeeDto> GetRowByCompanyId(int companyId);

    TbEmployeeDto? GetRowByEntityId(int companyId, int entityId);
}