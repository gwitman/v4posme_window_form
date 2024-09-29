using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class TagModel : ITagModel
{
    public void UpdateAppPosme(int tagId, TbTag data)
    {
        using var context = new DataContext();
        var find = context.TbTags.Find(tagId);
        if (find is null) return;
        data.TagID = find.TagID;
        context.Entry(find).CurrentValues.SetValues(data);
        context.SaveChanges();
    }

    public void DeleteAppPosme(int tagId)
    {
        using var context = new DataContext();
        var find = context.TbTags.Find(tagId);
        if (find is null) return;
        find.IsActive = false;
        context.SaveChanges();
    }

    public int InsertAppPosme(TbTag data)
    {
        using var context = new DataContext();
        var add = context.Add(data);
        context.SaveChanges();
        return add.Entity.TagID;
    }

    public List<TbTag> GetRows()
    {
        using var context = new DataContext();
        return context.TbTags.Where(tag => tag.IsActive!.Value).ToList();
    }

    public TbTag GetRowByPk(int tagId)
    {
        using var context = new DataContext();
        return context.TbTags
            .Single(tag => tag.IsActive!.Value
                          && tag.TagID == tagId);
    }

    public TbTag GetRowByName(string? name)
    {
        using var context = new DataContext();
        return context.TbTags
            .First(tag => tag.IsActive!.Value
                          && tag.Name == name);
    }
}