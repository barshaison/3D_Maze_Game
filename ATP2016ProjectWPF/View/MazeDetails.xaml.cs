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
    /// Interaction logic for MazeDetails.xaml
    /// </summary>
    public partial class MazeDetails : Window
    {
        /// <summary>
        /// maze name
        /// </summary>
        private string m_mazeName;

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
        /// width
        /// </summary>
        private string m_width;

        /// <summary>
        /// width property
        /// </summary>
        public string Width1
        {
            get
            {
                return m_width;
            }

            set
            {
                m_width = value;
            }
        }
        /// <summary>
        /// height
        /// </summary>
        private string m_height;

        /// <summary>
        /// height property
        /// </summary>
        public string Height1
        {
            get
            {
                return m_height;
            }

            set
            {
                m_height = value;
            }
        }
        /// <summary>
        /// layers
        /// </summary>
        private string m_layers;

        /// <summary>
        /// layers property
        /// </summary>
        public string Layers
        {
            get
            {
                return m_layers;
            }

            set
            {
                m_layers = value;
            }
        }

        /// <summary>
        /// initialize the window
        /// </summary>
        public MazeDetails()
        {
            InitializeComponent();
        }

        /// <summary>
        /// generate the maze
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void btn_generate_Click(object sender, RoutedEventArgs e)
        {
            m_mazeName = txtbx_mazeName.Text;
            m_width = txtbx_width.Text;
            m_height = txtbx_height.Text;
            m_layers = txtbx_layers.Text;
            int w = Int32.Parse(m_width);
            int h = Int32.Parse(m_height);
            int l = Int32.Parse(m_layers);
            if(w >50 || h > 50 || l>50)
            {
                MessageBox.Show("Warning: Mazes with sizes larger than 50 may function slow and have problems");
            }
            if(w != h)
            {
                MessageBox.Show("It's recommended that the width and height would be equal");
            }
            this.Close();
        }

        /// <summary>
        /// cancels operation
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            m_mazeName = "x";
            m_width = "x";
            m_height = "x";
            m_layers = "x";
            this.Close();
        }
    }
}
