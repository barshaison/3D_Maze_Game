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
    /// Interaction logic for SolutionDetails.xaml
    /// </summary>
    public partial class SolutionDetails : Window
    {
        /// <summary>
        /// maze name
        /// </summary>
        private string m_mazeName;
        /// <summary>
        /// algorithm to solve with
        /// </summary>
        private string m_algorithm;
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
        /// algo property
        /// </summary>
        public string Algorithm
        {
            get
            {
                return m_algorithm;
            }

            set
            {
                m_algorithm = value;
            }
        }
        /// <summary>
        /// init the window
        /// </summary>
        public SolutionDetails()
        {
            InitializeComponent();
        }

        /// <summary>
        /// cancels solution operation
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            m_mazeName = "$";
            m_algorithm = "$";
            this.Close();
        }

        /// <summary>
        /// solves the maze
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void btn_solve_Click(object sender, RoutedEventArgs e)
        {
            m_mazeName = txtbx_mazeName.Text;
            m_algorithm = "BFS";
            this.Close();
        }

        /// <summary>
        /// sets algo from user chose
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void bfs_selected(object sender, RoutedEventArgs e)
        {
            m_algorithm = bfs.Content.ToString();
        }

        /// <summary>
        /// sets algo from user chose
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void dfs_selected(object sender, RoutedEventArgs e)
        {
            m_algorithm = dfs.Content.ToString();
        }
    }
}
