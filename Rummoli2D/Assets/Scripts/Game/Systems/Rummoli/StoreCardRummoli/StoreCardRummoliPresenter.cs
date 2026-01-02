using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreCardRummoliPresenter : IStoreCardRummoliProvider, IStoreCardRummoliListener
{
    private readonly StoreCardRummoliModel _model;

    public StoreCardRummoliPresenter(StoreCardRummoliModel model)
    {
        _model = model;
    }

    public void Initialize()
    {
        _model.Initialize();
    }

    public void Dispose()
    {

    }

    #region Output

    public event Action<CardData> OnCurrentCardDataChanged
    {
        add => _model.OnCurrentCardDataChanged += value;
        remove => _model.OnCurrentCardDataChanged -= value;
    }

    #endregion

    #region Input
    public CardData CurrentCardData{ get => _model.CurrentCardData; set => _model.CurrentCardData = value;
    }
    public CardData NextCard() => _model.NextCard();
    public CardData ChooseSuit(CardSuit suit) => _model.ChooseSuit(suit);
    public CardData ChooseRandomSuit() => _model.ChooseRandomSuit();
    public void Reset() => _model.Reset();

    #endregion
}

public interface IStoreCardRummoliListener
{
    public event Action<CardData> OnCurrentCardDataChanged;
}

public interface IStoreCardRummoliProvider
{
    public CardData CurrentCardData { get; set; }
    public CardData NextCard();
    public CardData ChooseSuit(CardSuit suit);
    public CardData ChooseRandomSuit();
    public void Reset();
}
