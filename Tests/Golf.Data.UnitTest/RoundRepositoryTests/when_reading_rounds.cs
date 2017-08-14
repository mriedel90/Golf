using FakeItEasy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Golf.Data.Entities;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Infrastructure;
using Golf.Data.EF;
using Golf.Data.Repository;
using Golf.Data.Repository.Interfaces;

namespace Golf.Data.UnitTest.RoundRepositoryTests
{
    [TestFixture]
    public class when_reading_rounds : RepositoryBase
    {
        private RoundRepository _repo;

        [SetUp]
        public void Setup()
        {
            _repo = new RoundRepository(_fakeDb);
            A.CallTo(() => _fakeDb.Rounds).Returns(A.Fake<DbSet<Round>>(o => o.Implements(typeof(IQueryable<Round>)).Implements(typeof(IDbAsyncEnumerable<Round>)))
                .SetupData(new List<Round>
                {
                    new Round() { Id = 1, TeeTime = DateTime.Now.AddDays(-10) }
                }));
        }

        [Test]
        public void given_round_exists_return_round()
        {
            //Arrange

            //Act
            var round = _repo.GetRound(1);

            //Assert
            Assert.IsNotNull(round);
        }

        [Test]
        public void given_round_does_not_exist_return_null()
        {
            //Arrange

            //Act
            var round = _repo.GetRound(2);

            //Assert
            Assert.IsNull(round);
        }
    }
}
