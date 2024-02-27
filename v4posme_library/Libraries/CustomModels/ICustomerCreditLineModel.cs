using System.ComponentModel.Design;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface ICustomerCreditLineModel
{
    void UpdateAppPosme(int customerCreditLineId, TbCustomerCreditLine data);

    int InsertAppPosme(TbCustomerCreditLine data);

    void DeleteAppPosme(int customerCreditLineId);

    void DeleteWhereIdNotIn(int companyId,int branchId,int entityId,List<int> listCustomerCreditLineId);
    
    List<TbCustomerCreditLine> GetRowByEntityAndLine(int companyId,int branchId,int entityId,int creditLineId);
    
    List<TbCustomerCreditLine> GetRowByEntity(int companyId,int branchId,int entityId);
    
    List<TbCustomerCreditLine> GetRowByEntityBalanceMayorCero(int companyId,int branchId,int entityId);
    
    List<TbCustomerCreditLine> GetRowByBranchId(int companyId,int branchId);
    
    TbCustomerCreditLine GetRowByPk(int customerCreditLineId);
}