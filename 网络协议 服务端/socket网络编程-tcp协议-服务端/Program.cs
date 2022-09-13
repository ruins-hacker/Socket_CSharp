using System;
using System.Net.Sockets;
using System.Net;
using System.Collections;
using System.Collections.Generic;

namespace socket网络编程_tcp协议_服务端
{
    class Program
    {
        static List<Client> clientList = new List<Client>();
        static void Main(string[] args)
        {
            Socket socketServer = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


            socketServer.Bind(new IPEndPoint(IPAddress.Parse("192.168.0.106"), 7788));

            socketServer.Listen(100);
            Console.WriteLine("Server is running");
            while(true)
            {
                Socket clientSocket = socketServer.Accept();
                Console.WriteLine("A client is Connected");
                Client client = new Client(clientSocket);

                clientList.Add(client);
                
            }
        }

        public static void BroadcastMessage(string message) {

            var unconnectedList = new List<Client>();
            for(int i = 0;i < clientList.Count;i++)
            {
                if (clientList[i].clientSocket.Connected)
                {
                    clientList[i].SendMessage(message);
                }
                else
                {
                    unconnectedList.Add(clientList[i]);
                }
            }
            //foreach(var item in clientList)
            //{
            //    if(item.clientSocket.Connected)
            //    {
            //        item.SendMessage();
            //    } else
            //    {
            //        unconnectedList.Add(item);
            //    }
            //}
            for (int i = 0; i < unconnectedList.Count; i++)
            {
                clientList.Remove(unconnectedList[i]);
            }
            //foreach (var item in unconnectedList)
            //{
            //    clientList.Remove(item);
            //}
        }

    }
}
