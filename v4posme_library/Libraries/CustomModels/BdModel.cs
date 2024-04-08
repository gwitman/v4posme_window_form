using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class BdModel : IBdModel
{
    public T ExecuteRender<T>(string query, object[]? parameter=null)
    {
        using var context = new DataContext();
        return parameter != null
            ? (T)context.Database.SqlQueryRaw<T>(query, parameter)
            : (T)context.Database.SqlQueryRaw<T>(query, null!);
    }
}