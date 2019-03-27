using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Core.Network
{
    public interface IConnection
    {
        Task Start(Int32 port);
    }

    internal class Server : IConnection
    {
        public async Task Start(Int32 port)
        {
            var listenSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            listenSocket.Bind(new IPEndPoint(IPAddress.Loopback, port));
            
            listenSocket.Listen(120);

            while (true)
            {
                var socket = await listenSocket.AcceptAsync();
                _ = ProcessLinesAsync(socket);
            }
        }

        private async Task ProcessLinesAsync(Socket socket)
        {
            Console.WriteLine($"[{socket.RemoteEndPoint}]: connected");

            var pipe = new Pipe();
            Task writing = new WritePipeAsync(socket, pipe.Writer).Run();
            Task reading = new ReadPipeAsync(socket, pipe.Reader).Run();

            await Task.WhenAll(reading, writing);

            Console.WriteLine($"[{socket.RemoteEndPoint}]: disconnected");
        }

    }
}