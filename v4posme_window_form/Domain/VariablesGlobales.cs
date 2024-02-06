using System.Configuration;
using Unity;
using v4posme_window_form.Domain.Services;
using v4posme_window_form.Models;
using v4posme_window_form.Models.Tablas;

namespace v4posme_window_form.Domain
{
    public class VariablesGlobales
    {
        private readonly static VariablesGlobales _instance = new VariablesGlobales();

        private readonly IUnityContainer _unityContainer;

        private readonly static string _connectionString= ConfigurationManager.ConnectionStrings["posme.netdbkroqnguhldo1"].ConnectionString;
        private VariablesGlobales()
        {
            _unityContainer = new UnityContainer();
            _unityContainer.RegisterType<ICoreWebAuthentication,CoreWebAuthentication>();
            _unityContainer.RegisterType<IBranchService,BranchService>();
            _unityContainer.RegisterType<ICompanyService,CompanyService>();
            _unityContainer.RegisterType<IMembershipService,MembershipService>();
            _unityContainer.RegisterType<IRoleService,RoleService>();
            _unityContainer.RegisterType<IElementSevice,ElementService>();
            _unityContainer.RegisterType<ICoreMenu,CoreMenuService>();
            _unityContainer.RegisterType<IUserPermissionService,UserPermissionService>();
        }

        public static VariablesGlobales Instance
        {
            get
            {
                return _instance;
            }
        }

        public static string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }

        public IUnityContainer UnityContainer
        {
            get
            {
                return _unityContainer;
            }
        }
        public User User { get; set; }

        public Company Company { get; set; }

        public Branch Branch { get; set; }

        public Membership Membership { get; set; }

        public Role Role { get; set; }
    }
}
