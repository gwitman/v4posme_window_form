using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels
{
    public class BranchModel : IBranchModel
    {
        public List<TbBranch> findAll()
        {
            using var context = new DataContext();
            return context.TbBranches.ToList();
        }

        //id branch, id company y que este activa
        public TbBranch FindById(int id, int idCompany)
        {
            using var context = new DataContext();
            return context.TbBranches
                .Single(branch=>branch.BranchId == id
                && branch.CompanyId==idCompany
                && branch.IsActive!.Value);
        }
    }
}
