namespace v4posme_window_form.Models
{
    public class MenuElementView
    {
        public int CompanyId { get; set; }
        public int ElementId { get; set; }
        public int MenuElementId { get; set; }
        public int ParentMenuElementId { get; set; }
        public string Display { get; set; }
        public string Address { get; set; }
        public string Orden { get; set; }
        public string Template { get; set; }
        public int Nivel { get; set; }
    }
}
