using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameDifficultyVisualView : View
{
    [SerializeField] private List<GameDifficultyModul> gameDifficultyModuls = new List<GameDifficultyModul>();
    [SerializeField] private TextMeshProUGUI textDescription;

    public void Initialize()
    {
        foreach (var modul in gameDifficultyModuls)
        {
            modul.OnChooseGameDifficulty += ChooseGameDifficulty;
            modul.Initialize();
        }
    }

    public void Dispose()
    {
        foreach (var modul in gameDifficultyModuls)
        {
            modul.OnChooseGameDifficulty -= ChooseGameDifficulty;
            modul.Dispose();
        }
    }

    public void SetText(string text)
    {
        textDescription.text = text;
    }

    public void SelectModul(GameDifficulty difficulty)
    {
        var modul = GetGameDifficultyModul(difficulty);

        if(modul == null)
        {
            Debug.LogWarning("Not found FameDifficultyModulVisual with GameDifficulty - " + difficulty);
            return;
        }

        modul.Select();
    }

    public void DeselectModul(GameDifficulty difficulty)
    {
        var modul = GetGameDifficultyModul(difficulty);

        if (modul == null)
        {
            Debug.LogWarning("Not found FameDifficultyModulVisual with GameDifficulty - " + difficulty);
            return;
        }

        modul.Deselect();
    }

    private GameDifficultyModul GetGameDifficultyModul(GameDifficulty difficulty)
    {
        return gameDifficultyModuls.Find(modul => modul.GameDifficulty == difficulty);
    }

    #region Output

    public event Action<GameDifficulty> OnChooseGameDifficulty;

    private void ChooseGameDifficulty(GameDifficulty difficulty)
    {
        OnChooseGameDifficulty?.Invoke(difficulty);
    }

    #endregion
}

[System.Serializable]
public class GameDifficultyModul
{
    public GameDifficulty GameDifficulty => gameDifficulty;

    [SerializeField] private GameDifficulty gameDifficulty;
    [SerializeField] private GameDifficultyVisual gameDifficultyVisual;
    [SerializeField] private GameDifficultyLine gameDifficultyLine;

    public void Initialize()
    {
        gameDifficultyVisual.OnChooseGameDifficulty += ChooseGameDifficulty;
        gameDifficultyVisual.Initialize();
    }

    public void Dispose()
    {
        gameDifficultyVisual.OnChooseGameDifficulty -= ChooseGameDifficulty;
        gameDifficultyVisual.Dispose();
    }

    public void Select()
    {
        gameDifficultyVisual.Select();
        gameDifficultyLine.Activate();
    }

    public void Deselect()
    {
        gameDifficultyVisual.Deselect();
        gameDifficultyLine.Deactivate();
    }

    #region Output

    public event Action<GameDifficulty> OnChooseGameDifficulty;

    private void ChooseGameDifficulty()
    {
        OnChooseGameDifficulty?.Invoke(gameDifficulty);
    }

    #endregion
}
