using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using System.ComponentModel.Design;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class TransactionMasterModel : ITransactionMasterModel
{
    public void DeleteAppPosme(int companyId, int transactionId, int transactionMasterId)
    {
        using var context = new DataContext();
        context.TbTransactionMasters
            .Where(master => master.CompanyID == companyId
                             && master.TransactionID == transactionId
                             && master.TransactionMasterID == transactionMasterId)
            .ExecuteUpdate(calls => calls.SetProperty(master => master.IsActive, false));
    }

    public int InsertAppPosme(TbTransactionMaster? data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.TransactionMasterID;
    }

    public void UpdateAppPosme(int companyId, int transactionId, int transactionMasterId, TbTransactionMaster? data)
    {
        var context = VariablesGlobales.Instance.DataContext;
        var find = context.TbTransactionMasters
            .AsNoTracking()
            .FirstOrDefault(master => master.CompanyID == companyId
                                      && master.TransactionID == transactionId
                                      && master.TransactionMasterID == transactionMasterId);

        if (find is null) return;

        data.TransactionMasterID = find.TransactionMasterID;
        data.CompanyID = companyId;
        data.TransactionID = transactionId;


        context.TbTransactionMasters.Update(data);
        context.SaveChanges();
    }


    public TbTransactionMasterDto? GetRowByPk(int companyId, int transactionId, int transactionMasterId)
    {
        using var context = new DataContext();
        var result = from tm in context.TbTransactionMasters
            join ws in context.TbWorkflowStages on tm.StatusID equals ws.WorkflowStageID
            where tm.TransactionMasterID == transactionMasterId
                  && tm.TransactionID == transactionId
                  && tm.CompanyID == companyId
                  && tm.IsActive!.Value
            select new TbTransactionMasterDto
            {
                CompanyId = tm.CompanyID,
                TransactionId = tm.TransactionID,
                TransactionMasterId = tm.TransactionMasterID,
                BranchId = tm.BranchID,
                TransactionNumber = tm.TransactionNumber,
                TransactionCausalId = tm.TransactionCausalID,
                EntityId = tm.EntityID,
                TransactionOn = tm.TransactionOn,
                StatusIdchangeOn = tm.StatusIDChangeOn,
                ComponentId = tm.ComponentID,
                Tax1 = tm.Tax1,
                Tax2 = tm.Tax2,
                Tax3 = tm.Tax3,
                Tax4 = tm.Tax4,
                Discount = tm.Discount,
                SubAmount = tm.SubAmount,
                Note = tm.Note,
                Sign = tm.Sign,
                CurrencyId = tm.CurrencyID,
                CurrencyId2 = tm.CurrencyID2,
                ExchangeRate = tm.ExchangeRate,
                Reference1 = tm.Reference1,
                Reference2 = tm.Reference2,
                Reference3 = tm.Reference3,
                Reference4 = tm.Reference4,
                StatusId = tm.StatusID,
                Amount = tm.Amount,
                IsApplied = tm.IsApplied,
                JournalEntryId = tm.JournalEntryID,
                ClassId = tm.ClassID,
                AreaId = tm.AreaID,
                SourceWarehouseId = tm.SourceWarehouseID,
                TargetWarehouseId = tm.TargetWarehouseID,
                CreatedBy = tm.CreatedBy,
                CreatedAt = tm.CreatedAt,
                CreatedOn = tm.CreatedOn,
                CreatedIn = tm.CreatedIn,
                IsActive = tm.IsActive,
                WorkflowStageName = ws.Name,
                PriorityId = tm.PriorityID,
                TransactionOn2 = tm.TransactionOn2,
                IsTemplate = tm.IsTemplate,
                PeriodPay = tm.PeriodPay,
                NextVisit = tm.NextVisit,
                NumberPhone = tm.NumberPhone,
                PrinterQuantity = tm.PrinterQuantity,
                EntityIdsecondary = tm.EntityIDSecondary
            };
        return result.AsNoTracking().FirstOrDefault();
    }

    public TbTransactionMaster? GetRowByPKK(int transactionMasterId)
    {
        using var context = new DataContext();
        var result = context.TbTransactionMasters.FirstOrDefault(u => u.TransactionMasterID == transactionMasterId);
        return result;
    }

    public TbTransactionMaster? GetRowByTransactionMasterId(int companyId, int transactionMasterId)
    {
        using var context = new DataContext();
        return context.TbTransactionMasters
            .FirstOrDefault(master => master.IsActive!.Value
                                      && master.CompanyID == companyId
                                      && master.TransactionMasterID == transactionMasterId);
    }


    public TbTransactionMaster? GetRowByTransactionNumber(int companyId, string? transactionNumber)
    {
        using var context = new DataContext();
        return context.TbTransactionMasters
            .SingleOrDefault(master => master.CompanyID == companyId
                                       && master.TransactionNumber == transactionNumber
                                       && master.IsActive!.Value);
    }

    public List<TbTransactionMaster> GetRowByNotification(int companyId)
    {
        using var context = new DataContext();
        return context.TbTransactionMasters
            .Where(master => master.CompanyID == companyId
                             && master.NextVisit != null
                             && master.IsActive!.Value
                             && master.NextVisit != DateTime.Parse("0000-00-00")
                             && master.NotificationID == null)
            .ToList();
    }

    public List<TbTransactionMasterDto> GetRowInStatusRegister(int companyId, int transactionMasterId)
    {
        using var context = new DataContext();
        var result = from tm in context.TbTransactionMasters
            join ws in context.TbWorkflowStages on tm.StatusID equals ws.WorkflowStageID
            where tm.IsActive!.Value
                  && tm.CompanyID == companyId
                  && ws.EditableTotal!.Value
                  && tm.TransactionMasterID != transactionMasterId
            select new TbTransactionMasterDto
            {
                TransactionNumber = tm.TransactionNumber,
                NameStatus = ws.Name
            };
        return result.ToList();
    }

    public List<TbTransactionMaster> getRowByTransactionIDAndEntityID(int companyID, int transactionID, int entityID)
    {
        using var context = new DataContext();
        return context.TbTransactionMasters.AsNoTracking()
            .Where(tm => tm.TransactionID == transactionID && tm.CompanyID == companyID
                                                           && tm.EntityID == entityID && tm.IsActive!.Value)
            .OrderByDescending(tm => tm.TransactionMasterID)
            .Take(10).ToList();
    }
}