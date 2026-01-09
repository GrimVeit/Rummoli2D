using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardRummoliVisualModel
{
    private readonly CardThemesSO _cardThemesSO;
    private readonly IStoreCardRummoliListener _storeCardRummoliListener;
    private readonly List<Theme> _themes = new() { Theme.Standard, Theme.Custom };
    private readonly IStoreCardDesignInfoProvider _storeCardDesignInfoProvider;

    public CardRummoliVisualModel(IStoreCardRummoliListener storeCardRummoliListener, CardThemesSO cardThemesSO, IStoreCardDesignInfoProvider storeCardDesignInfoProvider)
    {
        _cardThemesSO = cardThemesSO;
        _storeCardRummoliListener = storeCardRummoliListener;
        _storeCardDesignInfoProvider = storeCardDesignInfoProvider;

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

        var sprite = _cardThemesSO.GetCardTheme(_themes[_storeCardDesignInfoProvider.GetCardDesignIndex()]).GetSpriteFace(cardData.Suit, cardData.Rank);

        OnCurrentCardDataChanged?.Invoke(sprite);
    }

    public event Action<Sprite> OnCurrentCardDataChanged;
}
