using Microsoft.EntityFrameworkCore;
using MySqlX.XDevAPI.Common;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class ParameterModel(DataContext context) : IParameterModel
{
    
    public TbParameter? GetRowByName(string? name)
    {
              
        var resul =  context.TbParameters.AsNoTracking()
            .FirstOrDefault(parameter => parameter!.Name != null
                                         && parameter.Name.Contains(name));

        return resul;
    }

    public List<TbParameter> GetAll()
    {
        return context.TbParameters.AsNoTracking().ToList();
    }
}