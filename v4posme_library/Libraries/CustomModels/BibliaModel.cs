using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class BibliaModel : IBibliaModel
{
    public List<TbBiblia> GetRowByDay(int companyId, int dia)
    {
        using var context = new DataContext();
        return context.TbBiblias
            .Where(bible => bible.Dia >= dia - 3
                            && bible.Dia <= dia - 1)
            .ToList();
    }
}