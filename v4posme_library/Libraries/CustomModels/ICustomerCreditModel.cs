using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface ICustomerCreditModel
{
    void UpdateAppPosme(int companyId,int branchId,int entityId,TbCustomerCredit data);
    
    int InsertAppPosme(TbCustomerCredit data);
    
    TbCustomerCredit GetRowByPk(int companyId,int branchId,int entityId);
}