namespace v4posme_library.ModelsDto
{
    public class MenuElementDto
    {
        //x.companyID,x.elementID,x.menuElementID,x.parentMenuElementID, x.display,x.address,x.orden,x.icon,x.template,x.nivel
        public int ElementId { get; set; }
        public int MenuElementId { get; set; }
        public int Nivel { get; set; }
        public string? Address { get; set; }
        public string? Display { get; set; }

        public string? Icon { get; set; }

        //public int TypeMenuElementId { get; set; }
        public string? Orden { get; set; }

        public string? Template { get; set; }

        //public sbyte IsActive { get; set; }
        public int CompanyId { get; set; }
        public int ParentMenuElementId { get; set; }
    }
}
