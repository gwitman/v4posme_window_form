using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public class TransactionMasterReferencesModel : ITransactionMasterReferencesModel
{
    public void DeleteAppPosme(int transactionMasterReferencesId)
    {
        using var context = new DataContext();
        var find = context.TbTransactionMasterReferences.SingleOrDefault(reference => reference.TransactionMasterReferenceID == transactionMasterReferencesId);
        if (find is null)
        {
            return;
        }

        context.Remove(find);
        context.SaveChanges();
    }

    public int InsertAppPosme(TbTransactionMasterReference tbTransactionMasterReference)
    {
        using var context = new DataContext();
        var add = context.TbTransactionMasterReferences.Add(tbTransactionMasterReference);
        context.SaveChanges();
        return add.Entity.TransactionMasterReferenceID;
    }

    public void UpdateAppPosme(int transactionMasterReferencesId, TbTransactionMasterReference tbTransactionMasterReference)
    {
        using var context = new DataContext();
        var find = context.TbTransactionMasterReferences.SingleOrDefault(reference => reference.TransactionMasterReferenceID == transactionMasterReferencesId);
        if (find is null)
        {
            return;
        }

        context.Entry(find).CurrentValues.SetValues(tbTransactionMasterReference);
        context.SaveChanges();
    }

    public void UpdateAppPosmeByTransactionMasterId(int transactionMasterId, TbTransactionMasterReference tbTransactionMasterReference)
    {
        using var context = new DataContext();
        var find = context.TbTransactionMasterReferences.SingleOrDefault(reference => reference.TransactionMasterID == transactionMasterId);
        if (find is null)
        {
            return;
        }

        context.Entry(find).CurrentValues.SetValues(tbTransactionMasterReference);
        context.SaveChanges();
    }

    public TbTransactionMasterReference? GetRowByPk(int transactionMasterReferencesId)
    {
        using var context = new DataContext();
        return context.TbTransactionMasterReferences.AsNoTracking().SingleOrDefault(reference => reference.TransactionMasterReferenceID == transactionMasterReferencesId);
    }

    public TbTransactionMasterReference? GetRowByTransactionMasterId(int transactionMasterId)
    {
        using var context = new DataContext();
        return context.TbTransactionMasterReferences.AsNoTracking().SingleOrDefault(reference => reference.TransactionMasterID == transactionMasterId);
    }
}