using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace v4posme_library.ModelsCode
{

    public partial class CustomerCreditDocument
    {
        public CustomerCreditDocument(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
