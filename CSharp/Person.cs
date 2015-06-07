using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp
{
    public enum Gender
    {
        Male,
        Female
    }

    public class Passport
    {
        public string Number { get; set; }

        public string Authority { get; set; }

        public DateTime ExpirationDate { get; set; }
    }

    public class Person
    {
        public Person() { }

        public static Person Generate()
        {
            return new Person
            {
                FirstName = "Liao",
                LastName = "Hua",
                Age = 25,
                Gender = Gender.Male,
                Passport = new Passport() { Number="default number!" }
            };
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public uint Age { get; set; }

        public Gender Gender { get; set; }

        public Passport Passport { get; set; }
    }
}
