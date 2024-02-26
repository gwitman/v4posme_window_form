using System.ComponentModel.Design;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface ICustomerCreditDocumentModel
{
    void UpdateAppPosme(int customerCreditDocumentId,TbCustomerCreditDocument data);

    void DeleteAppPosme(int customerCreditDocumentId);

    int InsertAppPosme(TbCustomerCreditDocument data);

    TbCustomerCreditDocument GetRowByPk(int customerCreditDocumentId);

    List<TbCustomerCreditDocument> GetRowByEntity(int companyId,int entityId);

    List<TbCustomerCreditDocument>  GetRowByEntityApplied(int companyId,int entityId,int currencyId );

    List<TbCustomerCreditDocument> GetRowByEntityCreditLine(int companyId,int entityId,int creditLineId);

    TbCustomerCreditDocument GetRowByDocument(int companyId, int entityId, string documentNumber);

    List<TbCustomerCreditDocument> GetRowByBalanceBetweenCeroAndCeroPuntoCinco(int companyId);

    List<TbCustomerCreditDocument> GetRowByBalancePending(int companyId,int entityId,int customerCreditDocumentId,int currencyId);
}