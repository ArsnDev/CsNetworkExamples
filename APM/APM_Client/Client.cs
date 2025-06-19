using System.Net;
using System.Net.Sockets;
using System.Text;
namespace APM_Client;

internal class Client
{
    static void Main(string[] args)
    {
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        EndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
        socket.BeginConnect(endPoint, ConnectCompleted, socket);

        while(true)
        {
            Thread.Sleep(1000);
        }
    }
    private static void ConnectCompleted(IAsyncResult ar)
    {
        Socket socket = ar.AsyncState as Socket;

        socket.EndConnect(ar);

        string str = Console.ReadLine();
        byte[] buffer = Encoding.UTF8.GetBytes(str);
        socket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, SendCompleted, socket);
    }
    private static void SendCompleted(IAsyncResult ar)
    {
        Socket socket = ar.AsyncState as Socket;
        socket.EndSend(ar);
        string newStr = Console.ReadLine();
        byte[] newbuffer = Encoding.UTF8.GetBytes(newStr);
        socket.BeginSend(newbuffer, 0, newbuffer.Length, SocketFlags.None, SendCompleted, socket);
    }
}
