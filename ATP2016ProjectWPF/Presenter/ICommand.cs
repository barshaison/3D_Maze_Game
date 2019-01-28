using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016ProjectWPF.Presenter
{
    /// <summary>
    /// Functioalitiy of a Command
    /// </summary>
    interface ICommand
    {
        /// <summary>
        /// Performs the command
        /// </summary>
        /// <param name="parameters">command parameters</param>
        void DoCommand(params string[] parameters);
        /// <summary>
        /// returns command's name
        /// </summary>
        /// <returns>command name</returns>
        string GetName();
    }
}
