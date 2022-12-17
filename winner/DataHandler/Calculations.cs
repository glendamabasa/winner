using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using winner.EnumsFolder;
using winner.Interfaces;

namespace winner.DataHandler
{
    public  class Calculations :ICalculations
    {
        #region Private Members
        private readonly IFileReader _fileReader;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fileReader"></param>
        /// <param name="errorHandler"></param>
        public Calculations(IFileReader fileReader)
        {
            _fileReader = fileReader;
        }
        #endregion

        #region Public Methods

        /// <inheritdoc />
        public List<IPlayerInfo> CalculateScore(string path)
        {
            try
            {
                //Reads from the text file
                var playerInfoHands = _fileReader.ReadFromFile(path);

                //Checks if there is any data coming from the text file
                if (playerInfoHands == null)
                {
                    Console.WriteLine("Player info hands collections was empty");
                    return null;
                }

                var playerInfoScore = CalculateCardFaceValue(playerInfoHands);
                
                //Checks if there are more than 1 values with the highest score
                if (playerInfoScore.Count > 1)
                {
                    var playerSuitValueScore = CalculateSuitValue(playerInfoScore);
                    return playerSuitValueScore;
                }

                //Returns a list of players with their scores
                return playerInfoScore;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred when doing calculations. Error Message : {ex.Message}");
                throw;
            }

        }

      
        #endregion

        #region Private Methods
        /// <summary>
        /// Calculates suit card value
        /// </summary>
        /// <param name="playerInfoScore"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static List<IPlayerInfo> CalculateSuitValue(List<IPlayerInfo> playerInfoScore)
        {
            try
            {
                var cardSuitValueSum = 0;

                foreach (var playerInfoHand in playerInfoScore)
                {
                    foreach (var score in playerInfoHand.PlayerHand)
                    {
                        var cardSuitSubString = score.Substring(score.Length - 1, 1);

                        //Switch using card suits to get the suit value
                        var cardSuitValue = 0;
                        switch (cardSuitSubString)
                        {
                            case nameof(Suits.C):
                                cardSuitValue = (int)Suits.C;
                                cardSuitValueSum += cardSuitValue;
                                break;
                            case nameof(Suits.D):
                                cardSuitValue = (int)Suits.D;
                                cardSuitValueSum += cardSuitValue;
                                break;
                            case nameof(Suits.H):
                                cardSuitValue = (int)Suits.H;
                                cardSuitValueSum += cardSuitValue;
                                break;
                            case nameof(Suits.S):
                                cardSuitValue = (int)Suits.S;
                                cardSuitValueSum += cardSuitValue;
                                break;
                            default:
                                throw new Exception("Invalid card suit option");

                        }
                    }

                    //Assigning a new score to the player based on the suit
                    playerInfoHand.PlayerScore = cardSuitValueSum;
                    //Setting the value to 0 to sum score for the next player
                    cardSuitValueSum = 0;
                }

                //Sorts values from big to small
                playerInfoScore.Sort();

                //Returns a list of highest score(s)
                var newPlayerInfoScore = playerInfoScore.Where(i => i.PlayerScore == playerInfoScore[0].PlayerScore).ToList();

                //Returns a list of players with their scores
                return newPlayerInfoScore;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while trying to calculate the suit value. : {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Calculates the card face value 
        /// </summary>
        /// <param name="playerInfoHands"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private static List<IPlayerInfo> CalculateCardFaceValue(List<IPlayerInfo> playerInfoHands)
        {
            try
            {

                //Stores the sum of scores per player
                var cardFaceValueSum = 0;
                foreach (var playerInfoHand in playerInfoHands)
                {
                    foreach (var score in playerInfoHand.PlayerHand)
                    {
                        //Substring the card hand to get desired card letter
                        var scoreSubstring = score.Substring(0, score.Length - 1);

                        //Try to parse the value to int ,if successful output cardFaceIntValue
                        var isCardValueParsed = int.TryParse(scoreSubstring, out int cardFaceIntValue);

                        if (!isCardValueParsed)
                        {
                            //Switch
                            cardFaceIntValue = scoreSubstring switch
                            {
                                nameof(FaceValue.A) => (int)FaceValue.A,
                                nameof(FaceValue.J) => (int)FaceValue.J,
                                nameof(FaceValue.K) => (int)FaceValue.K,
                                nameof(FaceValue.Q) => (int)FaceValue.Q,
                                _ => throw new Exception("Invalid card value option")
                            };
                        }
                        //Sum of card value per player
                        cardFaceValueSum += cardFaceIntValue;
                    }
                    //Giving each please a final score
                    playerInfoHand.PlayerScore = cardFaceValueSum;

                    //Setting the value to 0 to sum score for the next player
                    cardFaceValueSum = 0;
                }
                //Sorts values from big to small
                playerInfoHands.Sort();

                // Checking if there are equal values in the scores
                var playerInfoScore = playerInfoHands.Where(i => i.PlayerScore == playerInfoHands[0].PlayerScore).ToList();

                return playerInfoScore;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while trying to calculate the card face value. : {ex.Message}");
                throw;
            }
        }


        #endregion

    }

}
