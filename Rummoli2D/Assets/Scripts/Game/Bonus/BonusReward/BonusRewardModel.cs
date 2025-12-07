using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BonusRewardModel
{
    private readonly Dictionary<BonusType, int> weights = new Dictionary<BonusType, int>
    {
        { BonusType.Oracle, 3 },
        { BonusType.EvilTongue, 3 },
        { BonusType.Amulet, 5 },
        { BonusType.NormalKey, 2 },
        { BonusType.GoldenKey, 1 },
        { BonusType.Health, 2 }
     };

    private readonly IStoreBonusProvider _storeBonusProvider;
    private readonly IPlayerHealthProvider _playerHealthProvider;
    private readonly ISoundProvider _soundProvider;

    private List<BonusType> _currentBonusesType;

    public BonusRewardModel(IStoreBonusProvider storeBonusProvider, IPlayerHealthProvider playerHealthProvider, ISoundProvider soundProvider)
    {
        _storeBonusProvider = storeBonusProvider;
        _playerHealthProvider = playerHealthProvider;
        _soundProvider = soundProvider;
    }

    public void AllBonusRewarded()
    {
        OnAllBonusRewarded?.Invoke();
    }

    public void AddBonus(BonusType type)
    {
        if(type != BonusType.Health)
        {
            _storeBonusProvider.AddBonus(type, 1);
        }
        else
        {
            _playerHealthProvider.AddHealth(1);
        }
    }

    public void AddSound()
    {
        _soundProvider.PlayOneShot("AddBonus");
    }


    public void CreateBonuses(int count)
    {
        _currentBonusesType = GetRandomWeightedBonuses(count);

        OnChooseBonusesForReward?.Invoke(_currentBonusesType);
    }

    public void ActivateMove()
    {
        OnActivateMove?.Invoke();
    }

    private List<BonusType> GetRandomWeightedBonuses(int count)
    {
        if (count < 1 || count > weights.Count)
            throw new System.ArgumentOutOfRangeException(nameof(count), "Count must be between 1 and the number of bonus types.");

        var available = new Dictionary<BonusType, int>(weights);
        var result = new List<BonusType>();

        for (int i = 0; i < count; i++)
        {
            int totalWeight = available.Values.Sum();
            int r = UnityEngine.Random.Range(0, totalWeight);

            int cumulative = 0;
            foreach (var kvp in available)
            {
                cumulative += kvp.Value;
                if (r < cumulative)
                {
                    result.Add(kvp.Key);
                    available.Remove(kvp.Key);
                    break;
                }
            }
        }

        return result;
    }

    #region Output

    public event Action<List<BonusType>> OnChooseBonusesForReward;
    public event Action OnActivateMove;

    public event Action OnAllBonusRewarded;


    public bool IsHaveBonus(BonusType type)
    {
        return _currentBonusesType != null && _currentBonusesType.Contains(type);
    }
    #endregion
}
