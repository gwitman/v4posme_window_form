using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IUserWarehouseModel
{
    int DeleteByUser(int companyId,int userId);
    
    int insert_app_posme(TbUserWarehouse data);
    
    List<TbUserWarehouse> GetRowByUserIdAndFacturable(int companyId,int userId);
    
    List<TbUserWarehouse> GetRowByUserId(int companyId,int userId);
    
    List<TbUserWarehouse> GetRowByBranchId(int companyId,int branchId);
    
    List<TbWarehouse> GetRowByCompanyId(int companyId);
}