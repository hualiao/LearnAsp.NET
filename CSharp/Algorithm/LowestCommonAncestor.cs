using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.Algorithm
{
    public class Node<T> where T : IComparable
    {
        public T Value { get; private set; }
        public Node<T> Left { get; set; }
        public Node<T> Right { get; set; }
        public Node(T value)
        {
            Value = value;
        }
    }

    public class Tree<T> where T : IComparable
    {
        private Node<T> _root;
        public void Insert(T v)
        {
            if (_root == null) _root = new Node<T>(v);
            else NodeInsert(_root, v);
        }

        public void NodeInsert(Node<T> n, T v)
        {
            if (v.CompareTo(n.Value) < 0)
            {
                if (n.Left == null) n.Left = new Node<T>(v);
                else NodeInsert(n.Left, v);
            }
            else if (v.CompareTo(n.Value) > 0)
            {
                if (n.Right == null) n.Right = new Node<T>(v);
                NodeInsert(n.Right, v);
            }
        }

        public T LowestAncestor(T a, T b)
        {
            return NodeLowestAncestor(_root, a, b).Value;
        }

        private Node<T> NodeLowestAncestor(Node<T> n, T a, T b)
        {
            if (n == null) return null;
            if (n.Value.CompareTo(a) == 0 || n.Value.CompareTo(b) == 0) return n;
            var left = NodeLowestAncestor(n.Left, a, b);
            var right = NodeLowestAncestor(n.Right, a, b);
            if (left != null && right != null) return n;
            return left ?? right;
        }

        //// These next to method are my bit -- JMC
        //public T LowestAncestor(T a, T b)
        //{
        //    // ensure that a <= b
        //    if (a.CompareTo(b) > 0)
        //    {
        //        var t = a;
        //        a = b;
        //        b = t;
        //    }
        //    return NodeLowestAncestor(_root, a, b).Value;
        //}

        //private static Node<T> NodeLowestAncestor(Node<T> n, T a, T b)
        //{
        //    if (n == null) return null;
        //    // Is n between a & b ?   Then tht's the answer
        //    if (a.CompareTo(n.Value) <= 0 && n.Value.CompareTo(b) <= 0)
        //        return n;

        //    if (a.CompareTo(n.Value) < 0)
        //        return NodeLowestAncestor(n.Left, a, b);
        //    else
        //        return NodeLowestAncestor(n.Right, a, b);
        //}
    }

    class Program
    {
        static void Test(string[] args)
        {
            var values = new List<int>() {20, 10, 5, 15, 30, 25, 35, 1};
            var tree = new Tree<int>();   
            values.ForEach(tree.Insert);
             Action<int,int> printLCA = (a,b) => Console.WriteLine("Lowest Ancestor of {a} and {b} is {tree.LowestAncestor(a, b)}");
            printLCA(20, 35);
            printLCA(5, 15);
            printLCA(5, 35);
            printLCA(25, 35);
            printLCA(1, 15);
        }
    }

    /*************************************/

}
