﻿using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class TransactionMasterDetailCreditModel : ITransactionMasterDetailCreditModel
{
    public int InsertAppPosme(TbTransactionMasterDetailCredit data)
    {
        var context = VariablesGlobales.Instance.DataContext;
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.TransactionMasterDetailCreditID;
    }

    public void UpdateAppPosme(int transactionMasterDetailId, TbTransactionMasterDetailCredit? data)
    {
        using var context = new DataContext();
        var findDetailCredits = context.TbTransactionMasterDetailCredits
            .Where(credit => credit.TransactionMasterDetailID == transactionMasterDetailId)
            .ToList();
        foreach (var iteMasterDetailCredit in findDetailCredits)
        {
            data.TransactionMasterDetailCreditID = iteMasterDetailCredit.TransactionMasterDetailCreditID;
            context.Entry(iteMasterDetailCredit).CurrentValues.SetValues(data);
        }

        context.SaveChanges();
    }

    public int DeleteAppPosme(int transactionMasterDetailId)
    {
        using var context = new DataContext();
        return context.TbTransactionMasterDetailCredits
            .Where(credit => credit.TransactionMasterDetailID == transactionMasterDetailId)
            .ExecuteDelete();
    }

    public TbTransactionMasterDetailCredit? GetRowByPk(int transactionMasterDetailId)
    {
        using var context = new DataContext();
        return context.TbTransactionMasterDetailCredits
            .First(credit => credit.TransactionMasterDetailID == transactionMasterDetailId);
    }

    public int DeleteWhereIdNotIn(int transactionMasterId, List<int> listTmdId)
    {
        using var context = new DataContext();
        var findValues = context.TbTransactionMasterDetailCredits
            .Where(credit => credit.TransactionMasterID == transactionMasterId
                             && !listTmdId.Contains(credit.TransactionMasterDetailID)).ToList();
        foreach (var tbTransactionMasterDetailCredit in findValues)
        {
            context.Remove(tbTransactionMasterDetailCredit);
        }
        return context.SaveChanges();
    }
}