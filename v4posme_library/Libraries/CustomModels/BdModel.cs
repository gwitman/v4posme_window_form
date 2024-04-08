using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class BdModel : IBdModel
{
    public T ExecuteRenderWidthParameter<T>(string query, object[]? parameter=null)
    {
        using var context = new DataContext();
        return (T)context.Database.SqlQueryRaw<T>(query, parameter);
    }

    public T ExecuteRender<T>(string query)
    {
        using var context = new DataContext();
        return (T)context.Database.SqlQueryRaw<T>(query);
    }


}