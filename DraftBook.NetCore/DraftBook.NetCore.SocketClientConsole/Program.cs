using System;

namespace DraftBook.NetCore.SocketClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketClient.StartClient("10.0.75.1", 11000);
        }
    }
}
