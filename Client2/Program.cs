using Messages;
using Proto.Cluster;
using Proto.Cluster.Consul;
using Proto.Remote;
using System;
using System.Threading;
using ProtosReflection = Messages.ProtosReflection;

namespace Client2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Serialization.RegisterFileDescriptor(ProtosReflection.Descriptor);

            Cluster.Start("BorisNodeTest", "127.0.0.1", 12001, new ConsulProvider(new ConsulProviderOptions()));
            int counter = 300;
            var client = Grains.HelloGrain("Grain2");

            while (counter < 400)
            {
                var res = client.SayHello((new HelloRequest() { Sleep = 100, Count = counter })).Result;
                Console.WriteLine(res.Message);
                counter++;
            }
            Thread.Sleep(System.Threading.Timeout.Infinite);
            Console.WriteLine("Shutting Down...");
            Cluster.Shutdown();
        }
    }
}