using v4posme_library.Models;
using v4posme_library.ModelsViews;

namespace v4posme_library.Libraries.CustomModels;

public interface ICustomerCreditAmortizationModel
{
    void UpdateAppPosme(int creditAmortizationId,TbCustomerCreditAmortization data);
    
    void DeleteAppPosme(int creditAmortizationId);
    
    int InsertAppPosme(TbCustomerCreditAmortization data);

    TbCustomerCreditAmortization GetRowByPk(int creditAmortizationId);
    
    List<TbCustomerCreditAmortization> GetRowByDocument(int customerCreditDocumentId);
    
    List<TbCustomerCreditAmortization> GetRowByDocumentAndVinculable(int customerCreditDocumentId);
    
    List<TbCustomerCreditAmortization> GetRowByDocumentAndNonVinculable(int customerCreditDocumentId);
    
    List<CustomerCreditAmortizationView> GetRowByCustomerId(int customerId);
    
    List<CustomerCreditAmortizationView> GetRowShareLate(int customerId);

    CustomerCreditAmortizationView GetRowBySummaryInformationCredit(string documentNumber);
    
    CustomerCreditAmortizationView GetRowByCreditDocumentAndBalanceMinim(int customerCreditDocumentId);
}