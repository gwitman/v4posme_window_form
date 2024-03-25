namespace v4posme_library.ModelsViews
{
    public class TbUserPermissionDto
    {
        /*tb_user_permission.companyID,tb_user_permission.branchID,tb_user_permission.roleID,tb_user_permission.elementID,
        tb_user_permission.selected,tb_user_permission.inserted,tb_user_permission.deleted,
        tb_user_permission.edited,tb_menu_element.orden,tb_menu_element.display*/
        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
        public int? ElementId { get; set; }
        public bool? Selected { get; set; }
        public bool? Inserted { get; set; }
        public bool? Deleted { get; set; }
        public bool? Edited { get; set; }
        public string? MenuElementOrden { get; set; }
        public string? MenuElementDisplay { get; set; }
        public int? RoleId { get; set; }
    }
}
