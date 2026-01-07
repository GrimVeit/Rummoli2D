using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfoModel
{
    private readonly IStoreGameDifficultyInfoProvider _gameDifficultyInfoProvider;
    private readonly IStoreLanguageInfoProvider _languageInfoProvider;

    public GameInfoModel(IStoreGameDifficultyInfoProvider gameDifficultyInfoProvider, IStoreLanguageInfoProvider languageInfoProvider)
    {
        _gameDifficultyInfoProvider = gameDifficultyInfoProvider;
        _languageInfoProvider = languageInfoProvider;
    }

    public void Initialize()
    {
        OnSetDifficulty?.Invoke(NameLanguageUtility.GetGameInfo_Difficulty(_gameDifficultyInfoProvider.GameDifficulty, _languageInfoProvider.CurrentLanguage));
        OnSetDescription?.Invoke(NameLanguageUtility.GetGameInfo_Description(_gameDifficultyInfoProvider.GameDifficulty, _languageInfoProvider.CurrentLanguage));
    }

    public void Dispose()
    {

    }

    #region Output

    public event Action<string> OnSetDifficulty;
    public event Action<string> OnSetDescription;

    #endregion
}
