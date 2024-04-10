namespace v4posme_library.ModelsDto
{
    public class TbUserPermissionDto
    {
        /*tb_user_permission.companyID,tb_user_permission.branchID,tb_user_permission.roleID,tb_user_permission.elementID,
        tb_user_permission.selected,tb_user_permission.inserted,tb_user_permission.deleted,
        tb_user_permission.edited,tb_menu_element.orden,tb_menu_element.display*/
        public int? CompanyId { get; set; }
        public int? BranchId { get; set; }
        public int? ElementId { get; set; }
        public int? Selected { get; set; }
        public int? Inserted { get; set; }
        public int? Deleted { get; set; }
        public int? Edited { get; set; }
        public string? MenuElementOrden { get; set; }
        public string? MenuElementDisplay { get; set; }
        public int? RoleId { get; set; }
    }
}
