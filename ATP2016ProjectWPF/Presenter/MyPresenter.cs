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
    /// the presenter layer: controls the view and model
    /// </summary>
    class MyPresenter
    {
        /// <summary>
        /// model layer
        /// </summary>
        private IModel m_model;
        /// <summary>
        /// view layer
        /// </summary>
        private IView m_view;
        /// <summary>
        /// set of commands
        /// </summary>
        private Dictionary<string, ACommand> m_commands;
        /// <summary>
        /// constructs the presenter layer
        /// </summary>
        /// <param name="model">model</param>
        /// <param name="view">view</param>
        public MyPresenter(IModel model, IView view)
        {
            m_model = model;
            m_view = view;
            m_commands = GetCommands();
            SetEvents();
        }
        /// <summary>
        /// sets the commands dictionary
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, ACommand> GetCommands()
        {
            Dictionary<string, ACommand> commands = new Dictionary<string, ACommand>();
            ACommand Generate3dMaze = new Generate3dMazeCommand(m_model, m_view);
            commands.Add(Generate3dMaze.GetName(), Generate3dMaze);
            ACommand Display = new DisplayCommand(m_model, m_view);
            commands.Add(Display.GetName(), Display);
            ACommand SolveMaze = new SolveMazeCommand(m_model, m_view);
            commands.Add(SolveMaze.GetName(), SolveMaze);
            ACommand DisplaySolution = new DisplaySolutionCommand(m_model, m_view);
            commands.Add(DisplaySolution.GetName(), DisplaySolution);
            ACommand SaveMaze = new SaveMazeCommand(m_model, m_view);
            commands.Add(SaveMaze.GetName(), SaveMaze);
            ACommand LoadMaze = new LoadMazeCommand(m_model, m_view);
            commands.Add(LoadMaze.GetName(), LoadMaze);

            return commands;
        }
        /// <summary>
        /// sets delegates to model and view event handlers
        /// </summary>
        private void SetEvents()
        {
            m_model.ModelChanged += Mperform;
            m_view.ViewChanged += PerformCommand;
        }
        /// <summary>
        /// function of the model event handler: outputs msgs
        /// </summary>
        /// <param name="problemCode">message</param>
        private void Mperform(string problemCode)
        {
                m_view.Output(problemCode);
        }
        /// <summary>
        /// function of view event handler
        /// </summary>
        /// <param name="command">type of command</param>
        private void PerformCommand(string command)
        {
            if (command == "stop")
            {
                m_model.stop();
            }
            else if (command == "saveToDic")
            {
                m_model.writeDictionaryToZipFile();
            }
            else
            {
                m_commands[command].DoCommand();
            }
        }
    }
}
