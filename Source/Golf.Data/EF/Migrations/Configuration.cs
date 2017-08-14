using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Golf.Data.EF.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<GolfDataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"EF\Migrations";
            MigrationsNamespace = typeof(Configuration).Namespace;
        }
    }
}
