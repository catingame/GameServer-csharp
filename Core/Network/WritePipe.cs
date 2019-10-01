using System;
using System.Net.Sockets;
using System.IO.Pipelines;
using System.Threading.Tasks;

namespace Core.Network
{
    internal class WritePipeAsync
    {
        private readonly Socket _socket;
        private readonly PipeWriter _writer;
        public WritePipeAsync(Socket socket, PipeWriter writer)
        {
            _socket = socket;
            _writer = writer;
        }

        public async Task Run()
        {
            const int minimumBufferSize = 512;

            while (true)
            {
                try
                {
                    // Request a minimum of 512 bytes from the PipeWriter
                    Memory<byte> memory = _writer.GetMemory(minimumBufferSize);

                    int bytesRead = await _socket.ReceiveAsync(memory, SocketFlags.None);
                    if (bytesRead == 0)
                    {
                        break;
                    }

                    // Tell the PipeWriter how much was read
                    _writer.Advance(bytesRead);
                }
                catch
                {
                    break;
                }

                // Make the data available to the PipeReader
                FlushResult result = await _writer.FlushAsync();

                if (result.IsCompleted)
                {
                    break;
                }
            }

            // Signal to the reader that we're done writing
            _writer.Complete();
        }
    }
}