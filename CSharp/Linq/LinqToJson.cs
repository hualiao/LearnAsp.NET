using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using CSharp;

namespace CSharp.Linq
{
    /// <summary>
    /// Ref: http://www.newtonsoft.com/json/help/html/LINQtoJSON.htm
    /// </summary>
    public class LinqToJson
    {
        public static void ParseJson()
        {
            string json = @"{
              CPU: 'Intel',
              Drives: [
                'DVD read/writer',
                '500 gigabyte hard drive'
              ]
            }";

            JObject o = JObject.Parse(json);

            Console.WriteLine(o.ToString());

            json = @"[
              'Small',
              'Medium',
              'Large'
            ]";

            JArray a = JArray.Parse(json);
            Console.WriteLine(a.ToString());

            //using (StreamReader reader = File.OpenText(@"c:\person.json"))
            //{
            //    JObject o = (JObject)JToken.ReadFrom(new JsonTextReader(reader));
            //    // do stuff
            //}
        }

        public static void CreateJson()
        {
            // manually creating json
            JArray array = new JArray();
            JValue text = new JValue("Manual text");
            JValue date = new JValue(new DateTime(2000, 5, 23));
            
            array.Add(text);
            array.Add(date);
            string json = array.ToString();
            Console.WriteLine(json);

            // create json with linq
            List<Whiskey> posts = GetWhiskey();

             JObject rss =
                 new JObject(
                     new JProperty("channel",
                         new JObject(
                             new JProperty("title", "James Newton-King"),
                             new JProperty("link", "http://james.newtonking.com"),
                             new JProperty("description", "James Newton-King's blog."),
                             new JProperty("item",
                                 new JArray(
                                     from p in posts
                                     orderby p.Name
                                     select new JObject(
                                         new JProperty("title", p.Name),
                                         new JProperty("description", p.Country),
                                         new JProperty("link", p.Price),
                                         new JProperty("category",
                                             new JArray(
                                                 from c in p.Name
                                                 select new JValue(c)))))))));

             Console.WriteLine(rss.ToString());

            // create json from object
            JObject o = JObject.FromObject(new
            {
                channel = new
                {
                    title = "James Newton-King",
                    link = "http://james.newtonking.com",
                    description = "James Newton-King's blog.",
                    item =
                        from p in posts
                        orderby p.Name
                        select new
                        {
                            title = p.Name,
                            description = p.Country,
                            link = p.Age,
                            category = p.Price
                        }
                }
            });
            Console.WriteLine(o.ToString());
        }

        private static List<Whiskey> GetWhiskey()
        {
            return new List<Whiskey> { 
                new Whiskey{Name="liao",Country="Russia",Age=22,Price=2.32m,Ingredients={new Whiskey{Name="sub",Country="American",Age=30,Price=3222.2m}}},
                new Whiskey{Name="lili",Country="China",Age=30,Price=3222.2m},
                new Whiskey{Name="hua",Country="American",Age=30,Price=3222.2m}            
            };
        }

        public static void QueryJson()
        {
            // Get values by property name or collection index
            string json = @"{
              'channel': {
                'title': 'James Newton-King',
                'link': 'http://james.newtonking.com',
                'description': 'James Newton-King's blog.',
                'item': [
                  {
                    'title': 'Json.NET 1.3 + New license + Now on CodePlex',
                    'description': 'Annoucing the release of Json.NET 1.3, the MIT license and the source on CodePlex',
                    'link': 'http://james.newtonking.com/projects/json-net.aspx',
                    'categories': [
                      'Json.NET',
                      'CodePlex'
                    ]
                  },
                  {
                    'title': 'LINQ to JSON beta',
                    'description': 'Annoucing LINQ to JSON',
                    'link': 'http://james.newtonking.com/projects/json-net.aspx',
                    'categories': [
                      'Json.NET',
                      'LINQ'
                    ]
                  }
                ]
              }
            }";
            JObject rss = JObject.Parse(json);

            string rssTitle = (string)rss["channel"]["title"];
            // James Newton-King

            string itemTitle = (string)rss["channel"]["item"][0]["title"];
            // Json.NET 1.3 + New license + Now on CodePlex

            JArray categories = (JArray)rss["channel"]["item"][0]["categories"];
            // ["Json.NET", "CodePlex"]

            IList<string> categoriesText = categories.Select(c => (string)c).ToList();
            // Json.NET
            // CodePlex

            var postTitles =
                from p in rss["channel"]["item"]
                select (string)p["title"];
            foreach (var item in postTitles)
            {
                Console.WriteLine(item);
            }
            //LINQ to JSON beta
            //Json.NET 1.3 + New license + Now on CodePlex


            var categoriesLinq =
                from c in rss["channel"]["item"].Children()["category"].Values<string>()
                group c by c
                    into g
                    orderby g.Count() descending
                    select new { Category = g.Key, Count = g.Count() };
            foreach (var c in categoriesLinq)
            {
                Console.WriteLine(c.Category + " - Count: " + c.Count);
            }
            // Json.NET - Count: 2
            // LINQ - Count: 1
            // CodePlex - Count: 1


        }

        public static void DeserializeUseLinq()
        {
            string jsonText = @"{
              'short': {
                'original': 'http://www.foo.com/',
                'short': 'krehqk',
                'error': {
                  'code':0,
                  'msg':'No action taken'
                }
            }";
            
            JObject json = JObject.Parse(jsonText);

            Shortie shortie = new Shortie
            {
                Original = (string)json["short"]["original"],
                Short = (string)json["short"]["short"],
                Error = new ShortieException
                {
                    Code = (int)json["short"]["error"]["code"],
                    ErrorMessage = (string)json["short"]["error"]["msg"]
                }
            };

            Console.WriteLine(shortie.Original);
            // http://www.foo.com/
            Console.WriteLine(shortie.Error.ErrorMessage);
            // No action taken
        }

        public class Shortie
        {
            public string Original { get; set; }
            public string Shortened { get; set; }
            public string Short { get; set; }
            public ShortieException Error { get; set; }
        }

        public class ShortieException
        {
            public int Code { get; set; }
            public string ErrorMessage { get; set; }
        }

        public static void SelectToken()
        {
            JObject o = JObject.Parse(@"{
              'Stores': [
                'Lambton Quay',
                'Willis Street'
              ],
              'Manufacturers': [
                {
                  'Name': 'Acme Co',
                  'Products': [
                    {
                      'Name': 'Anvil',
                      'Price': 50
                    }
                  ]
                },
                {
                  'Name': 'Contoso',
                  'Products': [
                    {
                      'Name': 'Elbow Grease',
                      'Price': 99.95
                    },
                    {
                      'Name': 'Headlight Fluid',
                      'Price': 4
                    }
                  ]
                }
              ]
            }");

            /* SelectToken */

            string name = (string)o.SelectToken("Manufacturers[0].Name");
            // Acme Co

            // if value type return null what will happen
            decimal productPrice = (decimal)o.SelectToken("Manufacturers[0].Products[0].Price");
            // 50

            string productName = (string)o.SelectToken("Manufacturers[1].Products[0].Name");
            // Elbow Grease

            /* SelectToken with LINQ */

            IList<string> storeNames = o.SelectToken("Stores").Select(s => (string)s).ToList();
            // Lambton Quay
            // Willis Street

            IList<string> firstProductNames = o["Manufacturers"].Select(m => (string)m.SelectToken("Products[1].Name")).ToList();
            // null
            // Headlight Fluid

            decimal totalPrice = o["Manufacturers"].Sum(m => (decimal)m.SelectToken("Products[0].Price"));
            // 149.95
        }
    }
}
