using System.Net;
using System.Net.Sockets;
using System.Text;

namespace EAP_Client;
internal class Client
{
    static void Main(string[] args)
    {
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);

        SocketAsyncEventArgs arg = new SocketAsyncEventArgs();
        arg.RemoteEndPoint = endPoint;
        arg.Completed += ConnectCompleted;

        bool pending = socket.ConnectAsync(arg);
        if(pending == false)
        {
            ConnectCompleted(socket, arg);
        }

        while(true)
        {
            Thread.Sleep(1000);
        }
    }
    private static void ConnectCompleted(object? sender, SocketAsyncEventArgs e)
    {
        Socket socket = (Socket)sender;
        e.Dispose();

        string str = Console.ReadLine();
        byte[]buffer = Encoding.UTF8.GetBytes(str);

        SocketAsyncEventArgs arg = new SocketAsyncEventArgs();
        arg.SetBuffer(buffer, 0, buffer.Length);
        arg.Completed += SendCompleted;
        bool pending = socket.SendAsync(arg);
        if(pending == false)
        {
            SendCompleted(socket, arg);
        }

    }
    private static void SendCompleted(object? sender, SocketAsyncEventArgs e)
    {
        Socket socket = (Socket)sender;

        string str = Console.ReadLine();
        byte[] buffer = Encoding.UTF8.GetBytes(str);

        e.SetBuffer(buffer);

        bool pending = socket.SendAsync(e);
        if(pending == false)
        {
            SendCompleted(socket, e);
        }
    }
}