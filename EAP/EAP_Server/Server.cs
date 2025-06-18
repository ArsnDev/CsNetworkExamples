using System.Net;
using System.Net.Sockets;

namespace EAP_Server;
internal class Server
{
    static void Main(string[] args)
    {
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
        serverSocket.Bind(endPoint);
        serverSocket.Listen(1000);

        SocketAsyncEventArgs arg = new SocketAsyncEventArgs();
        arg.Completed += AcceptCompleted;
        bool pending = serverSocket.AcceptAsync(arg);
        if (pending == false)
        {
            AcceptCompleted(serverSocket, arg);
        }

        while (true)
        {
            Thread.Sleep(1000);
        }
    }

    private static void AcceptCompleted(object? sender, SocketAsyncEventArgs e)
    {
        Socket serverSocket = (Socket)sender;
        Socket clientSocket = e.AcceptSocket;
        Console.WriteLine(clientSocket.RemoteEndPoint);

        e.AcceptSocket = null;
        bool pending = serverSocket.AcceptAsync(e);
        if (pending == false)
        {
            AcceptCompleted(serverSocket, e);
        }
    }
}