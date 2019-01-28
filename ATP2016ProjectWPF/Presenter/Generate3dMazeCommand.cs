using ATP2016ProjectWPF.Model;
using ATP2016ProjectWPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016ProjectWPF.Presenter
{
    /// <summary>
    /// Command to generate 3D maze
    /// </summary>
    class Generate3dMazeCommand : ACommand
    {
        /// <summary>
        /// sets the model and view layers
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="view">view</param>
        public Generate3dMazeCommand(IModel model, IView view) : base(model, view)
        {
        }
        /// <summary>
        /// Performs the generate command
        /// </summary>
        /// <param name="parameters">command parameters</param>
        public override void DoCommand(params string[] parameters)
        {
            try
            {
                string mazeParameters = m_view.getMazeParameters(); //ask the user for the missing data to generate the maze
                if (mazeParameters == "x x x x") return; //if pressed cancel
                string[] mazeParams = mazeParameters.Split(' ');
                string mazeName = mazeParams[0];
                int width = Int32.Parse(mazeParams[1]);
                int length = Int32.Parse(mazeParams[2]);
                int layers = Int32.Parse(mazeParams[3]);
                m_model.Generate3dMaze(mazeName, width, length, layers);
            }
            catch(Exception)
            {
            }
        }
        /// <summary>
        /// returns the command's name
        /// </summary>
        /// <returns>command name</returns>
        public override string GetName()
        {
            return "Generate3dMaze";
        }
    }
}
