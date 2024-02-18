using DevExpress.Xpo;
using v4posme_library.ModelsCode;

namespace v4posme_library.Domain.Services
{
    public class CompanyService : ICompanyService
    {
        //validar que este activa
        public Company GetRowByPk(int id)
        {
            if (id == 0) return null;
            else
            {
                return Session.DefaultSession.GetObjectByKey<Company>(id,true);
            }
        }
    }
}
