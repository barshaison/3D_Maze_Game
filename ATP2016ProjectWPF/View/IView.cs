using ATP2016Library.MazeGenerators;
using ATP2016Library.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016ProjectWPF.View
{
    /// <summary>
    /// type of delegates added to view change event handler
    /// </summary>
    /// <param name="command">name of command to be operated</param>
    public delegate void viewEventDelegate(string command);
    /// <summary>
    /// View Layer
    /// </summary>
    interface IView
    {
        /// <summary>
        /// view change event handler
        /// </summary>
        event viewEventDelegate ViewChanged;
        /// <summary>
        /// runs the view
        /// </summary>
        void Start();
        
        /// <summary>
        /// output data to view UI
        /// </summary>
        /// <param name="output"></param>
        void Output(string output);
        /// <summary>
        /// reads parameters for maze generation
        /// </summary>
        /// <returns>parameters of maze</returns>
        string getMazeParameters();
        /// <summary>
        /// reads maze name 
        /// </summary>
        /// <returns>maze name</returns>
        string GetMazeName();
        /// <summary>
        /// reads maze name from user input
        /// </summary>
        /// <returns>maze name</returns>
        string GetMazeNameForSol();
        /// <summary>
        /// outputs maze to the UI
        /// </summary>
        /// <param name="maze">maze to output</param>
        void outputMaze(Maze3d maze);
        /// <summary>
        /// outputs solution to UI
        /// </summary>
        /// <param name="sol">solution to be outputed</param>
        void outputSolution(Solution sol);
        /// <summary>
        /// reads from user parameters for slove maze
        /// </summary>
        /// <returns>solution parameters</returns>
        string GetParamsForSolveMaze();
        /// <summary>
        /// reads from user parameters for save maze
        /// </summary>
        /// <returns>parameters read</returns>
        string GetParamsForSaveMaze();
        /// <summary>
        /// reads from user parameters for load maze
        /// </summary>
        /// <returns>parameters read</returns>
        string GetParamsForLoadMaze();
        
        /// <summary>
        /// reads maze details
        /// </summary>
        void getMazeDetails();
        /// <summary>
        /// sets maze data from model
        /// </summary>
        /// <param name="maze">maze</param>
        void setMazeToPlayOn(Maze3d maze);
    }
}
