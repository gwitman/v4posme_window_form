namespace v4posme_library.Libraries.CustomModels;

public interface IBdModel
{
    T ExecuteRender<T>(string query, object[] parameter = null);
}