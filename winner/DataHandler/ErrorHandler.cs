using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using winner.Interfaces;

namespace winner.DataHandler
{
    public  class ErrorHandler :IErrorHandler
    {
        /// <inheritdoc />
        public string Error { get; set; }
    }
}
