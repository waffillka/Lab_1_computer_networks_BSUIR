using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TcpClient
{

    class Program
    {
        static int port = 8005;
        static string address = "127.0.0.1";
        static void Main(string[] args)
        {
            try
            {
                IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(address), port);
                Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(ipPoint);

                Console.Write("Введите сообщение:");
                string message = Console.ReadLine();
                byte[] data = Encoding.Unicode.GetBytes(message);

                socket.Send(data);

                data = new byte[256];
                StringBuilder builder = new StringBuilder();
                int bytes = 0;

                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (socket.Available > 0);

                Console.WriteLine("Сервер: " + builder.ToString());

                socket.Shutdown(SocketShutdown.Both);
                socket.Close();

            }
            catch (Exception error)
            {
                Console.WriteLine(error.Message);
            }
            Console.Read();
        }
    }
}