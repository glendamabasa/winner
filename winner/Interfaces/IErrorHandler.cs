using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winner.Interfaces
{
    /// <summary>
    /// Error handler class
    /// </summary>
    public  interface IErrorHandler
    {
        /// <summary>
        /// Contains errors 
        /// </summary>
        string Error { get; set; }
    }
}
