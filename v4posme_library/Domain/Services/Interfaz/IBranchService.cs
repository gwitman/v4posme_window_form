using v4posme_library.Models;
namespace v4posme_library.Domain.Services.Interfaz
{
    public interface IBranchService
    {
        TbBranch findById(int id);

        List<TbBranch> findAll();
    }
}
