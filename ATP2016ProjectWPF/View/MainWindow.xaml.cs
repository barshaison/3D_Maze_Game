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
using ATP2016Library.Search;
using ATP2016Library.MazeGenerators;
using ATP2016Library.Compression;
using ATP2016ProjectWPF.Model;
using ATP2016ProjectWPF.View;
using ATP2016ProjectWPF.Presenter;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using ATP2016ProjectWPF.View.Controls;
using System.ComponentModel;

namespace ATP2016ProjectWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,IView
    {
        /// <summary>
        /// niew changed event handler
        /// </summary>
        public event viewEventDelegate ViewChanged;

        
        /// <summary>
        /// initializes the main window
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            displayMaze_name.IsEnabled = false;
            displaySol_name.IsEnabled = false;
            solveMaze_name.IsEnabled = false;
            curr_maze.Visibility = System.Windows.Visibility.Hidden;
            CurrMaze.Visibility = Visibility.Hidden;
            curr_floor.Visibility = Visibility.Hidden;
            CurrFloor.Visibility = Visibility.Hidden;
            upFloor.Visibility = Visibility.Hidden;
            up_floor.Visibility =Visibility.Hidden;
            down_floor.Visibility = Visibility.Hidden;
            downFloor.Visibility = Visibility.Hidden;
        }
        /// <summary>
        /// current maze to play on
        /// </summary>
        private Maze3d m_mazeToPlayOn;
        /// <summary>
        /// current player position: x axis
        /// </summary>
        private int m_x;
        /// <summary>
        /// current player position: y axis
        /// </summary>
        private int m_y;
        /// <summary>
        /// current player position: layer
        /// </summary>
        private int m_layer;
        /// <summary>
        /// draw solution flag
        /// </summary>
        private bool solutionOn=false;
        /// <summary>
        /// current solution 
        /// </summary>
        private Solution m_sol;

        /// <summary>
        /// reads maze name from user input
        /// </summary>
        /// <returns>maze name</returns>
        public string GetMazeName()
        {
            DisplayDetails dd = new DisplayDetails();
            dd.ShowDialog();
            string mazeName = dd.MazeName;
            CurrMaze.Text ="        " +mazeName;
            curr_maze.Visibility = System.Windows.Visibility.Visible;
            CurrMaze.Visibility = Visibility.Visible;
            curr_floor.Visibility = Visibility.Visible;
            CurrFloor.Visibility = Visibility.Visible;

            return mazeName;
        }

        /// <summary>
        /// reads maze parameters from user input
        /// </summary>
        /// <returns>maze parameters</returns>
        public string getMazeParameters()
        {
            MazeDetails mazeDetailsWindow = new MazeDetails();
            mazeDetailsWindow.ShowDialog();
            string mazeName = mazeDetailsWindow.MazeName;
            string width = mazeDetailsWindow.Width1;
            string height = mazeDetailsWindow.Height1;
            string layers = mazeDetailsWindow.Layers;

            return mazeName+ " " + width + " " + height + " " + layers;
        }

        /// <summary>
        /// reads parameters for the load command from user input
        /// </summary>
        /// <returns>load maze parameters</returns>
        public string GetParamsForLoadMaze()
        {
            LoadMazeDetails details = new LoadMazeDetails();
            details.ShowDialog();
            string path = details.Path;
            string mazeName = details.MazeName;
            return path + " " + mazeName;
        }

        /// <summary>
        /// reads parameters for the save command from user input
        /// </summary>
        /// <returns>save maze parameters</returns>
        public string GetParamsForSaveMaze()
        {
            SaveMazeDetails saveMazeDetails = new SaveMazeDetails();
            saveMazeDetails.ShowDialog();
            string mazeName = saveMazeDetails.MazeName;
            string path = saveMazeDetails.Path;
            return mazeName + " " + path;
        }

        /// <summary>
        /// reads parameters for the solve command from user input
        /// </summary>
        /// <returns>solve maze parameters</returns>
        public string GetParamsForSolveMaze()
        {
            SolutionDetails sd = new SolutionDetails();
            sd.ShowDialog();
            string mazeName = sd.MazeName;
            string algo = sd.Algorithm;
            return mazeName + " " + algo;
        }

        /// <summary>
        /// output msg to the screen
        /// </summary>
        /// <param name="output">msg to output</param>
        public void Output(string output)
        {
            MessageBox.Show(output);
        }

        /// <summary>
        /// displays the maze on the screen
        /// </summary>
        /// <param name="maze">maze to display</param>
        public void outputMaze(Maze3d maze)
        {
            try {
                m_mazeToPlayOn = maze;
                m_sol = null;
                solutionOn = false;
                m_x = maze.getStartPosition().Axis[0];
                m_y = maze.getStartPosition().Axis[1];
                cnvs_main.Children.Clear();
                MazeDisplayer mazeDisplayer = new MazeDisplayer(maze, m_x, m_y, 0, solutionOn, m_sol,400,400);
                cnvs_main.Children.Add(mazeDisplayer);
              // Canvas.SetLeft(mazeDisplayer, 0);
              // Canvas.SetTop(mazeDisplayer, 0);
                //Grid.SetRow(mazeDisplayer, 20);
                //Grid.SetColumn(mazeDisplayer, 20);
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// redraws maze
        /// </summary>
        /// <param name="maze">maze to draw</param>
        /// <param name="x">player x position</param>
        /// <param name="y">player y position</param>
        /// <param name="layer">player layer position</param>
        private void reDrawMaze(Maze3d maze,int x,int y, int layer)
        {
            cnvs_main.Children.Clear();
            MazeDisplayer mazeDisplayer = new MazeDisplayer(maze, x, y,layer,solutionOn,m_sol,400,400);
            cnvs_main.Children.Add(mazeDisplayer);
            Canvas.SetLeft(mazeDisplayer, 0);
            Canvas.SetTop(mazeDisplayer, 0);
        }

        /// <summary>
        /// drawsthe solution on the maze board
        /// </summary>
        /// <param name="sol">solution to current maze</param>
        public void outputSolution(Solution sol)
        {
            try
            {
                solutionOn = true;
                m_sol = sol;
                reDrawMaze(m_mazeToPlayOn, m_x, m_y, m_layer);
            }
            catch (Exception) { }
        }

       
        /// <summary>
        /// runs the GUI
        /// </summary>
        public void Start()
        {
            this.Show();
        }

        /// <summary>
        /// generate maze event handler
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void btn_GenerateMaze_Click(object sender, RoutedEventArgs e)
        {
            displayMaze_name.IsEnabled = true;
            solveMaze_name.IsEnabled = true;
            ViewChanged("Generate3dMaze");
        }

        /// <summary>
        /// reads maze details from user input
        /// </summary>
        public void getMazeDetails()
        {
            MazeDetails mazeDetailsWindow = new MazeDetails();
            mazeDetailsWindow.ShowDialog();
        }

        /// <summary>
        /// solve maze event handler
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void btn_SolveMaze(object sender, RoutedEventArgs e)
        {
            displaySol_name.IsEnabled = true;
            ViewChanged("SolveMaze");
        }

        /// <summary>
        /// sends notification to presenter when button save maze pressed
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">evenr args</param>
        private void btn_saveMaze_click(object sender, RoutedEventArgs e)
        {
            ViewChanged("SaveMaze");
        }

        /// <summary>
        /// load maze event handler
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void btn_loadMaze_click(object sender, RoutedEventArgs e)
        {
            ViewChanged("LoadMaze");
        }

        /// <summary>
        /// display event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_display_click(object sender, RoutedEventArgs e)
        {
            ViewChanged("Display");
            CurrFloor.Text = "         0" ;
            checkMovmentUpDown();
        }

        /// <summary>
        /// key down event handler
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            var key = e.Key;

            switch (key)
            {
                case Key.PageDown:
                    MoveDownLayer();
                    break;
                case Key.PageUp:
                    MoveUpLayer();
                    break;
                case Key.Down:
                    MoveDown();
                    break;
                case Key.Up:
                    MoveUp();
                    break;
                case Key.Right:
                    MoveRight();
                    break;
                case Key.Left:
                    MoveLeft();
                    break;
            }
        }

        /// <summary>
        /// checks if player got to goal point
        /// </summary>
        private void checkIfSolved()
        {
            if(m_x == m_mazeToPlayOn.getGoalPosition().Axis[0] && m_y== m_mazeToPlayOn.getGoalPosition().Axis[1]
                && m_layer == m_mazeToPlayOn.getGoalPosition().Axis[2])
            {
                Video vid = new Video();
                vid.Show();
            }
        }

        /// <summary>
        /// player move down
        /// </summary>
        private void MoveDownLayer()
        {
            if (m_layer > 0 && m_mazeToPlayOn.TwoDMazes[m_layer - 1].Board[m_y, m_x] == 0)
            {
                down_floor.Visibility = Visibility.Visible;
                downFloor.Visibility = Visibility.Visible;
                m_layer--;
                reDrawMaze(m_mazeToPlayOn, m_x, m_y, m_layer);
                CurrFloor.Text = "       " + m_layer;
                checkMovmentUpDown();

            }
            checkIfSolved();
        }

        /// <summary>
        /// player move up
        /// </summary>
        private void MoveUpLayer()
        {
            if (m_layer < m_mazeToPlayOn.TwoDMazes.Length-1 && m_mazeToPlayOn.TwoDMazes[m_layer+1].Board[m_y, m_x] == 0)
            {
                upFloor.Visibility = Visibility.Visible;
                up_floor.Visibility = Visibility.Visible;
                m_layer++;
                reDrawMaze(m_mazeToPlayOn, m_x, m_y, m_layer);
                CurrFloor.Text = "       " + m_layer;
                checkMovmentUpDown();
            }
            checkIfSolved();
        }
        /// <summary>
        /// checks if player moved up/down
        /// </summary>
        private void checkMovmentUpDown()
        {
            try
            {
                checkMovmentDown();
                checkMovmentup();
            }
            catch (Exception) { }
        }
        /// <summary>
        /// checks if player moved up
        /// </summary>
        private void checkMovmentup()
        {
            if (m_layer < m_mazeToPlayOn.TwoDMazes.Length - 1 && m_mazeToPlayOn.TwoDMazes[m_layer + 1].Board[m_y, m_x] == 0)
            {
                upFloor.Visibility = Visibility.Visible;
                up_floor.Visibility = Visibility.Visible;
            }
            else
            {
                upFloor.Visibility = Visibility.Hidden;
                up_floor.Visibility = Visibility.Hidden;
            }
        }
        /// <summary>
        /// checks if player moved down
        /// </summary>
        private void checkMovmentDown()
        {
            if (m_layer > 0 && m_mazeToPlayOn.TwoDMazes[m_layer - 1].Board[m_y, m_x] == 0)
            {

                down_floor.Visibility = Visibility.Visible;
                downFloor.Visibility = Visibility.Visible;
            }
            else
            {
                down_floor.Visibility = Visibility.Hidden;
                downFloor.Visibility = Visibility.Hidden;
            }
        }
        /// <summary>
        /// player move left
        /// </summary>
        private void MoveLeft()
        {
            try {
                if (m_x > 0 && m_mazeToPlayOn.TwoDMazes[m_layer].Board[m_y, m_x - 1] == 0)
                {
                    m_x--;
                    reDrawMaze(m_mazeToPlayOn, m_x, m_y, m_layer);
                    checkMovmentUpDown();
                }

                checkIfSolved();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// player move right
        /// </summary>
        private void MoveRight()
        {
            try
            {
                if (m_x < m_mazeToPlayOn.TwoDMazes[m_layer].Board.GetLength(1) && m_mazeToPlayOn.TwoDMazes[m_layer].Board[m_y, m_x + 1] == 0)
                {
                    m_x++;
                    reDrawMaze(m_mazeToPlayOn, m_x, m_y, m_layer);
                    checkMovmentUpDown();
                }

                checkIfSolved();
            }
            catch (Exception) { }
        }
        /// <summary>
        /// player move up
        /// </summary>
        private void MoveUp()
        {
            try
            {
                if (m_y > 0 && m_mazeToPlayOn.TwoDMazes[m_layer].Board[m_y - 1, m_x] == 0)
                {
                    m_y--;
                    reDrawMaze(m_mazeToPlayOn, m_x, m_y, m_layer);
                    checkMovmentUpDown();
                }
                checkIfSolved();
            }
            catch (Exception)
            {
            }
        }
        
        /// <summary>
        /// plater move down
        /// </summary>
        private void MoveDown()
        {
            try
            {
                if (m_y < m_mazeToPlayOn.TwoDMazes[m_layer].Board.GetLength(0) && m_mazeToPlayOn.TwoDMazes[m_layer].Board[m_y + 1, m_x] == 0)
                {
                    m_y++;
                    reDrawMaze(m_mazeToPlayOn, m_x, m_y, m_layer);
                    checkMovmentUpDown();
                }

                checkIfSolved();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// sets maze to play on
        /// </summary>
        /// <param name="maze">maze to play on</param>
        public void setMazeToPlayOn(Maze3d maze)
        {
            try
            {
                m_mazeToPlayOn = maze;
                m_x = maze.getStartPosition().Axis[0];
                m_y = maze.getStartPosition().Axis[1];

                m_layer = 0;
                checkMovmentUpDown();
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// display solution event handler
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">evenr args</param>
        private void btn_displaySol_click(object sender, RoutedEventArgs e)
        {
            ViewChanged("DisplaySolution");
            MediaPlayer mplayer = new MediaPlayer();
            mplayer.Open(new Uri("soundtrack.mp3", UriKind.Relative));
            mplayer.Play();
        }   

        /// <summary>
        /// reads maze name from user input
        /// </summary>
        /// <returns>maze name</returns>
        public string GetMazeNameForSol()
        {
            DisplaySolDetails dd = new DisplaySolDetails();
            dd.ShowDialog();
            string mazeName = dd.MazeName;
            return mazeName;
        }
        /// <summary>
        /// exit button event handker: performs orgenaized exit from app
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void btn_exit_click(object sender, RoutedEventArgs e)
        {
            ViewChanged("stop");
            ViewChanged("saveToDic");
            this.Close();
        }
        /// <summary>
        /// shows app abouts
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void btn_about_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("This app was developed by Daniel Barshai 203194048 \n" +
                "The algorithm used to generate the mazes, is The Recursive Devision\n"+
                "The algorithms used to solve the mazes are BFS & DFS");
        }

        /// <summary>
        /// shows app properties
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void btn_prop_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("App Settings:\n\n" +"Number Of Threads in threadPool: " + Properties.Settings1.Default.numOfThreadsInThreadPool +
                "\n" + "Default Algorithm to solve maze: "+ Properties.Settings1.Default.algorithmToSolve);
        }

        /// <summary>
        /// exit from app
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void btn_X_click(object sender, CancelEventArgs e)
        {
            ViewChanged("saveToDic");
        }

        /// <summary>
        /// shows game controls
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void controls_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Right Arrow --> Moves Right\n\n" +
                "Left Arrow --> Moves Left\n\n" +
                "Up Arrow --> Moves Up\n\n" +
                "Down Arrow --> Moves Down\n\n" +
                "Page Up --> Moves To Upper floor\n\n"+
                "Page Down --> Moves To Downer Floor");
        }

        /// <summary>
        /// shows game story
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void game_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You are the lovable character from GOT - Sersi.\n\n" +
"The high septon makes you walk naked infront of the people to atone for your sins.\n\n" +
"Your goal is to finish The Walk Of Shame as fast as you can.\n\n" +
"To achive that, you need to get home to the Red Castle\n\n"+ "Enjoy the game and be merciful , because the night is dark and full of terrors");
        }

        /// <summary>
        /// shows app markings
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void markings_click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Go Up: available --> appeares when you can go up floor\n"+
                "Go Down: available -->appeares when you can go down floor\n"+
                "Cuurent Floor : <number> --> shows in what floor you are at the moment");
        }
    }
}
