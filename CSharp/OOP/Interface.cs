using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.OOP
{
    class Interface
    {
        // Ref:http://stackoverflow.com/questions/17278333/abstract-base-classes-that-implement-an-interface
        interface IFoo { void T();}
        public class F1 : IFoo
        {
            public void T() { Console.WriteLine("F1"); }
        }
        public class F2 : F1
        {
            public void T() { Console.WriteLine("F2"); }
        }
        public class F3 : F2
        {
            public void T() { Console.WriteLine("F3"); }
        }
        public void CallInterfaceFromSubClass()
        {
            IFoo foo = new F1();
            foo.T();
            foo = new F2();
            foo.T();
            foo = new F3();
            foo.T();
            /* Output
                F1
                F1
                F1
             */
        }

        // Ref http://stackoverflow.com/questions/8913095/c-sharp-interface-and-base-classes
        interface IBar { void T(); void M(); void O(); void P();}
        public abstract class Base : IBar
        {
            public void T() { Console.WriteLine("Base T()"); }
            public virtual void M() { Console.WriteLine("Base M()"); }
            public abstract void O();
            public void P() { Console.WriteLine("Base P()"); }
        }
        public class Sub : Base
        {
            public override void M() { Console.WriteLine("Sub M()"); }
            public override void O() { Console.WriteLine("Sub O()"); }
            public new void P() { Console.WriteLine("Sub P()"); }
        }
        public class Third : Sub
        {
            public void T() { Console.WriteLine("Third T()"); }
            public override void M()
            {
                Console.WriteLine("Third M()");
            }
            public void O()
            {
                Console.WriteLine("Third O()");
            }
            public void P() { Console.WriteLine("Third P()"); }
        }
        public void InheritInterfaceByAbstractClass()
        {
            IBar foo = new Sub();
            foo.T();
            foo.M();
            foo.O();
            foo.P();
            foo = new Third();
            foo.T();
            foo.M();
            foo.O();
            foo.P();
            /** Output
                Base T()
                Sub M()
                Sub O()
                Base P()
                Base T()
                Third M()
                Sub O()
                Base P()             
             **/
        }

        // Ref:http://stackoverflow.com/questions/7582085/how-do-i-call-a-base-class-implementation-of-an-explicitly-impemented-interface
        interface MyInterface
        {
            bool DoSomething();
        }
        // BaseClass explicitly implements the interface
        public class BaseClass : MyInterface
        {
            // base class
            bool MyInterface.DoSomething()
            {
                return DoSomething();
            }
            protected virtual bool DoSomething() { return false; }
        }

        public class DerivedClass : BaseClass
        {
            protected override bool DoSomething()
            {
                return true;
                // changed version, perhaps calling base.DoSomething();
            }
        }
    }
}
