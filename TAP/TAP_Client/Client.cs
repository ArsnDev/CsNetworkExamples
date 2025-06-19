using System.Text;
using System.Net;
using System.Net.Sockets;

namespace TAP_Client;
internal class Client
{
    static async Task Main(string[] args)
    {
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        EndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
        await socket.ConnectAsync(endPoint);
        while (true)
        {
            string str = Console.ReadLine();
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            await socket.SendAsync(buffer, SocketFlags.None);
        }
    }
}