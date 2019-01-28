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
    /// represents a general command 
    /// </summary>
  abstract  class ACommand : ICommand
    {
        /// <summary>
        /// model layer instance
        /// </summary>
        protected IModel m_model;
        /// <summary>
        /// view layer instance
        /// </summary>
        protected IView m_view;
        /// <summary>
        /// constructs a command
        /// </summary>
        /// <param name="model">model instance to set</param>
        /// <param name="view">view instance to set</param>
        public ACommand(IModel model, IView view)
        {
            m_model = model;
            m_view = view;
        }
        /// <summary>
        /// performs the command
        /// </summary>
        /// <param name="parameters">command parameters</param>
        public abstract void DoCommand(params string[] parameters);
        /// <summary>
        /// returns command name
        /// </summary>
        /// <returns>command name</returns>
        public abstract string GetName();
    }
}
