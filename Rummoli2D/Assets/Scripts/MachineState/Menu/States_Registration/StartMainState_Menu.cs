using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMainState_Menu : IState
{
    private readonly IStateMachineProvider _machineProvider;
    private readonly FirebaseAuthenticationPresenter _firebaseAuthenticationPresenter;
    private readonly FirebaseDatabasePresenter _firebaseDatabasePresenter;

    public StartMainState_Menu(IStateMachineProvider machineProvider, FirebaseDatabasePresenter firebaseDatabasePresenter, FirebaseAuthenticationPresenter firebaseAuthenticationPresenter)
    {
        _machineProvider = machineProvider;
        _firebaseDatabasePresenter = firebaseDatabasePresenter;
        _firebaseAuthenticationPresenter = firebaseAuthenticationPresenter;
    }

    public StartMainState_Menu(IStateMachineProvider machineProvider)
    {
        _machineProvider = machineProvider;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - START MAIN STATE / MENU</color>");

        if (_firebaseAuthenticationPresenter.IsAuthorization())
        {
            _firebaseDatabasePresenter.SaveChangeToServer();
            //_firebaseDatabasePresenter.DisplayUsersRecords();
        }

        ChangeStateToMain();
    }

    public void ExitState()
    {

    }

    private void ChangeStateToMain()
    {
        _machineProvider.EnterState(_machineProvider.GetState<MainState_Menu>());
    }

}
