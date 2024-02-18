using System.Collections.Generic;
using v4posme_library.ModelsCode;
namespace v4posme_library.Domain.Services
{
    public interface IMenuElementService
    {
        List<MenuElement> GetRowByCompanyIdyElementId(int companyId, List<int> elementIdArray);
    }
}
