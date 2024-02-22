using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class CenterCostModel : ICenterCostModel
{
    public void DeleteAppPosme(int companyId, int classId)
    {
        using var context = new DataContext();
        var find = FindByCompanyIdAndClassId(companyId, classId, context);
        find.IsActive = false;
        context.BulkSaveChanges();
    }

    public void UpdateAppPosme(int companyId, int classId, TbCenterCost data)
    {
        using var context = new DataContext();
        var find = FindByCompanyIdAndClassId(companyId, classId, context);
        context.Entry(find).CurrentValues.SetValues(data);
        context.BulkSaveChanges();
    }

    public int InsertAppPosme(TbCenterCost data)
    {
        using var context = new DataContext();
        var add =context.TbCenterCosts.Add(data);
        context.BulkSaveChanges();
        return add.Entity.ClassId;
    }

    public TbCenterCost GetByClassNumber(string classNumber, int companyId)
    {
        using var context = new DataContext();
        return context.TbCenterCosts
            .Single(center => center.IsActive != null
                              && center.CompanyId == companyId
                              && center.Number == classNumber
                              && center.IsActive.Value);
    }

    public TbCenterCost GetRowByPk(int companyId, int classId)
    {
        using var context = new DataContext();
        var find = FindByCompanyIdAndClassId(companyId, classId, context);
        return find.IsActive != null && find.IsActive.Value ? find : new TbCenterCost();
    }

    public List<TbCenterCost> GetByCompany(int companyId)
    {
        using var context = new DataContext();
        return context.TbCenterCosts
            .Where(center => center.IsActive != null
                             && center.CompanyId == companyId
                             && center.IsActive.Value)
            .OrderBy(center => center.Number)
            .ToList();
    }

    private TbCenterCost FindByCompanyIdAndClassId(int companyId, int classId, DataContext dataContext)
    {
        return dataContext.TbCenterCosts.Single(center => center.CompanyId == companyId && center.ClassId == classId);
    }
}