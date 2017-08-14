using Golf.Data.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Golf.Data.EF
{
    public class GolfDataContext : DbContext
    {
        public GolfDataContext() : base("GolfConnection") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Round> Rounds { get; set; }
    }
}
