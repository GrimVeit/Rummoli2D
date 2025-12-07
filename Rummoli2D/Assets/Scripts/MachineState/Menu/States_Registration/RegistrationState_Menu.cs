using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegistrationState_Menu : IState
{
    private readonly IGlobalStateMachineProvider _globalStateMachineProvider;
    private readonly UIMainMenuRoot _sceneRoot;
    private readonly FirebaseAuthenticationPresenter _firebaseAuthenticationPresenter;
    private readonly FirebaseDatabasePresenter _firebaseDatabasePresenter;

    public RegistrationState_Menu(IGlobalStateMachineProvider globalStateMachineProvider, UIMainMenuRoot sceneRoot, FirebaseAuthenticationPresenter firebaseAuthenticationPresenter, FirebaseDatabasePresenter firebaseDatabasePresenter)
    {
        _globalStateMachineProvider = globalStateMachineProvider;
        _sceneRoot = sceneRoot;
        _firebaseAuthenticationPresenter = firebaseAuthenticationPresenter;
        _firebaseDatabasePresenter = firebaseDatabasePresenter;
    }

    public void EnterState()
    {
        Debug.Log("<color=red>ACTIVATE STATE - REGISTRATION STATE / MENU</color>");

        _firebaseAuthenticationPresenter.OnSignUp += _firebaseDatabasePresenter.CreateEmptyDataToServer;
        _firebaseAuthenticationPresenter.OnSignUp += ChangeStateToStartMainMenu;

        _firebaseAuthenticationPresenter.OnSignUpError += ChangeStateToNameAndAvatarInput;

        _firebaseAuthenticationPresenter.SignUp();

        _sceneRoot.OpenLoadRegistrationPanel();
    }

    public void ExitState()
    {
        _firebaseAuthenticationPresenter.OnSignUp -= _firebaseDatabasePresenter.CreateEmptyDataToServer;
        _firebaseAuthenticationPresenter.OnSignUp -= ChangeStateToStartMainMenu;

        _firebaseAuthenticationPresenter.OnSignUpError -= ChangeStateToNameAndAvatarInput;

        _sceneRoot.CloseLoadRegistrationPanel();
    }

    private void ChangeStateToNameAndAvatarInput()
    {
        _globalStateMachineProvider.SetState(_globalStateMachineProvider.GetState<NameAndAvatarInputState_Menu>());
    }

    private void ChangeStateToStartMainMenu()
    {
        _sceneRoot.CloseBackgroundSecondPanel();

        _globalStateMachineProvider.SetState(_globalStateMachineProvider.GetState<StartMainState_Menu>());
    }
}
