using ATP2016Library.Search;
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
    /// Command to display solution to a given maze
    /// </summary>
    class DisplaySolutionCommand : ACommand
    {
        /// <summary>
        /// sets the view and model layers
        /// </summary>
        /// <param name="model">model instance</param>
        /// <param name="view">Iview instance</param>
        public DisplaySolutionCommand(IModel model, IView view) : base(model, view)
        {
        }
        /// <summary>
        /// Perform the command
        /// </summary>
        /// <param name="parameters">command parameters</param>
        public override void DoCommand(params string[] parameters)
        {
            string mazeName = m_view.GetMazeNameForSol();
            Solution sol = m_model.GetSolution(mazeName);
            m_view.outputSolution(sol);
        }
        /// <summary>
        /// returns command name
        /// </summary>
        /// <returns>command name</returns>
        public override string GetName()
        {
            return "DisplaySolution";
        }
    }
}
