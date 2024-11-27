using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class TransactionMasterInfoModel : ITransactionMasterInfoModel
{
    public void DeleteAppPosme(int companyId, int transactionId, int transactionMasterId)
    {
        using var context = new DataContext();
        context.TbTransactionMasterInfos
            .Where(info => info.CompanyID == companyId
                           && info.TransactionID == transactionId
                           && info.TransactionMasterID == transactionMasterId)
            .ExecuteDelete();
    }

    public int InsertAppPosme(TbTransactionMasterInfo data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.TransactionMasterInfoID;
    }

    public void UpdateAppPosme(int companyId, int transactionId, int transactionMasterId, TbTransactionMasterInfo data)
    {
        var context = VariablesGlobales.Instance.DataContext;
        var find = context.TbTransactionMasterInfos.AsNoTracking()
            .SingleOrDefault(info => info.CompanyID == companyId
                                    && info.TransactionID == transactionId
                                    && info.TransactionMasterID == transactionMasterId);
        if (find is null) return;
        data.TransactionMasterInfoID = find.TransactionMasterInfoID;
        context.Update(data);
        context.SaveChanges();
    }

    public TbTransactionMasterInfoDto? GetRowByPk(int companyId, int transactionId, int transactionMasterId)
    {
        using var context = new DataContext();
        var result = from tm in context.TbTransactionMasterInfos
            join ci in context.TbCatalogItems on tm.ZoneID equals ci.CatalogItemID into ciJoin
            from ci in ciJoin.DefaultIfEmpty()
            join ci2 in context.TbCatalogItems on tm.MesaID equals ci2.CatalogItemID into ci2Join
            from ci2 in ci2Join.DefaultIfEmpty()
            where tm.TransactionMasterID == transactionMasterId
                  && tm.TransactionID == transactionId
                  && tm.CompanyID == companyId
            select new TbTransactionMasterInfoDto
            {
                TransactionMasterInfoId = tm.TransactionMasterInfoID,
                CompanyId = tm.CompanyID,
                TransactionId = tm.TransactionID,
                TransactionMasterId = tm.TransactionMasterID,
                ZoneId = tm.ZoneID,
                RouteId = tm.RouteID,
                MesaId = tm.MesaID,
                ReferenceClientName = tm.ReferenceClientName,
                ReferenceClientIdentifier = tm.ReferenceClientIdentifier,
                ChangeAmount = tm.ChangeAmount,
                ReceiptAmountPoint = tm.ReceiptAmountPoint,
                ReceiptAmount = tm.ReceiptAmount,
                ReceiptAmountDol = tm.ReceiptAmountDol,
                ReceiptAmountBank = tm.ReceiptAmountBank,
                ReceiptAmountBankId = tm.ReceiptAmountBankID,
                ReceiptAmountBankReference = tm.ReceiptAmountBankReference,
                ReceiptAmountBankDol = tm.ReceiptAmountBankDol,
                ReceiptAmountBankDolId = tm.ReceiptAmountBankDolID,
                ReceiptAmountBankDolReference = tm.ReceiptAmountBankDolReference,
                ReceiptAmountCard = tm.ReceiptAmountCard,
                ReceiptAmountCardBankId = tm.ReceiptAmountCardBankID,
                ReceiptAmountCardBankReference = tm.ReceiptAmountCardBankReference,
                ReceiptAmountCardDol = tm.ReceiptAmountCardDol,
                ReceiptAmountCardBankDolId = tm.ReceiptAmountCardBankDolID,
                ReceiptAmountCardBankDolReference = tm.ReceiptAmountCardBankDolReference,
                Reference1 = tm.Reference1,
                Reference2 = tm.Reference2,
                ZonaName = ci.Name,
                MesaName = ci2.Name
            };
        return result.First();
    }

    public TbTransactionMasterInfo? GetRowByPkPk(int transactionMasterId)
    {
        using var context = new DataContext();
        return context.TbTransactionMasterInfos.FirstOrDefault(master => master!.TransactionMasterID == transactionMasterId);
    }
}