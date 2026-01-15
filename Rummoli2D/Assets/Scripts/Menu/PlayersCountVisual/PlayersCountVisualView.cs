using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayersCountVisualView : View
{
    [SerializeField] private Button buttonIncrease;
    [SerializeField] private Button buttonDecrease;
    [SerializeField] private TextMeshProUGUI textCount;

    public void Initialize()
    {
        buttonIncrease.onClick.AddListener(Increase);
        buttonDecrease.onClick.AddListener(Decrease);
    }

    public void Dispose()
    {
        buttonIncrease.onClick.RemoveListener(Increase);
        buttonDecrease.onClick.RemoveListener(Decrease);
    }

    public void SetCount(int count)
    {
        textCount.text = count.ToString();
    }

    #region Output

    public event Action OnIncrease;
    public event Action OnDecrease;

    private void Increase()
    {
        OnIncrease?.Invoke();
    }

    private void Decrease()
    {
        OnDecrease?.Invoke();
    }

    #endregion
}
