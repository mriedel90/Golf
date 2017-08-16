using FakeItEasy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Golf.Data.Entities;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using System.Data.Entity.Infrastructure;
using Golf.Data.EF;
using Golf.Data.Repository;
using Golf.Data.Repository.Interfaces;

namespace Golf.Data.UnitTest.RoundRepositoryTests
{
    [TestFixture]
    public class when_deleting_rounds : RepositoryBase
    {
        private RoundRepository _repo;

        [SetUp]
        public void Setup()
        {
            _repo = new RoundRepository(_fakeContext);
            A.CallTo(() => _fakeContext.Rounds).Returns(A.Fake<DbSet<Round>>(o => o.Implements(typeof(IQueryable<Round>)).Implements(typeof(IDbAsyncEnumerable<Round>)))
                .SetupData(new List<Round>
                {
                    new Round() { Id = 1, TeeTime = DateTime.Now.AddDays(-10) }
                }));
        }

        [Test]
        public void given_round_exists_then_round_deleted()
        {
            //Arrange

            //Act
            _repo.DeleteRound(1);

            //Assert
            A.CallTo(() => _fakeContext.Rounds.Remove(A<Round>.Ignored)).MustHaveHappened();
            A.CallTo(() => _fakeContext.SaveChanges()).MustHaveHappened();
            Assert.AreEqual(0, _fakeContext.Rounds.Count());
        }

        [Test]
        public void given_round_does_not_exist_then_throw_exception()
        {
            //Arrange

            //Act
            TestDelegate createOrUpdateRoundTestDelegate = () => _repo.DeleteRound(2);

            //Assert
            Assert.That(createOrUpdateRoundTestDelegate, Throws.TypeOf<ObjectNotFoundException>().With.Message.EqualTo("Round with Id: 2 does not exist"));
            A.CallTo(() => _fakeContext.Rounds.Add(A<Round>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _fakeContext.SaveChanges()).MustNotHaveHappened();
            Assert.AreEqual(1, _fakeContext.Rounds.Count());
        }
    }
}
