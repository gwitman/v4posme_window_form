using Microsoft.EntityFrameworkCore;
using Models;
using v4posme_library_biometric.Models;

namespace v4posme_library_biometric.Libraries.CustomModels;

public class TempFingerprintModel : ITempFingerprintModel
{
    public void DeleteAppPosme(string tockenPc)
    {
        using var context = new DataContext();
        var tempFingerprints = context.TempFingerprints.Where(fingerprint => fingerprint.TokenPc!.Equals(tockenPc)).ToList();
        if (tempFingerprints.Count>0)
        {
            foreach (var tempFingerprint in tempFingerprints)
            {
                context.Remove(tempFingerprint);
            }

            context.SaveChanges();
        }
    }

    public void UpdateAppPosme(string tockenPc, TempFingerprint data)
    {
        using var context = new DataContext();
        var find = context.TempFingerprints.SingleOrDefault(fingerprint => fingerprint.TokenPc!.Equals(tockenPc));
        if (find == null) return;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public bool InsertAppPosme(TempFingerprint data)
    {
        using var context = new DataContext();
        var entry = context.TempFingerprints.Add(data);
        return context.SaveChanges() > 0;
    }
}