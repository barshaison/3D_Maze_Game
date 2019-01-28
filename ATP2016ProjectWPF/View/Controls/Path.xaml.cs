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
    /// Interaction logic for Path control
    /// </summary>
    public partial class Path : UserControl
    {
        /// <summary>
        /// constructs path control
        /// </summary>
        /// <param name="width">path width</param>
        /// <param name="height">path height</param>
        public Path(double width, double height)
        {
            InitializeComponent();
            path_name.Width = width;
            path_name.Height = height;
        }
    }
}
