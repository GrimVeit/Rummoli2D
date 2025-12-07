using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScreenPanel : MovePanel
{
    [SerializeField] private UIEffectCombination effectCombination;
    [SerializeField] private LazyMotionGroup motionGroup;
    [SerializeField] private TestLoading testLoading;

    private void Awake() => Initialize();

    private void OnDestroy() => Dispose();


    public override void Initialize()
    {
        base.Initialize();

        effectCombination.Initialize();
        motionGroup.Initialize();
    }

    public override void Dispose()
    {
        base.Dispose();

        effectCombination.Dispose();
        motionGroup.Dispose();
    }

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        testLoading.Activate();
        effectCombination.ActivateEffect();
        motionGroup.Activate();
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();

        testLoading.Deactivate();
        effectCombination.DeactivateEffect();
        motionGroup.Deactivate();
    }
}
