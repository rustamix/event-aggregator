using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Text;

namespace CourseWork
{
    public partial class UserContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
    }
}
