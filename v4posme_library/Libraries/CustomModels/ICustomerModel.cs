using System.ComponentModel.Design;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

public interface ICustomerModel
{
    void UpdateAppPosme(int companyId, int branchId, int entityId, TbCustomer data);
    
    void DeleteAppPosme(int companyId, int branchId, int entityId);

    int InsertAppPosme(TbCustomer data);

    List<TbCustomerDto> GetHappyBirthDay(int companyId);

    TbCustomer? GetRowByCode(int companyId, string? customerCode);
    TbCustomerDto? GetRowByCodeDto(int companyId, string? customerCode);

    TbCustomer? GetRowByIdentification(int companyId, string identification);

    List<TbCustomerDto> GetRowByCompanyPhoneAndEmail(int companyId);

    List<TbCustomerDto> GetRowByCompany(int companyId);

    TbCustomerDto? GetRowByEntity(int companyId, int entityId);

    TbCustomer? GetRowByPk(int companyId,int branchId,int entityId);
    TbCustomer? GetRowByPKK(int entityId);
}