using v4posme_library.Domain.Services.Interfaz;
using v4posme_library.Models;
namespace v4posme_library.Domain.Services.Implementacion
{
    public class BranchService : IBranchService
    {
        public List<TbBranch> findAll()
        {
            using var context = new DataContext();
            return context.TbBranches.ToList();
        }

        //id branch, id company y que este activa
        public TbBranch findById(int id)
        {
            using var context = new DataContext();
            return context.TbBranches.First(branch=>branch.BranchId == id);
        }
    }
}
