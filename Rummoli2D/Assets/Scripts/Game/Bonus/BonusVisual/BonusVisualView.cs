using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusVisualView : View
{
    [SerializeField] private List<BonusVisual> visuals = new();

    public void Initialize()
    {
        visuals.ForEach(x => x.OnChooseBonus += ChooseBonus);
    }

    public void Dispose()
    {
        visuals.ForEach(x => x.OnChooseBonus -= ChooseBonus);
    }

    public void SetBonusCount(BonusType bonusType, int count)
    {
        var visual = GetBonusVisual(bonusType);

        if(visual == null)
        {
            Debug.LogWarning("Not found BonusVisual with BonusType - " + bonusType);
            return;
        }

        visual.SetBonusCount(count);
    }

    public void ActivateInteraction() => visuals.ForEach(x => x.ActivateInteraction());
    public void DeactivateInteraction() => visuals.ForEach(x => x.DeactivateInteraction());

    private BonusVisual GetBonusVisual(BonusType bonusType)
    {
        return visuals.Find(data => data.BonusType == bonusType);
    }

    #region Output

    public event Action<BonusType> OnChooseBonus;

    private void ChooseBonus(BonusType bonusType)
    {
        OnChooseBonus?.Invoke(bonusType);
    }

    #endregion
}
