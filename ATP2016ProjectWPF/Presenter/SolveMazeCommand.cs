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
    /// Command to solve goven maze
    /// </summary>
    class SolveMazeCommand : ACommand
    {
        /// <summary>
        /// sets the view and model layers
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="view">view</param>
        public SolveMazeCommand(IModel model, IView view) : base(model, view)
        {
        }
        /// <summary>
        /// Performs the solve command
        /// </summary>
        /// <param name="parameters">command parameters</param>
        public override void DoCommand(params string[] parameters)
        {
            string commandParams = m_view.GetParamsForSolveMaze();
            if (commandParams == "$ $") return; //cancel was pressed
            string[] splitedParams = commandParams.Split(' ');
            string mazeName = splitedParams[0];
            string algorithm = splitedParams[1]; /*m_view.GetAlgorithmTosolve();*/
            m_model.solveMaze(mazeName, algorithm);
        }
        /// <summary>
        /// returns command name
        /// </summary>
        /// <returns>command name</returns>
        public override string GetName()
        {
            return "SolveMaze";
        }
    }
}
