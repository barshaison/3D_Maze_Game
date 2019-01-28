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
    /// Command to load maze from disk and save it to app memory
    /// </summary>
    class LoadMazeCommand : ACommand
    {
        /// <summary>
        /// sets model and view layers
        /// </summary>
        /// <param name="model">model instance</param>
        /// <param name="view">view instance</param>
        public LoadMazeCommand(IModel model, IView view) : base(model, view)
        {
        }
        /// <summary>
        /// Performs the Load maze command
        /// </summary>
        /// <param name="parameters">command parameters</param>
        public override void DoCommand(params string[] parameters)
        {
            string commandParams = m_view.GetParamsForLoadMaze();
            if (commandParams == "$ $") return;
            string[] splitedParams = commandParams.Split(' ');
            string path = splitedParams[0];
            string mazeName = splitedParams[1];
            m_model.LoadMaze(path, mazeName);
        }
        /// <summary>
        /// returns command name
        /// </summary>
        /// <returns>command name</returns>
        public override string GetName()
        {
            return "LoadMaze";
        }
    }
}
