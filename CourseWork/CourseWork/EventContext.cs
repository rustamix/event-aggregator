using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CourseWork
{
    public partial class EventContext : DbContext
    {
        public virtual DbSet<Event> Events { get; set; }
    }
}
