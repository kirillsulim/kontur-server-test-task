using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simple_app
{
    /// <summary>
    /// Interface for simple application
    /// </summary>
    interface ISimpleApplication
    {
        /// <summary>
        /// Run application with cin and cout as a console input and output
        /// </summary>
        /// <param name="cin">console input</param>
        /// <param name="cout">console output</param>
        void Run(TextReader cin, TextWriter cout);
    }
}
