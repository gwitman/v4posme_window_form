using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_library.Libraries.CustomLibraries.Interfaz;

public interface ICoreWebAuditoria
{
    void SetAuditCreated<T>(T obj, TbUser dataUser, string request);
    void SetAuditCreatedAdmin<T>(T obj, string request);
    List<TbComponentAuditDetailDto> GetAuditDetail(int companyId, int id, string tableName);
    void SetAudit(string tableName, object oldObject, object newObject, TbUser user, string request);
}