using v4posme_library.Models;
namespace v4posme_library.Domain.Services.Interfaz
{
    public interface IMenuElementModelService
    {
        List<TbMenuElement> GetRowByCompanyIdyElementId(int companyId, List<int> elementIdArray);

        List<TbMenuElement> GetRowByCompanyId(int companyId);
    }
}
