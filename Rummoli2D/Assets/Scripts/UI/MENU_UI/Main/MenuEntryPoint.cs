using System;
using System.Collections.Generic;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using UnityEngine;

public class MenuEntryPoint : MonoBehaviour
{
    [SerializeField] private Sounds sounds;
    [SerializeField] private ChipGroup chipGroup;
    [SerializeField] private UIMainMenuRoot menuRootPrefab;

    private UIMainMenuRoot sceneRoot;
    private ViewContainer viewContainer;

    private BankPresenter bankPresenter;
    private ParticleEffectPresenter particleEffectPresenter;
    private ParticleEffectMaterialPresenter particleEffectMaterialPresenter;
    private SoundPresenter soundPresenter;

    private NicknamePresenter nicknamePresenter;
    private AvatarPresenter avatarPresenter;
    private FirebaseAuthenticationPresenter firebaseAuthenticationPresenter;
    private FirebaseDatabasePresenter firebaseDatabasePresenter;
    private AvatarVisualPresenter avatarVisualPresenter_Main;
    private AvatarVisualPresenter avatarVisualPresenter_Update;

    private RulesVisualPresenter rulesVisualPresenter;

    private CustomSliderPresenter customSliderPresenter_Sound;
    private CustomSliderPresenter customSliderPresenter_Music;
    private VolumeSettingsPresenter volumeSettingsPresenter;

    private StoreLanguagePresenter storeTextTranslatePresenter;
    private LanguageChangePresenter textTranslateChangePresenter;
    private TextTranslatePresenter textTranslatePresenter;

    //------Shop------//
    private ShopScrollPresenter shopScrollPresenter;

    private StoreBackgroundPresenter storeBackgroundPresenter;
    private BackgroundBuyVisualPresenter backgroundBuyVisualPresenter;
    private BackgroundVisualPresenter backgroundVisualPresenter;

    private StoreCardDesignPresenter storeCardDesignPresenter;
    private CardDesignBuyVisualPresenter cardDesignBuyVisualPresenter;

    //----------------//
    private StateMachine_Menu stateMachine;

