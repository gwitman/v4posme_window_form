using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels
{
    public interface IBranchModel
    {
        TbBranch FindById(int id, int idCompany);

        List<TbBranch> findAll();
    }
}
