using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using v4posme_window_form.Models.Tablas;

namespace v4posme_window_form.Domain.Services
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