    public void Run(UIRootView uIRootView)
    {
        sceneRoot = menuRootPrefab;

        uIRootView.AttachSceneUI(sceneRoot.gameObject, Camera.main);

        viewContainer = sceneRoot.GetComponent<ViewContainer>();
        viewContainer.Initialize();

        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;

            if (dependencyStatus == DependencyStatus.Available)
            {
                FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false);
                FirebaseAuth firebaseAuth = FirebaseAuth.DefaultInstance;
                DatabaseReference databaseReference = FirebaseDatabase.DefaultInstance.RootReference;

                soundPresenter = new SoundPresenter
                    (new SoundModel(sounds.sounds, PlayerPrefsKeys.IS_MUTE_SOUNDS, PlayerPrefsKeys.KEY_VOLUME_SOUND, PlayerPrefsKeys.KEY_VOLUME_MUSIC),
                    viewContainer.GetView<SoundView>());

                particleEffectPresenter = new ParticleEffectPresenter
                    (new ParticleEffectModel(),
                    viewContainer.GetView<ParticleEffectView>());

                particleEffectMaterialPresenter = new ParticleEffectMaterialPresenter(new ParticleEffectMaterialModel(), viewContainer.GetView<ParticleEffectMaterialView>());

                bankPresenter = new BankPresenter(new BankModel(), viewContainer.GetView<BankView>());

                nicknamePresenter = new NicknamePresenter(new NicknameModel(PlayerPrefsKeys.NICKNAME, soundPresenter), viewContainer.GetView<NicknameView>());
                avatarPresenter = new AvatarPresenter(new AvatarModel(PlayerPrefsKeys.AVATAR), viewContainer.GetView<AvatarView>());
                firebaseAuthenticationPresenter = new FirebaseAuthenticationPresenter(new FirebaseAuthenticationModel(firebaseAuth, soundPresenter), viewContainer.GetView<FirebaseAuthenticationView>());
                firebaseDatabasePresenter = new FirebaseDatabasePresenter(new FirebaseDatabaseModel(firebaseAuth, databaseReference, bankPresenter));
                avatarVisualPresenter_Main = new AvatarVisualPresenter(new AvatarVisualModel(avatarPresenter, avatarPresenter, avatarPresenter, soundPresenter), viewContainer.GetView<AvatarVisualView>("Registration"));
                avatarVisualPresenter_Update = new AvatarVisualPresenter(new AvatarVisualModel(avatarPresenter, avatarPresenter, avatarPresenter, soundPresenter), viewContainer.GetView<AvatarVisualView>("Update"));

                rulesVisualPresenter = new RulesVisualPresenter(new RulesVisualModel(), viewContainer.GetView<RulesVisualView>());

                customSliderPresenter_Music = new CustomSliderPresenter(new CustomSliderModel(soundPresenter), viewContainer.GetView<CustomSliderView>("Music"));
                customSliderPresenter_Sound = new CustomSliderPresenter(new CustomSliderModel(soundPresenter), viewContainer.GetView<CustomSliderView>("Sound"));
                volumeSettingsPresenter = new VolumeSettingsPresenter(new VolumeSettingsModel(soundPresenter, customSliderPresenter_Sound, customSliderPresenter_Music));

                storeTextTranslatePresenter = new StoreLanguagePresenter(new StoreLanguageModel(PlayerPrefsKeys.LANGUAGE));
                textTranslateChangePresenter = new LanguageChangePresenter(new LanguageChangeModel(storeTextTranslatePresenter, storeTextTranslatePresenter), viewContainer.GetView<LanguageChangeView>());
                textTranslatePresenter = new TextTranslatePresenter(new TextTranslateModel(storeTextTranslatePresenter, storeTextTranslatePresenter), viewContainer.GetView<TextTranslateView>());


                shopScrollPresenter = new ShopScrollPresenter(new ShopScrollModel(), viewContainer.GetView<ShopScrollView>());

                storeBackgroundPresenter = new StoreBackgroundPresenter(new StoreBackgroundModel());
                backgroundBuyVisualPresenter = new BackgroundBuyVisualPresenter(new BackgroundBuyVisualModel(storeBackgroundPresenter, storeBackgroundPresenter, storeBackgroundPresenter, bankPresenter), viewContainer.GetView<BackgroundBuyVisualView>());
                backgroundVisualPresenter = new BackgroundVisualPresenter(new BackgroundVisualModel(storeBackgroundPresenter, storeBackgroundPresenter), viewContainer.GetView<BackgroundVisualView>());

                storeCardDesignPresenter = new StoreCardDesignPresenter(new StoreCardDesignModel());
                cardDesignBuyVisualPresenter = new CardDesignBuyVisualPresenter(new CardDesignBuyVisualModel(storeCardDesignPresenter, storeCardDesignPresenter, storeCardDesignPresenter, bankPresenter), viewContainer.GetView<CardDesignBuyVisualView>());

                stateMachine = new StateMachine_Menu
                (sceneRoot,
                nicknamePresenter,
                avatarPresenter,
                firebaseAuthenticationPresenter,
                firebaseDatabasePresenter,
                rulesVisualPresenter,
                shopScrollPresenter);

                sceneRoot.SetSoundProvider(soundPresenter);
                sceneRoot.Activate();

                ActivateEvents();

                Debug.Log("1");

                soundPresenter.Initialize();
                particleEffectPresenter.Initialize();
                particleEffectMaterialPresenter.Initialize();
                particleEffectMaterialPresenter.Activate();

                Debug.Log("1");

                sceneRoot.Initialize();
                bankPresenter.Initialize();
                nicknamePresenter.Initialize();
                avatarPresenter.Initialize();
                firebaseAuthenticationPresenter.Initialize();
                firebaseDatabasePresenter.Initialize();
                avatarVisualPresenter_Main.Initialize();
                avatarVisualPresenter_Update.Initialize();

                Debug.Log("1");

                rulesVisualPresenter.Initialize();

                customSliderPresenter_Music.Initialize();
                customSliderPresenter_Sound.Initialize();
                volumeSettingsPresenter.Initialize();

                Debug.Log("1");

                storeTextTranslatePresenter.Initialize();
                textTranslateChangePresenter.Initialize();
                textTranslatePresenter.Initialize();

                shopScrollPresenter.Initialize();

                Debug.Log("1");

                backgroundBuyVisualPresenter.Initialize();
                storeBackgroundPresenter.Initialize();
                backgroundVisualPresenter.Initialize();

                cardDesignBuyVisualPresenter.Initialize();
                storeCardDesignPresenter.Initialize();

                Debug.Log("1");

                stateMachine.Initialize();
            }
            else
            {
                Debug.LogError(string.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });
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
        sceneRoot.OnClickToPlay_Main += HandleClickToGame;
        sceneRoot.OnClickToPlay_Balance += HandleClickToGame;
    }

    private void DeactivateTransitions()
    {
        sceneRoot.OnClickToPlay_Main -= HandleClickToGame;
        sceneRoot.OnClickToPlay_Balance -= HandleClickToGame;
    }

    private void Deactivate()
    {
        particleEffectMaterialPresenter.Deactivate();

        sceneRoot.Deactivate();
        soundPresenter?.Dispose();
    }

    private void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }

    private void Dispose()
    {
        DeactivateEvents();

        soundPresenter?.Dispose();
        sceneRoot?.Dispose();
        particleEffectPresenter?.Dispose();
        particleEffectMaterialPresenter?.Dispose();
        bankPresenter?.Dispose();

        nicknamePresenter?.Dispose();
        firebaseAuthenticationPresenter?.Dispose();
        firebaseDatabasePresenter?.Dispose();

        avatarVisualPresenter_Main?.Dispose();
        avatarVisualPresenter_Update?.Dispose();
        avatarPresenter?.Dispose();

        rulesVisualPresenter?.Dispose();


        customSliderPresenter_Music?.Dispose();
        customSliderPresenter_Sound?.Dispose();
        volumeSettingsPresenter?.Dispose();

        storeTextTranslatePresenter?.Dispose();
        textTranslateChangePresenter?.Dispose();
        textTranslatePresenter?.Dispose();

        shopScrollPresenter?.Dispose();

        backgroundBuyVisualPresenter?.Dispose();
        storeBackgroundPresenter?.Dispose();
        backgroundVisualPresenter?.Dispose();

        cardDesignBuyVisualPresenter.Dispose();
        storeCardDesignPresenter.Dispose();

        stateMachine?.Dispose();
    }

    private void OnDestroy()
    {
        Dispose();
    }

    #region Output

    public event Action OnClickToGame;

    private void HandleClickToGame()
    {
        Deactivate();

        OnClickToGame?.Invoke();
    }

    #endregion
}
