using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;

namespace CSharp.Linq
{
    public class LinqToXML
    {
        public static void ReadAndTraverse()
        {
            foreach (var m in GetCustomerListlinq())
            {
                Console.WriteLine(m.Name + m.Contacts.Phone.Count);
            }
            // Using XElement
            XElement xelement = XElement.Load("Document\\Employees.xml");
            IEnumerable<XElement> employees = xelement.Elements();
            // Read the entire XML
            foreach (var employee in employees)
            {
                Console.WriteLine(employee.Element("Name").Value);
                //Console.WriteLine(employee);
            }

            IEnumerable<string> codes = from code in xelement.Elements("Employee")
                                        let zip = (string)code.Element("Address").Element("Zip")
                                        orderby zip
                                        select zip;
            Console.WriteLine("List and Sort all Zip Codes");

            foreach (string zp in codes)
                Console.WriteLine(zp);

            Console.WriteLine("List of all Zip Codes");
            foreach (XElement xEle in xelement.Descendants("Zip"))
            {
                Console.WriteLine((string)xEle);
            }

            var addresses = from address in xelement.Elements("Employee")
                            where (string)address.Element("Address").Element("City") == "Alta"
                            select address;
            Console.WriteLine("Details of Employees living in Alta City");
            foreach (XElement xEle in addresses)
                Console.WriteLine(xEle);

            var homePhone = from phoneno in xelement.Elements("Employee")
                            where (string)phoneno.Element("Phone").Attribute("Type") == "Home"
                            select phoneno;
            Console.WriteLine("List HomePhone Nos.");
            foreach (XElement xEle in homePhone)
            {
                Console.WriteLine(xEle.Element("Phone").Value);
            }

            var name = from nm in xelement.Elements("Employee")
                       where (string)nm.Element("Sex") == "Female"
                       select nm;
            Console.WriteLine("Details of Female Employees:");
            foreach (XElement xEle in name)
                Console.WriteLine(xEle);

            // Using XDocument
            XDocument xdocument = XDocument.Load("Document\\Employees.xml");
            IEnumerable<XElement> employees1 = xdocument.Root.Elements();
            foreach (var employee in employees1)
            {
                //Console.WriteLine(employee);
            }

            /* Part 2 Manipulate XML content and Persist the changes using LINQ To XML */
            XNamespace empNM = "urn:lst-emp:emp";
            XDocument xDoc = new XDocument(
                new XDeclaration("1.0", "UTF-16", null),
                new XElement(empNM + "Employees",
                    new XElement("Employee",
                        new XComment("Only 3 elements for demo purpose"),
                        new XElement("EmpId", "5"),
                        new XElement("Name", "Kimmy"),
                        new XElement("Sex", "Female")
                        )));
            StringWriter sw = new StringWriter();
            xDoc.Save(sw);
            Console.WriteLine(sw);

            XmlWriter xWrite = XmlWriter.Create(sw);
            xDoc.Save(xWrite);
            xWrite.Close();

            xDoc.Save("D:\\Something.xml");
            //Console.WriteLine("Saved");

            XmlReader xRead = XmlReader.Create("Document\\Employees.xml");
            XElement xEle1 = XElement.Load(xRead);
            Console.WriteLine(xEle1);
            xRead.Close();

            // find the second Employee Element
            // Using XElement
            Console.WriteLine("Using XElement");
            XElement xEle2 = XElement.Load("..\\..\\Employees.xml");
            var emp1 = xEle2.Descendants("Employee").ElementAt(1);
            Console.WriteLine(emp1);

            Console.WriteLine("------------");

            //// Using XDocument
            Console.WriteLine("Using XDocument");
            XDocument xDoc1 = XDocument.Load("..\\..\\Employees.xml");
            var emp2 = xDoc.Descendants("Employee").ElementAt(1);
            Console.WriteLine(emp2);

            // find the first two Elements using LINQ to XML
            XElement xEle3 = XElement.Load("..\\..\\Employees.xml");
            var emps3 = xEle3.Descendants("Employee").Take(2);
            foreach (var emp in emps3)
                Console.WriteLine(emp);

            // list the 2nd and 3rd Element using LINQ to XML
            XElement xEle4 = XElement.Load("..\\..\\Employees.xml");
            var emps4 = xEle4.Descendants("Employee").Skip(1).Take(2);
            foreach (var emp in emps4)
                Console.WriteLine(emp);

            // list the last 2 Elements using LINQ to XML
            XElement xEle5 = XElement.Load("..\\..\\Employees.xml");
            var emps5 = xEle5.Descendants("Employee").Reverse().Take(2);
            foreach (var emp in emps5)
                Console.WriteLine(emp.Element("EmpId") + "" + emp.Element("Name"));

            // Find the Element Count based on a condition using LINQ to XML
            XElement xelement6 = XElement.Load("..\\..\\Employees.xml");
            var stCnt = from address in xelement6.Elements("Employee")
                        where (string)address.Element("Address").Element("State") == "CA"
                        select address;
            Console.WriteLine("No of Employees living in CA State are {0}", stCnt.Count());

            // add a new Element at runtime using LINQ to XML
            XElement xEle7 = XElement.Load("..\\..\\Employees.xml");
            xEle7.Add(new XElement("Employee",
                new XElement("EmpId", 5),
                new XElement("Name", "George")));

            Console.Write(xEle7);

            XElement xEle8 = XElement.Load("..\\..\\Employees.xml");
            xEle8.AddFirst(new XElement("Employee",
                new XElement("EmpId", 5),
                new XElement("Name", "George")));

            Console.Write(xEle8);

            // Add an attribute to an Element, use the following code
            XElement xEle9 = XElement.Load("");
            xEle9.Add(new XElement("Employee",
                new XElement("EmpId", 5),
                new XElement("Phone", "434-555-4224", new XAttribute("Type", "Home"))));
            Console.WriteLine(xEle9);

            XElement xEle10 = XElement.Load("..\\..\\Employees.xml");
            var countries = xEle10.Elements("Employee").Elements("Address").Elements("Country").ToList();
            foreach (XElement cEle in countries)
                cEle.ReplaceNodes("United States Of America");
            Console.Write(xEle10);

            XElement xEle11 = XElement.Load("..\\..\\Employees.xml");
            var phone = xEle11.Elements("Employee").Elements("Phone").ToList();
            foreach (XElement pEle in phone)
                pEle.RemoveAttributes();
            Console.Write(xEle11);

            // Delete an Element based on a condition using LINQ to XML
            XElement xEle12 = XElement.Load("..\\..\\Employees.xml");
            var addr = xEle12.Elements("Employee").ToList();
            foreach (XElement addEle in addr)
                addEle.SetElementValue("Address", null);
            Console.Write(xEle12);


        }

