using v4posme_library.Models;
namespace v4posme_library.Libraries.Services.Interfaz
{
    public interface IElementSevice
    {
        List<TbElement> getRowByTypeAndLayout(int elementTypeId, int layoutId);
    }
}