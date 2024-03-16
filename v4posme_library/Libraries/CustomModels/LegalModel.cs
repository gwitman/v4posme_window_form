using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class LegalModel : ILegalModel
{
    public void UpdateAppPosme(int companyId, int branchId, int entityId, TbLegal data)
    {
        using var context = new DataContext();
        var find = context.TbLegals
            .FirstOrDefault(legal => legal.CompanyId == companyId
                                     && legal.BranchId == branchId
                                     && legal.EntityId == entityId);
        if (find is null) return;
        data.LegalId = find.LegalId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public void DeleteAppPosme(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        context.TbLegals
            .Where(legal => legal.CompanyId == companyId
                            && legal.BranchId == branchId
                            && legal.EntityId == entityId)
            .ExecuteUpdate(calls => calls.SetProperty(legal => legal.IsActive, false));
    }

    public int InsertAppPosme(TbLegal data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.LegalId;
    }

    public TbLegal get_rowByPK(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        return context.TbLegals
            .Single(legal => legal.CompanyId == companyId
                             && legal.BranchId == branchId
                             && legal.EntityId == entityId
                             && legal.IsActive!.Value);
    }
}