using DevExpress.Xpo;
namespace v4posme_window_form.Models
{
    [NonPersistent]
    public class MenuElementView : XPLiteObject 
    {
        //x.companyID,x.elementID,x.menuElementID,x.parentMenuElementID,x.display,x.address,x.orden,x.icon,x.template,x.nivel
        public int CompanyId
        {
            get { return companyID; }
            set{SetPropertyValue(nameof(CompanyId), ref companyID, value);}
        }
        private int companyID;
        public int ElementId
        {
            get { return elementID; }
            set{SetPropertyValue(nameof(ElementId), ref elementID, value);}
        }
        private int elementID;

        public int MenuElementId
        {
            get { return menuElementID; }
            set{SetPropertyValue(nameof(MenuElementId), ref menuElementID, value);}
        }
        private int menuElementID;

        public int ParentMenuElementId
        {
            get { return parentMenuElementID; }
            set{SetPropertyValue(nameof(ParentMenuElementId), ref parentMenuElementID, value);}
        }
        private int parentMenuElementID;

        public string Display
        {
            get { return display; }
            set{SetPropertyValue(nameof(Display), ref display, value);}
        }
        private string display;

        public string Address
        {
            get { return address; }
            set{SetPropertyValue(nameof(Address), ref address, value);}
        }
        private string address;

        public string Orden
        {
            get { return orden; }
            set{SetPropertyValue(nameof(Orden), ref orden, value);}
        }
        private string orden;

        public string Icon
        {
            get { return icon; }
            set{SetPropertyValue(nameof(Icon), ref icon, value);}
        }
        private string icon;

        public string Template
        {
            get { return template; }
            set{SetPropertyValue(nameof(Template), ref template, value);}
        }
        private string template;

        public int Nivel
        {
            get { return nivel; }
            set{SetPropertyValue(nameof(Nivel), ref nivel, value);}
        }
        private int nivel;
        public int TypeMenuElementID
        {
            get { return typeMenuElementID; }
            set{SetPropertyValue(nameof(TypeMenuElementID), ref typeMenuElementID, value);}
        }
        private int typeMenuElementID;
        public byte IsActive
        {
            get { return isActive; }
            set{SetPropertyValue(nameof(IsActive), ref isActive, value);}
        }
        private byte isActive;
    }
}
