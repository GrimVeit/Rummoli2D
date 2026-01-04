using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SectorSequenceCardCondition : SectorConditionBase
{
    private readonly CardRank[] _requiredRanks = { CardRank.Seven, CardRank.Eight, CardRank.Nine };
    private (ICard card, int playerId)[] _playedCards = new (ICard, int)[3];

    public SectorSequenceCardCondition(int sectorIndex)
    {
        SectorIndex = sectorIndex;
        Status = SectorStatus.Pending;
    }

    public override void CheckCard(int playerId, ICard card)
    {
        if (Status == SectorStatus.Claimed)
            return;

        for (int i = 0; i < _requiredRanks.Length; i++)
        {
            if (card.CardRank == _requiredRanks[i])
            {
                _playedCards[i] = (card, playerId);
            }
        }

        if (_playedCards.All(pc => pc.card != null))
        {
            int firstPlayerId = _playedCards[0].playerId;
            if (!_playedCards.All(pc => pc.playerId == firstPlayerId))
                return;

            CardSuit suit = _playedCards[0].card.CardSuit;
            if (_playedCards.All(pc => pc.card.CardSuit == suit))
            {
                Status = SectorStatus.Ready;
            }
        }
    }

    public override void Reset()
    {
        for (int i = 0; i < _playedCards.Length; i++)
            _playedCards[i] = (null, -1);
        Status = SectorStatus.Pending;
    }

    public override void Complete()
    {
        Status = SectorStatus.Claimed;
    }

    public override (int playerId, ICard card) GetClosingInfo()
    {
        if (_playedCards.Length == 0 || _playedCards[0].card == null)
            return (-1, null);

        return (_playedCards[0].playerId, _playedCards[0].card);
    }
}
