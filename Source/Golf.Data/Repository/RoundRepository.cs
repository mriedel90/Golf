using Golf.Data.Repository.Interfaces;
using System;
using System.Collections.Generic;
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

        public int CreateRound(RoundModel model)
        {
            var round = new Round()
            {
                TeeTime = model.TeeTime
            };

            round = _db.Rounds.Add(round);
            _db.SaveChanges();

            return round.Id;
        }

        public void DeleteRound(int id)
        {
            throw new NotImplementedException();
        }

        public RoundModel GetRound(int id)
        {
            throw new NotImplementedException();
        }

        public void UpdateRound(RoundModel model)
        {
            throw new NotImplementedException();
        }
    }
}
