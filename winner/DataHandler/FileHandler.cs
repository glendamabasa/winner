using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using winner.Interfaces;
using winner.PlayerData;

namespace winner.DataHandler
{
    /// <summary>
    /// Handles files
    /// </summary>
    public class FileHandler :IFileReader
    {
        #region Public Methods

        /// <inheritdoc />
        public List<IPlayerInfo> ReadFromFile(string path)
        {
            try
            {
                var result = new List<IPlayerInfo>();

                StreamReader streamReader = null;

                var isNewFile = false;
                var fileStream = new FileStream(Path.Combine(path), FileMode.OpenOrCreate, FileAccess.Read);
                using (streamReader = new StreamReader(fileStream))
                {
                    //Checks if there is data in the file being read,
                    //If not Call the write method to write to the newly created data
                    //Use the data in the newly created file
                    if (streamReader.ReadLine() == null)
                    {
                        WriteToNewlyCreatedFile(path);
                        isNewFile = true;
                        streamReader.Close();
                    }
                    else
                    {
                        // Read and display lines from the file until the end of
                        // the file is reached.
                        while (streamReader.ReadLine() is { } line)
                        {
                            var info = StringManipulations(line);
                            result.Add(info);
                        }
                        streamReader.Close();
                    }
                }

                if (!isNewFile) return result;
                {
                    var data = "";
                    var docPath =
                        Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    var newFileStream = new FileStream(Path.Combine(docPath,path), FileMode.Open, FileAccess.Read);

                    using var newStreamReader= new StreamReader(newFileStream);
                    while ((data = newStreamReader.ReadLine()) != null)
                    {
                        var info = StringManipulations(data);
                        result.Add(info);
                    }
                    newStreamReader.Close();
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while trying to read from file {path}. Error Message : {ex.Message}");
                return null;
            }
        }

         /// <inheritdoc />
        public void WriteToFile(List<IPlayerInfo> playerResults,string writeToTxtPath)
        {
            try
            {
                var fileStream = new FileStream(Path.Combine(writeToTxtPath), FileMode.OpenOrCreate, FileAccess.Write);
                using var streamWriter = new StreamWriter(fileStream);
                if (playerResults.Count == 1)
                {
                    streamWriter.WriteLine($"{playerResults[0].PlayerName}:{playerResults[0].PlayerScore}");
                }
                else
                {
                    foreach (var playerInfo in playerResults)
                    {
                        streamWriter.Write(playerInfo.PlayerName+",");
                    }
                    streamWriter.Write($":{playerResults[0].PlayerScore}");
                }
                streamWriter.Flush();
                streamWriter.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred when trying to write to file. Error Message : {ex.Message}");
                throw;
            }
        }

         /// <inheritdoc />
        public void WriteToFile(IErrorHandler error, string writeToTxtPath)
        {
            try
            {
                var fileStream = new FileStream(Path.Combine(writeToTxtPath), FileMode.OpenOrCreate, FileAccess.Write);
                using var writer = new StreamWriter(fileStream);
                writer.Write(error.Error);
                writer.Flush();
                writer.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred when trying to write to file. Error Message : {ex.Message}");
            }
        }
         #endregion

        #region Private Methods

        ///// <summary>
        ///// Manipulates player cards
        ///// </summary>
        ///// <param name="inputString"></param>
        ///// <returns></returns>
        private static IPlayerInfo StringManipulations(string inputString)
        {
            var player = new PlayerInfo();
            try
            {
                var playerNameAndResults = inputString.Split(":");

                for (var i = 0; i < playerNameAndResults.Length; i++)
                {
                    player.PlayerHand = playerNameAndResults[1].Split(",");
                    player.PlayerName = playerNameAndResults[0];
                }
                return player;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred when doing string manipulations. Error Message : {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Writes to a newly created file
        /// </summary>
        /// <param name="newFilePath"></param>
        private static void WriteToNewlyCreatedFile(string newFilePath)
        {
            #region MockData
            var mockData = new Dictionary<string, string>
            {
                { "Glenda","KD,QH,10C,4C,AC" },
                { "John", "KD,QH,10C,4C,AC" },
                { "Wendy", "6S,8D,3D,JH,2D" },
                { "Taylor", "5H,3S,KH,AS,9D" },
                { "Jane", "JS,3H,2H,2C,4D" }
            };

            #endregion
            try
            {
                var docPath =
                    Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                var fileStream = new FileStream(Path.Combine(docPath,newFilePath), FileMode.OpenOrCreate, FileAccess.Write);
                using var streamWriter = new StreamWriter(fileStream);
                foreach (var (playerName,playerHand) in mockData)
                {
                    streamWriter.WriteLine($"{playerName}:{playerHand}");
                }
                streamWriter.Flush();
                streamWriter.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred when trying to write to newly created file. Error Message : {ex.Message}");
            }
        }

        #endregion
    }

}
