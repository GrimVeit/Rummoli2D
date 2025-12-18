using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BetSystemModel
{
    public const int SECTOR_COUNT = 9;

    private readonly Dictionary<int, PlayerBetData> playerBets;

    private readonly int[] sectorTotals;

    public BetSystemModel(int playersCount)
    {
        playerBets = new Dictionary<int, PlayerBetData>();
        sectorTotals = new int[SECTOR_COUNT];

        for (int i = 0; i < playersCount; i++)
        {
            playerBets.Add(i, new PlayerBetData(i, SECTOR_COUNT));
        }
    }

    public void ChooseBet(int sectorIndex)
    {
        SubmitBet(0, sectorIndex);
    }

    public void AddBet(int playerIndex, int sectorIndex)
    {
        OnAddBet?.Invoke(playerIndex, sectorIndex);
    }

    public void SubmitBet(int playerIndex, int sectorIndex)
    {
        var playerData = playerBets[playerIndex];

        if (playerData.CheckAvailableSector(sectorIndex))
        {
            playerData.AddBet(sectorIndex);
            sectorTotals[sectorIndex]++;
            OnSectorChangeCountBet?.Invoke(sectorIndex, sectorTotals[sectorIndex]);

            if (playerData.IsCompleted)
            {
                Debug.Log("All bets in player index - " + playerIndex);
                OnPlayerBetCompleted?.Invoke(playerIndex);
            }
        }
        else
        {
            Debug.Log("Already have bet");
        }

    }

    public bool IsPlayerBetCompleted(int playerIndex)
    {
        return playerBets[playerIndex].IsCompleted;
    }

    public bool TryGetRandomAvailableSector(int playerIndex, out int sectorIndex)
    {
        return playerBets[playerIndex].TryGetRandomAvailableSector(out sectorIndex);
    }

    #region Output

    public event Action<int> OnPlayerBetCompleted;
    public event Action<int, int> OnAddBet;
    public event Action<int, int> OnSectorChangeCountBet;

    #endregion
}

public class PlayerBetData
{
    public int PlayerIndex { get; }
    public int[] BetsPerSector { get; }
    public int TotalBetsPlaced { get; private set; }

    public bool IsCompleted => TotalBetsPlaced >= BetsPerSector.Length;

    public PlayerBetData(int playerIndex, int sectorCount)
    {
        PlayerIndex = playerIndex;
        BetsPerSector = new int[sectorCount];
        TotalBetsPlaced = 0;
    }

    public bool CheckAvailableSector(int sectorIndex)
    {
        if(BetsPerSector[sectorIndex] != 0)
        {
            return false;
        }

        return true;
    }

    public bool TryGetRandomAvailableSector(out int sectorIndex)
    {
        List<int> freeSectors = new();

        for (int i = 0; i < BetsPerSector.Length; i++)
        {
            if (BetsPerSector[i] == 0)
                freeSectors.Add(i);
        }

        if(freeSectors.Count == 0)
        {
            sectorIndex = -1;
            return false;
        }

        sectorIndex = freeSectors[Random.Range(0, freeSectors.Count)];
        return true;
    }

    public void AddBet(int sectorIndex)
    {
        BetsPerSector[sectorIndex]++;
        TotalBetsPlaced += 1;
    }
}
