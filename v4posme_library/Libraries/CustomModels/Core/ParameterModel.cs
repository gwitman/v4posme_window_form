﻿using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class ParameterModel : IParameterModel
{
    public TbParameter? GetRowByName(string name)
    {
        using var context = new DataContext();
        return context.TbParameters
            .FirstOrDefault(parameter => parameter!.Name != null
                                         && parameter.Name.Contains(name));
    }

    public List<TbParameter?>? GetAll()
    {
        using var context = new DataContext();
        return context.TbParameters.ToList();
    }
}