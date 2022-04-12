using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Paup2022_Vjezba.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class BazaDbContext: DbContext
    {
        public DbSet<Student> PopisStudenata { get; set; }
    }
}