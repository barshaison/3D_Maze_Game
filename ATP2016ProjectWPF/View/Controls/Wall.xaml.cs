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
    /// Interaction logic for Wall control
    /// </summary>
    public partial class Wall : UserControl
    {
        /// <summary>
        /// constructs maze wall control
        /// </summary>
        /// <param name="width">wall's width</param>
        /// <param name="height">wall's height</param>
        public Wall(double width,double height)
        {
            InitializeComponent();
            rec_name.Width = width;
            rec_name.Height = height;
        }
    }
}
