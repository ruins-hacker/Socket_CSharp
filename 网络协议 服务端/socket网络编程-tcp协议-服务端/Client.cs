using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;

namespace socket网络编程_tcp协议_服务端
{
    public class Client
    {
        public Socket clientSocket;
        private string message;
        private byte[] data = new byte[1024];
        public Client(Socket clientSocket)
        {
            this.clientSocket = clientSocket;
            Thread thread = new Thread(() =>
            {
                while(true)
                {
                    if (clientSocket.Poll(10,SelectMode.SelectRead))
                    {
                        clientSocket.Close();
                        break;
                    }
                    int length = clientSocket.Receive(data);
                    message = Encoding.UTF8.GetString(data, 0, length);
                    //广播消息
                    Program.BroadcastMessage(message);



                    Console.WriteLine("收到了消息" + message);

                }
            });
            thread.Start();
        }

        public void SendMessage(string message)
        {
            if(clientSocket.Connected == false || message == null || message == "")
            {
                return;
            }
            clientSocket.Send(Encoding.UTF8.GetBytes(message));

        }
    }
}
