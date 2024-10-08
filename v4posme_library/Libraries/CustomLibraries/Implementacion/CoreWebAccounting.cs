﻿using Unity;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels;
using v4posme_library.Libraries.CustomModels.Core;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion;

public class CoreWebAccounting : ICoreWebAccounting
{
    private readonly ICompanyParameterModel _companyParameterModel;

    private readonly IComponentCycleModel _componentCycleModel;

    private readonly IParameterModel _parameterModel;

    private readonly IComponentPeriodModel _componentPeriodModel;

    private readonly IAccountModel _accountModel;

    private readonly IAccountingBalanceModel _accountingBalanceModel;

    private readonly ICoreWebParameter _coreWebParameter;

    public CoreWebAccounting(ICompanyParameterModel companyParameterModel, IComponentCycleModel componentCycleModel, IParameterModel parameterModel, IComponentPeriodModel componentPeriodModel, IAccountModel accountModel, IAccountingBalanceModel accountingBalanceModel, ICoreWebParameter coreWebParameter)
    {
        _companyParameterModel = companyParameterModel;
        _componentCycleModel = componentCycleModel;
        _parameterModel = parameterModel;
        _componentPeriodModel = componentPeriodModel;
        _accountModel = accountModel;
        _accountingBalanceModel = accountingBalanceModel;
        _coreWebParameter = coreWebParameter;
    }

    public bool CycleIsCloseById(int companyId, int cycleId)
    {
        var objParameter = _parameterModel.GetRowByName("ACCOUNTING_CYCLE_WORKFLOWSTAGECLOSED");
        if (objParameter is null)
        {
            throw new Exception("NO EXISTE EL PARAMETER");
        }

        var objCompanyParameter =
            _companyParameterModel.GetRowByParameterIdCompanyId(companyId, objParameter.ParameterID);
        var objCycle = _componentCycleModel.GetRowByCycleId(cycleId);
        if (objCycle is null)
        {
            throw new Exception("NO EXISTE EL CICLO CONTABLE");
        }

        if (objCompanyParameter is null)
        {
            throw new Exception("NO EXISTE EL COMPANY PARAMETER");
        }

        return objCycle.StatusID == Convert.ToInt32(objCompanyParameter.Value);
        ;
    }

    public bool CycleIsEmptyById(int companyId, int cycleId)
    {
        var objCycle = _componentCycleModel.GetRowByCycleId(cycleId);
        var countJournal = _componentCycleModel.CountJournalInCycle(cycleId, companyId);
        return countJournal > 0;
    }

    public bool CycleIsCloseByDate(int companyId, DateTime dateOn)
    {
        var objParameter = _parameterModel.GetRowByName("ACCOUNTING_CYCLE_WORKFLOWSTAGECLOSED");
        if (objParameter is null)
        {
            throw new Exception("NO EXISTE EL PARAMETER");
        }

        var objCompanyParameter =
            _companyParameterModel.GetRowByParameterIdCompanyId(companyId, objParameter.ParameterID);
        var objCycle = _componentCycleModel.GetRowByCompanyIdFecha(companyId, dateOn);
        if (objCycle is null)
        {
            throw new Exception("NO EXISTE EL CICLO CONTABLE");
        }

        if (objCompanyParameter is null)
        {
            throw new Exception("NO EXISTE EL COMPANY PARAMETER");
        }

        return objCycle.StatusID == Convert.ToInt32(objCompanyParameter.Value);
    }

    public bool CycleIsEmptyByDate(int companyId, DateTime dateOn)
    {
        var objCycle = _componentCycleModel.GetRowByCompanyIdFecha(companyId, dateOn);
        if (objCycle is null)
        {
            throw new Exception("NO EXISTE EL CICLO CONTABLE");
        }
        var countJournal = _componentCycleModel.CountJournalInCycle(objCycle.ComponentCycleID, companyId);
        return countJournal > 0;
    }

    public bool PeriodIsCloseById(int companyId, int periodId)
    {
        var objParameter = _parameterModel.GetRowByName("ACCOUNTING_PERIOD_WORKFLOWSTAGECLOSED");
        var objCompanyParameter =
            _companyParameterModel.GetRowByParameterIdCompanyId(companyId, objParameter.ParameterID);
        var objPeriod = _componentPeriodModel.GetRowByPk(periodId);
        if (objPeriod is null)
        {
            throw new Exception("NO EXISTE EL PERIODO CONTABLE");
        }

        if (objCompanyParameter is null)
        {
            throw new Exception("NO EXISTE EL COMPANY PARAMETER");
        }

        return objPeriod.StatusID == Convert.ToInt32(objCompanyParameter.Value);
    }

