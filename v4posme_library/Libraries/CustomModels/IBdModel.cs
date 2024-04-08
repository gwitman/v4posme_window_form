namespace v4posme_library.Libraries.CustomModels;

public interface IBdModel
{
    T ExecuteRenderWidthParameter<T>(string query, object[] parameter = null);

    T ExecuteRender<T>(string query);
}