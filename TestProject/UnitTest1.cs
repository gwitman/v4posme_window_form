using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomModels;
using v4posme_library.Models;

namespace TestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
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
        public void Test2()
        {
            var customerModel = VariablesGlobales.Instance.UnityContainer.Resolve<ICustomerModel>();
            var model = customerModel.GetRowByCode(2, "CLI00000366");
            model.Reference5 = "Prueba unitaria";
            customerModel.UpdateAppPosme(2, 2, 1, model);
            Assert.Pass("Actualización realizada con exito " + model);
        }

        [Test]
        public void Test3()
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
        public void Test4()
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
                IsActive = true
            };
            var add = employeeModel.InsertAppPosme(data);
            Console.WriteLine(@"Id del empleado ingresado: {0}", add);
        }

        [Test]
        public void Test5()
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
            notificationModel.UpdateAppPosmeBySumary("C000",data);
            Console.WriteLine(@"Se han actulizado los datos de forma correcta");
        }
    }
}