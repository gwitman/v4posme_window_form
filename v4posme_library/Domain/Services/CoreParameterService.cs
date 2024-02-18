using DevExpress.Xpo;
using System;
using System.Linq;
using v4posme_library.ModelsCode;
namespace v4posme_library.Domain.Services
{
    public class CoreParameterService : ICoreParameterService
    {

        public Parameter GetRowByName(string name)
        {
            try
            {
                var query = Session.DefaultSession.Query<Parameter>();
                return query.First(p => p.Name.Equals(name));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
