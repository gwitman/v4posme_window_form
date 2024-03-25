using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface IParameterModel
{
    TbParameter GetRowByName(string name);

    List<TbParameter> GetAll();
}