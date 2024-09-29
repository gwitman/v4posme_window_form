using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class LegalModel : ILegalModel
{
    public void UpdateAppPosme(int companyId, int branchId, int entityId, TbLegal data)
    {
        using var context = new DataContext();
        var find = context.TbLegals
            .FirstOrDefault(legal => legal.CompanyID == companyId
                                     && legal.BranchID == branchId
                                     && legal.EntityID == entityId);
        if (find is null) return;
        data.LegalID = find.LegalID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public void DeleteAppPosme(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        context.TbLegals
            .Where(legal => legal.CompanyID == companyId
                            && legal.BranchID == branchId
                            && legal.EntityID == entityId)
            .ExecuteUpdate(calls => calls.SetProperty(legal => legal.IsActive, false));
    }

    public int InsertAppPosme(TbLegal data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.LegalID;
    }

    public TbLegal? GetRowByPk(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        return context.TbLegals
            .SingleOrDefault(legal => legal.CompanyID == companyId
                             && legal.BranchID == branchId
                             && legal.EntityID == entityId
                             && legal.IsActive!.Value);
    }
}