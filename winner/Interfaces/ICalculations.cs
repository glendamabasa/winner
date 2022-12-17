using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winner.Interfaces
{
    /// <summary>
    /// Interface for calculations
    /// </summary>
    public  interface ICalculations
    {
        List<IPlayerInfo> CalculateScore(string path);
    }
}
