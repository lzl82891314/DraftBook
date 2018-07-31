using System;

namespace DraftBook.NetCore.SocketClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("input socket server ipaddress: ");
            var ip = Console.ReadLine();
            Console.WriteLine("input socket server port: ");
            var port = int.Parse(Console.ReadLine());
            SocketClient.StartClient(ip, port);
        }
    }
}
