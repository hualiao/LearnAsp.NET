using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using IOCMVCDemo.Models;

namespace IOCMVCDemo.Repository
{
    public interface IGoldMedalWinnersContext
    {
        IList<GoldMedalWinner> GoldMedalWinners { get; set; }
        IList<Country> Countries { get; set; }
        int SaveChanges();
    }
}
