using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface ICustomerCreditDocumentEntityRelatedModel
{
    void UpdateAppPosme(int customerCreditDocumentId,int entityId,TbCustomerCreditDocumentEntityRelated data);
    
    void DeleteAppPosme(int customerCreditDocumentId,int entityId);
    
    int InsertAppPosme(TbCustomerCreditDocumentEntityRelated data);
    
    TbCustomerCreditDocumentEntityRelated GetRowByPk(int ccEntityRelatedId);

    TbCustomerCreditDocumentEntityRelated GetRowByEntity(int customerCreditDocumentId, int entityId);

    List<TbCustomerCreditDocumentEntityRelated> GetRowByDocument(int customerCreditDocumentId);
}