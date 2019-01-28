using ATP2016ProjectWPF.Model;
using ATP2016ProjectWPF.Presenter;
using ATP2016ProjectWPF.View;
using ATP2016ProjectWPF.View.Controls;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ATP2016ProjectWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// sets and connect the MVP layers
        /// </summary>
        /// <param name="e">event args</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            MVP();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            //MessageBox.Show("bye");
        }

        /// <summary>
        /// sets and connect the MVP layers
        /// </summary>
        private static void MVP()
        {
            IModel model = new MyModel();
            IView view = new MainWindow();
            MyPresenter presenter = new MyPresenter(model, view);
            model.load_MazeToSolution(); 
            view.Start();
        }
    }
}
