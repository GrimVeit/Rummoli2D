using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class CardSpawnerSystemModel
{
    private readonly CardThemesSO _cardThemesSO;
    private readonly CardsSO _cardsSO;
    private readonly IStoreCardDesignInfoProvider _storeCardDesignInfoProvider;
    private readonly List<Theme> _themes = new() { Theme.Standard, Theme.Custom };

    private readonly List<ICard> _cards = new();

    public CardSpawnerSystemModel(CardThemesSO cardThemesSO, CardsSO cardsSO, IStoreCardDesignInfoProvider storeCardDesignInfoProvider)
    {
        _cardThemesSO = cardThemesSO;
        _cardsSO = cardsSO;
        _storeCardDesignInfoProvider = storeCardDesignInfoProvider;

        Reset();
    }

    public void Spawn(int playerId)
    {
        var card = _cards[Random.Range(0, _cards.Count)];

        _cards.Remove(card);

        OnSpawnCard?.Invoke(playerId, card, _storeCardDesignInfoProvider.GetCardDesignIndex());

        OnChangeCardCount?.Invoke(_cards.Count);
    }

    public void SpawnEnd(int playerId, ICard card)
    {
        OnSpawnCardEnd?.Invoke(playerId, card);
    }

    public void Reset()
    {
        var cardTheme = _cardThemesSO.GetCardTheme(_themes[_storeCardDesignInfoProvider.GetCardDesignIndex()]);

        _cards.Clear();

        for (int i = 0; i < _cardsSO.Cards.Count; i++)
        {
            var cardSO = _cardsSO.Cards[i];

            var card = new Card(cardSO, cardTheme.GetSpriteCover(), cardTheme.GetSpriteFace(cardSO.CardSuit, cardSO.CardRank));
            _cards.Add(card);
        }

        OnChangeCardCount?.Invoke(_cards.Count);
    }

    #region Output

    public event Action<int, ICard, int> OnSpawnCard;

    public event Action<int, ICard> OnSpawnCardEnd;

    public event Action<int> OnChangeCardCount;

    #endregion
}
