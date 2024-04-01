using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace v4posme_library.Libraries.CustomLibraries.Interfaz
{
    public interface ICoreWebTransactionMasterDetail
    {
        decimal GetCostCustomer(int companyId, string itemId, decimal unitaryCost, decimal unitaryPrice);
        public decimal GetPercentageCommission(int companyId, int listPriceId, string itemId, decimal price);
    }
}
