using Microsoft.EntityFrameworkCore;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomModels;

class UserTagModel : IUserTagModel
{
    public int DeleteByUser(int userId)
    {
        using var context = new DataContext();
        return context.TbUserTags
            .Where(tag => tag.UserID == userId)
            .ExecuteDelete();
    }

    public int DeleteAppPosme(int tagId, int userId)
    {
        using var context = new DataContext();
        return context.TbUserTags
            .Where(tag => tag.UserID == userId
                          && tag.TagID == tagId)
            .ExecuteDelete();
    }

    public int InsertAppPosme(TbUserTag data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.UserTagID;
    }

    public List<TbUserTagDto> GetRowByUser(int userId)
    {
        using var context = new DataContext();
        var result = from ut in context.TbUserTags
            join u in context.TbUsers on ut.UserID equals u.UserID
            join t in context.TbTags on ut.TagID equals t.TagID
            where u.UserID == userId && t.IsActive!.Value
            select new TbUserTagDto
            {
                TagId = ut.TagID,
                UserId = ut.UserID,
                CompanyId = ut.CompanyID,
                BranchId = ut.BranchID,
                UserEmail = u.Email,
                Name = t.Name,
                Description = t.Description,
                SendEmail = t.SendEmail,
                SendNotificationApp = t.SendNotificationApp,
                SendSms = t.SendSMS,
                IsActive = t.IsActive
            };
        return result.ToList();
    }

    public List<TbUserTagDto> GetRowByPk(int tagId)
    {
        using var context = new DataContext();
        var result = from ut in context.TbUserTags
            join u in context.TbUsers on ut.UserID equals u.UserID
            join t in context.TbTags on ut.TagID equals t.TagID
            where ut.TagID == tagId && t.IsActive!.Value
            select new TbUserTagDto
            {
                TagId = ut.TagID,
                UserId = ut.UserID,
                CompanyId = ut.CompanyID,
                BranchId = ut.BranchID,
                UserEmail = u.Email
            };
        return result.ToList();
    }
}