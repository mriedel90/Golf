﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Golf.Data.EF;
using FakeItEasy;

namespace Golf.Data.UnitTest
{
    public class RepositoryBase
    {
        public GolfDataContext _fakeContext;

        [SetUp]
        public void BaseSetup()
        {
            _fakeContext = A.Fake<GolfDataContext>();
        }

    }
}
