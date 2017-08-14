using FakeItEasy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Golf.Data.Entities;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Golf.Data.UnitTest.RoundRepository
{
    [TestFixture]
    public class when_reading_rounds : RepositoryBase
    {
        [SetUp]
        public void Setup()
        {
            var fakeSet = new List<Round>
            {
                new Round() { Id = 1, TeeTime = DateTime.Now.AddDays(-10) }
            };
            //var set = A.Fake<DbSet<Round>>(o => o.Implements(typeof(IQueryable<Round>)).Implements(typeof(IDbAsyncEnumerable<Round>)))
            //            .SetupData(fakeSet);
        }

        [Test]
        public void given_round_exists_return_round()
        {
            //Arrange
            //A.CallTo(() =>  _fakeDb.Rounds)


            //Act

            //Assert
        }
    }
}
