using FakeItEasy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Golf.Data.Entities;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Data.Entity.Infrastructure;
using Golf.Data.EF;
using Golf.Data.Repository;
using Golf.Data.Repository.Interfaces;
using Golf.Models;

namespace Golf.Data.UnitTest.RoundRepositoryTests
{
    [TestFixture]
    public class when_creating_or_updating_rounds : RepositoryBase
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
        public void given_new_round_then_round_created()
        {
            //Arrange
            var fakeRoundModel = new RoundModel()
            {
                Id = 0,
                TeeTime = DateTime.Now
            };

            //Act
            var result = _repo.CreateOrUpdateRound(fakeRoundModel);

            //Assert
            A.CallTo(() => _fakeDb.Rounds.Add(A<Round>.Ignored)).MustHaveHappened();
            A.CallTo(() => _fakeDb.SaveChanges()).MustHaveHappened();
            Assert.AreEqual(2, _fakeDb.Rounds.Count());
            Assert.IsNotNull(result);
            Assert.AreEqual(fakeRoundModel.TeeTime, result.TeeTime);
        }

        [Test]
        public void given_round_does_not_exist_then_exception_thrown()
        {
            //Arrange
            var fakeRoundModel = new RoundModel()
            {
                Id = 2,
                TeeTime = DateTime.Now
            };

            //Act
            TestDelegate createOrUpdateRoundTestDelegate = () => _repo.CreateOrUpdateRound(fakeRoundModel);
            
            //Assert
            Assert.That(createOrUpdateRoundTestDelegate, Throws.TypeOf<ObjectNotFoundException>().With.Message.EqualTo("Round with Id: 2 does not exist"));
            A.CallTo(() => _fakeDb.Rounds.Add(A<Round>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _fakeDb.SaveChanges()).MustNotHaveHappened();
            Assert.AreEqual(1, _fakeDb.Rounds.Count());
        }

        [Test]
        public void given_round_does_exist_then_round_updated()
        {
            //Arrange
            var fakeRoundModel = new RoundModel()
            {
                Id = 1,
                TeeTime = DateTime.Now
            };

            //Act
            var result = _repo.CreateOrUpdateRound(fakeRoundModel);

            //Assert
            A.CallTo(() => _fakeDb.Rounds.Add(A<Round>.Ignored)).MustNotHaveHappened();
            A.CallTo(() => _fakeDb.SaveChanges()).MustHaveHappened();
            Assert.IsNotNull(result);
            Assert.AreEqual(fakeRoundModel.TeeTime, result.TeeTime);
            Assert.AreEqual(1, _fakeDb.Rounds.Count());
        }
    }
}
