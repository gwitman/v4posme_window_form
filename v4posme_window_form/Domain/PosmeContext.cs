using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using v4posme_window_form.Models;

namespace v4posme_window_form.Domain
{
    // Code-Based Configuration and Dependency resolution
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class PosmeContext : DbContext
    {

        public PosmeContext(DbConnection existingConnection, bool contextOwnsConnection) : base(existingConnection, contextOwnsConnection)
        {
            
        }

        public PosmeContext() { 
            
        }
                
    }
}
