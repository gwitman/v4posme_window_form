﻿using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public class CenterCostModel : ICenterCostModel
{
    public void DeleteAppPosme(int companyId, int classId)
    {
        using var context = new DataContext();
        var find = FindByCompanyIdAndClassId(companyId, classId, context);
        find.IsActive = false;
        context.SaveChanges();
    }

    public void UpdateAppPosme(int companyId, int classId, TbCenterCost data)
    {
        using var context = new DataContext();
        var find = FindByCompanyIdAndClassId(companyId, classId, context);
        data.ClassID = find.ClassID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public int InsertAppPosme(TbCenterCost data)
    {
        using var context = new DataContext();
        var add = context.TbCenterCosts.Add(data);
        context.SaveChanges();
        return add.Entity.ClassID;
    }

    public TbCenterCost GetByClassNumber(string? classNumber, int companyId)
    {
        using var context = new DataContext();
        return context.TbCenterCosts
            .Single(center => center.CompanyID == companyId
                              && center.Number == classNumber
                              && center.IsActive!.Value);
    }

    public TbCenterCost GetRowByPk(int companyId, int classId)
    {
        using var context = new DataContext();
        var find = FindByCompanyIdAndClassId(companyId, classId, context);
        return find.IsActive!.Value ? find : new TbCenterCost();
    }

    public List<TbCenterCost> GetByCompany(int companyId)
    {
        using var context = new DataContext();
        return context.TbCenterCosts
            .Where(center => center.CompanyID == companyId
                             && center.IsActive!.Value)
            .OrderBy(center => center.Number)
            .ToList();
    }

    private TbCenterCost FindByCompanyIdAndClassId(int companyId, int classId, DataContext dataContext)
    {
        return dataContext.TbCenterCosts.Single(center => center.CompanyID == companyId && center.ClassID == classId);
    }
}