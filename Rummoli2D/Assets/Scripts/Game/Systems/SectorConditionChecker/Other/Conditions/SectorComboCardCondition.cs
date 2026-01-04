using System.Collections;
using System.Collections.Generic;
using System.Linq;
public class SectorComboCardCondition : SectorConditionBase
{
    private readonly CardData[] _requiredCards;
    private (ICard card, int playerId)[] _playedCards;

    public SectorComboCardCondition(CardData firstCard, CardData secondCard, int sectorIndex)
    {
        _requiredCards = new[] { firstCard, secondCard };
        _playedCards = new (ICard, int)[_requiredCards.Length];
        SectorIndex = sectorIndex;
        Status = SectorStatus.Pending;
    }

    public override void CheckCard(int playerId, ICard card)
    {
        if (Status == SectorStatus.Claimed)
            return;

        for (int i = 0; i < _requiredCards.Length; i++)
        {
            if (_requiredCards[i].Rank == card.CardRank && _requiredCards[i].Suit == card.CardSuit)
            {
                _playedCards[i] = (card, playerId);
            }
        }

        if (_playedCards.All(pc => pc.card != null))
        {
            int firstPlayerId = _playedCards[0].playerId;
            if (_playedCards.All(pc => pc.playerId == firstPlayerId))
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
