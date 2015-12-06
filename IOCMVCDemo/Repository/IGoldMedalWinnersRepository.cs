using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IOCMVCDemo.Models;

namespace IOCMVCDemo.Repository
{
    public interface IGoldMedalWinnersRepository
    {
        GoldMedalWinner GetById(int id);
        IEnumerable<GoldMedalWinner> GetAll();
        void Add(GoldMedalWinner goldMedalWinner);
    }
}
