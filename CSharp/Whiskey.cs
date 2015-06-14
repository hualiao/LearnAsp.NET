using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp
{
    public class Whiskey
    {
        public Whiskey()
        {
            Ingredients = new List<Whiskey>();
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public decimal Price { get; set; }
        public string Country { get; set; }

        public List<Whiskey> Ingredients { get; set; }

        public string IngredientsAsStrings
        {
            get
            {
                return String.Join(",", Ingredients.Select(x => x.Name).ToArray());
            }
        }
    }
}
