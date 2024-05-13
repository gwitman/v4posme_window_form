using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class CounterModel(DataContext context) : ICounterModel
{
    public TbCounter? GetRowByPk(int companyId, int branchId, int componentId, int componentItemId)
    {
        
        return context.TbCounters.AsNoTracking()   
            .FirstOrDefault(counter => counter!.CompanyId == companyId
                                       && counter.BranchId == branchId
                                       && counter.ComponentId == componentId
                                       && counter.ComponentItemId == componentItemId);
    }

    public void UpdateAppPosme(int companyId, int branchId, int componentId, int componentItemId, TbCounter data)
    {
        
        var find = context.TbCounters
            .FirstOrDefault(counter => counter!.CompanyId == companyId
                                       && counter.BranchId == branchId
                                       && counter.ComponentId == componentId
                                       && counter.ComponentItemId == componentItemId);
        if (find is null) return;
        data.CounterId = find.CounterId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }
}