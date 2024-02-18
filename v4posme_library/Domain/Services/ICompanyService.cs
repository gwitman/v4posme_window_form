using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using v4posme_library.ModelsCode;

namespace v4posme_library.Domain.Services
{
    public interface ICompanyService
    {
        Company GetRowByPk(int id);
    }
}
