using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using v4posme_library.Models;
using v4posme_library.ModelsDto;

namespace v4posme_window.Dto
{
    public class FormInvoiceApiGetLineByCustomerDTO
    {
        public TbCustomerDto? objCustomer { get; set;}
        public List<TbCustomerCreditLineDto>? ObjListCustomerCreditLine { get; set; }
        public decimal? ExchangeRate { get; set; }
        public List<TbCustomerCreditAmortizationDto>? ObjCustomerCreditAmoritizationAll { get; set; }
        public TbCurrency? objCurrencyDolares { get; set; }
        public TbCurrency? objCurrencyCordoba { get; set; }
        public TbCompanyParameter? ParameterCausalTypeCredit { get; set; }
    }
}
