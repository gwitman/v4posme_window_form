using System.Collections.Generic;
using v4posme_library.ModelsCode;
namespace v4posme_library.Domain.Services.Interfaz
{
    public interface IBranchService
    {
        Branch findById(int id);

        List<Branch> findAll();
    }
}
