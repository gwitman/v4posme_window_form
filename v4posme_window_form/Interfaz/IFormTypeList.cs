
namespace v4posme_window.Interfaz
{
    public interface IFormTypeList
    {
        void List();
        void Delete();
        void Edit(object? sender, EventArgs? args);
        void New();
    }
}
