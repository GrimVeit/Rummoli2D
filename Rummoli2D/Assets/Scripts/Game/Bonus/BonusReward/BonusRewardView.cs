using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BonusRewardView : View
{
    [SerializeField] private BonusRewardTransformsGroup bonusRewardTransformsGroup;
    [SerializeField] private BonusRewardConfigs bonusRewardConfigs;
    [SerializeField] private Transform transformSpawn;
    [SerializeField] private float durationMinWait;
    [SerializeField] private float durationMaxWait;
    [SerializeField] private float durationMinMove;
    [SerializeField] private float durationMaxMove;

    private List<BonusReward> _currentBonusRewards = new();

    public void ChooseBonusTypesForReward(List<BonusType> bonusTypes)
    {
        var tranformsPosition = bonusRewardTransformsGroup.GetBonusRewardTransforms(bonusTypes.Count);

        for (int i = 0; i < bonusTypes.Count; i++)
        {
            var config = bonusRewardConfigs.GetBonusRewardConfig(bonusTypes[i]);

            var visual = Instantiate(config.BonusRewardPrefab, transformSpawn);
            visual.transform.SetLocalPositionAndRotation(tranformsPosition.Transforms[i].localPosition, Quaternion.identity);
            visual.SetData(config.TransformEnd);

            visual.OnAddBonus += AddBonus;
            visual.OnAddSound += AddSound;
            visual.OnDestroyed += HandleVisualDestroyed;
            _currentBonusRewards.Add(visual);
        }
    }

    public void ActivateMove()
    {
        if(_currentBonusRewards.Count == 0)
        {
            Debug.LogError("Not found bonus rewarded");
            return;
        }

        _currentBonusRewards.ForEach(bonus => bonus.ActivateMove(Random.Range(durationMinWait, durationMaxWait), Random.Range(durationMinMove, durationMaxMove)));
    }

    private void HandleVisualDestroyed(BonusReward visual)
    {
        visual.OnAddBonus -= AddBonus;
        visual.OnAddSound -= AddSound;
        visual.OnDestroyed -= HandleVisualDestroyed;
        _currentBonusRewards.Remove(visual);

        if (_currentBonusRewards.Count == 0)
            OnAllBonusRewarded?.Invoke();
    }

    #region Output

    public event Action<BonusType> OnAddBonus;
    public event Action OnAddSound;
    public event Action OnAllBonusRewarded;

    private void AddBonus(BonusType type)
    {
        OnAddBonus?.Invoke(type);
    }

    private void AddSound()
    {
        OnAddSound?.Invoke();
    }

    #endregion
}

[System.Serializable]
public class BonusRewardConfigs
{
    [SerializeField] private List<BonusRewardConfig> bonusRewardConfigs;

    public BonusRewardConfig GetBonusRewardConfig(BonusType bonusType)
    {
        return bonusRewardConfigs.Find(config => config.BonusRewardPrefab.BonusType == bonusType);
    }
}

[System.Serializable]
public class BonusRewardConfig
{
    [SerializeField] private BonusReward bonusRewardPrefab;
    [SerializeField] private Transform transformEnd;

    public BonusReward BonusRewardPrefab => bonusRewardPrefab;
    public Transform TransformEnd => transformEnd;
}



[System.Serializable]
public class BonusRewardTransformsGroup
{
    [SerializeField] private List<BonusRewardTransforms> bonusRewardTransforms = new();

    public BonusRewardTransforms GetBonusRewardTransforms(int count)
    {
        return bonusRewardTransforms.Find(transforms => transforms.Count == count);
    }
}

[System.Serializable]
public class BonusRewardTransforms
{
    [SerializeField] private int count;
    [SerializeField] private List<Transform> transforms = new();

    public int Count => count;
    public List<Transform> Transforms => transforms;
}
