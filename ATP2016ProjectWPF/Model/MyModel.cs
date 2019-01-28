using ATP2016Library.Compression;
using ATP2016Library.MazeGenerators;
using ATP2016Library.Search;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ATP2016Library;

namespace ATP2016ProjectWPF.Model
{
    /// <summary>
    /// The Model Layer
    /// </summary>
    class MyModel : IModel
    {
        /// <summary>
        /// event handler
        /// </summary>
        public event modelEventDelegate ModelChanged;
        /// <summary>
        /// Dictionary of the generated mazes
        /// </summary>
        private Dictionary<string, Maze3d> m_generatedMazes;
        /// <summary>
        /// Dictionary of the solutions
        /// </summary>
        private Dictionary<string, Solution> m_mazeSolutions;
        /// <summary>
        /// Maps path to a maze saved in this path
        /// </summary>
        private Dictionary<string/*path*/, Maze3d> m_savedMazes;
        /// <summary>
        /// Maps maze to it's solution
        /// </summary>
        private Dictionary<Maze3d, Solution> m_MazeToSolution;
        /// <summary>
        /// list of stoppable objects
        /// </summary>
        private List<Istoppable> m_stoppableObjects;
        /// <summary>
        /// number of threads allowed in the thread pool
        /// </summary>
        private int m_numberOfThreadsInThreadPool;
        /// <summary>
        /// Constructs the model layer
        /// </summary>
        public MyModel()
        {
            m_generatedMazes = new Dictionary<string, Maze3d>();
            m_mazeSolutions = new Dictionary<string, Solution>();
            m_savedMazes = new Dictionary<string, Maze3d>();
            m_MazeToSolution = new Dictionary<Maze3d, Solution>();
            m_numberOfThreadsInThreadPool = Properties.Settings1.Default.numOfThreadsInThreadPool;
            m_stoppableObjects = new List<Istoppable>();
        }
        /// <summary>
        /// Adds stoppable object to the stoppable list
        /// </summary>
        /// <param name="obj"></param>
        private void setStoppableObjects(Istoppable obj)
        {
            m_stoppableObjects.Add(obj);
        }

        /// <summary>
        /// Saves generated solution of each run to a zip file
        /// </summary>
        public void writeDictionaryToZipFile() 
        {
            //convert dictionary to byte array
            byte[] byteArray;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, m_MazeToSolution);
            byteArray = ms.ToArray();

            byte[] bytesToCompress = byteArray;

            using (FileStream fileToCompress = File.Create("solutions.zip"))
            {
                using (GZipStream compressionStream = new GZipStream(fileToCompress, System.IO.Compression.CompressionMode.Compress))
                {
                    compressionStream.Write(bytesToCompress, 0, bytesToCompress.Length);
                }
            }

            using (FileStream length = File.Create("length"))
            {
                using (StreamWriter sr = new StreamWriter(length))
                {
                    sr.WriteLine(bytesToCompress.Length);
                }
            }
        }
        /// <summary>
        /// Performs orgenaized stop of every active thread in the app
        /// </summary>
        public void stop()
        {
            foreach(Istoppable s in m_stoppableObjects)
            {
                s.stop();
            }
        }

