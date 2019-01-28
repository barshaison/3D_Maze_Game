using ATP2016Library.MazeGenerators;
using ATP2016Library.Search;
using System;
using System.Collections;
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
    /// control of the maze board
    /// </summary>
    public partial class MazeDisplayer : UserControl
    {
        /// <summary>
        /// maze data
        /// </summary>
        private Maze3d m_maze;
        /// <summary>
        /// contol's width
        /// </summary>
        private double m_width;
        /// <summary>
        /// control's height
        /// </summary>
        private double m_height;
        /// <summary>
        /// width property
        /// </summary>
        public double Width1
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
        /// height property
        /// </summary>
        public double Height1
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
        /// constructs maze control
        /// </summary>
        /// <param name="maze">maze data</param>
        /// <param name="x">maze start position x axis</param>
        /// <param name="y">maze start position y axis</param>
        /// <param name="layer">current layer of the maze</param>
        /// <param name="solutionOn">flag of display solution</param>
        /// <param name="sol">maze solution</param>
        /// <param name="width">control width</param>
        /// <param name="height">control height</param>
        public MazeDisplayer(Maze3d maze,int x,int y, int layer, bool solutionOn, Solution sol, double width, double height)
        {
            InitializeComponent();
            m_maze = maze;
            DrawMaze(maze, x, y, layer, solutionOn,sol);
            m_width = width;
            m_height = height;
        }
        /// <summary>
        /// draws the maze board
        /// </summary>
        /// <param name="maze">maze data</param>
        /// <param name="x">maze start position x axis</param>
        /// <param name="y">maze start position y axis</param>
        /// <param name="layer">current layer</param>
        /// <param name="solutionOn">flag solution</param>
        /// <param name="sol">maze's solution</param>
        private void DrawMaze(Maze3d maze, int x, int y, int layer, bool solutionOn, Solution sol)
        {
            try
            {
                int[,] board = maze.TwoDMazes[layer].Board;
                double boardWidth = board.GetLength(0);
                double boardHeight = board.GetLength(1);

                for (int i = 0; i < board.GetLength(0); i++)
                {
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        if (board[i, j] == 1)
                        {
                            Wall wall = new Wall(this.Width / boardWidth, this.Height / boardHeight);
                            mazeBoard.Children.Add(wall);
                            Canvas.SetLeft(wall, (this.Width / boardWidth) * j);
                            Canvas.SetTop(wall, (this.Height / boardHeight) * i);
                        }
                    }
                }
                //add solution
                if (solutionOn)
                {
                    //ArrayList solsInFloor = new ArrayList();
                    ArrayList sols = sol.GetSolutionPath();
                    foreach (AState state in sols)
                    {
                        object sToComp;
                        if (sols.IndexOf(state) < sols.Count - 1)
                        {
                            sToComp = sols[sols.IndexOf(state) + 1];

                        }
                        else { sToComp = sols[sols.IndexOf(state)]; }

                        if (((Maze3dState)state).State.Axis[2] == layer)
                        {

                            Path path = new Path((this.Width / boardWidth), (this.Height / boardHeight));
                            mazeBoard.Children.Add(path);
                            if (((Maze3dState)sToComp).State.Axis[2] == ((Maze3dState)state).State.Axis[2] + 1)
                                path.path_name.Fill = new SolidColorBrush(System.Windows.Media.Colors.Blue);
                            else if (((Maze3dState)sToComp).State.Axis[2] == ((Maze3dState)state).State.Axis[2] - 1)
                                path.path_name.Fill = new SolidColorBrush(System.Windows.Media.Colors.Brown);
                            else
                                path.path_name.Fill = new SolidColorBrush(System.Windows.Media.Colors.Green);
                            Canvas.SetLeft(path, (this.Width / boardWidth) * ((Maze3dState)state).State.Axis[0]);
                            Canvas.SetTop(path, (this.Height / boardHeight) * ((Maze3dState)state).State.Axis[1]);
                        }
                    }
                }
                Cat cat = new Cat((this.Width / boardWidth), (this.Height / boardHeight));
                cat.rec_name.Fill = new ImageBrush(new BitmapImage(
                 new Uri("Sersi.jpg", UriKind.Relative)));
                mazeBoard.Children.Add(cat);
                Canvas.SetLeft(cat, (this.Width / boardWidth) * x);
                Canvas.SetTop(cat, (this.Height / boardHeight) * y);

                //add exit pic
                if (layer == maze.getGoalPosition().Axis[2])
                {
                    Exit exit = new Exit((this.Width / boardWidth), (this.Height / boardHeight));
                    exit.exit_name.Fill = new ImageBrush(new BitmapImage(
                     new Uri("Exit.jpg", UriKind.Relative)));
                    mazeBoard.Children.Add(exit);
                    Canvas.SetLeft(exit, (this.Width / boardWidth) * maze.getGoalPosition().Axis[0]);
                    Canvas.SetTop(exit, (this.Height / boardHeight) * maze.getGoalPosition().Axis[1]);
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
