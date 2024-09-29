using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class ErrorModel : IErrorModel
{
    public void UpdateAppPosme(int errorId, TbError data)
    {
        using var context = new DataContext();
        var find = context.TbErrors.Find(errorId);
        if (find is null) return;
        data.ErrorID = find.ErrorID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public void UpdateTagId(int tagId, int companyId, TbError data)
    {
        using var context = new DataContext();
        context.TbErrors
            .Where(error => error.TagID == tagId)
            .ExecuteUpdate(calls =>
                calls.SetProperty(error => error.Message, data.Message)
                    .SetProperty(error => error.Notificated, data.Notificated)
                    .SetProperty(error => error.IsActive, data.IsActive)
                    .SetProperty(error => error.IsRead, data.IsRead)
                    .SetProperty(error => error.ReadOn, data.ReadOn));
    }

    public int DeleteAppPosme(int errorId)
    {
        using var context = new DataContext();
        return context.TbErrors
            .Where(error => error.ErrorID == errorId)
            .ExecuteUpdate(calls =>
                calls.SetProperty(error => error.IsActive, (sbyte?)0));
    }

    public int DeleteByTagId(int tagId, int companyId)
    {
        using var context = new DataContext();
        return context.TbErrors
            .Where(error => error.TagID == tagId)
            .ExecuteDelete();
    }

    public int InsertAppPosme(TbError data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.ErrorID;
    }

    public TbError GetRowByPk(int errorId)
    {
        using var context = new DataContext();
        return context.TbErrors
            .Single(error => error.ErrorID == errorId
                             && error.IsActive == 1);
    }

    public List<TbError> GetRowByUser(int userId)
    {
        using var context = new DataContext();
        return context.TbErrors
            .Where(error => error.UserID == userId
                            && error.IsActive == 1
                            && error.ReadOn == null)
            .Take(5)
            .ToList();
    }

    public int GetRowByUserCount(int userId)
    {
        using var context = new DataContext();
        return context.TbErrors
            .Count(error => error.UserID == userId
                            && error.IsActive == 1
                            && error.ReadOn == null);
    }

    public List<TbError> GetRowByUserAllAndTagId(int userId, int tagId)
    {
        using var context = new DataContext();
        return context.TbErrors
            .Where(error => error.UserID == userId
                            && error.IsActive == 1
                            && error.TagID == tagId
                            && error.ReadOn == null)
            .ToList();
    }

    public List<TbError> GetRowByUserAll(int userId)
    {
        using var context = new DataContext();
        return context.TbErrors
            .Where(error => error.UserID == userId
                            && error.IsActive == 1
                            && error.ReadOn == null)
            .ToList();
    }

    public TbError GetRowByMessageUser(int userId, string? message)
    {
        using var context = new DataContext();
        return context.TbErrors
            .Single(error => userId == 0
                ? error.UserID == null
                : error.UserID == userId
                  && error.IsActive == 1
                  && error.Message == message
                  && error.ReadOn == null);
    }
}