using Server.Controller;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATP2016Library.MazeGenerators;
using ATP2016Library.Search;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Server.Model
{
    class MyModel:IModel
    {
        private IController m_controller;
        private MyServer m_server;

        private Dictionary<Maze3d, Solution> m_MazeToSolution;

        public MyModel(IController controller)
        {
            m_controller = controller;
            m_MazeToSolution = new Dictionary<Maze3d, Solution>();
            initServer();
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddToMazeToSolution(Maze3d maze, Solution sol)
        {
            m_MazeToSolution.Add(maze, sol);
        }

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

        private void initServer()
        {
            int port = 5400;
            int listeningInterval = 5000;
            int numOfThreads = 10;
            m_server = new MyServer(port, listeningInterval, numOfThreads, MazeSolver);
        }

        public void StartServer()
        {
            m_server.Start();
        }

        public void StopServer()
        {
            m_server.Stop();
        }

        public void MazeSolver(Stream rw)
        {
            //StreamReader fromClient = new StreamReader(stream);
            //StreamWriter toClient = new StreamWriter(stream);


            //string command = fromClient.ReadLine();
            // string[] splited = command.Split('|');

            //  string mazeString = /*splited[0];*/fromClient.ReadLine();
            //  Console.WriteLine("client sent me: " + mazeString);
            //  string algorithm = /*splited[1];*/"BFS";
            //
            //  byte[] mazeByteArr = Encoding.ASCII.GetBytes(mazeString);
            //  Maze3d maze;
            //  using (MemoryStream memStream = new MemoryStream())
            //  {
            //      BinaryFormatter binForm = new BinaryFormatter();
            //      memStream.Write(mazeByteArr, 0, mazeByteArr.Length);
            //      memStream.Seek(0, SeekOrigin.Begin);
            //      maze = (Maze3d)binForm.Deserialize(memStream);
            //  }

            IFormatter formatter = new BinaryFormatter();
            Maze3d maze = (Maze3d)formatter.Deserialize(rw);
            string solString;
            if (m_MazeToSolution.ContainsKey(maze))
            {
                Solution sol = m_MazeToSolution[maze];
               
                formatter.Serialize(rw, sol);
                //solString = convertSolutionToString(sol);
                //toClient.WriteLine(solString);
                //toClient.Flush();

            }

                SearchableMaze3d searchableMaze = new SearchableMaze3d(maze);
            string algorithm = "BFS";
            if (algorithm == "BFS")
            {
                BFS bfs = new BFS(); //**
                
                Solution bfsSolution = bfs.Solve(searchableMaze);
                //solString = convertSolutionToString(bfsSolution);
                // toClient.WriteLine(solString);
                // toClient.Flush();
                formatter.Serialize(rw, bfsSolution);
                try
                {
                    
                    AddToMazeToSolution(maze, bfsSolution);
                    
                }
                catch (Exception)
                {
                }
            }
            else if (algorithm == "DFS")
            {
                DFS dfs = new DFS();
                
                Solution dfsSolution = dfs.Solve(searchableMaze);
               // solString = convertSolutionToString(dfsSolution);
               // toClient.WriteLine(solString);
               // toClient.Flush();
                formatter.Serialize(rw, dfsSolution);

                try
                {
                    
                    AddToMazeToSolution(maze, dfsSolution);
                    
                }
                catch (Exception)
                {
                }
            }

        }

        private string convertSolutionToString(Solution sol)
        {
            string solString;
            byte[] byteArray;
            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, sol);
                byteArray = ms.ToArray();
                 solString = Encoding.ASCII.GetString(byteArray);
            }

            return solString;
        }
    }
}
