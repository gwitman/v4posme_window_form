using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IWarehouseModel
{
    void DeleteAppPosme(int companyId,int warehouseId);
    
    void UpdateAppPosme(int companyId,int branchId,int warehouseId,TbWarehouse data);
    
    int InsertAppPosme(TbWarehouse data);
    
    List<TbWarehouse> GetByCode(int companyId,string code);
    
    TbWarehouse GetRowByPk(int companyId,int warehouseId);
    
    List<TbWarehouse> GetByCompany(int companyId);
}