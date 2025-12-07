using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMainState_Menu : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly FirebaseAuthenticationPresenter _firebaseAuthenticationPresenter;
    private readonly FirebaseDatabasePresenter _firebaseDatabasePresenter;

    public StartMainState_Menu(IGlobalStateMachineProvider machineProvider, FirebaseDatabasePresenter firebaseDatabasePresenter, FirebaseAuthenticationPresenter firebaseAuthenticationPresenter)
    {
        _machineProvider = machineProvider;
        _firebaseDatabasePresenter = firebaseDatabasePresenter;
        _firebaseAuthenticationPresenter = firebaseAuthenticationPresenter;
    }

    public StartMainState_Menu(IGlobalStateMachineProvider machineProvider)
    {
        _machineProvider = machineProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - START MAIN STATE / MENU</color>");

        if (_firebaseAuthenticationPresenter.IsAuthorization())
        {
            _firebaseDatabasePresenter.SaveChangeToServer();
            _firebaseDatabasePresenter.DisplayUsersRecords();
        }

        ChangeStateToMain();
    }

    public void ExitState()
    {

    }

    private void ChangeStateToMain()
    {
        _machineProvider.SetState(_machineProvider.GetState<MainState_Menu>());
    }

}
