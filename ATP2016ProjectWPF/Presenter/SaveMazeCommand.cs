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
    /// Command to save maze in app memory
    /// </summary>
    class SaveMazeCommand : ACommand
    {
        /// <summary>
        /// sets the view and model layers
        /// </summary>
        /// <param name="model">model instance</param>
        /// <param name="view">view instance</param>
        public SaveMazeCommand(IModel model, IView view) : base(model, view)
        {
        }
        /// <summary>
        /// Performs the save command
        /// </summary>
        /// <param name="parameters">command parameters</param>
        public override void DoCommand(params string[] parameters)
        {
            string commandParams = m_view.GetParamsForSaveMaze();
            if (commandParams == "$ $") return;
            string[] splitedParams = commandParams.Split(' ');
            string mazeName = splitedParams[0];
            string path = splitedParams[1];
            m_model.SaveMaze(mazeName, path);
        }
        /// <summary>
        /// returns command name
        /// </summary>
        /// <returns>command name</returns>
        public override string GetName()
        {
            return "SaveMaze";
        }
    }
}
