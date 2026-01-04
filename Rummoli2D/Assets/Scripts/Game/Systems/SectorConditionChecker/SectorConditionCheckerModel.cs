using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SectorConditionCheckerModel
{
    private readonly List<SectorConditionBase> _conditions;

    public SectorConditionCheckerModel()
    {
        _conditions = new List<SectorConditionBase>()
        {
            new SectorSingleCardCondition(CardRank.Ten, CardSuit.Spades, 1),
            new SectorSingleCardCondition(CardRank.Jack, CardSuit.Diamonds, 2),
            new SectorSingleCardCondition(CardRank.Queen, CardSuit.Clubs, 3),
            new SectorSingleCardCondition(CardRank.King, CardSuit.Hearts, 4),
            new SectorSingleCardCondition(CardRank.Ace, CardSuit.Spades, 5),
            new SectorComboCardCondition(new CardData(CardRank.King, CardSuit.Diamonds), new CardData(CardRank.Ace, CardSuit.Diamonds), 6),
            new SectorSequenceCardCondition(7)

        };
    }

    public void AddCard(int playerId, ICard card)
    {
        foreach (var condition in _conditions)
        {
            condition.CheckCard(playerId, card);
        }
    }

    public bool IsHaveClosedSectors()
    {
        return _conditions.Any(c => c.Status == SectorStatus.Ready);
    }

    public List<(int sectorIndex, int playerId)> GetClosedSectors()
    {
        return _conditions
            .Where(c => c.Status == SectorStatus.Ready)
            .Select(c => (c.SectorIndex, c.GetClosingInfo().playerId))
            .ToList();
    }

    public void ClaimSector(int sectorIndex)
    {
        var condition = _conditions.FirstOrDefault(c => c.SectorIndex == sectorIndex);
        condition?.Complete();
    }

    public void Reset()
    {
        foreach (var c in _conditions)
            c.Reset();
    }
}
