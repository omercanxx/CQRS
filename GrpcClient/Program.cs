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


            while (true)
            {
                Console.WriteLine("If you wanna get orders, say '1'\n " +
                                  "If you wanna get favorites, say '2'\n" +
                                  "If you wanna quit say 'Q'");

                string line = Console.ReadLine();

                if (line == "Q")
                {
                    Console.WriteLine("Exiting...");
                    break;
                }

                else if (line == "1")
                {
                    var client = new OrderService.OrderServiceClient(channel);
                    var reply = client.GetOrders(
                        new GetOrdersRequest() { }
                    );

                    Console.WriteLine("Reply: " + reply.Products);
                }
                else if (line == "2")
                {
                    var client = new UserService.UserServiceClient(channel);
                    var reply = client.GetFavorites(
                        new GetFavoritesRequest() { }
                    );

                    Console.WriteLine("Reply: " + reply.Products);
                }
                else
                    Console.WriteLine("Try again please");
                
            }
        }
    }
}