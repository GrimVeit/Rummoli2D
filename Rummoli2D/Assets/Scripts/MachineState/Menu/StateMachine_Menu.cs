using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine_Menu : IGlobalStateMachineProvider
{
    private readonly Dictionary<Type, IState> states = new();

    private IState _currentState;

    public StateMachine_Menu
        (UIMainMenuRoot sceneRoot,
        NicknamePresenter nicknamePresenter,
        AvatarPresenter avatarPresenter,
        FirebaseAuthenticationPresenter firebaseAuthenticationPresenter,
        FirebaseDatabasePresenter firebaseDatabasePresenter,
        IRulesVisualProvider rulesVisualProvider,
        IShopScrollProvider shopScrollProvider)
    {
        states[typeof(CheckAuthorizationState_Menu)] = new CheckAuthorizationState_Menu(this, firebaseAuthenticationPresenter);
        states[typeof(NameAndAvatarInputState_Menu)] = new NameAndAvatarInputState_Menu(this, sceneRoot, nicknamePresenter, firebaseAuthenticationPresenter, firebaseDatabasePresenter, avatarPresenter);
        states[typeof(RegistrationState_Menu)] = new RegistrationState_Menu(this, sceneRoot, firebaseAuthenticationPresenter, firebaseDatabasePresenter);


        states[typeof(StartMainState_Menu)] = new StartMainState_Menu(this, firebaseDatabasePresenter, firebaseAuthenticationPresenter);
        states[typeof(MainState_Menu)] = new MainState_Menu(this, sceneRoot);

        states[typeof(RulesState_Menu)] = new RulesState_Menu(this, sceneRoot, rulesVisualProvider);
        states[typeof(ProfileState_Menu)] = new ProfileState_Menu(this, sceneRoot);
        states[typeof(BalanceState_Menu)] = new BalanceState_Menu(this, sceneRoot);
        states[typeof(SettingsState_Menu)] = new SettingsState_Menu(this, sceneRoot);
        states[typeof(ShopState_Menu)] = new ShopState_Menu(this, sceneRoot, shopScrollProvider);
    }

    public void Initialize()
    {
        SetState(GetState<MainState_Menu>());
    }

    public void Dispose()
    {

    }

    public IState GetState<T>() where T : IState
    {
        return states[typeof(T)];
    }

    public void SetState(IState state)
    {
        _currentState?.ExitState();

        _currentState = state;
        _currentState.EnterState();
    }
}
