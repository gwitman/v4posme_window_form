using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface ICurrencyModel
{
    TbCurrency GetRowName(string? name);
    
    TbCurrency GetRowByPk(int currencyId);

    List<TbCurrency> GetList();
}