using DevExpress.Xpo;
using System;
using System.Linq;
using v4posme_window_form.Models.Tablas;

namespace v4posme_window_form.Domain.Services
{
    public class CompanyParamterService : ICompanyParameterService
    {

        public CompanyParameter GetRowByParameterIdCompanyId(int companyId, int parameterId)
        {
            try
            {
                var query = Session.DefaultSession.Query<CompanyParameter>();
                return query.First(parameter => parameter.CompanyID == companyId && parameter.ParameterID == parameterId);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
