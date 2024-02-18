using System.Collections.Generic;
using v4posme_library.ModelsCode;
namespace v4posme_library.Domain.Services.Interfaz
{
    public interface IElementSevice
    {
        List<Element> getRowByTypeAndLayout(int elementTypeId, int layoutId);
    }
}