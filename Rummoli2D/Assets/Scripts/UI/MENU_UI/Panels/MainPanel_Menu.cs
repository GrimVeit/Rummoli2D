using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel_Menu : MovePanel
{
    [SerializeField] private Button buttonLeaderboard;
    [SerializeField] private Button buttonPlay;
    [SerializeField] private Button buttonShop;
    [SerializeField] private Button buttonProfile;

    [SerializeField] private List<UIEffectCombination> uIEffectCombinations = new List<UIEffectCombination>();

    public override void Initialize()
    {
        base.Initialize();

        buttonLeaderboard.onClick.AddListener(() => OnClickToLeaderboard?.Invoke());
        buttonPlay.onClick.AddListener(() => OnClickToPlay?.Invoke());
        buttonShop.onClick.AddListener(() => OnClickToShop?.Invoke());
        buttonProfile.onClick.AddListener(() => OnClickToProfile?.Invoke());

        uIEffectCombinations.ForEach(data => data.Initialize());
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonLeaderboard.onClick.RemoveListener(() => OnClickToLeaderboard?.Invoke());
        buttonPlay.onClick.RemoveListener(() => OnClickToPlay?.Invoke());
        buttonShop.onClick.RemoveListener(() => OnClickToShop?.Invoke());
        buttonProfile.onClick.RemoveListener(() => OnClickToProfile?.Invoke());

        uIEffectCombinations.ForEach(data => data.Dispose());
    }

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        uIEffectCombinations.ForEach(data => data.ActivateEffect());
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();

        uIEffectCombinations.ForEach(data => data.DeactivateEffect());
    }

    #region Output

    public event Action OnClickToLeaderboard;
    public event Action OnClickToPlay;
    public event Action OnClickToShop;
    public event Action OnClickToProfile;

    #endregion
}
