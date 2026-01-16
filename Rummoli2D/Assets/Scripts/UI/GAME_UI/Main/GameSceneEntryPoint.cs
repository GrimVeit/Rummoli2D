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
    [SerializeField] private CardThemesSO cardThemesSO;
    [SerializeField] private CardsSO cardsSO;
    [SerializeField] private UIGameRoot menuRootPrefab;

    private UIGameRoot sceneRoot;
    private ViewContainer viewContainer;

    private BankPresenter bankPresenter;
    private ParticleEffectPresenter particleEffectPresenter;
    private ParticleEffectMaterialPresenter particleEffectMaterialPresenter;
    private SoundPresenter soundPresenter;
    private AvatarPresenter avatarPresenter;

    private StoreCardDesignPresenter storeCardDesignPresenter;

    private StoreBackgroundPresenter storeBackgroundPresenter;
    private BackgroundVisualPresenter backgroundVisualPresenter;

    private PlayerPopupEffectSystemPresenter playerPopupEffectSystemPresenter;
    private StoreCardRummoliPresenter storeCardRummoliPresenter;
    private CardRummoliVisualPresenter cardRummoliVisualPresenter;
    private StoreLanguagePresenter storeLanguagePresenter;
    private StoreGameDifficultyPresenter storeGameDifficultyPresenter;
    private TextTranslatePresenter textTranslatePresenter;
    private CardPokerHandSelectorPresenter cardPokerHandSelectorPresenter;
    private BetSystemPresenter betSystemPresenter;
    private HighlightSystemPresenter highlightSystemPresenter;
    private PlayerPresentationSystemPresenter playerPresentationSystemPresenter;
    private RoundPhasePresentationSystemPresenter roundPhasePresentationSystemPresenter;
    private CardBankPresentationSystemPresenter cardBankPresentationSystemPresenter;
    private CardSpawnerSystemPresenter cardSpawnerSystemPresenter;
    private PlayerPokerPresenter playerPokerPresenter;
    private SectorConditionCheckerPresenter sectorConditionCheckerPresenter;
    private StoreRoundCountPresenter storeRoundCountPresenter;
    private StorePlayersCountPresenter storePlayersCountPresenter;
    private StoreRoundCurrentNumberPresenter storeRoundCurrentNumberPresenter;
    private RoundNumberVisualPresenter roundNumberVisualPresenter;
    private CounterPassPlayerSystemPresenter counterPassPlayerSystemPresenter;
    private RummoliTablePresentationSystemPresenter rummoliTablePresentationSystemPresenter;
    private ScoreEarnLeaderboardPresenter scoreEarnLeaderboardPresenter;
    private GameInfoPresenter gameInfoPresenter;
    private HintSystemPresenter hintSystemPresenter;
    private PlayerSetupPresenter playerSetupPresenter;
    private TextEffectHideShowPresenter textEffectHideShowPresenter;

    private PlayerPeople playerPeople;
    private PlayerBot playerBot_1;
    private PlayerBot playerBot_2;
    private PlayerBot playerBot_3;
    private PlayerBot playerBot_4;

    private StateMachine_GameFlow stateMachine_GameFlow;
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

        storeCardDesignPresenter = new StoreCardDesignPresenter(new StoreCardDesignModel());

        storeBackgroundPresenter = new StoreBackgroundPresenter(new StoreBackgroundModel());
        backgroundVisualPresenter = new BackgroundVisualPresenter(new BackgroundVisualModel(storeBackgroundPresenter, storeBackgroundPresenter), viewContainer.GetView<BackgroundVisualView>());

        playerPopupEffectSystemPresenter = new PlayerPopupEffectSystemPresenter(viewContainer.GetView<PlayerPopupEffectSystemView>());
        storeCardRummoliPresenter = new StoreCardRummoliPresenter(new StoreCardRummoliModel());
        cardRummoliVisualPresenter = new CardRummoliVisualPresenter(new CardRummoliVisualModel(storeCardRummoliPresenter, cardThemesSO, storeCardDesignPresenter), viewContainer.GetView<CardRummoliVisualView>());
        storeLanguagePresenter = new StoreLanguagePresenter(new StoreLanguageModel(PlayerPrefsKeys.LANGUAGE));
        textTranslatePresenter = new TextTranslatePresenter(new TextTranslateModel(storeLanguagePresenter, storeLanguagePresenter), viewContainer.GetView<TextTranslateView>());
        storeGameDifficultyPresenter = new StoreGameDifficultyPresenter(new StoreGameDifficultyModel(PlayerPrefsKeys.GAME_DIFFICULTY));
        cardPokerHandSelectorPresenter = new CardPokerHandSelectorPresenter(new CardPokerHandSelectorModel(storeGameDifficultyPresenter));
        betSystemPresenter = new BetSystemPresenter(new BetSystemModel(5), viewContainer.GetView<BetSystemView>());
        highlightSystemPresenter = new HighlightSystemPresenter(viewContainer.GetView<HighlightSystemView>());
        playerPresentationSystemPresenter = new PlayerPresentationSystemPresenter(new PlayerPresentationSystemModel(), viewContainer.GetView<PlayerPresentationSystemView>());
        roundPhasePresentationSystemPresenter = new RoundPhasePresentationSystemPresenter(new RoundPhasePresentationSystemModel(), viewContainer.GetView<RoundPhasePresentationSystemView>());
        cardBankPresentationSystemPresenter = new CardBankPresentationSystemPresenter(new CardBankPresentationSystemModel(storeCardDesignPresenter), viewContainer.GetView<CardBankPresentationSystemView>());
        cardSpawnerSystemPresenter = new CardSpawnerSystemPresenter(new CardSpawnerSystemModel(cardThemesSO, cardsSO, storeCardDesignPresenter), viewContainer.GetView<CardSpawnerSystemView>());
        playerPokerPresenter = new PlayerPokerPresenter(new PlayerPokerModel(cardPokerHandSelectorPresenter, storeLanguagePresenter), viewContainer.GetView<PlayerPokerView>());
        sectorConditionCheckerPresenter = new SectorConditionCheckerPresenter(new SectorConditionCheckerModel());
        storeRoundCurrentNumberPresenter = new StoreRoundCurrentNumberPresenter(new StoreRoundCurrentNumberModel());
        storeRoundCountPresenter = new StoreRoundCountPresenter(new StoreRoundCountModel(PlayerPrefsKeys.ROUND_COUNT));
        storePlayersCountPresenter = new StorePlayersCountPresenter(new StorePlayersCountModel(PlayerPrefsKeys.PLAYERS_COUNT));
        roundNumberVisualPresenter = new RoundNumberVisualPresenter(new RoundNumberVisualModel(storeRoundCurrentNumberPresenter, storeRoundCurrentNumberPresenter, storeLanguagePresenter, storeLanguagePresenter), viewContainer.GetView<RoundNumberVisualView>());
        counterPassPlayerSystemPresenter = new CounterPassPlayerSystemPresenter(new CounterPassPlayerSystemModel(storeLanguagePresenter), viewContainer.GetView<CounterPassPlayerSystemView>());
        rummoliTablePresentationSystemPresenter = new RummoliTablePresentationSystemPresenter(viewContainer.GetView<RummoliTablePresentationSystemView>());
        scoreEarnLeaderboardPresenter = new ScoreEarnLeaderboardPresenter(new ScoreEarnLeaderboardModel(storePlayersCountPresenter, storeRoundCountPresenter, storeGameDifficultyPresenter), viewContainer.GetView<ScoreEarnLeaderboardView>());
        gameInfoPresenter = new GameInfoPresenter(new GameInfoModel(storeGameDifficultyPresenter, storeLanguagePresenter, storeRoundCountPresenter, storePlayersCountPresenter), viewContainer.GetView<GameInfoView>());
        hintSystemPresenter = new HintSystemPresenter(new HintSystemModel(storeLanguagePresenter), viewContainer.GetView<HintSystemView>());
        textEffectHideShowPresenter = new TextEffectHideShowPresenter(viewContainer.GetView<TextEffectHideShowView>());

        playerPeople = new PlayerPeople(0, highlightSystemPresenter, hintSystemPresenter, soundPresenter, cardPokerHandSelectorPresenter, betSystemPresenter, bankPresenter, sceneRoot, viewContainer);
        playerBot_1 = new PlayerBot(1, "Bot_1", highlightSystemPresenter, cardPokerHandSelectorPresenter, betSystemPresenter, storeGameDifficultyPresenter, viewContainer);
        playerBot_2 = new PlayerBot(2, "Bot_2", highlightSystemPresenter, cardPokerHandSelectorPresenter, betSystemPresenter, storeGameDifficultyPresenter, viewContainer);
        playerBot_3 = new PlayerBot(3, "Bot_3", highlightSystemPresenter, cardPokerHandSelectorPresenter, betSystemPresenter, storeGameDifficultyPresenter, viewContainer);
        playerBot_4 = new PlayerBot(4, "Bot_4", highlightSystemPresenter, cardPokerHandSelectorPresenter, betSystemPresenter, storeGameDifficultyPresenter, viewContainer);

        playerSetupPresenter = new PlayerSetupPresenter(new PlayerSetupModel(storePlayersCountPresenter, playerPeople, new List<PlayerBot>() { playerBot_1, playerBot_2, playerBot_3, playerBot_4 }), viewContainer.GetView<PlayerSetupView>());

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

        storeCardDesignPresenter.Initialize();

        storeBackgroundPresenter.Initialize();
        backgroundVisualPresenter.Initialize();

        storeLanguagePresenter.Initialize();
        textTranslatePresenter.Initialize();
        storeGameDifficultyPresenter.Initialize();
        betSystemPresenter.Initialize();
        cardSpawnerSystemPresenter.Initialize();
        playerPokerPresenter.Initialize();
        cardBankPresentationSystemPresenter.Initialize();
        cardRummoliVisualPresenter.Initialize();
        storeCardRummoliPresenter.Initialize();
        storeRoundCurrentNumberPresenter.Initialize();
        storeRoundCountPresenter.Initialize();
        storePlayersCountPresenter.Initialize();
        roundNumberVisualPresenter.Initialize();
        counterPassPlayerSystemPresenter.Initialize();
        scoreEarnLeaderboardPresenter.Initialize();
        gameInfoPresenter.Initialize();
        hintSystemPresenter.Initialize();

        playerPeople.Initialize();
        playerBot_1.Initialize();
        playerBot_2.Initialize();
        playerBot_3.Initialize();
        playerBot_4.Initialize();

        playerSetupPresenter.Setup();
        playerSetupPresenter.SetStartPositions(playerSetupPresenter.GetPlayers().Count);
        stateMachine_GameFlow = new(sceneRoot, textEffectHideShowPresenter, hintSystemPresenter, scoreEarnLeaderboardPresenter);
        stateMachine = new StateMachine_Game
            (playerSetupPresenter.GetPlayers(),
            sceneRoot,
            playerPresentationSystemPresenter,
            highlightSystemPresenter,
            roundPhasePresentationSystemPresenter,
            cardBankPresentationSystemPresenter,
            cardSpawnerSystemPresenter,
            cardSpawnerSystemPresenter,
            playerPokerPresenter,
            playerPokerPresenter,
            betSystemPresenter,
            betSystemPresenter,
            storeCardRummoliPresenter,
            cardRummoliVisualPresenter,
            playerPopupEffectSystemPresenter,
            sectorConditionCheckerPresenter,
            storeRoundCurrentNumberPresenter,
            storeRoundCurrentNumberPresenter,
            counterPassPlayerSystemPresenter,
            counterPassPlayerSystemPresenter,
            rummoliTablePresentationSystemPresenter,
            storeRoundCountPresenter,
            scoreEarnLeaderboardPresenter,
            scoreEarnLeaderboardPresenter);

        stateMachine.Initialize();
        stateMachine_GameFlow.Initialize();
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
        sceneRoot.OnClickToExit_Left += HandleClickToMenu;
        sceneRoot.OnClickToExit_FinishButtons += HandleClickToMenu;

        sceneRoot.OnClickToNewGame_FinishButtons += HandleClickToGame;
    }

    private void DeactivateTransitions()
    {
        sceneRoot.OnClickToExit_Left -= HandleClickToMenu;
        sceneRoot.OnClickToExit_FinishButtons -= HandleClickToMenu;

        sceneRoot.OnClickToNewGame_FinishButtons -= HandleClickToGame;
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

        storeCardDesignPresenter?.Dispose();

        storeBackgroundPresenter?.Dispose();
        backgroundVisualPresenter?.Dispose();

        storeCardRummoliPresenter?.Dispose();
        storeLanguagePresenter?.Dispose();
        textTranslatePresenter?.Dispose();
        storeGameDifficultyPresenter.Dispose();
        betSystemPresenter?.Dispose();
        cardSpawnerSystemPresenter?.Dispose();
        playerPokerPresenter?.Dispose();
        cardBankPresentationSystemPresenter?.Dispose();
        cardRummoliVisualPresenter?.Dispose();
        storeRoundCurrentNumberPresenter?.Dispose();
        storeRoundCountPresenter?.Dispose();
        storePlayersCountPresenter?.Dispose();
        roundNumberVisualPresenter?.Dispose();
        counterPassPlayerSystemPresenter?.Dispose();
        scoreEarnLeaderboardPresenter?.Dispose();
        gameInfoPresenter?.Dispose();
        hintSystemPresenter?.Dispose();

        playerPeople.Dispose();
        playerBot_1.Dispose();
        playerBot_2.Dispose();
        playerBot_3.Dispose();
        playerBot_4.Dispose();

        stateMachine?.Dispose();
        stateMachine_GameFlow?.Dispose();
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
