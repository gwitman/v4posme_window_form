using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomLibraries.Implementacion
{
    public class CompanyService : ICompanyService
    {
        //validar que este activa
        public TbCompany? GetRowByPk(int id)
        {
            using var context = new DataContext();
            return context.TbCompanies.First(company => company.CompanyId == id);
        }
    }
}