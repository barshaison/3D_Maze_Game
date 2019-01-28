using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ATP2016ProjectWPF.View.Controls
{
    /// <summary>
    /// Interaction logic for maze exit
    /// </summary>
    public partial class Exit : UserControl
    {
        /// <summary>
        /// constructs exit control
        /// </summary>
        /// <param name="width">exit's width</param>
        /// <param name="height">exit's height</param>
        public Exit(double width,double height)
        {
            InitializeComponent();
            exit_name.Width = width;
            exit_name.Height = height;
        }
    }
}
