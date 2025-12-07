using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAuthorizationState_Menu : IState
{
    private readonly IGlobalStateMachineProvider _machineProvider;
    private readonly FirebaseAuthenticationPresenter _firebaseAuthentication;

    public CheckAuthorizationState_Menu(IGlobalStateMachineProvider machineProvider, FirebaseAuthenticationPresenter firebaseAuthentication)
    {
        _machineProvider = machineProvider;
        _firebaseAuthentication = firebaseAuthentication;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - AUTHORIZATION STATE / MENU</color>");

        if (_firebaseAuthentication.IsAuthorization())
        {
            ChangeStateToStartMain();
        }
        else
        {
            ChangeStateToStartRegistration();
        }
    }

    public void ExitState()
    {

    }

    private void ChangeStateToStartRegistration()
    {
        _machineProvider.SetState(_machineProvider.GetState<NameAndAvatarInputState_Menu>());
    }

    private void ChangeStateToStartMain()
    {
        _machineProvider.SetState(_machineProvider.GetState<StartMainState_Menu>());
    }
}
