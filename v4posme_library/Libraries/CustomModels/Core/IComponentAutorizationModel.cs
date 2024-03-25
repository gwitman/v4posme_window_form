using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface IComponentAutorizationModel
{
    List<TbComponentAutorization> GetRowByCompanyId(int companyId);
}