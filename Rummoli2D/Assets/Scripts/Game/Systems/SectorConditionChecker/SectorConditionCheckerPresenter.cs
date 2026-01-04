using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectorConditionCheckerPresenter : ISectorConditionCheckerProvider
{
    private readonly SectorConditionCheckerModel _model;

    public SectorConditionCheckerPresenter(SectorConditionCheckerModel model)
    {
        _model = model;
    }

    #region Input

    public void AddCard(int playerId, ICard card) => _model.AddCard(playerId, card);

    public bool IsHaveClosedSectors() => _model.IsHaveClosedSectors();

    public List<(int sectorIndex, int playerId)> GetClosedSectors() => _model.GetClosedSectors();

    public void ClaimSector(int sectorIndex) => _model.ClaimSector(sectorIndex);

    public void Reset() => _model.Reset();

    #endregion
}

public interface ISectorConditionCheckerProvider
{
    public void AddCard(int playerId, ICard card);

    public bool IsHaveClosedSectors();

    public List<(int sectorIndex, int playerId)> GetClosedSectors();

    public void ClaimSector(int sectorIndex);

    public void Reset();
}
