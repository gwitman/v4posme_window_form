using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels
{
    public interface ICompanyComponentConceptModel
    {
        int InsertAppPosme(TbCompanyComponentConcept? data);

        void UpdateAppPosme(int companyId, int componentId, int componentItemId, string name,
            TbCompanyComponentConcept data);

        void DeleteWhereComponentItemId(int companyId,int componentId,int componentItemId);

        TbCompanyComponentConcept? GetRowByPk(int companyId, int componentId, int componentItemId, String name);

        List<TbCompanyComponentConcept?> GetRowByComponentItemId(int companyId, int componentId, int componentItemId);

        List<TbCompanyComponentConcept> GetRowByTransactionMasterId(int companyId, int componentId, int transactionMasterId);
    }
}
