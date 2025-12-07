using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusVisibleState_Game : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly IBonusApplierProvider _bonusApplierProvider;
    private readonly IBonusApplierEventsProvider _bonusApplierEventsProvider;
    private readonly UIGameRoot _sceneRoot;

    private IEnumerator timer;

    public BonusVisibleState_Game(IGlobalStateMachineProvider machineProvider, IBonusApplierProvider bonusApplierProvider, UIGameRoot sceneRoot, IBonusApplierEventsProvider bonusApplierEventsProvider)
    {
        _machineProvider = machineProvider;
        _bonusApplierProvider = bonusApplierProvider;
        _sceneRoot = sceneRoot;
        _bonusApplierEventsProvider = bonusApplierEventsProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE BONUS VISIBLE STATE</color>");

        _bonusApplierEventsProvider.OnNotApplyBonus += _sceneRoot.OpenNoApplyBonusPanel;

        if (timer != null) Coroutines.Stop(timer);

        timer = Timer();
        Coroutines.Start(timer);

        _bonusApplierProvider.ApplyBonus();
    }

    public void ExitState()
    {
        _bonusApplierEventsProvider.OnNotApplyBonus -= _sceneRoot.OpenNoApplyBonusPanel;

        if (timer != null) Coroutines.Stop(timer);

        _sceneRoot.OpenFooterPanel();
        _sceneRoot.CloseNoApplyBonusPanel();
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(1.2f);

        ChangeStateToMainMenu();
    }

    private void ChangeStateToMainMenu()
    {
        _machineProvider.SetState(_machineProvider.GetState<MainState_Game>());
    }
}
