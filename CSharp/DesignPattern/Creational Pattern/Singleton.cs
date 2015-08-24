using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharp.DesignPattern.Creational_Pattern
{
    /// <summary>
    /// The 'Singleton' class
    /// </summary>
    class Singleton
    {
        private static Singleton _instance;

        // Constructor is 'protected'
        protected Singleton() { }

        public static Singleton Instance()
        {
            // Use lazy initialization.
            // Note: this is not thread saft.
            if (_instance == null)
            {
                _instance = new Singleton();
            }
            return _instance;
        }
    }

    /// <summary>
    /// The 'Singleton' class
    /// </summary>
    class LoadBalancer
    {
        private static LoadBalancer _instance;
        private List<string> _servers = new List<string>();
        private Random _random = new Random();

        // Lock synchronization object
        private static object syncLock = new object();

        protected LoadBalancer()
        {
            // List of available servers
            _servers.Add("ServerI");
            _servers.Add("ServerII");
            _servers.Add("ServerIII");
            _servers.Add("ServerIV");
            _servers.Add("ServerV");
        }

        public static LoadBalancer GetLoadBalancer()
        {
            if (_instance == null)
            {
                lock (syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new LoadBalancer();
                    }
                }
            }
            return _instance;
        }

        // Simple, but effective random load balancer
        public string Server
        {
            get
            {
                int r = _random.Next(_servers.Count);
                return _servers[r].ToString();
            }
        }
    }

    sealed class LoadBalancerOpti
    {
        // Static members are 'eagerly initialized', that is,
        // immediately when class is loaded from the frist time.
        // .NET guarantees thread safety for static initialization
        private static readonly LoadBalancerOpti _instance =
            new LoadBalancerOpti();

        private List<Server> _servers;
        private Random _random = new Random();

        private LoadBalancerOpti()
        {
            _servers = new List<Server>
            {
                 new Server{ Name = "ServerI", IP = "120.14.220.18" },
                 new Server{ Name = "ServerII", IP = "120.14.220.19" },
                 new Server{ Name = "ServerIII", IP = "120.14.220.20" },
                 new Server{ Name = "ServerIV", IP = "120.14.220.21" },
                 new Server{ Name = "ServerV", IP = "120.14.220.22" }
            };
        }

        public static LoadBalancerOpti GetLoadBalancer()
        {
            return _instance;
        }

        // Simple, but effective load balancer
        public Server NextServer
        {
            get
            {
                int r = _random.Next(_servers.Count);
                return _servers[r];
            }
        }
    }

    /// <summary>
    /// Represents a server machine
    /// </summary>
    class Server
    {
        // Gets or sets server name
        public string Name { get; set; }

        // Gets or sets server IP address
        public string IP { get; set; }
  }
}
