using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Text;

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

        SocketAsyncEventArgs arg = new SocketAsyncEventArgs();
        arg.Completed += ReceiveCompleted;
        byte[] buffer = new byte[256];
        arg.SetBuffer(buffer, 0, buffer.Length);
        bool pending_2 = clientSocket.ReceiveAsync(arg);
        if (pending_2 == false)
        {
            ReceiveCompleted(clientSocket, arg);
        }
    }
    private static void ReceiveCompleted(object? sender, SocketAsyncEventArgs e)
    {
        Socket clientSocket = (Socket)sender;
        if(e.BytesTransferred < 1)
        {
            Console.WriteLine("client disconnect");
            clientSocket.Dispose();
            e.Dispose();
            return;
        }
        Console.WriteLine(Encoding.UTF8.GetString(e.Buffer));
        byte[] buffer = new byte[256];
        e.SetBuffer(buffer, 0, buffer.Length);
        bool pending = clientSocket.ReceiveAsync(e);
        if (pending == false)
        {
            ReceiveCompleted(clientSocket, e);
        }
    }
}