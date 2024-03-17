using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class ProviderModel : IProviderModel
{
    public void UpdateAppPosme(int companyId, int branchId, int entityId, TbProvider data)
    {
        using var context = new DataContext();
        var find = context.TbProviders
            .First(provider => provider.CompanyId == companyId
                               && provider.BranchId == branchId
                               && provider.EntityId == entityId);
        data.ProviderId = find.ProviderId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public void DeleteAppPosme(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        context.TbProviders
            .Where(provider => provider.CompanyId == companyId
                               && provider.BranchId == branchId
                               && provider.EntityId == entityId)
            .ExecuteUpdate(calls => calls
                .SetProperty(provider => provider.IsActive, (ulong?)0));
    }

    public int InsertAppPosme(TbProvider data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.ProviderId;
    }

    public TbProvider GetRowByEntity(int companyId, int entityId)
    {
        using var context = new DataContext();
        return context.TbProviders
            .Single(provider => provider.CompanyId == companyId
                                && provider.EntityId == entityId
                                && provider.IsActive == 1);
    }

    public List<TbProvider> GetRowByCompany(int companyId)
    {
        using var context = new DataContext();
        return context.TbProviders
            .Join(context.TbEntities, p => p.EntityId, e => e.EntityId, (p, e) => new { p, e })
            .Join(context.TbNaturales, t => t.p.EntityId, nat => nat.EntityId, (t, nat) => new { t, nat })
            .Where(t => t.t.p.CompanyId == companyId && t.t.p.IsActive == 1)
            .Select(t => new TbProvider
            {
                EntityId = t.t.p.EntityId,
                CompanyId = t.t.p.CompanyId,
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
            .Single(provider => provider.CompanyId == companyId
                                && provider.BranchId== branchId
                                && provider.EntityId == entityId
                                && provider.IsActive == 1);
    }

    public TbProvider GetRowByProviderNumber(int companyId, string providerNumber)
    {
        using var context = new DataContext();
        return context.TbProviders
            .Single(provider => provider.CompanyId == companyId
                                && provider.ProviderNumber== providerNumber
                                && provider.IsActive == 1);
    }
}