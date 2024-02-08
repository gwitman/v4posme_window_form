using System.Collections.Generic;
using v4posme_window_form.Models.Tablas;
namespace v4posme_window_form.Domain.Services
{
    public interface IMenuElementService
    {
        List<MenuElement> GetRowByCompanyIdyElementId(int companyId, List<int> elementIdArray);
    }
}
