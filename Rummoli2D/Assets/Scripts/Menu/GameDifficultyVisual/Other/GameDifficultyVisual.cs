using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class GameDifficultyVisual : MonoBehaviour
{
    [SerializeField] private Button buttonVisual;

    [Header("Visual")]
    [SerializeField] private Image imageVisual;
    [SerializeField] private Color colorSelect;
    [SerializeField] private Color colorDeselect;
    [SerializeField] private float timeChangeColor;
    private Tween tweenColor;

    public void Initialize()
    {
        buttonVisual.onClick.AddListener(ChooseGameDifficulty);
    }

    public void Dispose()
    {
        buttonVisual.onClick.RemoveListener(ChooseGameDifficulty);
    }

    public void Select()
    {
        tweenColor?.Kill();

        tweenColor = imageVisual.DOColor(colorSelect, timeChangeColor);
    }

    public void Deselect()
    {
        tweenColor?.Kill();

        tweenColor = imageVisual.DOColor(colorDeselect, timeChangeColor);
    }

    #region Output

    public event Action OnChooseGameDifficulty;

    private void ChooseGameDifficulty()
    {
        OnChooseGameDifficulty?.Invoke();
    }

    #endregion
}
