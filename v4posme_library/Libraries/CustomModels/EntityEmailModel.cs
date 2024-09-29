using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class EntityEmailModel : IEntityEmailModel
{
    public int DeleteAppPosme(int companyId, int branchId, int entityId, int entityEmailId)
    {
        using var context = new DataContext();
        return context.TbEntityEmails
            .Where(email => email.CompanyID == companyId
                            && email.BranchID == branchId
                            && email.EntityID == entityId
                            && email.EntityEmailID == entityEmailId)
            .ExecuteDelete();
    }

    public int DeleteByEntity(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        return context.TbEntityEmails
            .Where(email => email.CompanyID == companyId
                            && email.BranchID == branchId
                            && email.EntityID == entityId)
            .ExecuteDelete();
    }

    public long InsertAppPosme(TbEntityEmail data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.EntityEmailID;
    }

    public void UpdateAppPosme(int companyId, int branchId, int entityId, int entityEmailId, TbEntityEmail data)
    {
        using var context = new DataContext();
        var find = context.TbEntityEmails
            .Single(email => email.CompanyID == companyId
                            && email.BranchID == branchId
                            && email.EntityID == entityId
                            && email.EntityEmailID == entityEmailId);
        data.EntityEmailID = find.EntityEmailID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public TbEntityEmail GetRowByPk(int companyId, int branchId, int entityId, int entityEmailId)
    {
        using var context = new DataContext();
        return context.TbEntityEmails
            .Single(email => email.CompanyID == companyId
                             && email.BranchID == branchId
                             && email.EntityID == entityId
                             && email.EntityEmailID == entityEmailId);
    }

    public List<TbEntityEmail> GetRowByEntity(int companyId, int branchId, int entityId)
    {
        using var context = new DataContext();
        return context.TbEntityEmails
            .Where(email => email.CompanyID == companyId
                             && email.BranchID == branchId
                             && email.EntityID == entityId)
            .ToList();
    }

    public List<TbEntityEmail> GetRowByCompany(int companyId)
    {
        using var context = new DataContext();
        return context.TbEntityEmails
            .Where(email => email.CompanyID == companyId)
            .ToList();
    }
}