using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels.Core;

class UserModel : IUserModel
{
    public int InsertAppPosme(TbUser data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.UserId;
    }

    public void UpdateAppPosme(int companyId, int branchId, int userId, TbUser data)
    {
        using var context = new DataContext();
        var find = context
            .TbUsers.FirstOrDefault(user => user.CompanyId == companyId
                                            && user.BranchId == branchId
                                            && user.UserId == userId);
        if (find is null) return;
        data.UserId = userId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public List<TbUser> GetRowByComercio(string comercio)
    {
        using var context = new DataContext();
        return context.TbUsers
            .Where(user => user.Comercio == comercio
                           && user.IsActive!.Value)
            .ToList();
    }

    public List<TbUser> GetRowByFoto(string foto)
    {
        using var context = new DataContext();
        return context.TbUsers
            .Where(user => user.Foto == foto
                           && user.IsActive!.Value)
            .ToList();
    }

    public TbUser GetRowByExistNickname(string nickname)
    {
        using var context = new DataContext();
        return context.TbUsers
            .Single(user => user.Nickname == nickname
                            && user.IsActive!.Value);
    }

    public TbUser GetRowByNiknamePassword(string nickname, string password)
    {
        using var context = new DataContext();
        return context.TbUsers
            .Single(user => user.Nickname == nickname
                            && user.Password == password
                            && user.IsActive!.Value);
    }

    public TbUser GetRowByEmail(string email)
    {
        using var context = new DataContext();
        return context.TbUsers
            .Single(user => user.Email == email
                            && user.IsActive!.Value);
    }

    public TbUser GetRowByPk(int companyId, int branchId, int userId)
    {
        using var context = new DataContext();
        return context.TbUsers
            .Single(user => user.CompanyId == companyId
                            && user.BranchId == branchId
                            && user.UserId == userId
                            && user.IsActive!.Value);
    }

    public List<TbUser> GetAll(int companyId)
    {
        using var context = new DataContext();
        return context.TbUsers
            .Where(user => user.CompanyId == companyId
                           && user.IsActive!.Value)
            .ToList();
    }

    public List<TbUser> GetUserByBussnes(int companyId, string bussines)
    {
        using var context = new DataContext();
        return context.TbUsers
            .Where(user => user.CompanyId == companyId
                           && user.Nickname!.Contains(bussines))
            .ToList();
    }

    public int GetCountUser(int companyId)
    {
        using var context = new DataContext();
        return context.TbUsers
            .Where(user => user.CompanyId == companyId
                           && user.IsActive!.Value)
            .Select(user => user)
            .Count();
    }

    public int GetCount(int companyId)
    {
        using var context = new DataContext();
        return context.TbUsers
            .Where(user => user.CompanyId == companyId
                           && user.IsActive!.Value)
            .Select(user => user)
            .Count();
    }
}