using Golf.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Golf.Data.Repository.Interfaces
{
    public interface IRoundRepository
    {
        int CreateRound(RoundModel model);
        RoundModel GetRound(int id);
        void UpdateRound(RoundModel model);
        void DeleteRound(int id);
    }
}
