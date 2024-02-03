using DevExpress.Xpo;
using v4posme_window_form.Models.Tablas;

namespace v4posme_window_form.Domain.Services
{
    public class CompanyService : ICompanyService
    {
        public Company findById(int id)
        {
            if (id == 0) return null;
            else
            {
                return Session.DefaultSession.GetObjectByKey<Company>(id,true);
            }
        }
    }
}
