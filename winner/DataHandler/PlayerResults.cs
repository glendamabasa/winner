using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using winner.Interfaces;

namespace winner.DataHandler
{
    public class PlayerResults : IPlayerResults
    {
        private readonly ICalculations _calculations;
        private readonly IErrorHandler _errorHandler;
        private readonly IFileReader _fileReader;
        public PlayerResults(ICalculations calculations, IErrorHandler errorHandler, IFileReader fileReader)
        {
            _calculations = calculations;
            _errorHandler = errorHandler;
            _fileReader = fileReader;
        }

        /// <inheritdoc />
        public void WritePlayerScores(string readFromTxtPath, string writeToTxtPath)
        {
            //Called to calculate player scores
            var calculatedScores = _calculations.CalculateScore(readFromTxtPath);

            if (calculatedScores == null)
            {
                _errorHandler.Error = "ERROR";

                //Writing error to the text file
                _fileReader.WriteToFile(_errorHandler, writeToTxtPath);
                return;
            }

            //Writing to text file
            _fileReader.WriteToFile(calculatedScores, writeToTxtPath);
            Console.WriteLine();

            Console.WriteLine("_________________________________________________________________________________");
            Console.WriteLine("The winner(s) :");
            Console.WriteLine("_________________________________________________________________________________");
            Console.WriteLine();


            //Displaying winners on the console
            foreach (var item in calculatedScores)
            {
                Console.WriteLine($" {item.PlayerName} : {item.PlayerScore}");
            }

        }
    }
}
