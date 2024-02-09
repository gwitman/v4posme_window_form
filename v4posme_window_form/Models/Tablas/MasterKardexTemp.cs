using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace v4posme_window_form.Models.Tablas
{

    public partial class MasterKardexTemp
    {
        public MasterKardexTemp(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
