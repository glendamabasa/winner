using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using winner.DataHandler;

namespace winner.Interfaces
{
    /// <summary>
    /// File handler interface
    /// </summary>
    public interface IFileReader
    {
        /// <summary>
        /// Reads values from the text file
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        List<IPlayerInfo> ReadFromFile(string path);

        /// <summary>
        /// Writes player(s) who won to the text file
        /// </summary>
        /// <param name="playerResults"></param>
        /// <param name="writeToTxtPath"></param>
        void WriteToFile(List<IPlayerInfo> playerResults,string writeToTxtPath);

        /// <summary>
        /// Writes player(s) who won to the text file
        /// </summary>
        /// <param name="error"></param>
        /// <param name="writeToTxtPath"></param>
        void WriteToFile(IErrorHandler error, string writeToTxtPath);
    }
}
