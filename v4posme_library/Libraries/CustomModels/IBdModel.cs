﻿using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

public interface IBdModel
{
    void ExecuteSqlRaw(string? query);
    void ExecuteProcedure(string? query);
    
    T? ExecuteRenderWidthParameter<T>(string? query, object[] parameter);

    T? ExecuteRender<T>(string? query);
    List<Dictionary<string?, object>>? ExecuteRenderQueryable(string? query);
}