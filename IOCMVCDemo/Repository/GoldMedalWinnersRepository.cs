using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IOCMVCDemo.Models;

namespace IOCMVCDemo.Repository
{
    public class GoldMedalWinnersRepository : IGoldMedalWinnersRepository
    {
        private readonly IGoldMedalWinnersContext _goldContext;

        public GoldMedalWinnersRepository(IGoldMedalWinnersContext goldContext)
        {
            _goldContext = goldContext;
        }

        #region IGoldMedalWinnersRepository Members

        public GoldMedalWinner GetById(int id)
        {
            return null;
            //return _goldContext.GoldMedalWinners.Find(id);
        }

        public IEnumerable<GoldMedalWinner> GetAll()
        {
            return _goldContext.GoldMedalWinners.ToList();
        }

        public void Add(GoldMedalWinner goldMedalWinner)
        {
            _goldContext.GoldMedalWinners.Add(goldMedalWinner);
            _goldContext.SaveChanges();
        }

        #endregion
    }
}