using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public interface IUserWarehouseModel
{
    int DeleteByUser(int companyId,int userId);
    
    int insert_app_posme(TbUserWarehouse data);
    
    List<TbUserWarehouseDto> GetRowByUserIdAndFacturable(int companyId, int userId);
    
    List<TbUserWarehouseDto> GetRowByUserId(int companyId, int userId);
    
    List<TbUserWarehouseDto> GetRowByBranchId(int companyId, int branchId);
    
    List<TbWarehouse> GetRowByCompanyId(int companyId);
}