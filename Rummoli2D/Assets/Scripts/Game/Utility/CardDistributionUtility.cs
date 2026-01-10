using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardDistributionUtility
{
    public static int GetCardsPerPlayer(int totalPlayers)
    {
        switch (totalPlayers)
        {
            case 2: return 26;
            case 3: return 17;
            case 4: return 13;
            case 5: return 10;
            default:
                Debug.LogWarning("Unsupported number of players. Defaulting to 8 cards per player.");
                return 8;
        }
    }
}
