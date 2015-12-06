using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using IOCMVCDemo.Models;

namespace IOCMVCDemo.Models
{
    public class GoldMedalWinnersModel
    {
        private IEnumerable<GoldMedalWinner> _winners;
        public IEnumerable<GoldMedalWinner> Winners
        {
            get
            {
                if (_winners == null)
                {
                    _winners = new List<GoldMedalWinner>();
                }
                return _winners;
            }
            set { _winners = value; }
        }
    }
}