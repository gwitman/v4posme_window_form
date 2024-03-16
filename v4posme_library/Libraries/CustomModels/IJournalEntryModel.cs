using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IJournalEntryModel
{
    void DeleteAppPosme(int companyId, int journalEntryId);

    void UpdateAppPosme(int companyId, int journalEntryId, TbJournalEntry data);

    int InsertAppPosme(TbJournalEntry data);

    TbJournalEntry GetRowByCode(int companyId, string journalNumber);

    TbJournalEntry GetRowByPk(int companyId, int journalEntryId);
    
    TbJournalEntry get_rowByPK_Next(int companyId,int journalEntryId);

    TbJournalEntry GetRowByPkBack(int companyId, int journalEntryId);
}