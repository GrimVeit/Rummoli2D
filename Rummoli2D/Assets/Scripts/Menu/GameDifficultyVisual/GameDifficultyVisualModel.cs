using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDifficultyVisualModel
{
    private readonly IStoreGameDifficultyInfoProvider _gameDifficultyInfoProvider;
    private readonly IStoreGameDifficultyListener _gameDifficultyListener;
    private readonly IStoreGameDifficultyProvider _gameDifficultyProvider;
    private readonly ITextEffectHideShowActivator _textEffectHideShowActivator;
    private readonly IStoreLanguageInfoProvider _languageInfoProvider;
    private readonly IStoreLanguageListener _languageListener;

    private GameDifficulty _currentGameDifficulty;

    public GameDifficultyVisualModel(IStoreGameDifficultyInfoProvider gameDifficultyInfoProvider, IStoreGameDifficultyListener gameDifficultyListener, IStoreGameDifficultyProvider gameDifficultyProvider, IStoreLanguageInfoProvider storeLanguageInfoProvider, ITextEffectHideShowActivator textEffectHideShowActivator, IStoreLanguageListener languageListener)
    {
        _gameDifficultyInfoProvider = gameDifficultyInfoProvider;
        _gameDifficultyListener = gameDifficultyListener;
        _gameDifficultyProvider = gameDifficultyProvider;
        _languageInfoProvider = storeLanguageInfoProvider;
        _textEffectHideShowActivator = textEffectHideShowActivator;
        _languageListener = languageListener;
    }

    public void Initialize()
    {
        SetNew(_gameDifficultyInfoProvider.GameDifficulty);

        _gameDifficultyListener.OnGameDifficultyChanged += ChangeGameDifficulty;
        _languageListener.OnChangeLanguage += ChangeLanguage;
    }

    public void Dispose()
    {
        _gameDifficultyListener.OnGameDifficultyChanged -= ChangeGameDifficulty;
        _languageListener.OnChangeLanguage -= ChangeLanguage;
    }

    public void SetGameDifficulty(GameDifficulty gameDifficulty)
    {
        _gameDifficultyProvider.SetDifficulty(gameDifficulty);
    }

    private void ChangeLanguage(Language language)
    {
        OnChangeDescription?.Invoke(NameLanguageUtility.GetGameInfo_Description(_currentGameDifficulty, language));
    }

    #region Output

    public event Action<GameDifficulty> OnSelectGameDifficulty;
    public event Action<GameDifficulty> OnDeselectGameDifficulty;
    public event Action<string> OnChangeDescription;

    private void ChangeGameDifficulty(GameDifficulty gameDifficulty)
    {
        if(_currentGameDifficulty == gameDifficulty) return;

        OnDeselectGameDifficulty?.Invoke(_currentGameDifficulty);

        SetNew(gameDifficulty);
    }

    private void SetNew(GameDifficulty gameDifficulty)
    {
        _currentGameDifficulty = gameDifficulty;
        OnSelectGameDifficulty?.Invoke(_currentGameDifficulty);
        _textEffectHideShowActivator.ActivateVisual(0.2f);
        OnChangeDescription?.Invoke(NameLanguageUtility.GetGameInfo_Description(_currentGameDifficulty, _languageInfoProvider.CurrentLanguage));
    }

    #endregion
}
