using System.Net.Sockets;

using System.Text;

namespace _12th
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Socket sock = null;

            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            string SVR_IP = "127.0.0.1";
            int SVR_PORT = 9000;

            sock.Connect(SVR_IP, SVR_PORT);

            byte[] SendData = Encoding.UTF8.GetBytes("Hi Im Youngsu");
            int sizeData = SendData.Length;

            sock.Send(SendData, 0, sizeData, SocketFlags.None);

            sock.Close();
            Console.Read();
            

        }
    }
}
