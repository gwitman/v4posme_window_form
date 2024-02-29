using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class CustomerCreditModel : ICustomerCreditModel
{
    public void UpdateAppPosme(int companyId, int branchId, int entityId, TbCustomerCredit data)
    {
        using var context = new DataContext();
        var find = context.TbCustomerCredits
            .Single(credit => credit.CompanyId == companyId
                              && credit.EntityId == entityId
                              && credit.BranchId == branchId);
        context.Entry(find).CurrentValues.SetValues(data);
        context.BulkSaveChanges();
    }

    public int InsertAppPosme(TbCustomerCredit data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.BulkSaveChanges();
        return add.Entity.CustomerCreditId;
    }

    public TbCustomerCredit GetRowByPk(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        return context.TbCustomerCredits
            .Single(credit => credit.CompanyId == companyId
                              && credit.EntityId == entityId
                              && credit.BranchId == branchId);
    }
}