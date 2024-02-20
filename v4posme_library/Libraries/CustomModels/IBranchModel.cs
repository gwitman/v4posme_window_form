using v4posme_library.Models;
namespace v4posme_library.Libraries.CustomModels
{
    public interface IBranchModel
    {
        TbBranch? findById(int id);

        List<TbBranch?> findAll();
    }
}
