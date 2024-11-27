using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class CustomerCreditModel : ICustomerCreditModel
{
    public void UpdateAppPosme(int companyId, int branchId, int entityId, TbCustomerCredit? data)
    {
        var context = VariablesGlobales.Instance.DataContext;
        var find = context.TbCustomerCredits
            .Single(credit => credit.CompanyID == companyId
                              && credit.EntityID == entityId
                              && credit.BranchID == branchId);
        data.CustomerCreditID = find.CustomerCreditID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public int InsertAppPosme(TbCustomerCredit data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.CustomerCreditID;
    }

    public TbCustomerCredit? GetRowByPk(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        return context.TbCustomerCredits
            .SingleOrDefault(credit => credit.CompanyID == companyId
                              && credit.EntityID == entityId
                              && credit.BranchID == branchId);
    }
}