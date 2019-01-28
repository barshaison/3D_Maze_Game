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
    /// Interaction logic for Load Maze command
    /// </summary>
    public partial class LoadMazeDetails : Window
    {
        /// <summary>
        /// path from where to load
        /// </summary>
        private string m_path;
        /// <summary>
        /// name of maze to be saved
        /// </summary>
        private string m_mazeName;
        /// <summary>
        /// Path property
        /// </summary>
        public string Path
        {
            get
            {
                return m_path;
            }

            set
            {
                m_path = value;
            }
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
        /// initializes the window
        /// </summary>
        public LoadMazeDetails()
        {
            InitializeComponent();
        }
        /// <summary>
        /// cancel btn event handker
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            m_mazeName = "$";
            m_path = "$";
            this.Close();
        }
        /// <summary>
        /// sets the typed data
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void btn_load_Click(object sender, RoutedEventArgs e)
        {
            m_mazeName = txtbx_mazeName.Text;
            m_path = txtbx_path.Text;
            this.Close();
        }
    }
}
