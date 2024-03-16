using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IJournalEntryDetailModel
{
    void DeleteAppPosme(int companyId,int journalEntryId,int journalEntryDetailId);
    
    void DeleteWhereIdNotIn(int companyId,int journalEntryId,List<int> listDetailId);
    
    void UpdateAppPosme(int companyId,int journalEntryId,int journalEntryDetailId,TbJournalEntryDetail data);
    
    int InsertAppPosme(TbJournalEntryDetail data);
    
    List<TbJournalEntryDetail> GetRowByJournalEntryId(int companyId,int journalEntryId);
    
    TbJournalEntryDetail GetRowByPk(int companyId,int journalEntryId,int journalEntryDetailId);
}