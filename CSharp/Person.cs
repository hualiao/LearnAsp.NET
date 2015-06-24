using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using ProtoBuf;

namespace CSharp
{
    [DataContract]
    [Serializable]
    public enum Gender
    {
        [EnumMember]
        Male,
        [EnumMember]
        Female
    }

    [DataContract]
    [ProtoContract]
    [Serializable]
    public class Passport
    {
        [DataMember]
        [ProtoMember(1)]
        public string Number { get; set; }

        [DataMember]
        [ProtoMember(2)]
        public string Authority { get; set; }

        [DataMember]
        [ProtoMember(3)]
        public DateTime ExpirationDate { get; set; }
    }

    [DataContract]
    [ProtoContract]
    [Serializable]
    public class Person
    {
        public Person() { }

        public static Person Generate()
        {
            return new Person
            {
                FirstName = Randomizer.Name,
                LastName = Randomizer.Name,
                Age = (uint)Randomizer.Rand.Next(120),
                Gender = (Randomizer.Rand.Next(0, 1) == 0) ? Gender.Male : Gender.Female,
                Passport = new Passport() 
                { 
                    Number = Randomizer.Id,
                    Authority=Randomizer.Phrase,
                    ExpirationDate=Randomizer.GetDate(DateTime.UtcNow,DateTime.UtcNow+TimeSpan.FromDays(1000))
                }
            };
        }

        [DataMember]
        [ProtoMember(1)]
        public string FirstName { get; set; }

        [DataMember]
        [ProtoMember(2)]
        public string LastName { get; set; }

        [DataMember]
        [ProtoMember(3)]
        public uint Age { get; set; }

        [DataMember]
        [ProtoMember(4)]
        public Gender Gender { get; set; }

        [DataMember]
        [ProtoMember(5)]
        public Passport Passport { get; set; }

        public List<string> Compare(Person comparable)
        {
            var errors = new List<string> { "  ************** Comparison failed! "};
            if (comparable == null)
            {
                errors.Add("comparable is null");
                return errors;
            }

            Compare("FirstName", FirstName, comparable.FirstName, errors);
            Compare("LastName", LastName, comparable.LastName, errors);
            Compare("Age", Age, comparable.Age, errors);
            Compare("Gender", Gender, comparable.Gender, errors);
            Compare("Passport.Number", Passport.Number, comparable.Passport.Number, errors);
            Compare("Passport.Authority", Passport.Authority, comparable.Passport.Authority, errors);
            Compare("Passport.ExpirationDate", Passport.ExpirationDate, comparable.Passport.ExpirationDate, errors);

            return errors;
        }

        private static void Compare(string objectName, object left, object right,
            List<string> errors)
        {
            if(!left.Equals(right))
                errors.Add(String.Format("\t{0}: {1} != {2}",objectName,left,right));
        }
    }
}
