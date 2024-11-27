using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public class TransactionMasterDetailReferencesModel : ITransactionMasterDetailReferencesModel
{
    public void DeleteAppPosme(int transactionMasterDetailRefereceId)
    {
        using var context = new DataContext();
        var tbTransactionMasterDetailReference = context.TbTransactionMasterDetailReferences.SingleOrDefault(reference => reference.TransactionMasterDetailRefereceID == transactionMasterDetailRefereceId);
        if (tbTransactionMasterDetailReference is null)
        {
            return;
        }

        tbTransactionMasterDetailReference.IsActive = 0;
        context.SaveChanges();
    }

    public void DeleteWhereIdNotIn(List<int> listTmdId)
    {
        using var context = new DataContext();
        var tbTransactionMasterDetailReferences = context.TbTransactionMasterDetailReferences.Where(reference => !listTmdId.Contains(reference.TransactionMasterDetailID)).ToList();
        foreach (var tbTransactionMasterDetailReference in tbTransactionMasterDetailReferences)
        {
            tbTransactionMasterDetailReference.IsActive = 0;
        }

        context.SaveChanges();
    }

    public int InsertAppPosme(TbTransactionMasterDetailReference data)
    {
        var context = VariablesGlobales.Instance.DataContext;
        var add = context.Add(data).Entity;
        context.SaveChanges();
        return add.TransactionMasterDetailRefereceID;
    }

    public void UpdateAppPosme(int transactionMasterDetailRefereceId, TbTransactionMasterDetailReference data)
    {
        using var context = new DataContext();
        var find = context.TbTransactionMasterDetailReferences.SingleOrDefault(reference => reference.TransactionMasterDetailRefereceID == transactionMasterDetailRefereceId);
        if (find is not null)
        {
            context.Update(data);
        }

        context.SaveChanges();
    }

    public TbTransactionMasterDetailReference? GetRowByPk(int transactionMasterDetailRefereceId)
    {
        using var context = new DataContext();
        return context.TbTransactionMasterDetailReferences.AsNoTracking().SingleOrDefault(reference => reference.TransactionMasterDetailRefereceID == transactionMasterDetailRefereceId && reference.IsActive == 1);
    }

    public List<TbTransactionMasterDetailReferenceDTO> GetRowByTransactionMasterIdAndComponentId(int transactionMasterId, int componentId)
    {
        using var context = new DataContext();
        return (from tdr in context.TbTransactionMasterDetailReferences
            join td in context.TbTransactionMasterDetails
                on tdr.TransactionMasterDetailID equals td.TransactionMasterDetailID
            join tm in context.TbTransactionMasters
                on td.TransactionMasterID equals tm.TransactionMasterID
            join i in context.TbItems
                on tdr.ComponentItemID equals i.ItemID
            where tm.TransactionMasterID == transactionMasterId &&
                  tdr.ComponentID == componentId &&
                  tdr.IsActive == 1
            select new TbTransactionMasterDetailReferenceDTO
            {
                TransactionMasterDetailReferenceId = tdr.TransactionMasterDetailRefereceID,
                TransactionMasterDetailId = tdr.TransactionMasterDetailID,
                ComponentId = tdr.ComponentID.Value,
                ComponentItemId = tdr.ComponentItemID.Value,
                IsActive = tdr.IsActive.Value,
                Quantity = tdr.Quantity,
                CreatedOn = tdr.CreatedOn,
                ItemName = i.Name,
                UnitaryPrice = td.UnitaryPrice.Value,
                Amount = td.UnitaryPrice.Value,
                Tax1 = td.Tax1.Value
            }).ToList();
    }

    public List<TbTransactionMasterDetailReference> GetRowByTransactionMasterId(int transactionMasterId)
    {
        using var context = new DataContext();
        return (from tdr in context.TbTransactionMasterDetailReferences
            join td in context.TbTransactionMasterDetails
                on tdr.TransactionMasterDetailID equals td.TransactionMasterDetailID
            where td.TransactionMasterID == transactionMasterId &&
                  tdr.IsActive == 1 &&
                  td.IsActive.Value
            select tdr).ToList();
    }
}