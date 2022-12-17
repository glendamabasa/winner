using System;
using System.Collections;

namespace winner.Interfaces
{
    /// <summary>
    /// Interface for player infor
    /// </summary>
    public interface IPlayerInfo :IComparable
    {
        /// <summary>
        /// Contains the name of the player
        /// </summary>
        string PlayerName { get; set; }

        /// <summary>
        /// Has all the player card hand
        /// </summary>
        string[] PlayerHand { get; set; }

        /// <summary>
        /// Player score
        /// </summary>
        int PlayerScore { get; set; }
    }
}