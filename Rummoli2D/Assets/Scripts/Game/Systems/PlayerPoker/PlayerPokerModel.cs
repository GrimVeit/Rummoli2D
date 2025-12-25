using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPokerModel
{
    public void SetCountPlayer(int count)
    {
        OnSetCount?.Invoke(count);
    }

    public void SetPlayer(int playerId, string name, List<ICard> cards)
    {
        var data = new PlayerPokerData(playerId, name, cards);
        OnSetPlayer?.Invoke(data);
    }

    public void ClearAll()
    {
        OnClearAll?.Invoke();
    }

    #region Output

    public event Action<int> OnSetCount;
    public event Action<PlayerPokerData> OnSetPlayer;
    public event Action OnClearAll;

    #endregion
}

public class PlayerPokerData
{
    public int PlayerId { get; }
    public string Name { get; }
    public List<ICard> Cards { get; }

    public PlayerPokerData(int playerId, string name, List<ICard> cards)
    {
        PlayerId = playerId;
        Name = name;
        Cards = cards;
    }
}
