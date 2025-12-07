using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoreBonusModel
{
    private readonly Dictionary<BonusType, int> _bonusTypes = new()
    {
        { BonusType.Oracle, 0 },
        { BonusType.EvilTongue, 0 },
        { BonusType.Amulet, 0 },
        { BonusType.NormalKey, 0 },
        { BonusType.GoldenKey, 0 }
    };

    public void Initialize()
    {
        foreach (var bonusType in _bonusTypes)
        {
            OnChangedBonusCount?.Invoke(bonusType.Key, bonusType.Value);
        }
    }

    public void Dispose()
    {
        
    }


    #region Input

    public void AddBonus(BonusType bonusType, int count)
    {
        _bonusTypes[bonusType] += count;
        OnChangedBonusCount?.Invoke(bonusType, _bonusTypes[bonusType]);
    }

    public void RemoveBonus(BonusType bonusType, int count)
    {
        _bonusTypes[bonusType] -= count;

        if(_bonusTypes[bonusType] < 0)
        {
            Debug.Log("Bonus count cannot be negative. Set to 0.");
            _bonusTypes[bonusType] = 0;
        }

        OnChangedBonusCount?.Invoke(bonusType, _bonusTypes[bonusType]);
    }

    public int BonusCount(BonusType bonusType) => _bonusTypes[bonusType];

    #endregion

    #region Output

    public event Action<BonusType, int> OnChangedBonusCount;

    #endregion
}

public enum BonusType
{
    Oracle = 0,
    EvilTongue = 1,
    Amulet = 2,
    NormalKey = 3,
    GoldenKey = 4,
    Health = 5
}
