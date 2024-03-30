using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels.Core;

public interface IExchangerateModel
{
    void UpdateAppPosme(int companyId, DateOnly date, int currencyIdSource, int currencyIdTarget, TbExchangeRate data);

    int InsertAppPosme(TbExchangeRate data);

    TbExchangeRate? GetDefault(int companyId);

    TbExchangeRate? GetRowByPk(int companyId, DateOnly date, int currencyIdSource, int currencyIdTarget);

    List<TbExchangeRateDto> GetByCompanyAndDate(int companyId, DateOnly dateStartOn, DateOnly dateEndOn);
}