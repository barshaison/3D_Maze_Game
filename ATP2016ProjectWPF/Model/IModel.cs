using ATP2016Library.MazeGenerators;
using ATP2016Library.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016ProjectWPF.Model
{
    /// <summary>
    /// delegate type that  matches the event handler 
    /// </summary>
    /// <param name="problemCode">description of the event</param>
    public delegate void modelEventDelegate(string problemCode);
    /// <summary>
    /// Represents the Model's layer functioality
    /// </summary>
    interface IModel
    {
        /// <summary>
        /// event handler
        /// </summary>
        event modelEventDelegate ModelChanged;
        /// <summary>
        /// Generates 3D Maze
        /// </summary>
        /// <param name="mazeName">name</param>
        /// <param name="width">width</param>
        /// <param name="length">length</param>
        /// <param name="layers">layers</param>
        void Generate3dMaze(string mazeName,int width,int length,int layers);
        /// <summary>
        /// Gets maze
        /// </summary>
        /// <param name="mazeName">maze name</param>
        /// <returns></returns>
        Maze3d GetMaze(string mazeName);
        /// <summary>
        /// Solves maze
        /// </summary>
        /// <param name="mazeName">maze name</param>
        /// <param name="algorithm">algorithm to solve with</param>
        void solveMaze(string mazeName,string algorithm);
        /// <summary>
        /// Gets solution for given maze
        /// </summary>
        /// <param name="mazeName">maze name</param>
        /// <returns></returns>
        Solution GetSolution(string mazeName);
        /// <summary>
        /// Saves maze
        /// </summary>
        /// <param name="mazeName">maze name</param>
        /// <param name="path">path</param>
        void SaveMaze(string mazeName, string path);
        /// <summary>
        /// Loads maze
        /// </summary>
        /// <param name="path">path</param>
        /// <param name="mazeName">maze name</param>
        void LoadMaze(string path, string mazeName);
        /// <summary>
        /// Write the dictionary of solutions to zip file
        /// </summary>
        void writeDictionaryToZipFile();
        /// <summary>
        /// Loads the solutions saved in the zip file
        /// </summary>
        void load_MazeToSolution();
        /// <summary>
        /// Performs orgenized stop of every working thread in the system
        /// </summary>
        void stop();

        
    }
}
