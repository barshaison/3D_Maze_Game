using Server.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.View
{
    class CLI:IView
    {
        private IController m_controler;

        public CLI(IController controler)
        {
            m_controler = controler;
        }

        public void Start()
        {
            Console.WriteLine("Server CLI started!\n");
            Console.WriteLine("Available Commands:\nstartServer\nstopserver\nexit\n\n");
            string command;
            while (true)
            {
                Console.Write(">>");
                command = Console.ReadLine();
                if (command == "exit")
                {
                    break;
                }
                else if(command == "startServer")
                {
                    m_controler.startServer();
                }
                else if(command == "stopServer")
                {
                    m_controler.stopServer();
                }
                else
                {
                    Console.WriteLine("Unrecognized Command!");
                }
            }
        }
    }
}
