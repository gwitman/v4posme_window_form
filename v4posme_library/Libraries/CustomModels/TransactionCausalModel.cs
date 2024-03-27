using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class TransactionCausalModel : ITransactionCausalModel
{
    public List<TbTransactionCausal> GetCausalByBranch(int companyId, int transactionId, int branchId)
    {
        using var context = new DataContext();
        return context.TbTransactionCausals
            .Where(causal => causal.CompanyId == companyId
                             && causal.TransactionId == transactionId
                             && causal.BranchId == branchId
                             && causal.IsActive )
            .ToList();
    }

    public TbTransactionCausal GetCausalDefaultId(int companyId, int transactionId)
    {
        using var context = new DataContext();
        return context.TbTransactionCausals
            .Single(causal => causal.CompanyId == companyId
                              && causal.TransactionId == transactionId
                              && causal.IsActive  && causal.IsDefault );
    }

    public List<TbTransactionCausalDto> GetByCompanyAndTransaction(int companyId, int transactionId)
    {
        using var context = new DataContext();
        var result = from tc in context.TbTransactionCausals
            join b in context.TbBranches on tc.BranchId equals b.BranchId
            join w in context.TbWarehouses on tc.WarehouseSourceId equals w.WarehouseId into wJoin
            from w in wJoin.DefaultIfEmpty()
            join w2 in context.TbWarehouses on tc.WarehouseTargetId equals w2.WarehouseId into w2Join
            from w2 in w2Join.DefaultIfEmpty()
            where tc.CompanyId == companyId
                  && tc.TransactionId == transactionId
                  && tc.IsActive
            select new TbTransactionCausalDto
            {
                CompanyId = tc.CompanyId,
                TransactionId = tc.TransactionId,
                TransactionCausalId = tc.TransactionCausalId,
                BranchId = tc.BranchId,
                Name = tc.Name,
                WarehouseSourceId = tc.WarehouseSourceId,
                WarehouseTargetId = tc.WarehouseTargetId,
                IsDefault = tc.IsDefault,
                IsActive = tc.IsActive,
                Branch = b.Name,
                WarehouseSourceDescription = w.Name,
                WarehouseTargetDescription = w2.Name
            };
        return result.ToList();
    }

    public TbTransactionCausal GetByCompanyAndTransactionAndCausal(int companyId, int transactionId, int causalId)
    {
        using var context = new DataContext();
        return context.TbTransactionCausals
            .Single(causal => causal.CompanyId == companyId
                              && causal.TransactionId == transactionId
                              && causal.TransactionCausalId == causalId
                              && causal.IsActive);
    }

    public void DeleteAppPosme(int companyId, int transactionId, List<int> listCausal)
    {
        using var context = new DataContext();
        context.TbTransactionCausals
            .Where(causal => causal.CompanyId == companyId
                             && causal.TransactionId == transactionId
                             && !listCausal.Contains(causal.TransactionCausalId))
            .ExecuteUpdate(calls => calls.SetProperty(causal => causal.IsActive, false));
    }

    public int InsertAppPosme(TbTransactionCausal data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.TransactionCausalId;
    }

    public void UpdateAppPosme(int companyId, int transactionId, int causalId, TbTransactionCausal data)
    {
        using var context = new DataContext();
        var find = context.TbTransactionCausals
            .Single(causal => causal.CompanyId == companyId
                              && causal.TransactionId == transactionId
                              && causal.TransactionCausalId == causalId);
        data.TransactionCausalId = find.TransactionCausalId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public int CountCausalDefault(int companyId, int transactionId)
    {
        using var context = new DataContext();
        return context.TbTransactionCausals
            .Count(causal => causal.CompanyId == companyId
                             && causal.TransactionId == transactionId
                             && causal.IsActive
                             && causal.IsDefault);
    }
}