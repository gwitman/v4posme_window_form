using v4posme_library.Models;
namespace v4posme_library.Libraries.ComponentModels;

public interface IComponentCycleModel
{
    TbAccountingCycle GetRowByCycleId(int cycleId);
    int CountJournalInCycle(int cycleId, int companyId);

    TbAccountingCycle GetRowByCompanyIdFecha(int companyId, DateTime dateStart);
    TbAccountingCycle GetRowByPk(int periodId, int cycleId);
}
