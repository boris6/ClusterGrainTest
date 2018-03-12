using Messages;
using Proto.Cluster;
using Proto.Cluster.Consul;
using Proto.Remote;
using System;
using System.Threading;
using ProtosReflection = Messages.ProtosReflection;

namespace Client1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Serialization.RegisterFileDescriptor(ProtosReflection.Descriptor);

            Cluster.Start("BorisNodeTest", "127.0.0.1", 12002, new ConsulProvider(new ConsulProviderOptions()));
            int counter = 0;
            Random rand = new Random();
            var client = Grains.HelloGrain("Grain1");

            while (counter < 100)
            {
                var res = client.SayHello((new HelloRequest() { Sleep = 2000, Count = counter })).Result;
                Console.WriteLine(res.Message);
                counter++;
            }
            Thread.Sleep(System.Threading.Timeout.Infinite);
            Console.WriteLine("Shutting Down...");
            Cluster.Shutdown();
        }
    }
}