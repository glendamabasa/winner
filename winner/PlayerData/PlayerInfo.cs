using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using winner.Interfaces;

namespace winner.PlayerData
{
    public  class PlayerInfo : IPlayerInfo, IComparable
    {
        /// <inheritdoc />
        public string PlayerName { get; set; }

        /// <inheritdoc />
        public string[] PlayerHand { get; set; }

        /// <inheritdoc />
        public int PlayerScore { get; set; }

        /// <inheritdoc />
        public int CompareTo(object player1)
        {
            var p1= (PlayerInfo)player1;
            if (this.PlayerScore < p1.PlayerScore)
                return 1;
            if (this.PlayerScore > p1.PlayerScore)
                return -1;
            else
                return 0;
        }
    }
}
