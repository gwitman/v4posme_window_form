using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels
{
    public interface IAccountTypeModel
    {
        void DeleteAppPosme(int companyId, int accountTypeId);

        void UpdateAppPosme(int companyId, int accountTypeId, TbAccountType data);

        int InsertAppPosme(TbAccountType data);

        int GetCountInAccount(int companyId, int accountTypeId);

        List<TbAccountType> GetByCompany(int companyId);

        TbAccountType GetRowByPk(int companyId, int accountTypeId);
    }
}