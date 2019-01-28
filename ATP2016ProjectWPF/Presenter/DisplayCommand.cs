using ATP2016ProjectWPF.Model;
using ATP2016ProjectWPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATP2016Library.MazeGenerators;

namespace ATP2016ProjectWPF.Presenter
{
    /// <summary>
    /// Command to display 3D maze
    /// </summary>
    class DisplayCommand : ACommand
    {
        /// <summary>
        /// sets the view and model layers
        /// </summary>
        /// <param name="model">model instance</param>
        /// <param name="view">view instance</param>
        public DisplayCommand(IModel model, IView view) : base(model, view)
        {
        }
        /// <summary>
        /// perform the disaply command
        /// </summary>
        /// <param name="parameters">command parameters</param>
        public override void DoCommand(params string[] parameters)
        {
            try
            {
                string mazeName = m_view.GetMazeName();
                Maze3d maze = m_model.GetMaze(mazeName);
                if (maze == null)
                {
                    m_view.Output("maze " +mazeName+ " doesn't exist");
                    return;
                }
                   
                m_view.outputMaze(maze);
                m_view.setMazeToPlayOn(maze);
            }
            catch
            {
            }
        }
        /// <summary>
        /// returns the command name
        /// </summary>
        /// <returns>command name</returns>
        public override string GetName()
        {
            return "Display";
        }
    }
}
