using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class ItemCategoryModel : IItemCategoryModel
{
    public void DeleteAppPosme(int companyId, int itemCategoryId)
    {
        using var context = new DataContext();
        context.TbItemCategories
            .Where(category => category.CompanyId == companyId
                               && category.InventoryCategoryId == itemCategoryId)
            .ExecuteUpdate(calls =>
                calls.SetProperty(category => category.IsActive, false));
    }

    public int InsertAppPosme(TbItemCategory data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.InventoryCategoryId;
    }

    public void UpdateAppPosme(int companyId, int itemCategoryId, TbItemCategory data)
    {
        using var context = new DataContext();
        var find = context.TbItemCategories
            .Single(category => category.CompanyId == companyId
                                && category.InventoryCategoryId == itemCategoryId);
        data.InventoryCategoryId = find.InventoryCategoryId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public List<TbItemCategory> GetByCompany(int companyId)
    {
        using var context = new DataContext();
        return context.TbItemCategories
            .Where(category => category.CompanyId == companyId
                               && category.IsActive!.Value)
            .ToList();
    }

    public TbItemCategory GetByPk(int companyId, int itemCategoryId)
    {
        using var context = new DataContext();
        return context.TbItemCategories
            .Single(category => category.CompanyId == companyId
                                && category.InventoryCategoryId == itemCategoryId
                                && category.IsActive!.Value);
    }
}