using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ServiceStack.Redis;

namespace Redis
{
    class Program
    {
        //static void Main(string[] args)
        //{
        //    using (var redis = new RedisClient("127.0.0.1"))
        //    {
        //        redis.FlushAll();

        //    }

            
        //    var senderThread = new Thread(() =>
        //    {
        //        using (var redis = new RedisClient("127.0.0.1"))
        //        {
        //            Console.WriteLine("Input message: ");
        //            var message = Console.ReadLine();
        //            while (!String.IsNullOrWhiteSpace(message))
        //            {
        //                redis.EnqueueItemOnList("default", message);
        //                Console.WriteLine("Input message: ");
        //                message = Console.ReadLine();
        //            }
        //        }
        //    });

        //    senderThread.Start();

        //    using (var redis = new RedisClient("127.0.0.1"))
        //    {
        //        var result = String.Empty;
        //        while (String.Compare("q", result, false) != 0)
        //        {
        //            result = redis.BlockingDequeueItemFromList("default", TimeSpan.FromSeconds(5));
        //            Console.WriteLine("DEQUEUE RESULT = [{0}]", result);
        //        }
        //    }

        //    Console.WriteLine("Done!");
        //    Console.ReadKey();
        //}

        static void Main(string[] args)
        {
            using (RedisClient redis = new RedisClient("127.0.0.1"))
            {
                redis.FlushAll();
            }

            var consumerThread1 = new Thread(new ParameterizedThreadStart(ConsumerAction));
            var consumerThread2 = new Thread(new ParameterizedThreadStart(ConsumerAction));
            var consumerThread3 = new Thread(new ParameterizedThreadStart(ConsumerAction));
            consumerThread1.Start("Consumer 1");
            consumerThread2.Start("Consumer 2");
            consumerThread3.Start("COnsumer 3");

            using (var publisher = new RedisClient("127.0.0.1"))
            {
                Console.WriteLine("Input message: ");
                var message = Console.ReadLine();
                while (!String.IsNullOrWhiteSpace(message))
                {
                    publisher.PublishMessage("default", message);
                    Console.WriteLine("Input message: ");
                    message = Console.ReadLine();
                }
            }

            Console.WriteLine("Done!");
            Console.ReadKey();
            //why no quit after enter key
        }

        static void ConsumerAction(object name)
        {
            //Is connect need auth
            using (var consumer = new RedisClient("127.0.0.1"))
            {
                using (var subscription = consumer.CreateSubscription())
                {
                    subscription.OnSubscribe = (channel) =>
                    {
                        Console.WriteLine("[{0}] Subscribe to channel '{1}'.", name, channel);
                    };
                    subscription.OnUnSubscribe = (channel) =>
                    {
                        Console.WriteLine("[{0}] Unsubscribe to channel '{1}'.", name, channel);
                    };
                    subscription.OnMessage = (channel, message) =>
                    {
                        Console.WriteLine("[{0}] Received message '{1}' from channel '{2}'.", name, message, channel);
                    };
                    subscription.SubscribeToChannels("default");
                }
            }
        }
    }
}
