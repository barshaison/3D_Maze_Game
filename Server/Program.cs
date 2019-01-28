using Server.Controller;
using Server.Model;
using Server.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            MVC();
        }

        private static void MVC()
        {
            IController controller = new MyController();
            IModel model = new MyModel(controller);
            controller.SetModel(model);
            IView view = new CLI(controller);
            controller.SetView(view);
            view.Start();
        }
    }
}
