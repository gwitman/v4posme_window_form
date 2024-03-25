using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface ILogModel
{
    TbLog GetRowByPk(int companyId,int branchId,int loginId,string token);
    
    TbLog GetRowByNameParameterOutput(int companyId,int branchId,int loginId,string token,string nameParameterOutput);
}