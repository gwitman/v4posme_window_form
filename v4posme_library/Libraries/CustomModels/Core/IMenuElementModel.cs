using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core
{
    public interface IMenuElementModel
    {
        List<TbMenuElement>? GetRowByCompanyIdyElementId(int companyId, List<int> elementIdArray);

        List<TbMenuElement> GetRowByCompanyId(int companyId);
    }
}
