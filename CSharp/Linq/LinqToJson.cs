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
    }
}
