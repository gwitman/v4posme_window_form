using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class ProviderItemModel : IProviderItemModel
{
    public int DeleteWhereItemId(int companyId, int itemId)
    {
        using var context = new DataContext();
        return context.TbProviderItems
            .Where(item => item.CompanyId == companyId
                           && item.ItemId == itemId)
            .ExecuteDelete();
    }

    public int DeleteWhereItemIdyProviderId(int companyId, int itemId, int providerId)
    {
        using var context = new DataContext();
        return context.TbProviderItems
            .Where(item => item.CompanyId == companyId
                           && item.ItemId == itemId
                           && item.EntityId == providerId)
            .ExecuteDelete();
    }

    public int InsertAppPosme(TbProviderItem data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.ProviderItemId;
    }

    public List<TbProviderItemDto> GetRowByItemId(int companyId, int itemId)
    {
        using var context = new DataContext();
        var result = from ip in context.TbProviderItems
            join p in context.TbProviders on ip.EntityId equals p.EntityId
            join n in context.TbNaturales on p.EntityId equals n.EntityId into naturales
            from n in naturales.DefaultIfEmpty()
            join l in context.TbLegals on p.EntityId equals l.EntityId into legales
            from l in legales.DefaultIfEmpty()
            where ip.CompanyId == companyId && ip.ItemId == itemId
            select new TbProviderItemDto
            {
                EntityId = ip.EntityId,
                ProviderNumber = p.ProviderNumber,
                FirstName = n.FirstName,
                LastName = n.LastName,
                ComercialName = l.ComercialName
            };
        return result.ToList();
    }

    public TbProviderItemDto GetByPk(int companyId, int itemId, int providerId)
    {
        using var context = new DataContext();
        var result = from ip in context.TbProviderItems
            join p in context.TbProviders on ip.EntityId equals p.EntityId
            join n in context.TbNaturales on p.EntityId equals n.EntityId into naturales
            from n in naturales.DefaultIfEmpty()
            join l in context.TbLegals on p.EntityId equals l.EntityId into legales
            from l in legales.DefaultIfEmpty()
            where ip.CompanyId == companyId && ip.ItemId == itemId && ip.EntityId == providerId
            select new TbProviderItemDto
            {
                EntityId = ip.EntityId,
                ProviderNumber = p.ProviderNumber,
                FirstName = n.FirstName,
                LastName = n.LastName,
                ComercialName = l.ComercialName
            };
        return result.First();
    }
}