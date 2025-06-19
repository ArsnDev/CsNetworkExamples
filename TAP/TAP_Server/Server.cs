using System.Text;
using System.Net;
using System.Net.Sockets;

namespace TAP_Server;
internal class Server
{
    static async Task Main(string[] args)
    {
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        EndPoint endPoint = new IPEndPoint(IPAddress.Any, 12345);
        serverSocket.Bind(endPoint);
        serverSocket.Listen(1000);

        while (true)
        {
            Socket clientSocket = await serverSocket.AcceptAsync();
            Console.WriteLine($"Client connected: {clientSocket.RemoteEndPoint}");
            _ = HandleClientAsync(clientSocket);
        }
    }
    static async Task HandleClientAsync(Socket clientSocket)
    {
        var buffer = new byte[256];
        while(true)
        {
            int received = await clientSocket.ReceiveAsync(new ArraySegment<byte>(buffer), SocketFlags.None);
            if(received == 0)
            {
                break;
            }
            string receivedData = Encoding.UTF8.GetString(buffer, 0, received);
            Console.WriteLine($"Received from {clientSocket.RemoteEndPoint}: {receivedData}");
        }
        Console.WriteLine($"Client disconnected: {clientSocket.RemoteEndPoint}");
        clientSocket.Close();
    }
}