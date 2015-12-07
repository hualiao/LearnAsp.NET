using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Linq;

namespace CSharp.Utility
{
    class XmLOperation
    {

    }

    public class XMLPath
    {
        public const string xmlElement = @"<Names>
                                                <Name type=""M"">John</Name>
                                                <Name type=""F"">Susan</Name>
                                                <Name type=""M"">David</Name>
                                            </Names>";

        public const string xmlElement2 = @"<Names>
                                                <Name>
                                                    <FirstName>John</FirstName>
                                                    <LastName>Smith</LastName>
                                                </Name>
                                                <Name>
                                                    <FirstName>James</FirstName>
                                                    <LastName>White</LastName>
                                                </Name>
                                            </Names>";

        public const string xmlElement3 = @"<Names>
                                                <Name>James</Name>
                                                <Name>John</Name>
                                                <Name>Robert</Name>
                                                <Name>Michael</Name>
                                                <Name>William</Name>
                                                <Name>David</Name>
                                                <Name>Richard</Name>
                                            </Names>";

        public static void SelectNodesbyAttribute()
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlElement);

            XmlNodeList xnList = xml.SelectNodes("/Names/Name[@type='M']");
            foreach (XmlNode xn in xnList)
            {
                Console.WriteLine(xn.InnerText);
                Console.WriteLine(xn.InnerXml);
            }
        }

        public static void SelectNodebyName()
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlElement2);
            // The first slash means that the <Names> node must be a root node.
            XmlNodeList xnList = xml.SelectNodes("/Names/Name");
            foreach (XmlNode xn in xnList)
            {
                string firestName = xn["FirstName"].InnerText;
                string lastName = xn["LastName"].InnerText;
                Console.WriteLine("Name: {0} {1}", firestName, lastName);
            }
        }

        /// <summary>
        /// Ref: http://www.csharp-examples.net/xpath-top-xml-nodes/
        /// </summary>
        public static void SelectTopNodes()
        {
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(xmlElement3);

            XmlNodeList xnList = xml.SelectNodes("/Names/Name[position()<=5]");
            foreach (XmlNode xn in xnList)
            {
                Console.WriteLine(xn.InnerText);
            }

        }

        /// <summary>
        /// Ref:http://www.codeproject.com/Articles/52079/Using-XPathNavigator-in-C
        /// </summary>
        public static void XPathNavigatorDemo()
        {
            try
            {
                string xmlFileName = "books.xml";

                // create an XPathDocument object
                XPathDocument xmlPathDoc = new XPathDocument(xmlFileName);


            }
            catch (XmlException e)
            {
                Console.WriteLine("Exception: " + e.ToString());
            }
        }

        public static void NavigateBooksXml(XPathNavigator p_xPathNav)
        {
            // move to the root and the first element - <books>
            p_xPathNav.MoveToRoot();
            p_xPathNav.MoveToFirstChild();

            // move to first <book> element
            p_xPathNav.MoveToFirstChild();
            Console.WriteLine("Printing contents of books.xml");

            //begin looping through the nodes
            do
            {
                if (p_xPathNav.MoveToFirstAttribute())
                {
                    Console.WriteLine(p_xPathNav.Name + "=" + p_xPathNav.Value);
                    // go back from the attributes to the parent element
                    p_xPathNav.MoveToParent();
                }

                //display the child nodes
                if (p_xPathNav.MoveToFirstChild())
                {
                    Console.WriteLine(p_xPathNav.Name + "=" + p_xPathNav.Value);
                    while (p_xPathNav.MoveToNext())
                    {
                        Console.WriteLine(p_xPathNav.Name + "=" + p_xPathNav.Value);
                    }
                    p_xPathNav.MoveToParent();
                }

            } while (p_xPathNav.MoveToNext());
        }

        /*
           FindAllTitles method
           Accpets: XPathNavigator p_xPathNav
           Returns: Nothing
           Purpose: This method will iterate through the xml document provided in the 
           XPathNavigator object and display all of the title elements    
        */
        public static void FindAllTitles(XPathNavigator p_xPathNav)
        {
            //run the XPath query
            XPathNodeIterator xPathIt = p_xPathNav.Select("//book/title");

            //use the XPathNodeIterator to display the results
            if (xPathIt.Count > 0)
            {
                Console.WriteLine("");
                Console.WriteLine("The catalog contains the following titles:");

                //begin to loop through the titles and begin to display them
                while (xPathIt.MoveNext())
                {
                    Console.WriteLine(xPathIt.Current.Value);
                }
            }
            else
            {
                Console.WriteLine("No titles found in catalog.");
            }
        }

        /*
        FindBooksByCategory method
        Accpets: XPathNavigator object
                string p_Category
        Returns: Nothing
        Purpose: This method will iterate through the XML document provided 
                in the XPathNavigator object and search book elements 
                for the category attribute passed into the method
        */
        public static void FindBooksByCategory(XPathNavigator p_xPathNav,string p_Category)
        {
            string query = "//book[@category=\'" + p_Category + "\']";
            XPathNodeIterator xPathIt = p_xPathNav.Select(query);

            //use the XPathNodeIterator to display the results
            if (xPathIt.Count > 0)
            {
                Console.WriteLine("");
                Console.WriteLine("The following books are " + 
                                    "in the \{0\} category:", p_Category);
                while (xPathIt.MoveNext())
                {
                    Console.WriteLine(xPathIt.Current.Value);
                }
            }
            else
            {
                Console.WriteLine("No books found in the \{0\} category", p_Category);
            }
        }

    }

    /// <summary>
    /// Ref:http://blogs.msdn.com/b/xmlteam/archive/2007/03/24/streaming-with-linq-to-xml-part-2.aspx
    /// </summary>
    public class XMLReader
    {
        public static IEnumerable<XElement> SimpleStreamAxis(
            string inputUrl,string matchName)
        {
            using(XmlReader reader=XmlReader.Create(inputUrl))
            {
                reader.MoveToContent();
                while(reader.Read())
                {
                    switch(reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if(reader.Name==matchName)
                            {
                                XElement el=XElement.ReadFrom(reader)
                                                     as XElement;
                                if(el!=null)
                                    yield return el;
                            }
                            break;
                    }
                }
                reader.Close();
            }
        }

        public static void Test()
        {
            string inputUrl=
                @"http://download.wikimedia.org/enwikiquote/20070225/enwikiquote-20070225-abstract.xml";
            IEnumerable<string> bardQuotes=
                from el in SimpleStreamAxis(inputUrl,"doc")
                where el.Element("abstract").Value.Contains("Shakespeare")
                select (string)el.Element("url");

            foreach(string str in bardQuotes)
            {
                Console.WriteLine(str);
            }
        }
    }
}
