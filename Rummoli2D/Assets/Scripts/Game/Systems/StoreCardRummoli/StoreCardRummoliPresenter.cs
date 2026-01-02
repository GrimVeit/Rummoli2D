using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreCardRummoliPresenter
{
    private readonly StoreCardRummoliModel _model;

    public StoreCardRummoliPresenter(StoreCardRummoliModel model)
    {
        _model = model;
    }

    #region Input
    public CardData CurrentCardData => _model.CurrentCardData;
    public CardData NextCard() => _model.NextCard();
    public CardData ChooseSuit(CardSuit suit) => _model.ChooseSuit(suit);
    public CardData ChooseRandomSuit() => _model.ChooseRandomSuit();
    public void Reset() => _model.Reset();

    #endregion
}

public interface IStoreCardRummoliProvider
{
    public CardData CurrentCardData { get; }
    public CardData NextCard();
    public CardData ChooseSuit(CardSuit suit);
    public CardData ChooseRandomSuit();
    public void Reset();
}
