using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels
{
    public interface IBibliaModel
    {
        List<TbBiblia> GetRowByDay(int companyId, int dia);
    }
}
