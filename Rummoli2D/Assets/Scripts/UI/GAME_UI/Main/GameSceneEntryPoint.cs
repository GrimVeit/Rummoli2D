using System;
using System.Collections;
using System.Collections.Generic;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine;

public class GameSceneEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private UIGameRoot menuRootPrefab;

    private UIGameRoot sceneRoot;
    private ViewContainer viewContainer;

    private BankPresenter bankPresenter;
    private ParticleEffectPresenter particleEffectPresenter;
    private ParticleEffectMaterialPresenter particleEffectMaterialPresenter;
    private SoundPresenter soundPresenter;
    private AvatarPresenter avatarPresenter;

    private BetSystemPresenter betSystemPresenter;
    private HighlightSystemPresenter highlightSystemPresenter;
    private PlayerPresentationSystemPresenter playerPresentationSystemPresenter;

    private PlayerPeople playerPeople;
    private PlayerBot playerBot_1;
    private PlayerBot playerBot_2;
    private PlayerBot playerBot_3;
    private PlayerBot playerBot_4;

    private StateMachine_Game stateMachine;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = menuRootPrefab;

        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        soundPresenter = new SoundPresenter
                    (new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS, PlayerPrefsKeys.KEY_VOLUME_SOUND, PlayerPrefsKeys.KEY_VOLUME_MUSIC),
                    viewContainer.GetView<SoundView>());

        particleEffectPresenter = new ParticleEffectPresenter
            (new ParticleEffectModel(),
            viewContainer.GetView<ParticleEffectView>());

        particleEffectMaterialPresenter = new ParticleEffectMaterialPresenter(new ParticleEffectMaterialModel(), viewContainer.GetView<ParticleEffectMaterialView>());

        bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());
        avatarPresenter = new AvatarPresenter(new AvatarModel(PlayerPrefsKeys.AVATAR), viewContainer.GetView<AvatarView>());

        betSystemPresenter = new BetSystemPresenter(new BetSystemModel(5), viewContainer.GetView<BetSystemView>());
        highlightSystemPresenter = new HighlightSystemPresenter(viewContainer.GetView<HighlightSystemView>());
        playerPresentationSystemPresenter = new PlayerPresentationSystemPresenter(new PlayerPresentationSystemModel(), viewContainer.GetView<PlayerPresentationSystemView>());

        playerPeople = new PlayerPeople(0, highlightSystemPresenter, betSystemPresenter, viewContainer);
        playerBot_1 = new PlayerBot(1, highlightSystemPresenter, betSystemPresenter, viewContainer);
        playerBot_2 = new PlayerBot(2, highlightSystemPresenter, betSystemPresenter, viewContainer);
        playerBot_3 = new PlayerBot(3, highlightSystemPresenter, betSystemPresenter, viewContainer);
        playerBot_4 = new PlayerBot(4, highlightSystemPresenter, betSystemPresenter, viewContainer);

        stateMachine = new StateMachine_Game
            (new List<IPlayer>() { playerPeople, playerBot_1, playerBot_2, playerBot_3, playerBot_4},
            sceneRoot,
            playerPresentationSystemPresenter);

        sceneRoot.SetSoundProvider(soundPresenter);
        sceneRoot.Activate();

        ActivateEvents();

        soundPresenter.Initialize();
        particleEffectPresenter.Initialize();
        particleEffectMaterialPresenter.Initialize();
        particleEffectMaterialPresenter.Activate();
        sceneRoot.Initialize();
        bankPresenter.Initialize();
        avatarPresenter.Initialize();

        betSystemPresenter.Initialize();
        playerPresentationSystemPresenter.Initialize();

        playerPeople.Initialize();
        playerBot_1.Initialize();
        playerBot_2.Initialize();
        playerBot_3.Initialize();
        playerBot_4.Initialize();

        stateMachine.Initialize();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!betSystemPresenter.IsPlayerBetCompleted(1))
            {
                if(betSystemPresenter.TryGetRandomAvailableSector(1, out int index))
                {
                    betSystemPresenter.AddBet(1, index);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!betSystemPresenter.IsPlayerBetCompleted(2))
            {
                if (betSystemPresenter.TryGetRandomAvailableSector(2, out int index))
                {
                    betSystemPresenter.AddBet(2, index);
                }
            }
        }
    }

    private void ActivateEvents()
    {
        ActivateTransitions();
    }

    private void DeactivateEvents()
    {
        DeactivateTransitions();
    }

    private void ActivateTransitions()
    {
        //sceneRoot.OnClickToExit_Main += HandleClickToMenu;
    }

    private void DeactivateTransitions()
    {
        //sceneRoot.OnClickToExit_Main -= HandleClickToMenu;
    }

    private void Deactivate()
    {
        particleEffectMaterialPresenter.Deactivate();

        sceneRoot.Deactivate();
        soundPresenter?.Dispose();
    }

    private void Dispose()
    {
        DeactivateEvents();

        soundPresenter?.Dispose();
        sceneRoot.Dispose();
        particleEffectPresenter?.Dispose();
        particleEffectMaterialPresenter?.Dispose();
        bankPresenter?.Dispose();
        avatarPresenter?.Dispose();

        betSystemPresenter?.Dispose();
        playerPresentationSystemPresenter?.Dispose();

        playerPeople.Dispose();
        playerBot_1.Dispose();
        playerBot_2.Dispose();
        playerBot_3.Dispose();
        playerBot_4.Dispose();

        stateMachine?.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region Output


    public event Action OnClickToMenu;
    public event Action OnClickToGame;

    private void HandleClickToMenu()
    {
        Deactivate();

        OnClickToMenu?.Invoke();
    }

    private void HandleClickToGame()
    {
        Deactivate();

        OnClickToGame?.Invoke();
    }

    #endregion
}
