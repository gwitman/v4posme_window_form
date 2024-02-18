using System.Collections.Generic;
using v4posme_library.ModelsCode;
namespace v4posme_library.Domain.Services.Interfaz
{
    public interface IMenuElementModelService
    {
        List<MenuElement> GetRowByCompanyIdyElementId(int companyId, List<int> elementIdArray);

        List<MenuElement> GetRowByCompanyId(int companyId);
    }
}
