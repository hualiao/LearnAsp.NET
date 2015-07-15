using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp
{
    /// <summary>
    /// Ref: http://www.codeproject.com/Articles/193537/C-4-Tuples
    /// </summary>
    public class TupleDemo
    {
        static List<Tuple<int, string, string, DateTime>> lista;

        /// <summary>
        /// Return of Method without the need to create ref or out parameters
        /// </summary>
        /// <returns></returns>
        static Tuple<int, string, DateTime> RetornaCliente()
        {
            Tuple<int, string, DateTime> _cliente =
                Tuple.Create(1, "Frederico", new DateTime(1975, 3, 24));
            return _cliente;
        }

        private static void CarregaLista()
        {
            lista = new List<Tuple<int, string, string, DateTime>>();
            lista.Add(new Tuple<int,string,string,DateTime>
                            (0,"","",DateTime.MinValue));
            lista.Add(new Tuple<int,string,string,DateTime>
                            (1,"fred","M",new DateTime(1975,3,24)));
            lista.Add(new Tuple<int, string, string, DateTime>
                            (2, "Rubia", "F", new DateTime(1983, 12, 17)));
            lista.Add(new Tuple<int, string, string, DateTime>
                            (3, "João", "M", new DateTime(2004, 4, 16)));
            lista.Add(new Tuple<int, string, string, DateTime>
                            (4, "Tatá", "F", new DateTime(1999, 7, 14)));
        }

        /// <summary>
        /// return a list of an anonymous type
        /// </summary>
        /// <param name="sex"></param>
        /// <returns></returns>
        public static IEnumerable<Tuple<int, string>> SelecionaClientes(string sex)
        {
            var ret = from t in lista
                      where t.Item3 == sex
                      select new Tuple<int, string>(t.Item1, t.Item2);
            return ret;
        }

        /// <summary>
        /// Composite Key in a Dictionary
        /// </summary>
        /// <returns></returns>
        public static Dictionary<Tuple<int, int>, ClienteConta> ListaClienteConta()
        {
            Dictionary<Tuple<int, int>, ClienteConta> lista =
                new Dictionary<Tuple<int, int>, ClienteConta>();
            ClienteConta cc1 = new ClienteConta()
            {
                Codigo_Cliente=1,
                Codigo_Conta=1,
                Saldo=524.00
            };
            ClienteConta cc2 = new ClienteConta()
            {
                Codigo_Cliente = 1,
                Codigo_Conta = 2,
                Saldo = 765.00
            };
            lista.Add(Tuple.Create(cc1.Codigo_Cliente, cc1.Codigo_Conta), cc1);
            lista.Add(Tuple.Create(cc2.Codigo_Cliente, cc2.Codigo_Conta), cc2);
            return lista;
        }

        private List<Tuple<int, string>> RetornaListaPessoasOrdenadaPorNome()
        {
            var lstOrdenada = lista.OrderBy(t => t.Item2).Select(t => Tuple.Create(t.Item1,t.Item2)).ToList();
            return lstOrdenada;
        }
        private List<Tuple<int, string>> RetornaListaPessoasOrdenadaPorCodigo()
        {
            var lstOrdenada = lista.OrderBy(t => t.Item1).Select(t => Tuple.Create(t.Item1, t.Item2)).ToList();
            return lstOrdenada;
        }
        /// <summary>
        /// Replace Classes or Structs that are Created Just to Carry a Return or to Fill a List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, EventArgs e)
        {
            List<Tuple<int, string>> listaOrdenada;
            //if (rbtNome.Checked)
            //    listaOrdenada = RetornaListaPessoasOrdenadaPorNome();
            //else
            //    listaOrdenada = RetornaListaPessoasOrdenadaPorCodigo();
            //lstNomes.DataSource = listaOrdenada;
            //lstNomes.ValueMember = "Item1";
            //lstNomes.DisplayMember = "Item2";
        }

        public static void ComparingAndOrdering()
        {
            Tuple<int, int> t1 = Tuple.Create(3, 9);
            Tuple<int, int> t2 = Tuple.Create(3, 9);
            Tuple<int, int> t3 = Tuple.Create(9, 3);
            Tuple<int, int> t4 = Tuple.Create(9, 4);

            Console.WriteLine("t1 = t2 : {0}", t1.Equals(t2)); //true
            Console.WriteLine("t1 = t2 : {0}", t1 == t2); //false
            Console.WriteLine("t1 = t3 : {0}", t1.Equals(t3)); //false

            Console.WriteLine("t1 < t3 : {0}", ((IComparable)t1).CompareTo(t3) < 0); //true
            Console.WriteLine("t3 < t4 : {0}", ((IComparable)t3).CompareTo(t4) < 0); //true
        }

        public class ClienteConta
        {
            public int Codigo_Cliente { get; set; }
            public int Codigo_Conta { get; set; }
            public double Saldo { get; set; }
        }
    }
}
