using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class ProviderModel : IProviderModel
{
    public void UpdateAppPosme(int companyId, int branchId, int entityId, TbProvider data)
    {
        using var context = new DataContext();
        var find = context.TbProviders
            .First(provider => provider.CompanyID == companyId
                               && provider.BranchID == branchId
                               && provider.EntityID == entityId);
        data.ProviderID = find.ProviderID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public void DeleteAppPosme(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        context.TbProviders
            .Where(provider => provider.CompanyID == companyId
                               && provider.BranchID == branchId
                               && provider.EntityID == entityId)
            .ExecuteUpdate(calls => calls
                .SetProperty(provider => provider.IsActive, false));
    }

    public int InsertAppPosme(TbProvider data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.ProviderID;
    }

    public TbProvider GetRowByEntity(int companyId, int entityId)
    {
        using var context = new DataContext();
        return context.TbProviders
            .Single(provider => provider.CompanyID == companyId
                                && provider.EntityID == entityId
                                && provider.IsActive!.Value);
    }

    public List<TbProviderDto>? GetRowByCompany(int companyId)
    {
        using var context = new DataContext();
        return context.TbProviders
            .Join(context.TbEntities, p => p.EntityID, e => e.EntityID, (p, e) => new { p, e })
            .Join(context.TbNaturales, t => t.p.EntityID, nat => nat.EntityID, (t, nat) => new { t, nat })
            .Where(t => t.t.p.CompanyID == companyId && t.t.p.IsActive!.Value)
            .Select(t => new TbProviderDto
            {
                EntityId = t.t.p.EntityID,
                CompanyId = t.t.p.CompanyID,
                ProviderNumber = t.t.p.ProviderNumber,
                NumberIdentification = t.t.p.NumberIdentification,
                FirstName = t.nat.FirstName,
                LastName = t.nat.LastName
            }).ToList();

    }

    public TbProvider GetRowByPk(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        return context.TbProviders
            .Single(provider => provider.CompanyID == companyId
                                && provider.BranchID== branchId
                                && provider.EntityID == entityId
                                && provider.IsActive!.Value);
    }

    public TbProvider GetRowByProviderNumber(int companyId, string? providerNumber)
    {
        using var context = new DataContext();
        return context.TbProviders
            .Single(provider => provider.CompanyID == companyId
                                && provider.ProviderNumber== providerNumber
                                && provider.IsActive!.Value);
    }
}