using System.Globalization;
using Unity;
using v4posme_library.Libraries;
using v4posme_library.Libraries.CustomHelper;
using v4posme_library.Libraries.CustomModels.Core;

namespace TestProject
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void General()
        {
            var bankModel = VariablesGlobales.Instance.UnityContainer.Resolve<IBankModel>();
            Console.WriteLine(bankModel.GetByCompany(2).Count);
        }
    }
}