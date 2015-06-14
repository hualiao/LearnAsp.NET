using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Linq
{
    public class Aggregate
    {
        public static void AggregateTest()
        {
            // with seed
            var whiskeyNames = new[] { "Ardbeg 1998", "Glenmorangie", "Talisker", "Cragganmore" };
            var listOfWhiskies = whiskeyNames.Aggregate("Whiskies: ", (accumulated, next) =>
            {
                Console.Out.WriteLine("(Adding [{0}] to the list [{1}])", next, accumulated);
                return accumulated + next;
            });
            Console.Out.WriteLine(listOfWhiskies);
            // without seed
            listOfWhiskies = whiskeyNames.Aggregate((accumulated, next) =>
            {
                Console.Out.WriteLine("(Adding [{0}] to the list [{1}])", next, accumulated);
                return accumulated + next;
            });
            Console.Out.WriteLine(listOfWhiskies);

            Whiskey ardbeg = new Whiskey { Name = "Ardbeg 1998", Age = 12, Price = 49.95m, Country = "Scotland" };
            Whiskey glenmorangie = new Whiskey { Name = "Glenmorangie", Age = 10, Price = 28.95m, Country = "Scotland" };
            Whiskey talisker = new Whiskey { Name = "Talisker", Age = 18, Price = 57.95m, Country = "Scotland" };
            Whiskey cragganmore = new Whiskey { Name = "Cragganmore", Age = 12, Price = 30.95m, Country = "Scotland" };
            Whiskey redbreast = new Whiskey { Name = "Redbreast", Age = 12, Price = 27.95m, Country = "Ireland" };
            Whiskey greenspot = new Whiskey { Name = "Green spot", Age = 8, Price = 44.48m, Country = "Ireland" };

            List<Whiskey> whiskies = new List<Whiskey> { ardbeg, glenmorangie, talisker, cragganmore, redbreast, greenspot };
            // Find the best item by foreach
            Whiskey mostExpensiveWhiskey = null;
            foreach (var chanllenger in whiskies)
            {
                if (mostExpensiveWhiskey == null)
                {
                    mostExpensiveWhiskey = chanllenger;
                }
                if (chanllenger.Price > mostExpensiveWhiskey.Price)
                {
                    mostExpensiveWhiskey = chanllenger;
                }
            }
            Console.WriteLine("Most expensive is {0}", mostExpensiveWhiskey.Name);
            // Find the best item by Aggregate
            mostExpensiveWhiskey = whiskies.Aggregate((champion, challenger) =>
                challenger.Price > champion.Price ? challenger : champion
            );
            Console.WriteLine("Most expensive is {0}", mostExpensiveWhiskey.Name);
            // Create a new 'aggregated' object
            var blendedWhiskey = new Whiskey { Name = "Tesco value whiskey", Age = 3, Country = "Scotland" };
            foreach (var whiskey in whiskies)
            {
                if (whiskey.Country != "Scotland")
                    continue;
                blendedWhiskey.Ingredients.Add(whiskey);
                blendedWhiskey.Price = blendedWhiskey.Price + (whiskey.Price / 10);
            }
            Console.WriteLine("Blended Whiskey Name: {0}", blendedWhiskey.Name);
            Console.WriteLine("Blended Whiskey Price: {0}", blendedWhiskey.Price);
            Console.WriteLine("Blended Whiskey Ingredients: {0}", blendedWhiskey.IngredientsAsStrings);
            // refactor with Where and Aggregate
            blendedWhiskey = whiskies.Where(x => x.Country == "Scotland")
                .Aggregate(new Whiskey() { Name = "Tesco value whiskey", Age = 3, Country = "Scotland" },
                (newWhiskey, nextWhiskey) =>
                {
                    newWhiskey.Ingredients.Add(nextWhiskey);
                    newWhiskey.Price += (nextWhiskey.Price / 10);
                    return newWhiskey;
                });
            Console.WriteLine("Blended Whiskey Name: {0}", blendedWhiskey.Name);
            Console.WriteLine("Blended Whiskey Price: {0}", blendedWhiskey.Price);
            Console.WriteLine("Blended Whiskey Ingredients: {0}", blendedWhiskey.IngredientsAsStrings);
        }
    }
}
