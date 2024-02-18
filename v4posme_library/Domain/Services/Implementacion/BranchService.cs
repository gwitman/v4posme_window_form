using DevExpress.Xpo;
using System.Collections.Generic;
using System.Linq;
using v4posme_library.Domain.Services.Interfaz;
using v4posme_library.ModelsCode;
namespace v4posme_library.Domain.Services.Implementacion
{
    public class BranchService : IBranchService
    {
        public List<Branch> findAll()
        {
            return Session.DefaultSession.Query<Branch>().ToList();
        }

        //id branch, id company y que este activa
        public Branch findById(int id)
        {
            return Session.DefaultSession.GetObjectByKey<Branch>(id);
        }
    }
}
