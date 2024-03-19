using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class TagModel : ITagModel
{
    public void UpdateAppPosme(int tagId, TbTag data)
    {
        using var context = new DataContext();
        var find = context.TbTags.Find(tagId);
        if (find is null) return;
        data.TagId = find.TagId;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public void DeleteAppPosme(int tagId)
    {
        using var context = new DataContext();
        var find = context.TbTags.Find(tagId);
        if (find is null) return;
        find.IsActive = 0;
        context.SaveChanges();
    }

    public int InsertAppPosme(TbTag data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.TagId;
    }

    public List<TbTag> GetRows()
    {
        using var context = new DataContext();
        return context.TbTags.Where(tag => tag.IsActive == 1).ToList();
    }

    public TbTag GetRowByPk(int tagId)
    {
        using var context = new DataContext();
        return context.TbTags
            .Single(tag => tag.IsActive == 1
                          && tag.TagId == tagId);
    }

    public TbTag GetRowByName(string name)
    {
        using var context = new DataContext();
        return context.TbTags
            .First(tag => tag.IsActive == 1
                          && tag.Name == name);
    }
}