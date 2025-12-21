using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardDistributionUtility
{
    public static int GetCardsPerPlayer(int totalPlayers)
    {
        switch (totalPlayers)
        {
            case 2: case 3: return 12;
            case 4: return 10;
            case 5: return 8;
            case 6: return 6;
            default:
                Debug.LogWarning("Unsupported number of players. Defaulting to 8 cards per player.");
                return 8;
        }
    }
}
