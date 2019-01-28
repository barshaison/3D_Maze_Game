using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ATP2016Library.MazeGenerators;
using ATP2016Library.Search;

namespace ATP2016ProjectWPF.UnitTesting
{
    /// <summary>
    /// Test class
    /// </summary>
    [TestClass]
    public class TestMyMaze3dGenerator
    {
        /// <summary>
        /// tests bad arguments in maze generation
        /// </summary>
        [TestMethod]
        public void TestBadArguments()
        {
            try
            {
                var mg = new MyMaze3dGenerator();
                Maze3d maze = (Maze3d)mg.generate(2, -10, 2);
                Assert.Fail("Test Bad Arguments Faild");
            }
            catch (OverflowException)
            {
            }
        }

        /// <summary>
        /// tests generation of maze 100 times
        /// </summary>
        [TestMethod]
        public void TestRunAlotOfTimes()
        {
            var mg = new MyMaze3dGenerator();
            for (int i=0; i < 100; i++)
            {
               mg.generate(2, 2, 2);
            }
           
        }

        /// <summary>
        /// tests that the solve algo solves the maze
        /// </summary>
        [TestMethod]
        public void TestIsASolution()
        {
            //arrange
            var mg = new MyMaze3dGenerator();
            Maze3d maze = (Maze3d)mg.generate(7, 7, 4);
            BFS bfs = new BFS();
            SearchableMaze3d sMaze = new SearchableMaze3d(maze);
            Solution sol = bfs.Solve(sMaze);

            //act
            bfs.Solve(sMaze);

            //assert
            Assert.IsNotNull(sol);
        }
    }
}
