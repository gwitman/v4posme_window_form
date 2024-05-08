using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mysqlx.Crud;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class TransactionMasterModel : ITransactionMasterModel
{
    public void DeleteAppPosme(int companyId, int transactionId, int transactionMasterId)
    {
        using var context = new DataContext();
        context.TbTransactionMasters
            .Where(master => master.CompanyId == companyId
                             && master.TransactionId == transactionId
                             && master.TransactionMasterId == transactionMasterId)
            .ExecuteUpdate(calls => calls.SetProperty(master => master.IsActive, false));
    }

    public int InsertAppPosme(TbTransactionMaster data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.TransactionMasterId;
    }

    public void UpdateAppPosme(int companyId, int transactionId, int transactionMasterId, TbTransactionMaster data)
    {
        using var context = new DataContext();
        var config = new MapperConfiguration(cfg =>
            cfg.CreateMap<TbTransactionMaster, TbTransactionMaster>()
                .ForAllMembers(expression => expression.Condition((master, transactionMaster, srcMember) => srcMember != null)));
        var find = context.TbTransactionMasters
            .AsNoTracking()
            .FirstOrDefault(master => master.CompanyId == companyId
                                      && master.TransactionId == transactionId
                                      && master.TransactionMasterId == transactionMasterId);
        if (find is null) return;
        data.TransactionMasterId = find.TransactionMasterId;
        data.CompanyId = companyId;
        data.TransactionId = transactionId;
        var mapper = new Mapper(config);
        var dataUpdate = mapper.Map(data, find);
        context.TbTransactionMasters.Update(dataUpdate);
        context.SaveChanges();
    }


    public TbTransactionMasterDto? GetRowByPk(int companyId, int transactionId, int transactionMasterId)
    {
        using var context = new DataContext();
        var result = from tm in context.TbTransactionMasters
            join ws in context.TbWorkflowStages on tm.StatusId equals ws.WorkflowStageId
            where tm.TransactionMasterId == transactionMasterId
                  && tm.TransactionId == transactionId
                  && tm.CompanyId == companyId
                  && tm.IsActive!.Value
            select new TbTransactionMasterDto
            {
                CompanyId = tm.CompanyId,
                TransactionId = tm.TransactionId,
                TransactionMasterId = tm.TransactionMasterId,
                BranchId = tm.BranchId,
                TransactionNumber = tm.TransactionNumber,
                TransactionCausalId = tm.TransactionCausalId,
                EntityId = tm.EntityId,
                TransactionOn = tm.TransactionOn,
                StatusIdchangeOn = tm.StatusIdchangeOn,
                ComponentId = tm.ComponentId,
                Tax1 = tm.Tax1,
                Tax2 = tm.Tax2,
                Tax3 = tm.Tax3,
                Tax4 = tm.Tax4,
                Discount = tm.Discount,
                SubAmount = tm.SubAmount,
                Note = tm.Note,
                Sign = tm.Sign,
                CurrencyId = tm.CurrencyId,
                CurrencyId2 = tm.CurrencyId2,
                ExchangeRate = tm.ExchangeRate,
                Reference1 = tm.Reference1,
                Reference2 = tm.Reference2,
                Reference3 = tm.Reference3,
                Reference4 = tm.Reference4,
                StatusId = tm.StatusId,
                Amount = tm.Amount,
                IsApplied = tm.IsApplied,
                JournalEntryId = tm.JournalEntryId,
                ClassId = tm.ClassId,
                AreaId = tm.AreaId,
                SourceWarehouseId = tm.SourceWarehouseId,
                TargetWarehouseId = tm.TargetWarehouseId,
                CreatedBy = tm.CreatedBy,
                CreatedAt = tm.CreatedAt,
                CreatedOn = tm.CreatedOn,
                CreatedIn = tm.CreatedIn,
                IsActive = tm.IsActive,
                WorkflowStageName = ws.Name,
                PriorityId = tm.PriorityId,
                TransactionOn2 = tm.TransactionOn2,
                IsTemplate = tm.IsTemplate,
                PeriodPay = tm.PeriodPay,
                NextVisit = tm.NextVisit,
                NumberPhone = tm.NumberPhone,
                PrinterQuantity = tm.PrinterQuantity,
                EntityIdsecondary = tm.EntityIdsecondary
            };
        return result.First();
    }

    public TbTransactionMaster GetRowByTransactionMasterId(int companyId, int transactionMasterId)
    {
        using var context = new DataContext();
        return context.TbTransactionMasters
            .First(master => master.IsActive!.Value
                             && master.CompanyId == companyId
                             && master.TransactionMasterId == transactionMasterId);
    }

    public TbTransactionMaster GetRowByTransactionNumber(int companyId, string transactionNumber)
    {
        using var context = new DataContext();
        return context.TbTransactionMasters
            .First(master => master.CompanyId == companyId
                             && master.TransactionNumber.Contains(transactionNumber)
                             && master.IsActive!.Value);
    }

    public List<TbTransactionMaster> GetRowByNotification(int companyId)
    {
        using var context = new DataContext();
        return context.TbTransactionMasters
            .Where(master => master.CompanyId == companyId
                             && master.NextVisit != null
                             && master.IsActive!.Value
                             && master.NextVisit != DateTime.Parse("0000-00-00")
                             && master.NotificationId == null)
            .ToList();
    }

    public List<TbTransactionMasterDto> GetRowInStatusRegister(int companyId, int transactionMasterId)
    {
        using var context = new DataContext();
        var result = from tm in context.TbTransactionMasters
            join ws in context.TbWorkflowStages on tm.StatusId equals ws.WorkflowStageId
            where tm.IsActive!.Value
                  && tm.CompanyId == companyId
                  && ws.EditableTotal!.Value
                  && tm.TransactionMasterId != transactionMasterId
            select new TbTransactionMasterDto
            {
                TransactionNumber = tm.TransactionNumber,
                NameStatus = ws.Name
            };
        return result.ToList();
    }
}