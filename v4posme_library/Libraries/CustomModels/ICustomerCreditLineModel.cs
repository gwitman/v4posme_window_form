using System.ComponentModel.Design;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public interface ICustomerCreditLineModel
{
    void UpdateAppPosme(int customerCreditLineId, TbCustomerCreditLine data);

    int InsertAppPosme(TbCustomerCreditLine data);

    void DeleteAppPosme(int customerCreditLineId);

    void DeleteWhereIdNotIn(int companyId,int branchId,int entityId,List<int> listCustomerCreditLineId);
    
    List<TbCustomerCreditLineDto> GetRowByEntityAndLine(int companyId, int branchId, int entityId, int creditLineId);
    
    List<TbCustomerCreditLineDto> GetRowByEntity(int companyId, int branchId, int entityId);
    
    List<TbCustomerCreditLineDto> GetRowByEntityBalanceMayorCero(int companyId, int branchId, int entityId);
    
    List<TbCustomerCreditLineDto> GetRowByBranchId(int companyId, int branchId);
    
    TbCustomerCreditLine GetRowByPk(int customerCreditLineId);
}