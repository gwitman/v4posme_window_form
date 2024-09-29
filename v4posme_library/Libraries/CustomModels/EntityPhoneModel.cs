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
            .Where(phone => phone.CompanyID == companyId
                            && phone.BranchID == branchId
                            && phone.EntityID == entityId
                            && phone.EntityPhoneID == entityPhoneId)
            .ExecuteDelete();
    }

    public void DeleteByEntity(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        context.TbEntityPhones
            .Where(phone => phone.CompanyID == companyId
                            && phone.BranchID == branchId
                            && phone.EntityID == entityId)
            .ExecuteDelete();
    }

    public long InsertAppPosme(TbEntityPhone data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.EntityPhoneID;
    }

    public void UpdateAppPosme(int companyId, int branchId, int entityId, int entityPhoneId, TbEntityPhone data)
    {
        using var context = new DataContext();
        var find = context.TbEntityPhones
            .Single(phone => phone.CompanyID == companyId
                             && phone.BranchID == branchId
                             && phone.EntityID == entityId
                             && phone.EntityPhoneID == entityPhoneId);
        data.EntityPhoneID = find.EntityPhoneID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public TbEntityPhoneDto GetRowByPk(int companyId, int branchId, int entityId, int entityPhoneId)
    {
        using var context = new DataContext();
        var result = from tm in context.TbEntityPhones
            join ci in context.TbCatalogItems on tm.TypeID equals ci.CatalogItemID
            where tm.EntityPhoneID == entityPhoneId
                  && tm.EntityID == entityId
                  && tm.BranchID == branchId
                  && tm.CompanyID == companyId
            select new TbEntityPhoneDto
            {
                CompanyId = tm.CompanyID,
                BranchId = tm.BranchID,
                EntityId = tm.EntityID,
                EntityPhoneId = tm.EntityPhoneID,
                TypeId = tm.TypeID,
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
            join ci in context.TbCatalogItems on tm.TypeID equals ci.CatalogItemID
            where tm.EntityID == entityId
                  && tm.BranchID == branchId
                  && tm.CompanyID == companyId
            select new TbEntityPhoneDto
            {
                CompanyId = tm.CompanyID,
                BranchId = tm.BranchID,
                EntityId = tm.EntityID,
                EntityPhoneId = tm.EntityPhoneID,
                TypeId = tm.TypeID,
                TypeIdDescription = ci.Name,
                Number = tm.Number,
                IsPrimary = tm.IsPrimary
            };
        return result.ToList();
    }
}