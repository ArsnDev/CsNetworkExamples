using System.Net;
using System.Net.Sockets;
using System.Text;

namespace APM_Server;

internal class Server
{
    static void Main(string[] args)
    {
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        EndPoint endPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 12345);
        serverSocket.Bind(endPoint);
        serverSocket.Listen(1000);

        // Accept
        serverSocket.BeginAccept(AcceptCompleted, serverSocket);

        while (true)
        {
            Thread.Sleep(1000);
        }
    }
    private static void AcceptCompleted(IAsyncResult ar)
    {
        Socket serverSocket = ar.AsyncState as Socket;
        Socket clientSocket = serverSocket.EndAccept(ar);
        Console.WriteLine($"클라이언트 연결됨: {clientSocket.RemoteEndPoint}");

        serverSocket.BeginAccept(AcceptCompleted, serverSocket);

        byte[] buffer = new byte[256];
        clientSocket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, ReceiveCompleted, (clientSocket, buffer));
    }
    private static void ReceiveCompleted(IAsyncResult ar)
    {
        (Socket clientSocket, byte[] buffer) = ((Socket, byte[]))ar.AsyncState;
        int n = clientSocket.EndReceive(ar);
        if(n < 1)
        {
            Console.WriteLine("client disconnect");
            clientSocket.Dispose();
            return;
        }
        Console.WriteLine(Encoding.UTF8.GetString(buffer));
        byte[] newBuffer = new byte[256];
        clientSocket.BeginReceive(newBuffer, 0, newBuffer.Length, SocketFlags.None, ReceiveCompleted, (clientSocket, newBuffer));
    }
}