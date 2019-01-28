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
using System.Windows.Shapes;

namespace ATP2016ProjectWPF.View
{
    /// <summary>
    /// Interaction logic for Display SolDetails Window
    /// </summary>
    public partial class DisplaySolDetails : Window
    {
        /// <summary>
        /// maze name
        /// </summary>
        private string m_mazeName;
        /// <summary>
        /// initializes the window
        /// </summary>
        public DisplaySolDetails()
        {
            InitializeComponent();
        }
        /// <summary>
        /// maze name property
        /// </summary>
        public string MazeName
        {
            get
            {
                return m_mazeName;
            }

            set
            {
                m_mazeName = value;
            }
        }
        /// <summary>
        /// cancel clicked event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            MazeName = "$";
            this.Close();
        }
        /// <summary>
        /// display btn click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_display_Click(object sender, RoutedEventArgs e)
        {
            m_mazeName = txtbx_mazeName.Text;
            MessageBox.Show("Instructions:\nGreen circle --> Move at this floor\nBlue Circle --> Go up\nBrown circle-->Go down");
           
            this.Close();
        }
    }
}
