using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Golf.Data.EF;

namespace Golf.Data.IntegrationTest
{
    public class RepositoryBase
    {
        public GolfDataContext _context;

        [OneTimeSetUp]
        public void BaseSetup()
        {
            _context = new GolfDataContext();
        }

        [OneTimeTearDown]
        public void BaseTearDown()
        {
            _context.Dispose();
        }

    }
}
