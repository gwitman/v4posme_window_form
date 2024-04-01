using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public interface ICustomerCreditAmortizationModel
{
    void UpdateAppPosme(int creditAmortizationId,TbCustomerCreditAmortization data);
    
    void DeleteAppPosme(int creditAmortizationId);
    
    int InsertAppPosme(TbCustomerCreditAmortization data);

    TbCustomerCreditAmortization GetRowByPk(int creditAmortizationId);
    
    List<TbCustomerCreditAmortization> GetRowByDocument(int customerCreditDocumentId);
    
    List<TbCustomerCreditAmortization>? GetRowByDocumentAndVinculable(int customerCreditDocumentId);
    
    List<TbCustomerCreditAmortization> GetRowByDocumentAndNonVinculable(int customerCreditDocumentId);
    
    List<CustomerCreditAmortizationDto> GetRowByCustomerId(int customerId);
    
    List<CustomerCreditAmortizationDto> GetRowShareLate(int customerId);

    CustomerCreditAmortizationDto GetRowBySummaryInformationCredit(string documentNumber);
    
    List<CustomerCreditAmortizationDto> GetRowByCreditDocumentAndBalanceMinim(int customerCreditDocumentId);
}