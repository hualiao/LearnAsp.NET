using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CSharp
{
    /// <summary>
    /// Ref: http://www.codeproject.com/Articles/47887/C-Delegates-Anonymous-Methods-and-Lambda-Expressio
    /// </summary>
    public class DelegateDemo
    {
        /// <summary>
        /// Ref: http://weblogs.asp.net/zeeshanhirani/not-all-lambdas-can-be-converted-to-expression-trees
        /// </summary>
        public static void ExpressionTree()
        {
            // can be convert to expression tree because it has expression body
            Expression<Func<int, int, int>> add = (a, b) => a + b;
            
            // lambdas that uses statement body cannot be converted
            // to expression trees
            //Expression<Func<int, int, int>> add2 = (a, b) => { return a + b; };
        }

        public static void IQueryExpressionTest()
        {
            List<string> list = new List<string>();
            IQueryable<string> query = list.AsQueryable();
            list.Add("one");
            list.Add("two");
            list.Add("three");

            string foo = list.First(x => x.EndsWith("o"));
            string bar = query.First(x => x.EndsWith("o"));
            // foo and bar are now both 'two' as expected 
            foo = list.First(x => { return x.EndsWith("e"); }); //no error 
            //bar = query.First(x => { return x.EndsWith("e"); }); //error 
            bar = query.First((Func<string, bool>)(x => { return x.EndsWith("e"); })); //no error 
        }

        class Customer
        {
            public int ID { get; set; }
            public static bool Test(Customer x)
            {
                return x.ID == 5;
            }
        }

        public void AnonymousTest()
        {
            List<Customer> custs = new List<Customer>();
            custs.Add(new Customer() { ID = 1 });
            custs.Add(new Customer() { ID = 5 });

            custs.First(new Func<Customer, bool>(delegate(Customer x) { return x.ID == 5; }));
            custs.First(new Func<Customer, bool>((Customer x) => x.ID == 5));
            custs.First(delegate(Customer x) { return x.ID == 5; });
            custs.First((Customer x) => x.ID == 5);
            custs.First(x => x.ID == 5);
            custs.First(Customer.Test); 
        }
    }
}
