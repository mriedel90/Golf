using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Golf.Data.IntegrationTest;
using Golf.Data.Repository;
using Golf.Models;
using NUnit.Framework;

namespace Golf.Data.IntegrationTests.RepositoryTests
{
    [TestFixture]
    public class when_using_round_repository : RepositoryBase
    {
        private RoundRepository _repo;
        private List<int> _idsToDelete;

        [OneTimeSetUp]
        public void Setup()
        {
            _repo = new RoundRepository(_context);
            _idsToDelete = new List<int>();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            foreach (var id in _idsToDelete)
            {
                try
                {
                    _repo.DeleteRound(id);
                }
                catch { }
            }
        }


        [Test]
        public void given_valid_data_then_round_created()
        {
            //Arrange
            var model = new RoundModel()
            {
                TeeTime = DateTime.Now.AddDays(-10)
            };

            //Act
            var result = _repo.CreateOrUpdateRound(model);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RoundModel>(result);
            Assert.IsTrue(result.Id > 0);
            _idsToDelete.Add(result.Id);
            Assert.AreEqual(model.TeeTime, result.TeeTime);
        }

        [Test]
        public void given_valid_id_then_round_read()
        {
            //Arrange
            var model = new RoundModel()
            {
                TeeTime = DateTime.Now.AddDays(-10)
            };
            model = _repo.CreateOrUpdateRound(model);
            _idsToDelete.Add(model.Id);

            //Act
            var result = _repo.GetRound(model.Id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RoundModel>(result);
            Assert.AreEqual(model.Id, result.Id);
            Assert.AreEqual(model.TeeTime, result.TeeTime);
        }
        
        [Test]
        public void given_valid_data_then_round_updated()
        {
            //Arrange
            var model = new RoundModel()
            {
                TeeTime = DateTime.Now.AddDays(-10)
            };
            model = _repo.CreateOrUpdateRound(model);
            _idsToDelete.Add(model.Id);
            model.TeeTime = DateTime.Now.AddDays(-5);

            //Act
            var result = _repo.CreateOrUpdateRound(model);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RoundModel>(result);
            Assert.AreEqual(model.Id, result.Id);
            Assert.AreEqual(model.TeeTime, result.TeeTime);
        }

        [Test]
        public void given_valid_id_then_round_deleted()
        {
            //Arrange
            var model = new RoundModel()
            {
                TeeTime = DateTime.Now.AddDays(-10)
            };
            model = _repo.CreateOrUpdateRound(model);

            //Act
            _repo.DeleteRound(model.Id);

            //Assert
            var round = _repo.GetRound(model.Id);
            Assert.IsNull(round);
        }
    }
}
