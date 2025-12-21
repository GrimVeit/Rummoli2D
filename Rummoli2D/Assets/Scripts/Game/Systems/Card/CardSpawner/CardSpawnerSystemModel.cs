using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class CardSpawnerSystemModel
{
    private readonly CardThemesSO _cardThemesSO;
    private readonly CardsSO _cardsSO;

    private readonly List<ICard> _cards = new();

    public CardSpawnerSystemModel(CardThemesSO cardThemesSO, CardsSO cardsSO)
    {
        _cardThemesSO = cardThemesSO;
        _cardsSO = cardsSO;

        Reset();
    }

    public void Spawn(int playerId)
    {
        var card = _cards[Random.Range(0, _cards.Count)];

        _cards.Remove(card);

        OnSpawnCard?.Invoke(playerId, card);
    }

    public void SpawnEnd(int playerId, ICard card)
    {
        OnSpawnCardEnd?.Invoke(playerId, card);
    }

    public void Reset()
    {
        var cardTheme = _cardThemesSO.GetCardTheme(Theme.Standard);

        _cards.Clear();

        for (int i = 0; i < _cardsSO.Cards.Count; i++)
        {
            var cardSO = _cardsSO.Cards[i];

            var card = new Card(cardSO, cardTheme.GetSpriteCover(), cardTheme.GetSpriteFace(cardSO.CardSuit, cardSO.CardRank));
            _cards.Add(card);
        }
    }

    #region Output

    public event Action<int, ICard> OnSpawnCard;

    public event Action<int, ICard> OnSpawnCardEnd;

    #endregion
}
