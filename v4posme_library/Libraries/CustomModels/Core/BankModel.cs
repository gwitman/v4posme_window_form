using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class BankModel(DataContext context) : IBankModel
{
    public void DeleteAppPosme(int companyId, int bankId)
    {
        
        context.TbBanks.AsNoTracking()
            .Where(bank => bank.CompanyId == companyId
                           && bank.BankId == bankId)
            .ExecuteUpdate(calls => calls.SetProperty(bank => bank.IsActive, 0));
    }

    public void UpdateAppPosme(int companyId, int bankId, TbBank data)
    {
        
        var find = context.TbBanks.AsNoTracking().FirstOrDefault(bank => bank.CompanyId == companyId
                                                          && bank.BankId == bankId);
        if (find is null) return;
        data.BankId = find.BankId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public int InsertAppPosme(int companyId, TbBank data)
    {
        
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.BankId;
    }

    public TbBank GetRowByPk(int companyId, int bankId)
    {
        
        return context.TbBanks.AsNoTracking().First(bank =>
            bank.CompanyId == companyId
            && bank.BankId == bankId
            && bank.IsActive == 1);
    }

    public List<TbBank> GetByCompany(int companyId)
    {
        
        return context.TbBanks.AsNoTracking().Where(bank => bank.CompanyId == companyId && bank.IsActive == 1).ToList();
    }
}