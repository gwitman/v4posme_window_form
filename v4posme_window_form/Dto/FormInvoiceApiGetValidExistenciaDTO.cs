using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using v4posme_library.ModelsDto;

namespace v4posme_window.Dto
{
    public class FormInvoiceApiGetValidExistenciaDTO
    {
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Mensaje { get; set; }
        public decimal? QuantityInWarehouse { get; set; }

    }
}
