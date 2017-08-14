using Golf.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Golf.Models;
using Golf.Data.EF;
using Golf.Data.Entities;

namespace Golf.Data.Repository
{
    public class RoundRepository : IRoundRepository
    {
        private GolfDataContext _db;
        public RoundRepository(GolfDataContext db)
        {
            _db = db;
        }

        public RoundModel CreateOrUpdateRound(RoundModel model)
        {
            Round round;
            if (model.Id == 0)
            {
                round = new Round();
                _db.Rounds.Add(round);
            }
            else
            {
                round = _db.Rounds.FirstOrDefault(x => x.Id == model.Id);
                if (round == null) throw new ObjectNotFoundException($"Round with Id: {model.Id} does not exist");
            }

            round.TeeTime = model.TeeTime;
            _db.SaveChanges();

            return new RoundModel()
            {
                Id = round.Id,
                TeeTime = round.TeeTime
            };
        }

        public void DeleteRound(int id)
        {
            var round = _db.Rounds.FirstOrDefault(x => x.Id == id);
            if (round == null) throw new ObjectNotFoundException($"Round with Id: {id} does not exist");
            _db.Rounds.Remove(round);
            _db.SaveChanges();
        }

        public RoundModel GetRound(int id)
        {
            var round = _db.Rounds.FirstOrDefault(x => x.Id == id);
            if (round == null) return null;
            var model = new RoundModel()
            {
                Id = round.Id,
                TeeTime = round.TeeTime
            };
            return model;
        }
    }
}
