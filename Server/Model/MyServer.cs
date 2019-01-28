using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Model
{
    public delegate void ClientHandler(Stream rw);

    class MyServer
    {
        int m_port;
        int m_listeningInterval;
        ClientHandler m_clientHandler;
        volatile bool m_stop;

        public MyServer(int port, int listeningInterval, int numOfThreads, ClientHandler clientHandler)
        {
            m_port = port;
            m_clientHandler = clientHandler;
            ThreadPool.SetMaxThreads(numOfThreads, 0);
            m_listeningInterval = listeningInterval;
            m_stop = false;
        }

        public void Start()
        {
            Console.WriteLine("server started!\n");
            new Thread(() =>
            {
                IPAddress ipAddress = new IPAddress(new byte[] { 127, 0, 0, 1 });
                TcpListener listener = new TcpListener(ipAddress, m_port);
                listener.Start();
                while (!m_stop)
                {
                    if (listener.Pending())
                    {
                        ThreadPool.QueueUserWorkItem((Object o) =>
                        {
                            using (Socket client = listener.AcceptSocket())
                            {
                                using (Stream clientStream = new NetworkStream(client))
                                {
                                    m_clientHandler(clientStream);
                                }
                            }
                        });
                    }
                    else
                    {
                        Thread.Sleep(m_listeningInterval);
                    }
                }
            }).Start();
        }

        public void Stop()
        {
            m_stop = true;
        }
    }
}
