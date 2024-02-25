using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface ICompanyLogModel
{
    //No existe la tabla tb_company_log
    List<T> getInfo<T>(int companyId,int branchId,int loginId,int app);
}