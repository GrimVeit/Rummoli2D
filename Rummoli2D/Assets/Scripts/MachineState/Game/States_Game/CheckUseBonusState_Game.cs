using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckUseBonusState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly IBonusApplierInfoProvider _bonusApplierInfoProvider;

    public CheckUseBonusState_Game(IGlobalStateMachineProvider machineProvider, IBonusApplierInfoProvider bonusApplierInfoProvider)
    {
        _machineProvider = machineProvider;
        _bonusApplierInfoProvider = bonusApplierInfoProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE CHECK USE BONUS STATE</color>");

        switch (_bonusApplierInfoProvider.CurrentBonusType())
        {
            case BonusType.Oracle:
            case BonusType.EvilTongue:
                ChangeStateToBonusVisible();
                break;
            case BonusType.Amulet:
            case BonusType.NormalKey:
            case BonusType.GoldenKey:
                ChangeStateToBonusTarget();
                break;
        }
    }

    public void ExitState()
    {

    }

    private void ChangeStateToBonusVisible()
    {
        _machineProvider.SetState(_machineProvider.GetState<BonusVisibleState_Game>());
    }

    private void ChangeStateToBonusTarget()
    {
        _machineProvider.SetState(_machineProvider.GetState<BonusTargetState_Game>());
    }
}
