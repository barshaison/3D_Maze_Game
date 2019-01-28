using Server.Model;
using Server.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controller
{
    class MyController : IController
    {
        private IModel m_model;
        private IView m_view;

        public MyController() { }

        public void SetModel(IModel model)
        {
            m_model = model;
        }
        
        public void SetView(IView view)
        {
            m_view = view;
        }
        public void startServer()
        {
            m_model.StartServer();
        }

        public void stopServer()
        {
            m_model.StopServer();
        }
    }
}
