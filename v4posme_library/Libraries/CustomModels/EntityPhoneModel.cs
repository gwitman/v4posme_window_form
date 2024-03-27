using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class EntityPhoneModel : IEntityPhoneModel
{
    public void DeleteAppPosme(int companyId, int branchId, int entityId, int entityPhoneId)
    {
        using var context = new DataContext();
        context.TbEntityPhones
            .Where(phone => phone.CompanyId == companyId
                            && phone.BranchId == branchId
                            && phone.EntityId == entityId
                            && phone.EntityPhoneId == entityPhoneId)
            .ExecuteDelete();
    }

    public void DeleteByEntity(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        context.TbEntityPhones
            .Where(phone => phone.CompanyId == companyId
                            && phone.BranchId == branchId
                            && phone.EntityId == entityId)
            .ExecuteDelete();
    }

    public long InsertAppPosme(TbEntityPhone data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.EntityPhoneId;
    }

    public void UpdateAppPosme(int companyId, int branchId, int entityId, int entityPhoneId, TbEntityPhone data)
    {
        using var context = new DataContext();
        var find = context.TbEntityPhones
            .Single(phone => phone.CompanyId == companyId
                             && phone.BranchId == branchId
                             && phone.EntityId == entityId
                             && phone.EntityPhoneId == entityPhoneId);
        data.EntityPhoneId = find.EntityPhoneId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public TbEntityPhoneDto GetRowByPk(int companyId, int branchId, int entityId, int entityPhoneId)
    {
        using var context = new DataContext();
        var result = from tm in context.TbEntityPhones
            join ci in context.TbCatalogItems on tm.TypeId equals ci.CatalogItemId
            where tm.EntityPhoneId == entityPhoneId
                  && tm.EntityId == entityId
                  && tm.BranchId == branchId
                  && tm.CompanyId == companyId
            select new TbEntityPhoneDto
            {
                CompanyId = tm.CompanyId,
                BranchId = tm.BranchId,
                EntityId = tm.EntityId,
                EntityPhoneId = tm.EntityPhoneId,
                TypeId = tm.TypeId,
                TypeIdDescription = ci.Name,
                Number = tm.Number,
                IsPrimary = tm.IsPrimary
            };
        return result.Single();
    }

    public List<TbEntityPhoneDto> GetRowByEntity(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        var result = from tm in context.TbEntityPhones
            join ci in context.TbCatalogItems on tm.TypeId equals ci.CatalogItemId
            where tm.EntityId == entityId
                  && tm.BranchId == branchId
                  && tm.CompanyId == companyId
            select new TbEntityPhoneDto
            {
                CompanyId = tm.CompanyId,
                BranchId = tm.BranchId,
                EntityId = tm.EntityId,
                EntityPhoneId = tm.EntityPhoneId,
                TypeId = tm.TypeId,
                TypeIdDescription = ci.Name,
                Number = tm.Number,
                IsPrimary = tm.IsPrimary
            };
        return result.ToList();
    }
}