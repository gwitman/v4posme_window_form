using System.Text.RegularExpressions;
using MySqlConnector;
using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomHelper;
using v4posme_library.Libraries.CustomLibraries.Interfaz;
using v4posme_library.Libraries.CustomModels;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace TestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CustomerModel0()
        {
            var customerModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerModel>();
            var x = customerModel.InsertAppPosme(new TbCustomer
            {
                IsActive = false,
                CompanyId = 2,
                Address = "Test Address",
                BirthDate = DateTime.Now,
                Identification = "001"
            });
            Assert.Pass("Valor de x: " + x);
        }

        [Test]
        public void CustomerModel1()
        {
            var customerModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerModel>();
            var model = customerModel.GetRowByCode(2, "CLI00000366");
            model.Reference5 = "Prueba unitaria";
            customerModel.UpdateAppPosme(2, 2, 1, model);
            Assert.Pass("Actualización realizada con exito " + model);
        }

        [Test]
        public void CustomerModel2()
        {
            var customerModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerModel>();
            var lista = customerModel.GetRowByCompany(2);
            Console.WriteLine(@"Count {0}", lista.Count);
            foreach (var customer in lista)
            {
                Console.WriteLine(customer.Identification);
            }
        }

        [Test]
        public void EmployeeModel()
        {
            var employeeModel = VariablesGlobales.Instance.UnityContainer.Resolve<IEmployeeModel>();
            var data = new TbEmployee
            {
                CompanyId = 2,
                BranchId = 2,
                EntityId = 664,
                EmployeNumber = "EMP00000012",
                NumberIdentification = "0013107870024J",
                IdentificationTypeId = 85,
                Address = "Urbanizacion San Miguel",
                CountryId = 42,
                StateId = 205,
                CityId = 227,
                DepartamentId = 121,
                AreaId = 123,
                ClasificationId = 126,
                CategoryId = 128,
                TypeEmployeeId = 130,
                HourCost = 100,
                StartOn = DateOnly.FromDateTime(DateTime.Now),
                CreatedOn = DateTime.Now,
                IsActive = 1
            };
            var add = employeeModel.InsertAppPosme(data);
            Console.WriteLine(@"Id del empleado ingresado: {0}", add);
        }

        [Test]
        public void EmployeeCalendarPayDetailModel()
        {
            var employeeCalendarPayDetailModel =
                VariablesGlobales.Instance.UnityContainer.Resolve<IEmployeeCalendarPayDetailModel>();
            int[] array1 = [2, 3, 4, 5, 6];
            employeeCalendarPayDetailModel.DeleteWhereIdNotIn(1, [..array1]);
            Console.WriteLine("Ejecutado con exito");
        }

        [Test]
        public void EntityEmailModel()
        {
            var entityEmailModel = VariablesGlobales.Instance.UnityContainer.Resolve<IEntityEmailModel>();
            var delete = entityEmailModel.DeleteAppPosme(2, 2, 11, 36);
            Console.WriteLine(@"Canitdad de filas afectadas: {0}", delete);
        }

        [Test]
        public void NotificationModelTest()
        {
            var notificationModel = VariablesGlobales.Instance.UnityContainer.Resolve<INotificationModel>();
            var data = notificationModel.GetRowByPk(5);
            data.Message = "Nuevo mensaje";
            notificationModel.UpdateAppPosmeBySumary("C000", data);
            Console.WriteLine(@"Se han actulizado los datos de forma correcta");
        }

        [Test]
        public void RememberModelTest()
        {
            var rememberModel = VariablesGlobales.Instance.UnityContainer.Resolve<IRememberModel>();
            var fecha = DateTime.Now;
            var processNotification = rememberModel.GetProcessNotification(1, fecha);
            Console.WriteLine(@"Registro {0}", processNotification.Description);
        }

        [Test]
        public void CoreWebCatalogTest()
        {
            var coreWebCatalog = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebCatalog>();
            var catalogAllItem = coreWebCatalog.GetCatalogAllItem("tb_customer", "sexoID", 2);
            foreach (var catalogItem in catalogAllItem)
            {
                Console.WriteLine($@"Dato recuperado de la consulta: {catalogItem.Name}");
            }

            Console.WriteLine($@"Tamaño de la lista o array: {catalogAllItem.Count}");
        }

        [Test]
        public void CoreWebWorkflowTest()
        {
            var coreWebWorkflow = VariablesGlobales.Instance.UnityContainer.Resolve<ICoreWebWorkflow>();
            var coreWebWorkflowData =
                coreWebWorkflow.GetWorkflowAllStage("tb_transaction_master_billing", "statusID", 2, 2, 201);

            if (coreWebWorkflowData is not null)
            {
                Console.WriteLine($@"Tamaño de la lista 1: {coreWebWorkflowData.Count}");
            }
        }

        [Test]
        public void ProbarDictionary()
        {
            var lista = new List<TableAmortizationDetailDto>();
            var simpleDto = new TableAmortizationDto(0, 0, 0, 0,lista);
            //simpleDto.ListDetailDto = lista;
            if (simpleDto.ListDetailDto is null)
            {
                Console.WriteLine(@"La lista es null");
            }

            lista.Add(new TableAmortizationDetailDto(0,DateTime.Now, 0,0,0,0,0,0));
            lista.Add(new TableAmortizationDetailDto(0,DateTime.Now, 0,0,0,0,0,0));
            lista.Add(new TableAmortizationDetailDto(0,DateTime.Now, 0,0,0,0,0,0));
            lista.Add(new TableAmortizationDetailDto(0,DateTime.Now, 0,0,0,0,0,0));
            lista.Add(new TableAmortizationDetailDto(0,DateTime.Now, 0,0,0,0,0,0));
            lista.Add(new TableAmortizationDetailDto(0,DateTime.Now, 0,0,0,0,0,0));
            lista.Add(new TableAmortizationDetailDto(0,DateTime.Now, 0,0,0,0,0,0));
            lista.Add(new TableAmortizationDetailDto(0,DateTime.Now, 0,0,0,0,0,0));
            lista.Add(new TableAmortizationDetailDto(0,DateTime.Now, 0,0,0,0,0,0));
            Console.WriteLine($@"Tamaño de la list {simpleDto.ListDetailDto!.Count}");
        }

        [Test]
        public void TestWebToolsHelper()
        {
            var helper = new WebToolsHelper();
            Console.WriteLine(helper.HelperGetDate());
        }

        /*[Test]
        public void TestWebToolsCustomizationViewHelper()
        {
            var webTools = new WebToolsCustomizationViewHelper();
            foreach (var keyValuePair in webTools.ReadXmlFile())
            {
                Console.WriteLine($@"{keyValuePair.Key} {keyValuePair.Value}");
            }
        }*/

        [Test]
        public void BdModelTest()
        {
            var input = "CALL pr_transaction_revert (@companyIdOriginal,@transactionIdOriginal,@transactionMasterIdOriginal,@transactionIdRevert,@transactionMasterIdRevert)";
            var bdModel = VariablesGlobales.Instance.UnityContainer.Resolve<IBdModel>();
            bdModel.ExecuteRenderWidthParameter(input, [2,0]);
        }
    }
}