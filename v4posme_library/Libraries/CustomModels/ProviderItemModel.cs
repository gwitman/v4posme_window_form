using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class ProviderItemModel : IProviderItemModel
{
    public int DeleteWhereItemId(int companyId, int itemId)
    {
        using var context = new DataContext();
        var findValues= context.TbProviderItems
            .Where(item => item.CompanyID == companyId
                           && item.ItemID == itemId);
        return findValues.Any() ? findValues.ExecuteDelete() : 0;
    }

    public int DeleteWhereItemIdyProviderId(int companyId, int itemId, int providerId)
    {
        using var context = new DataContext();
        var findValues = context.TbProviderItems
            .Where(item => item.CompanyID == companyId
                           && item.ItemID == itemId
                           && item.EntityID == providerId);
        return findValues.Any() ? findValues.ExecuteDelete() : 0;
    }

    public int InsertAppPosme(TbProviderItem data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.ProviderItemID;
    }

    public List<TbProviderItemDto> GetRowByItemId(int companyId, int itemId)
    {
        using var context = new DataContext();
        var result = from ip in context.TbProviderItems
            join p in context.TbProviders on ip.EntityID equals p.EntityID
            join n in context.TbNaturales on p.EntityID equals n.EntityID into naturales
            from n in naturales.DefaultIfEmpty()
            join l in context.TbLegals on p.EntityID equals l.EntityID into legales
            from l in legales.DefaultIfEmpty()
            where ip.CompanyID == companyId && ip.ItemID == itemId
            select new TbProviderItemDto
            {
                EntityId = ip.EntityID,
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
            join p in context.TbProviders on ip.EntityID equals p.EntityID
            join n in context.TbNaturales on p.EntityID equals n.EntityID into naturales
            from n in naturales.DefaultIfEmpty()
            join l in context.TbLegals on p.EntityID equals l.EntityID into legales
            from l in legales.DefaultIfEmpty()
            where ip.CompanyID == companyId && ip.ItemID == itemId && ip.EntityID == providerId
            select new TbProviderItemDto
            {
                EntityId = ip.EntityID,
                ProviderNumber = p.ProviderNumber,
                FirstName = n.FirstName,
                LastName = n.LastName,
                ComercialName = l.ComercialName
            };
        return result.First();
    }
}