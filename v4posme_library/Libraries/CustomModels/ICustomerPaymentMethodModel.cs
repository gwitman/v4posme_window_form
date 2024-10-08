using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface ICustomerPaymentMethodModel
{
    int InsertAppPosme(TbCustomerPaymentMethod data);
    
    void UpdateAppPosme(int entityId, TbCustomerPaymentMethod data);
    
    void DeleteAppPosme(int companyId, int branchId, int entityId);
    
    TbCustomerPaymentMethod? GetRowByEntity(int companyId, int entityId);
}