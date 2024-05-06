using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class TransactionMasterDetailCreditModel : ITransactionMasterDetailCreditModel
{
    public int InsertAppPosme(TbTransactionMasterDetailCredit data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.TransactionMasterDetailCreditId;
    }

    public void UpdateAppPosme(int transactionMasterDetailId, TbTransactionMasterDetailCredit? data)
    {
        using var context = new DataContext();
        var findDetailCredits = context.TbTransactionMasterDetailCredits
            .Where(credit => credit.TransactionMasterDetailId == transactionMasterDetailId)
            .ToList();
        foreach (var iteMasterDetailCredit in findDetailCredits)
        {
            data.TransactionMasterDetailCreditId = iteMasterDetailCredit.TransactionMasterDetailCreditId;
            context.Entry(iteMasterDetailCredit).CurrentValues.SetValues(data);
        }

        context.BulkSaveChanges();
    }

    public int DeleteAppPosme(int transactionMasterDetailId)
    {
        using var context = new DataContext();
        return context.TbTransactionMasterDetailCredits
            .Where(credit => credit.TransactionMasterDetailId == transactionMasterDetailId)
            .ExecuteDelete();
    }

    public TbTransactionMasterDetailCredit? GetRowByPk(int transactionMasterDetailId)
    {
        using var context = new DataContext();
        return context.TbTransactionMasterDetailCredits
            .First(credit => credit.TransactionMasterDetailId == transactionMasterDetailId);
    }

    public int DeleteWhereIdNotIn(int transactionMasterId, List<int> listTmdId)
    {
        using var context = new DataContext();
        return context.TbTransactionMasterDetailCredits
            .Where(credit => credit.TransactionMasterId == transactionMasterId
            && !listTmdId.Contains(credit.TransactionMasterDetailId))
            .ExecuteDelete();
    }
}