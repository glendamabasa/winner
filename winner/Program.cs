using System;
using System.Collections.Concurrent;
using System.Threading;
using winner.DataHandler;
using winner.Interfaces;

namespace winner
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args[1].Length < 5 || args[3].Length < 5)
                {
                    Console.WriteLine("File names should have more than 4 characters");
                }

                if (args.Length < 4)
                {
                    throw new Exception("Enter 4 command parameters " +
                                        "1. --in " +
                                        "2.Input text file " +
                                        "3.--out " +
                                        "4.Output text file");
                }
                #region FileNames

                var inputFile = "";
                var outputFile = "";
                #endregion

                #region Switch Statements

                if (args.Length == 4)
                {
                    var firstArgCount = args[0].Length;
                    var thirdArgCount = args[2].Length;

                    var getInputFileType = "";
                    var getOutputFileType = "";
                    switch (args[0])
                    {
                        case "--in":
                            inputFile = args[1];
                            outputFile = args[3];
                            if (firstArgCount <= 4)
                            {
                                inputFile = args[1] + ".txt";
                            }
                            if (thirdArgCount <= 4)
                            {
                                outputFile = args[3] + ".txt";
                            }
                            //Checking if the input file is of type txt
                            getInputFileType = args[0].Substring(args[0].Length, args[0].Length - 4);
                            if (getInputFileType != ".txt")
                            {
                                inputFile = args[1] + ".txt";
                            }

                            //Checking if the output file is of type txt
                            getOutputFileType = args[3].Substring(args[0].Length, args[0].Length - 4); ;
                            if (getOutputFileType != ".txt")
                            {
                                outputFile = args[3] + ".txt";
                            }

                            break;
                        case "--out":
                            outputFile = args[1];
                            inputFile = args[3];

                            if (firstArgCount <= 4)
                            {
                                inputFile = args[3] + ".txt";
                            }
                            if (thirdArgCount <= 4)
                            {
                                outputFile = args[1] + ".txt";
                            }

                            //Checking if the output file is of type txt
                            getOutputFileType = args[1].Substring(args[0].Length, args[0].Length - 4); ;
                            if (getOutputFileType != ".txt")
                            {
                                outputFile = args[1] + ".txt";
                            }
                            //Checking if the input file is of type txt
                            getInputFileType = args[3].Substring(args[0].Length, args[0].Length - 4);
                            if (getInputFileType != ".txt")
                            {
                                inputFile = args[3] + ".txt";
                            }


                            break;
                        default:
                            throw new Exception(
                                "The first input should either be --in for input file ,or --out for output file");

                    }
                }
                else
                {
                    throw new Exception("Enter 4 command parameters " +
                                        "1. --in " +
                                        "2.Input text file " +
                                        "3.--out " +
                                        "4.Output text file");
                }
                #endregion


                IFileReader fileReader = new FileHandler();
                IErrorHandler errorHandler = new ErrorHandler();
                ICalculations calculations = new Calculations(fileReader);
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("*********************************************************************************");
                Console.ResetColor();
                Console.WriteLine();
                Console.WriteLine("                                  Card Game                                    ");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("*********************************************************************************");
                Console.ResetColor();
                Console.WriteLine();

                IPlayerResults playerResults = new PlayerResults(calculations, errorHandler, fileReader);
                playerResults.WritePlayerScores(inputFile,outputFile);
                Console.WriteLine();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("*********************************************************************************");
                Console.ResetColor();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An Error occurred. : {ex.Message}");
            }
            Console.ReadLine();
        }
    }
}
