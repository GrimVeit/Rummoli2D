using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorSingleCardCondition : SectorConditionBase
{
    private readonly CardRank _rank;
    private readonly CardSuit _suit;

    private int _closingPlayerId;
    private ICard _closingCard;

    public SectorSingleCardCondition(CardRank rank, CardSuit suit, int sectorIndex)
    {
        _rank = rank;
        _suit = suit;
        SectorIndex = sectorIndex;
        Status = SectorStatus.Pending;
    }

    public override void CheckCard(int playerId, ICard card)
    {
        if (Status == SectorStatus.Claimed) return;

        if (card.CardRank == _rank && card.CardSuit == _suit)
        {
            _closingPlayerId = playerId;
            _closingCard = card;
            Status = SectorStatus.Ready;
        }
    }

    public override void Reset()
    {
        _closingPlayerId = -1;
        _closingCard = null;
        Status = SectorStatus.Pending;
    }

    public override void Complete()
    {
        Status = SectorStatus.Claimed;
    }

    public override (int playerId, ICard card) GetClosingInfo()
    {
        return (_closingPlayerId, _closingCard);
    }
}
