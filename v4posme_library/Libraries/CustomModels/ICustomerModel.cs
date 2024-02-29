using System.ComponentModel.Design;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface ICustomerModel
{
    void UpdateAppPosme(int companyId, int branchId, int entityId, TbCustomer data);

    void DeleteAppPosme(int companyId, int branchId, int entityId);

    int InsertAppPosme(TbCustomer data);

    List<TbCustomer> GetHappyBirthDay(int companyId);

    TbCustomer GetRowByCode(int companyId, int customerCode);

    TbCustomer GetRowByIdentification(int companyId, int identification);

    List<TbCustomer> GetRowByCompanyPhoneAndEmail(int companyId);

    List<TbCustomer>  GetRowByCompany(int companyId);

    TbCustomer GetRowByEntity(int companyId, int entityId);

    TbCustomer GetRowByPk(int companyId,int branchId,int entityId);
}