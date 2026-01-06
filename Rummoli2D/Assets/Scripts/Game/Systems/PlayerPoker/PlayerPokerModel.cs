using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPokerModel
{
    private readonly IPokerHandEvaluator _pokerHandEvaluator;
    private readonly IStoreLanguageInfoProvider _languageInfoProvider;

    private readonly List<PlayerPokerData> _players = new();

    public PlayerPokerModel(IPokerHandEvaluator pokerHandEvaluator, IStoreLanguageInfoProvider storeLanguageInfoProvider)
    {
        _pokerHandEvaluator = pokerHandEvaluator;
        _languageInfoProvider = storeLanguageInfoProvider;
    }

    public void SetCountPlayer(int count)
    {
        OnSetCount?.Invoke(count);
    }

    public void SetPlayer(int playerId, string name, List<ICard> cards)
    {
        var handRank = _pokerHandEvaluator.GetHandRank(cards);
        var data = new PlayerPokerData(playerId, name, cards, handRank, NameLanguageUtility.GetHandRankName(handRank, _languageInfoProvider.CurrentLanguage));
        OnSetPlayer?.Invoke(data);

        _players.Add(data);
    }

    public void SearchWinner()
    {
        Dictionary<int, List<ICard>> playersDict = new();

        for (int i = 0; i < _players.Count; i++)
        {
            playersDict.Add(_players[i].PlayerId, _players[i].Cards);
        }

        var playerIdWinner = _pokerHandEvaluator.GetWinner(playersDict);

        OnSearchWinner?.Invoke(playerIdWinner);
    }

    public void ShowAll()
    {
        OnShowAll?.Invoke();
    }

    public void ClearAll()
    {
        _players.Clear();

        OnClearAll?.Invoke();
    }

    #region Output

    public event Action<int> OnSetCount;
    public event Action<PlayerPokerData> OnSetPlayer;
    public event Action OnShowAll;
    public event Action OnClearAll;

    public event Action<int> OnSearchWinner;

    #endregion
}

public class PlayerPokerData
{
    public int PlayerId { get; }
    public string Nickname { get; }
    public HandRank HandRank { get; }
    public string HandRankName { get; }
    public List<ICard> Cards { get; }

    public PlayerPokerData(int playerId, string name, List<ICard> cards, HandRank handRank, string handRankName)
    {
        PlayerId = playerId;
        Nickname = name;
        Cards = cards;
        HandRank = handRank;
        HandRankName = handRankName;
    }
}