        // Using LINQ
        private static List<Customer> GetCustomerListlinq()
        {
            XElement xmlDoc = XElement.Load("Document\\Meeting.xml");
            var customers =
                    from cust in xmlDoc.Descendants("item")
                    select new Customer
                    {
                        ID = Convert.ToInt32(cust.Element("id").Value),
                        Name = cust.Element("customer").Value,
                        MeetingType = cust.Element("type").Value,
                        CallDate = Convert.ToDateTime(cust.Element("date").Value),
                        DurationInHours = Convert.ToInt32(cust.Element("hours").Value),
                        Contacts = new Contact()
                        {
                            Phone = new List<PhoneContact>(from phn in cust.Descendants("phone")
                                                           select new PhoneContact
                                                           {
                                                               Type = phn.Element("type").Value,
                                                               Number = phn.Element("no").Value
                                                           })
                        }
                    };
            return customers.ToList();
        }

        // Using LAMDA
        private List<Customer> GetCustomerListLamda()
        {
            XElement xmlDoc = XElement.Load("Document\\Meeting.xml");

            var customers =
                    xmlDoc.Descendants("item").Select(cust => new Customer
                    {
                        ID = Convert.ToInt32(cust.Element("id").Value),
                        Name = cust.Element("customer").Value,
                        MeetingType = cust.Element("type").Value,
                        CallDate = Convert.ToDateTime(cust.Element("date").Value),
                        DurationInHours = Convert.ToInt32(cust.Element("hours").Value),
                        Contacts = new Contact
                        {
                            Phone = cust.Descendants("phone").Select(phn => new PhoneContact
                            {
                                Number = phn.Element("type").Value,
                                Type = phn.Element("no").Value
                            }).ToList()
                        }
                    });

            return customers.ToList();
        }
    }

    /// <summary>
    /// Ref: http://geekswithblogs.net/pabothu/archive/2014/04/29/reading-a-complex-xml-using-linq-in-c-sharp.aspx
    /// </summary>
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string MeetingType { get; set; }
        public DateTime CallDate { get; set; }
        public int DurationInHours { get; set; }
        public Contact Contacts { get; set; }
    }

    public class Contact
    {
        public List<PhoneContact> Phone = new List<PhoneContact>();
    }

    public class PhoneContact
    {
        public string Type { get; set; }
        public string Number { get; set; }
    }
}
