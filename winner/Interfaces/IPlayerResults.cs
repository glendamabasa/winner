using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winner.Interfaces
{
    /// <summary>
    /// Player results interface
    /// </summary>
    public  interface IPlayerResults
    {
        /// <summary>
        /// Writes player results to the text file and the console
        /// </summary>
        /// <param name="readFromTxtPath"></param>
        /// <param name="writeToTxtPath"></param>
        void WritePlayerScores(string readFromTxtPath, string writeToTxtPath);
    }
}
