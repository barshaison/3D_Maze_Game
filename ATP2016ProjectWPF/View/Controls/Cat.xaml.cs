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
    /// Interaction logic for game player
    /// </summary>
    public partial class Cat : UserControl
    {
        /// <summary>
        /// construct a game player
        /// </summary>
        /// <param name="width">player's width</param>
        /// <param name="height">player's height</param>
        public Cat(double width,double height)
        {
            InitializeComponent();
            rec_name.Width = width;
            rec_name.Height = height;
        }
    }
}
