using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using IOCMVCDemo.Models;

namespace IOCMVCDemo.Repository
{
    public class GoldMedalWinnersContext : IGoldMedalWinnersContext
    {
        private IList<GoldMedalWinner> _goldMedalWinners;
        public IList<GoldMedalWinner> GoldMedalWinners
        {
            get {
                if (_goldMedalWinners == null){
                    _goldMedalWinners = new List<GoldMedalWinner>();
                    _goldMedalWinners.Add(new GoldMedalWinner());
                }
                return _goldMedalWinners;
            }
            set { _goldMedalWinners = value; }
        }
        public IList<Country> Countries { get; set; }
        public int SaveChanges() { return 1; }
    }
}