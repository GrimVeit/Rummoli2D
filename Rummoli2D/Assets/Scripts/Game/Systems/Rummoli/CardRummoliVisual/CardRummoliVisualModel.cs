using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRummoliVisualModel
{
    private readonly CardThemesSO _cardThemesSO;
    private readonly IStoreCardRummoliListener _storeCardRummoliListener;

    public CardRummoliVisualModel(IStoreCardRummoliListener storeCardRummoliListener, CardThemesSO cardThemesSO)
    {
        _cardThemesSO = cardThemesSO;
        _storeCardRummoliListener = storeCardRummoliListener;

        _storeCardRummoliListener.OnCurrentCardDataChanged += ChangeCurrentCardData;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _storeCardRummoliListener.OnCurrentCardDataChanged -= ChangeCurrentCardData;
    }

    private void ChangeCurrentCardData(CardData cardData)
    {
        if (cardData == null) return;

        var sprite = _cardThemesSO.GetCardTheme(Theme.Standard).GetSpriteFace(cardData.Suit, cardData.Rank);

        OnCurrentCardDataChanged?.Invoke(sprite);
    }

    public event Action<Sprite> OnCurrentCardDataChanged;
}