    public bool PeriodIsEmptyById(int companyId, int periodId)
    {
        var countJournal = _componentPeriodModel.CountJournalInPeriod(periodId, companyId);
        return countJournal <= 0;
    }

    public bool PeriodIsCloseByDate(int companyId, DateTime dateOn)
    {
        var objParameter = _parameterModel.GetRowByName("ACCOUNTING_PERIOD_WORKFLOWSTAGECLOSED");
        var objCompanyParameter =
            _companyParameterModel.GetRowByParameterIdCompanyId(companyId, objParameter.ParameterID);
        var objPeriod = _componentPeriodModel.GetRowByCompanyIdFecha(companyId, dateOn);
        if (objCompanyParameter is null)
        {
            throw new Exception("NO EXISTE EL COMPANY PARAMETER");
        }
        return objPeriod.StatusID == Convert.ToInt32(objCompanyParameter.Value);
    }

    public bool PeriodIsEmptyByDate(int companyId, DateTime dateOn)
    {
        var objPeriod = _componentPeriodModel.GetRowByCompanyIdFecha(companyId, dateOn);
        var countJournal = _componentPeriodModel.CountJournalInPeriod(objPeriod.ComponentPeriodID, companyId);
        return countJournal > 0;
    }

    public void MayorizateAccount(int companyId, int branchId, int loginId, int accountId,
        int componentPeriodId, int componentCycleId, decimal balance, decimal debit, decimal credit)
    {
        var objAccount = _accountModel.GetRowByPk(companyId, accountId);
        if (objAccount.ParentAccountID != null)
        {
            MayorizateAccount(companyId, branchId, loginId, objAccount.ParentAccountID.Value, componentPeriodId,
                componentCycleId, balance, debit, credit);
        }

        _accountingBalanceModel.UpdateBalance(companyId, componentPeriodId, componentCycleId, accountId, balance, debit,
            credit);
    }

    public int MayorizateCycle(int companyId, int branchId, int loginId, int componentPeriodId, int componentCycleId)
    {
        const decimal balance = decimal.Zero;
        const int componentAccountId = 4;
        var objCycle = _componentCycleModel.GetRowByPk(componentPeriodId, componentCycleId);
        var workflowStageCycleClose =
            _coreWebParameter.GetParameter("ACCOUNTING_CYCLE_WORKFLOWSTAGECLOSED", companyId)!.Value;
        if (objCycle.StatusID == Convert.ToInt32(workflowStageCycleClose))
        {
            return 1;
        }

        //Obtener el comprobante de Cierre
        var journalTypeClosed = _coreWebParameter.GetParameter("ACCOUNTING_CYCLE_WORKFLOWSTAGECLOSED", companyId)!.Value;
        //Limpiar la tabla Temporal
        _accountingBalanceModel.DeleteJournalEntryDetailSummary(companyId, branchId, loginId);
        //Obtener los comprobantes resumidos
        _accountingBalanceModel.SetJournalSummary(companyId, branchId, loginId, componentCycleId,
            Convert.ToInt32(journalTypeClosed));
        //Ingresar las cuentas en la tabla balance
        _accountingBalanceModel.SetAccountBalance(companyId, branchId, loginId, componentCycleId, componentPeriodId,
            componentAccountId);
        //Mayorizar Cuentas
        _accountingBalanceModel.ClearCycle(companyId, componentPeriodId, componentCycleId);
        var minAccountId = _accountingBalanceModel.GetMinAccount(companyId, branchId, loginId);
        var maxAccountId = _accountingBalanceModel.GetMaxAccount(companyId, branchId, loginId);
        if (minAccountId <= maxAccountId)
        {
            var objAccountBalance =
                _accountingBalanceModel.GetInfoAccount(companyId, branchId, loginId, minAccountId.Value);
            if (objAccountBalance is { Debit: not null, Credit: not null })
            {
                MayorizateAccount(companyId,
                    branchId,
                    loginId,
                    minAccountId.Value,
                    componentPeriodId,
                    componentCycleId,
                    balance,
                    objAccountBalance.Debit.Value,
                    objAccountBalance.Credit.Value);
            }

            minAccountId = _accountingBalanceModel.GetMinAccountBy(companyId, branchId, loginId, minAccountId.Value);
        }

        _accountingBalanceModel.DeleteJournalEntryDetailSummary(companyId, branchId, loginId);
        return 1;
    }
}