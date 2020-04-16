using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

//https://nowonbun.tistory.com/155https://nowonbun.tistory.com/155

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello World!");
        ServerNet server = new ServerNet();
    }
}


public class ServerNet
{
    public ServerNet()
    {
        IPEndPoint ip_point = new IPEndPoint(IPAddress.Any, 9999);
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        socket.Bind(ip_point);
        socket.Listen(20);

        while (true)
        {
            Socket client_socket = socket.Accept();
            Thread client_thread = new Thread(() => RunClient(ref client_socket));
            client_thread.Start();
        }
    }



    void RunClient(ref Socket socket)
    {
        Console.WriteLine("Socket Connect");
        while (true)
        {
            Byte[] data = new Byte[1024];
            socket.Receive(data);
            String msg = Encoding.Default.GetString(data);
            Console.WriteLine("수신 : " + msg);
            String send_msg = "response : " + msg;
            Byte[] send_data = new Byte[1024];
            send_data = Encoding.Default.GetBytes(send_msg);
            socket.Send(send_data);
        }
        socket.Close();
        socket.Close();
    }



    public void ServerNet2()
    {
        IPEndPoint ip_point = new IPEndPoint(IPAddress.Any, 9999);
        Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
        socket.Bind(ip_point);
        socket.Listen(20);
        Socket client_socket = socket.Accept();

        IPEndPoint clinet_ip_point = (IPEndPoint)client_socket.RemoteEndPoint;
        Console.WriteLine("Socket Connect");

        while (true)
        {
            Byte[] data = new Byte[1024];
            client_socket.Receive(data);
            String msg = Encoding.Default.GetString(data);
            Console.WriteLine("수신 : " + msg);
            String send_msg = "response : " + msg;

            Byte[] send_data = new Byte[1024];
            send_data = Encoding.Default.GetBytes(send_msg);
            client_socket.Send(send_data);
        }
        client_socket.Close();
        socket.Close();
    }
}