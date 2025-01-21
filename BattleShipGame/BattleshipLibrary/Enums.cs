using System;
using System.Collections.Generic;


namespace BattleshipLibrary.Models
{
    public enum GridSpotStatus
    {
        Empty,
        Ship,
        Hit,
        Miss,
        Shot
    }

    public enum TileLetterIndex
    {
        A = 0,
        B = 1,
        C = 2,
        D = 3, 
        E = 4
    }
}
