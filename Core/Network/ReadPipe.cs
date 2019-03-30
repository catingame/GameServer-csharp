using System;
using System.Buffers;
using System.IO.Pipelines;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Core.Network
{
    internal class ReadPipeAsync
    {
        private Socket _socket;
        private PipeReader _reader;
        public ReadPipeAsync(Socket socket, PipeReader reader)
        {
            _socket = socket;
            _reader = reader;
        }

        public async Task Run()
        {
            while (true)
            {
                ReadResult result = await _reader.ReadAsync();

                ReadOnlySequence<byte> buffer = result.Buffer;
                SequencePosition? position = null;

                do
                {
                    // Find the EOL
                    position = buffer.PositionOf((byte)'\n');

                    if (position != null)
                    {
                        var line = buffer.Slice(0, position.Value);
                        ProcessLine(_socket, line);

                        // This is equivalent to position + 1
                        var next = buffer.GetPosition(1, position.Value);

                        // Skip what we've already processed including \n
                        buffer = buffer.Slice(next);
                    }
                }
                while (position != null);

                // We sliced the buffer until no more data could be processed
                // Tell the PipeReader how much we consumed and how much we left to process
                _reader.AdvanceTo(buffer.Start, buffer.End);

                if (result.IsCompleted)
                {
                    break;
                }
            }

            _reader.Complete();
        }

        private static void ProcessLine(Socket socket, in ReadOnlySequence<byte> buffer)
        {
            // @something
            // @test...
            Console.Write($"[{socket.RemoteEndPoint}]: ");
            foreach (var segment in buffer)
            {
                Console.Write(Encoding.UTF8.GetString(segment.Span));
            }
            Console.WriteLine();
        }
    }
}