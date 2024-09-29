using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public interface ICustomerCreditAmortizationModel
{
    void UpdateAppPosme(int creditAmortizationId,TbCustomerCreditAmoritization data);
    
    void DeleteAppPosme(int creditAmortizationId);
    
    int InsertAppPosme(TbCustomerCreditAmoritization data);

    TbCustomerCreditAmoritization GetRowByPk(int creditAmortizationId);
    
    List<TbCustomerCreditAmoritization> GetRowByDocument(int customerCreditDocumentId);
    
    List<TbCustomerCreditAmoritization>? GetRowByDocumentAndVinculable(int customerCreditDocumentId);
    
    List<TbCustomerCreditAmoritization> GetRowByDocumentAndNonVinculable(int customerCreditDocumentId);
    
    List<TbCustomerCreditAmortizationDto> GetRowByCustomerId(int customerId);
    
    List<TbCustomerCreditAmortizationDto> GetRowShareLate(int customerId);

    TbCustomerCreditAmortizationDto GetRowBySummaryInformationCredit(string? documentNumber);
    
    List<TbCustomerCreditAmortizationDto> GetRowByCreditDocumentAndBalanceMinim(int customerCreditDocumentId);
}