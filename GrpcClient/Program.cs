using Grpc.Net.Client;
using GrpcServer;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            AppContext.SetSwitch(
              "System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
            var httpHandler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
            HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
            using var channel = GrpcChannel.ForAddress("https://localhost:8001",
        new GrpcChannelOptions { HttpHandler = httpHandler });

            var client = new OrderService.OrderServiceClient(channel);

            while (true)
            {
                Console.WriteLine("If you wanna get orders, say 'Y'\n If you wanna quit say 'Q'");

                string line = Console.ReadLine();

                if (line == "Q")
                {
                    Console.WriteLine("Exiting...");
                    break;
                }

                else if (line == "Y")
                {
                    var reply = client.GetOrders(
                        new GetOrdersRequest() { }
                    );

                    Console.WriteLine("Reply: " + reply.Orders);
                }
            }
        }
    }
}