using NUnit.Framework.Constraints;
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
            ICustomerModel customerModel = new CustomerModel();
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
            ICustomerModel customerModel = new CustomerModel();
            var model = customerModel.GetRowByCode(2, "CLI00000366");
            model.Reference5 = "Prueba unitaria";
            customerModel.UpdateAppPosme(2, 2, 1, model);
            Assert.Pass("Actualización realizada con exito " + model);
        }
    }
}