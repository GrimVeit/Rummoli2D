using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPokerHandSelectorPresenter : ICardPokerSelectorPlayerProvider, ICardPokerSelectorBotProvider, IPokerHandEvaluator
{
    private readonly CardPokerHandSelectorModel _model;

    public CardPokerHandSelectorPresenter(CardPokerHandSelectorModel model)
    {
        _model = model;
    }

    #region Input
    public List<ICard> ChooseHandPlayer(List<ICard> cards) => _model.ChooseHandPlayer(cards);
    public List<ICard> ChooseHandBot(List<ICard> cards) => _model.ChooseHandBot(cards);

    public int GetWinner(Dictionary<int, List<ICard>> playersHands) => _model.GetWinner(playersHands);
    public HandRank GetHandRank(List<ICard> hand) => _model.GetHandRank(hand);

    #endregion
}

public interface ICardPokerSelectorPlayerProvider
{
    public List<ICard> ChooseHandPlayer(List<ICard> cards);
}

public interface ICardPokerSelectorBotProvider
{
    public List<ICard> ChooseHandBot(List<ICard> cards);
}

public interface IPokerHandEvaluator
{
    public int GetWinner(Dictionary<int, List<ICard>> playersHands);
    public HandRank GetHandRank(List<ICard> hand);
}
