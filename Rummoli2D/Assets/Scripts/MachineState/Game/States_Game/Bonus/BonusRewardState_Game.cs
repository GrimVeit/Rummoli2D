using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusRewardState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly UIGameRoot _sceneRoot;
    private readonly IBonusRewardProvider _bonusRewardProvider;
    private readonly IBonusRewardEventsProvider _bonusRewardEventsProvider;
    private readonly IBonusRewardInfoProvider _bonusRewardInfoProvider;

    public BonusRewardState_Game(IGlobalStateMachineProvider machineProvider, UIGameRoot sceneRoot, IBonusRewardProvider bonusRewardProvider, IBonusRewardEventsProvider bonusRewardEventsProvider, IBonusRewardInfoProvider bonusRewardInfoProvider)
    {
        _machineProvider = machineProvider;
        _sceneRoot = sceneRoot;
        _bonusRewardProvider = bonusRewardProvider;
        _bonusRewardEventsProvider = bonusRewardEventsProvider;
        _bonusRewardInfoProvider = bonusRewardInfoProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE BONUS REWARD STATE</color>");

        _bonusRewardEventsProvider.OnAllBonusRewarded += ChangeStateToCheckWinLose;

        _bonusRewardProvider.ActivateMove();
        _sceneRoot.OpenFooterPanel();

        if (_bonusRewardInfoProvider.IsHaveBonus(BonusType.Health))
        {
            _sceneRoot.OpenMainPanel();
        }
    }

    public void ExitState()
    {
        _bonusRewardEventsProvider.OnAllBonusRewarded -= ChangeStateToCheckWinLose;

        _sceneRoot.CloseBonusRewardPanel();
    }

    private void ChangeStateToCheckWinLose()
    {
        _machineProvider.SetState(_machineProvider.GetState<CheckWinLoseState_Game>());
    }
}
