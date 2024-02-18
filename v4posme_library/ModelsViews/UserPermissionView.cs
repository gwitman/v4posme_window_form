using DevExpress.Xpo;
namespace v4posme_library.ModelsViews
{
    [NonPersistent]
    public class UserPermissionView: XPLiteObject
    {
        //up.CompanyID, up.BranchID, up.RoleID, up.ElementID, up.Selected, up.Inserted, up.Deleted, up.Edited, menu.Orden,menu.Display
        public int CompanyId { get; set; }
        public int BranchId { get; set; }
        public int ElementId { get; set; }
        public bool Selected { get; set; }
        public bool Inserted { get; set; }
        public bool Deleted { get; set; }
        public bool Edited { get; set; }
        public string Orden { get; set; }
        public string Display { get; set; }
    }
}
