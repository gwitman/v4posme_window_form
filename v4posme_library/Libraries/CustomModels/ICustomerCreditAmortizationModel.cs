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
    
    List<TbCustomerCreditAmortization> GetRowByDocumentAndVinculable(int customerCreditDocumentId);
    
    List<TbCustomerCreditAmortization> GetRowByDocumentAndNonVinculable(int customerCreditDocumentId);
    
    List<TbCustomerCreditAmortizationDto> GetRowByCustomerId(int customerId);
    
    List<TbCustomerCreditAmortizationDto> GetRowShareLate(int customerId);

    TbCustomerCreditAmortizationDto GetRowBySummaryInformationCredit(string documentNumber);
    
    List<TbCustomerCreditAmortizationDto> GetRowByCreditDocumentAndBalanceMinim(int customerCreditDocumentId);
}