        /// <summary>
        /// Loads to the app memory the solutions from the zip file
        /// </summary>
        public void load_MazeToSolution() 
        {
            //read byteArray length from seperate file
            try
            {
                StreamReader sr = new StreamReader("length");

                int length = Int32.Parse(sr.ReadLine());

                sr.Close();

                byte[] decompressedBytes = new byte[length];
                using (FileStream fileToDecompress = File.Open("solutions.zip", FileMode.Open))
                {
                    using (GZipStream decompressionStream = new GZipStream(fileToDecompress, System.IO.Compression.CompressionMode.Decompress))
                    {
                        decompressionStream.Read(decompressedBytes, 0, length);
                    }
                }
                var mStream = new MemoryStream();
                var binFormatter = new BinaryFormatter();

                mStream.Write(decompressedBytes, 0, decompressedBytes.Length);
                mStream.Position = 0;
                m_MazeToSolution = binFormatter.Deserialize(mStream) as Dictionary<Maze3d, Solution>;
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// synchronized function to addition to maze to solution dictionary
        /// </summary>
        /// <param name="maze">3D maze</param>
        /// <param name="sol">Solution</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddToMazeToSolution(Maze3d maze, Solution sol)
        {
            m_MazeToSolution.Add(maze, sol);
        }
        /// <summary>
        /// synchronized function to addition to mazes dictionary
        /// </summary>
        /// <param name="mazeName">maze name</param>
        /// <param name="maze">3D maze</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddToGeneratedMazesDictionary(string mazeName,Maze3d maze)
        {
            try { m_generatedMazes.Add(mazeName, maze); }
            catch (Exception)
            {
                ModelChanged("this maze was already generated");
            }
        }
        /// <summary>
        /// synchronized function to addition to maze's solutions dictionary
        /// </summary>
        /// <param name="mazeName">maze name</param>
        /// <param name="sol">solution</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void AddToMazeSolutions(string mazeName, Solution sol)
        {
            m_mazeSolutions.Add(mazeName, sol);
        }
        /// <summary>
        /// synchronized function to addition to saved maze's dictionary
        /// </summary>
        /// <param name="path"></param>
        /// <param name="maze"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        private void AddToSavedMazes(string path, Maze3d maze)
        {
            m_savedMazes.Add(path, maze);
        }
        /// <summary>
        /// Generates 3D maze
        /// </summary>
        /// <param name="mazeName">maze name</param>
        /// <param name="width">maze width</param>
        /// <param name="length">maze length</param>
        /// <param name="layers">number of layers</param>
        public void Generate3dMaze(string mazeName, int width, int length, int layers)
        {
            Generate3dMazeInThreadPool( mazeName,  width,  length,  layers);
        }
        /// <summary>
        /// Generates 3D maze in a new thread in the thread pool
        /// </summary>
        /// <param name="mazeName"></param>
        /// <param name="width"></param>
        /// <param name="length"></param>
        /// <param name="layers"></param>
        private void Generate3dMazeInThreadPool(string mazeName, int width, int length, int layers)
        {
            ThreadPool.SetMaxThreads(m_numberOfThreadsInThreadPool, m_numberOfThreadsInThreadPool);
            ThreadPool.QueueUserWorkItem
            (
                new WaitCallback((state) =>
                {
                    MyMaze3dGenerator mazeGenerator = new MyMaze3dGenerator();//**
                    setStoppableObjects(mazeGenerator);
                    Maze3d maze = (Maze3d)mazeGenerator.generate(length, width, layers);
                    AddToGeneratedMazesDictionary(mazeName, maze);
                  ModelChanged("maze " + mazeName+ " is ready");
                })
            );
        }
        /// <summary>
        /// Gets maze by name
        /// </summary>
        /// <param name="mazeName">maze name</param>
        /// <returns></returns>
        public Maze3d GetMaze(string mazeName)
        {
            if (m_generatedMazes.ContainsKey(mazeName))
            {
                return m_generatedMazes[mazeName];
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Saves maze by name to a spesific path
        /// </summary>
        /// <param name="mazeName">maze name</param>
        /// <param name="path">path</param>
        public void SaveMaze(string mazeName, string path)
        {
            if (m_generatedMazes.ContainsKey(mazeName))
            {
                Maze3d maze = m_generatedMazes[mazeName];
                AddToSavedMazes(path, maze);//add the maze to the saved mazes Dictionary

                using (FileStream fileOutStream = new FileStream(path, FileMode.Create))
                {
                    using (Stream outStream = new MyCompressorStream(fileOutStream, ATP2016Library.Compression.CompressionMode.Compress))
                    {
                        outStream.Write(maze.toByteArray(), 0, maze.toByteArray().Length);
                        outStream.Flush();
                    }
                }

                ModelChanged("maze " + mazeName + " was saved in " + path);
                
            }
            else
            {
                ModelChanged("maze doesnt exist");
            }
        }
        /// <summary>
        /// Loads maze from given path and saves it in app memory by given name
        /// </summary>
        /// <param name="path">path</param>
        /// <param name="mazeName">maze name</param>
        public void LoadMaze(string path, string mazeName)
        {
            if (!m_savedMazes.ContainsKey(path))
            {
                ModelChanged("path doesnt exit");
            }
            else //file path exist
            {
                Maze3d savedMaze = m_savedMazes[path];
                byte[] mazeBytes = new byte[savedMaze.toByteArray().Length];
                using (FileStream fileInStream = new FileStream(path, FileMode.Open))
                {
                    using (Stream inStream = new MyCompressorStream(fileInStream, ATP2016Library.Compression.CompressionMode.Decompress))
                    {
                        inStream.Read(mazeBytes, 0, mazeBytes.Length);
                    }
                }
                Maze3d loadedMaze = new Maze3d(mazeBytes);
                AddToGeneratedMazesDictionary(mazeName, loadedMaze);
                ModelChanged("maze " + mazeName + " was loaded successfully");
            }
        }
        /// <summary>
        /// returns solution of given maze
        /// </summary>
        /// <param name="mazeName">maze name</param>
        /// <returns></returns>
        public Solution GetSolution(string mazeName)
        {
            Solution sol=null;
            try {
                if (m_MazeToSolution.ContainsKey(m_generatedMazes[mazeName]))
                {
                    sol = m_MazeToSolution[m_generatedMazes[mazeName]];
                    return sol;
                }
                else
                {
                    sol = null;
                }
            }
            catch (Exception )
            {
            }
            return sol;
        }
        /// <summary>
        /// Solves maze in a new thread in the thread pool
        /// </summary>
        /// <param name="mazeName"></param>
        /// <param name="algorithm"></param>
        private void solveMazeInThreadPool(string mazeName, string algorithm)
        {
           ThreadPool.SetMaxThreads(m_numberOfThreadsInThreadPool, m_numberOfThreadsInThreadPool);
            ThreadPool.QueueUserWorkItem(
             new WaitCallback((state) =>
              {
                    if (m_generatedMazes.ContainsKey(mazeName))
                    {
                        Maze3d maze = m_generatedMazes[mazeName];
                        SearchableMaze3d searchableMaze = new SearchableMaze3d(maze);

                        if (algorithm == "BFS")
                        {
                          BFS bfs = new BFS(); //**
                          setStoppableObjects(bfs);
                          Solution bfsSolution = bfs.Solve(searchableMaze);
                            try
                            {
                                AddToMazeSolutions(mazeName, bfsSolution);
                                AddToMazeToSolution(maze, bfsSolution);
                                ModelChanged("Solution for "+ mazeName +  " is ready");
                            }
                            catch (Exception)
                            {
                            }
                        }
                        else if (algorithm == "DFS")
                        {
                          DFS dfs = new DFS();
                          setStoppableObjects(dfs);
                          Solution dfsSolution = dfs.Solve(searchableMaze);

                            try
                            {
                                AddToMazeSolutions(mazeName, dfsSolution);
                                AddToMazeToSolution(maze, dfsSolution);
                                ModelChanged("Solution for " + mazeName + " is ready");
                            }
                            catch (Exception)
                            {
                            }
                        }
                    }
                    else
                    {
                        ModelChanged("Maze named " + mazeName + " does'nt exist");
                    }
                })
            );
        }
        /// <summary>
        /// Solves maze by given name
        /// </summary>
        /// <param name="mazeName">maze name</param>
        /// <param name="algorithm">algorithm to solve with</param>
        public void solveMaze(string mazeName, string algorithm)
        {
            if (!m_generatedMazes.ContainsKey(mazeName))
            {
                ModelChanged("Maze named " + mazeName + " doesn't exist");
                return;
            }
            if (m_MazeToSolution.ContainsKey(m_generatedMazes[mazeName]))
            {
                ModelChanged("Caching operated: Took the solution from system memory: \n solution is ready");
            }
            else
            {
                solveMazeInThreadPool(mazeName, algorithm);
            }
        }
    }
}
