using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorNothingPanel_Game : MovePanel
{
    [SerializeField] private LazyMotionGroup _group;

    public override void Initialize()
    {
        base.Initialize();

        _group.Initialize();
    }

    public override void Dispose()
    {
        base.Dispose();

        _group.Dispose();
    }

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        _group.Deactivate();
        _group.Activate();
    }
}
