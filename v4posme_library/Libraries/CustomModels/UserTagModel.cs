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
            .Where(tag => tag.UserId == userId)
            .ExecuteDelete();
    }

    public int DeleteAppPosme(int tagId, int userId)
    {
        using var context = new DataContext();
        return context.TbUserTags
            .Where(tag => tag.UserId == userId
                          && tag.TagId == tagId)
            .ExecuteDelete();
    }

    public int InsertAppPosme(TbUserTag data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.UserTagId;
    }

    public List<TbUserTagDto> GetRowByUser(int userId)
    {
        using var context = new DataContext();
        var result = from ut in context.TbUserTags
            join u in context.TbUsers on ut.UserId equals u.UserId
            join t in context.TbTags on ut.TagId equals t.TagId
            where u.UserId == userId && t.IsActive!.Value == 1
            select new TbUserTagDto
            {
                TagId = ut.TagId,
                UserId = ut.UserId,
                CompanyId = ut.CompanyId,
                BranchId = ut.BranchId,
                UserEmail = u.Email,
                Name = t.Name,
                Description = t.Description,
                SendEmail = t.SendEmail,
                SendNotificationApp = t.SendNotificationApp,
                SendSms = t.SendSms,
                IsActive = t.IsActive
            };
        return result.ToList();
    }

    public List<TbUserTagDto> GetRowByPk(int tagId)
    {
        using var context = new DataContext();
        var result = from ut in context.TbUserTags
            join u in context.TbUsers on ut.UserId equals u.UserId
            join t in context.TbTags on ut.TagId equals t.TagId
            where ut.TagId == tagId && t.IsActive == 1
            select new TbUserTagDto
            {
                TagId = ut.TagId,
                UserId = ut.UserId,
                CompanyId = ut.CompanyId,
                BranchId = ut.BranchId,
                UserEmail = u.Email
            };
        return result.ToList();
    }
}