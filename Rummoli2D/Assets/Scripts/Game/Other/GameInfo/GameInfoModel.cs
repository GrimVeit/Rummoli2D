using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfoModel
{
    private readonly IStoreGameDifficultyInfoProvider _gameDifficultyInfoProvider;
    private readonly IStoreLanguageInfoProvider _languageInfoProvider;
    private readonly IStoreRoundCountInfoProvider _roundCountInfoProvider;
    private readonly IStorePlayersCountInfoProvider _playersCountInfoProvider;

    public GameInfoModel(IStoreGameDifficultyInfoProvider gameDifficultyInfoProvider, IStoreLanguageInfoProvider languageInfoProvider, IStoreRoundCountInfoProvider roundCountInfoProvider, IStorePlayersCountInfoProvider playersCountInfoProvider)
    {
        _gameDifficultyInfoProvider = gameDifficultyInfoProvider;
        _languageInfoProvider = languageInfoProvider;
        _roundCountInfoProvider = roundCountInfoProvider;
        _playersCountInfoProvider = playersCountInfoProvider;
    }

    public void Initialize()
    {
        OnSetDifficulty?.Invoke(NameLanguageUtility.GetGameInfo_Difficulty(_gameDifficultyInfoProvider.GameDifficulty, _languageInfoProvider.CurrentLanguage));
        OnSetRoundCount?.Invoke(NameLanguageUtility.GetGameInfo_RoundCount(_roundCountInfoProvider.RoundsCount, _languageInfoProvider.CurrentLanguage));
        OnSetPlayersCount?.Invoke(NameLanguageUtility.GetGameInfo_PlayersCount(_playersCountInfoProvider.PlayersCount, _languageInfoProvider.CurrentLanguage));
        OnSetDescription?.Invoke(NameLanguageUtility.GetGameInfo_Description(_gameDifficultyInfoProvider.GameDifficulty, _languageInfoProvider.CurrentLanguage));
    }

    public void Dispose()
    {

    }

    #region Output

    public event Action<string> OnSetDifficulty;
    public event Action<string> OnSetRoundCount;
    public event Action<string> OnSetPlayersCount;

    public event Action<string> OnSetDescription;

    #endregion
}
