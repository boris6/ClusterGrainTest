using Messages;
using Proto.Cluster;
using Proto.Cluster.Consul;
using Proto.Remote;
using System;
using System.Threading;
using System.Threading.Tasks;
using ProtosReflection = Messages.ProtosReflection;

namespace ClusterGrainsApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Serialization.RegisterFileDescriptor(ProtosReflection.Descriptor);

            Grains.HelloGrainFactory(() => new HelloGrain());

            Cluster.Start("BorisNodeTest", "127.0.0.1", 12000, new ConsulProvider(new ConsulProviderOptions(), c => c.Address = new Uri("http://127.0.0.1:8500/")));
            Thread.Sleep(System.Threading.Timeout.Infinite);
            Console.WriteLine("Shutting Down...");
            Cluster.Shutdown();
        }
    }

    public class HelloGrain : IHelloGrain
    {
        public Task<HelloResponse> SayHello(HelloRequest request)
        {
            return Task.Run(() =>
            {
                Thread.Sleep(request.Sleep);
                Console.WriteLine($"Hello from node {request.Count}");

                return new HelloResponse
                {
                    Message = $"Hello from node {request.Count}"
                };
            });
        }
    }
}