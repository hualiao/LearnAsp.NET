using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Linq
{
    /// <summary>
    /// Ref: https://jasonneylon.wordpress.com/2010/02/23/refactoring-to-linq-part-1-death-to-the-foreach/
    /// </summary>
    class Foreach
    {
        public static void ForeachTest()
        {
            Whiskey ardbeg = new Whiskey { Name = "Ardbeg 1998", Age = 12, Price = 49.95m, Country = "Scotland" };
            Whiskey glenmorangie = new Whiskey { Name = "Glenmorangie", Age = 10, Price = 28.95m, Country = "Scotland" };
            Whiskey talisker = new Whiskey { Name = "Talisker", Age = 18, Price = 57.95m, Country = "Scotland" };
            Whiskey cragganmore = new Whiskey { Name = "Cragganmore", Age = 12, Price = 30.95m, Country = "Scotland" };
            Whiskey redbreast = new Whiskey { Name = "Redbreast", Age = 12, Price = 27.95m, Country = "Ireland" };
            Whiskey greenspot = new Whiskey { Name = "Green spot", Age = 8, Price = 44.48m, Country = "Ireland" };

            List<Whiskey> whiskies = new List<Whiskey> { ardbeg, glenmorangie, talisker, cragganmore, redbreast, greenspot };

            // Create one list of objects from another
            var whiskeyNames = whiskies.Select(x => x.Name).ToList();
            Console.WriteLine("Whiskey names: {0}", String.Join(", ", whiskeyNames));

            // Filtering a list
            var goodValueWhiskies = whiskies.Where(x => x.Price <= 30m).ToList();

            // Counting the number of items matching a condition in a list
            var howMany12YearOldWhiskies = whiskies.Where(x => x.Age == 12).Count();

            howMany12YearOldWhiskies = whiskies.Count(x => x.Age == 12);

            // Checking if some or all of the items in a list match a criteria
            var allAreScottish = whiskies.All(x => x.Country == "Scotland");
            var isThereIrishWhiskey = whiskies.Any(x => x.Country == "Ireland");

            // splitting up complex foreach statements
            var scottishWhiskiesCount = 0;
            var scottishWhiskiesTotal = 0m;
            var scottishWhiskies = whiskies.Where(x => x.Country == "Scotland");
            scottishWhiskiesCount = scottishWhiskies.Count();
            scottishWhiskiesTotal = scottishWhiskies.Sum(x => x.Price);
        }
    }
}
