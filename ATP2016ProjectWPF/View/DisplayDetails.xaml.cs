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
    /// Interaction logic for Display window
    /// </summary>
    public partial class DisplayDetails : Window
    {
        /// <summary>
        /// maze's name
        /// </summary>
        private string m_mazeName;
        /// <summary>
        /// initialize window
        /// </summary>
        public DisplayDetails()
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
        /// cancel click eventhandler
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event arg</param>
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            m_mazeName = "$";
            this.Close();
        }
        /// <summary>
        /// display click event handler
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">evenr arg</param>
        private void btn_display_Click(object sender, RoutedEventArgs e)
        {
            m_mazeName = txtbx_mazeName.Text;
            this.Close();
        }
    }
}
