
namespace v4posme_window.Interfaz
{
    public interface IFormTypeList
    {
        void List();
        void Delete(object? sender, EventArgs? args);
        void Edit(object? sender, EventArgs? args);
        void New(object? sender, EventArgs? args);
    }
}
