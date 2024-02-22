using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IComponentPeriodModel
{
    TbAccountingPeriod GetRowByPk(int componentPeriodId);
    TbAccountingPeriod GetRowByCompanyIdFecha(int companyID,DateTime dateStart);
    int CountJournalInPeriod(int periodId, int companyId);
}
