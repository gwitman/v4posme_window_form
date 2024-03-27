using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public interface IJournalEntryModel
{
    void DeleteAppPosme(int companyId, int journalEntryId);

    void UpdateAppPosme(int companyId, int journalEntryId, TbJournalEntry data);

    int InsertAppPosme(TbJournalEntry data);

    TbJournalEntryDto GetRowByCode(int companyId, string journalNumber);

    TbJournalEntryDto GetRowByPk(int companyId, int journalEntryId);
    
    TbJournalEntry GetRowByPkNext(int companyId,int journalEntryId);

    TbJournalEntry GetRowByPkBack(int companyId, int journalEntryId);
}