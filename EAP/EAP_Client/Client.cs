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
        socket.Connect(endPoint);

        while (true)
        {
            string str = Console.ReadLine();
            socket.Send(Encoding.UTF8.GetBytes(str));
        }
    }
}