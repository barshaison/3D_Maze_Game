using Server.Model;
using Server.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Controller
{
    interface IController
    {
        void startServer();
        void stopServer();
        void SetModel(IModel model);
        void SetView(IView view);
    }
}
