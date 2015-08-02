using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp
{
    public interface IGoodObjects
    {
        string PropertyOne { get; set; }
        int PropertyTwo { get; set; }
        bool PropertyThree { get; set; }
    }

    public class GoodObjectOne : IGoodObjects
    {
        public string PropertyOne { get; set; }
        public int PropertyTwo { get; set; }
        public bool PropertyThree { get; set; }
        public int ThisIsAPropertyThatOnlyGoodObjectOneHas
        { get; set; }
    }

    public class GoodObjectTwo : IGoodObjects
    {
        public string PropertyOne { get; set; }
        public int PropertyTwo { get; set; }
        public bool PropertyThree { get; set; }
        public int ThisIsAPropertyThatOnlyGoodObjectTwoHas
        { get; set; }
    }

    /// <summary>
    /// Wrap extend class by type parameter , not direct inherite Interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GoodObjectWorker<T> where T : IGoodObjects
    {
        private T _inboundObject;

        public GoodObjectWorker(T initialObject)
        {
            _inboundObject = initialObject;
        }

        public void SetUpData()
        {
            _inboundObject.PropertyOne = "This is a string";
            _inboundObject.PropertyTwo = 20;
            _inboundObject.PropertyThree = false;
        }

        public void DisplayData()
        {
            Console.WriteLine("Property One: {0}",
                _inboundObject.PropertyOne);
            Console.WriteLine("Property Two: {0}",
                _inboundObject.PropertyTwo);
            Console.WriteLine("Property Three: {0}",
                _inboundObject.PropertyThree);
            Console.WriteLine(typeof(T));
        }
    }


    /// <summary>
    /// Ref: http://www.codeguru.com/columns/dotnet/vectors-for-the-c-developer.html
    /// </summary>
    class GenericWrap
    {
        public static void GenericWrapTest()
        {
            GoodObjectWorker<GoodObjectOne> workerOne = new 
                GoodObjectWorker<GoodObjectOne>(new GoodObjectOne());

            GoodObjectWorker<GoodObjectTwo> workerTwo = new
               GoodObjectWorker<GoodObjectTwo>(new GoodObjectTwo());

            workerOne.SetUpData();
            workerTwo.SetUpData();

            workerOne.DisplayData();
            workerTwo.DisplayData();
        }
    }
}
