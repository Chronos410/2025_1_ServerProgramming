﻿using System.Net.Sockets;
using System.Net;
using System.Text;

namespace TCP_SERVER
{
    internal class Program
    {
        const int SERVERPORT = 9000;
        const int BUFSIZE = 512;

        static void Main(string[] args)
        {
            int retval;

            Socket listen_sock = null;
            try
            {
                // 소켓 생성
                listen_sock = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);
                //listen_sock = new Socket(AddressFamily.InterNetworkV6,
                //    SocketType.Stream, ProtocolType.Tcp);

                // Bind()
                listen_sock.Bind(new IPEndPoint(IPAddress.Any, SERVERPORT));
                //listen_sock.Bind(new IPEndPoint(IPAddress.IPv6Any, SERVERPORT));

                // Listen()
                //listen_sock.Listen(Int32.MaxValue);
                listen_sock.Listen(2);

                // Listen 실습용 (backlog, 접속가능여부 확인)
                //while (true)
                //{
                //    Thread.Sleep(1000);
                //}
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(1);
            }

            // 데이터 통신에 사용할 변수
            Socket client_sock = null;
            IPEndPoint clientaddr = null;
            byte[] buf = new byte[BUFSIZE];

            while (true)
            {
                try
                {
                    // accept()
                    client_sock = listen_sock.Accept();

                    // 접속한 클라이언트 정보 출력
                    clientaddr = (IPEndPoint)client_sock.RemoteEndPoint;
                    Console.WriteLine(
                        "\n[TCP 서버] 클라이언트 접속: IP 주소={0}, 포트 번호={1}",
                        clientaddr.Address, clientaddr.Port);

                    // 클라이언트와 데이터 통신
                    while (true)
                    {
                        // 데이터 받기
                        retval = client_sock.Receive(buf, BUFSIZE, SocketFlags.None);
                        if (retval == 0) break;

                        // 받은 데이터 출력
                        Console.WriteLine("[TCP/{0}:{1}] {2}",
                            clientaddr.Address, clientaddr.Port,
                            Encoding.Default.GetString(buf, 0, retval));

                        // 데이터 보내기
                        client_sock.Send(buf, retval, SocketFlags.None);
                    }

                    // 소켓 닫기
                    client_sock.Close();
                    Console.WriteLine(
                        "[TCP 서버] 클라이언트 종료: IP 주소={0}, 포트 번호={1}",
                        clientaddr.Address, clientaddr.Port);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    break;
                }
            }

            // 소켓 닫기
            listen_sock.Close();
        }
    }
}
