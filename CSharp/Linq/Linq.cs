using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CSharp;

namespace CSharp.Linq
{
    class Linq
    {

        private static IList<Person> customers = new List<Person> { 
            new Person { FirstName = "Liao", LastName = "Hua" }, 
            new Person { FirstName = "FirstName", LastName = "LastName" } 
        };


        /// <summary>
        /// OrderBy overwrites any previous OrderBy clauses; ThenBy does not.
        /// Ref: https://social.msdn.microsoft.com/Forums/en-US/baf47c50-24c2-4459-97fe-3912b979d32f/orderby-and-thenby?forum=linqprojectgeneral
        /// </summary>
        public static IEnumerable<Person> OrderTest()
        {
            // first orders by LastName, then by FirstName (without upsetting the LastName ordering). 
            //return customers
            //    .OrderBy(p => p.FirstName)
            //    .ThenBy(p => p.LastName);
            // first sorts customers by LastName, then re-sorts them by FirstName
            return customers
                .OrderBy(p => p.FirstName)
                .OrderBy(p => p.LastName);
        }
    }
}
