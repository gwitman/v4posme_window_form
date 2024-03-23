using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class TransactionMasterInfoModel : ITransactionMasterInfoModel
{
    public void DeleteAppPosme(int companyId, int transactionId, int transactionMasterId)
    {
        using var context = new DataContext();
        context.TbTransactionMasterInfoes
            .Where(info => info.CompanyId == companyId
                           && info.TransactionId == transactionId
                           && info.TransactionMasterId == transactionMasterId)
            .ExecuteDelete();
    }

    public int InsertAppPosme(TbTransactionMasterInfo data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.TransactionMasterInfoId;
    }

    public void UpdateAppPosme(int companyId, int transactionId, int transactionMasterId, TbTransactionMasterInfo data)
    {
        using var context = new DataContext();
        var find = context.TbTransactionMasterInfoes
            .FirstOrDefault(info => info.CompanyId == companyId
                                    && info.TransactionId == transactionId
                                    && info.TransactionMasterId == transactionMasterId);
        if (find is null) return;
        data.TransactionMasterInfoId = find.TransactionMasterInfoId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public TbTransactionMasterInfo GetRowByPk(int companyId, int transactionId, int transactionMasterId)
    {
        using var context = new DataContext();
        var result = from tm in context.TbTransactionMasterInfoes
            join ci in context.TbCatalogItems on tm.ZoneId equals ci.CatalogItemId into ciJoin
            from ci in ciJoin.DefaultIfEmpty()
            join ci2 in context.TbCatalogItems on tm.MesaId equals ci2.CatalogItemId into ci2Join
            from ci2 in ci2Join.DefaultIfEmpty()
            where tm.TransactionMasterId == transactionMasterId
                  && tm.TransactionId == transactionId
                  && tm.CompanyId == companyId
            select new TbTransactionMasterInfo
            {
                TransactionMasterInfoId = tm.TransactionMasterInfoId,
                CompanyId = tm.CompanyId,
                TransactionId = tm.TransactionId,
                TransactionMasterId = tm.TransactionMasterId,
                ZoneId = tm.ZoneId,
                RouteId = tm.RouteId,
                MesaId = tm.MesaId,
                ReferenceClientName = tm.ReferenceClientName,
                ReferenceClientIdentifier = tm.ReferenceClientIdentifier,
                ChangeAmount = tm.ChangeAmount,
                ReceiptAmountPoint = tm.ReceiptAmountPoint,
                ReceiptAmount = tm.ReceiptAmount,
                ReceiptAmountDol = tm.ReceiptAmountDol,
                ReceiptAmountBank = tm.ReceiptAmountBank,
                ReceiptAmountBankId = tm.ReceiptAmountBankId,
                ReceiptAmountBankReference = tm.ReceiptAmountBankReference,
                ReceiptAmountBankDol = tm.ReceiptAmountBankDol,
                ReceiptAmountBankDolId = tm.ReceiptAmountBankDolId,
                ReceiptAmountBankDolReference = tm.ReceiptAmountBankDolReference,
                ReceiptAmountCard = tm.ReceiptAmountCard,
                ReceiptAmountCardBankId = tm.ReceiptAmountCardBankId,
                ReceiptAmountCardBankReference = tm.ReceiptAmountCardBankReference,
                ReceiptAmountCardDol = tm.ReceiptAmountCardDol,
                ReceiptAmountCardBankDolId = tm.ReceiptAmountCardBankDolId,
                ReceiptAmountCardBankDolReference = tm.ReceiptAmountCardBankDolReference,
                Reference1 = tm.Reference1,
                Reference2 = tm.Reference2,
                ZonaName = ci.Name,
                MesaName = ci2.Name
            };
        return result.First();
    }
}