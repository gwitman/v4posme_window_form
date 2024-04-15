using System.ComponentModel.Design;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public interface ICustomerCreditDocumentModel
{
    void UpdateAppPosme(int customerCreditDocumentId,TbCustomerCreditDocument data);

    void DeleteAppPosme(int customerCreditDocumentId);

    int InsertAppPosme(TbCustomerCreditDocument data);

    TbCustomerCreditDocumentDto? GetRowByPk(int customerCreditDocumentId);

    List<TbCustomerCreditDocument> GetRowByEntity(int companyId,int entityId);

    List<TbCustomerCreditDocumentDto> GetRowByEntityApplied(int companyId, int entityId, int currencyId);

    TbCustomerCreditDocumentDto? GetRowByDocument(int companyId, int entityId, string documentNumber);

    List<TbCustomerCreditDocument> GetRowByBalanceBetweenCeroAndCeroPuntoCinco(int companyId);

    List<TbCustomerCreditDocumentDto> GetRowByBalancePending(int companyId, int entityId, int customerCreditDocumentId,
        int currencyId);
}