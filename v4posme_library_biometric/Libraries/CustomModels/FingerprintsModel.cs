using Models;
using v4posme_library_biometric.Models;

namespace v4posme_library_biometric.Libraries.CustomModels;

public class FingerprintsModel : IFingerprintsModel
{
    public void DeleteAppPosme(int companyId)
    {
        using (var context = new DataContext())
        {
            //no tiene companyID
        }
    }

    public void UpdateAppPosme(int fingerId, Fingerprint data)
    {
        using var context = new DataContext();
        var find = context.Fingerprints.SingleOrDefault(fingerprint => fingerprint.Id == fingerId);
        if (find == null) return;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public int InsertAppPosme(Fingerprint data)
    {
        using var context = new DataContext();
        var entity = context.Fingerprints.Add(data);
        context.SaveChanges();
        return entity.Entity.Id;
    }

    public int GetCount()
    {
        using var context = new DataContext();
        return context.Fingerprints.Count();
    }

    public Fingerprint? GetFingerprintId(int id)
    {
        using var context = new DataContext();
        return context.Fingerprints.SingleOrDefault(fingerprint => fingerprint.Id == id);
    }
}