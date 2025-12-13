using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChipBuyView : View
{
    [SerializeField] private List<ChipBuy> chipBuys = new List<ChipBuy>();

    public void Initialize()
    {
        chipBuys.ForEach(cb =>
        {
            cb.OnIncrease += HandleIncrease;
            cb.OnDecrease += HandleDecrease;
            cb.Initialize();
        });
    }

    public void Dispose()
    {
        chipBuys.ForEach(cb =>
        {
            cb.OnIncrease -= HandleIncrease;
            cb.OnDecrease -= HandleDecrease;
            cb.Dispose();
        });
    }

    #region Output

    public event Action<int> OnIncrease;
    public event Action<int> OnDecrease;

    private void HandleIncrease(int id)
    {
        OnIncrease?.Invoke(id);
    }

    private void HandleDecrease(int id)
    {
        OnDecrease?.Invoke(id);
    }

    #endregion
}

[System.Serializable]
public class ChipBuy
{
    public int ID => id;

    [SerializeField] private int id;
    [SerializeField] private Button buttonIncrease;
    [SerializeField] private Button buttonDecrease;

    public void Initialize()
    {
        buttonIncrease.onClick.AddListener(() => OnIncrease?.Invoke(id));
        buttonDecrease.onClick.AddListener(() => OnDecrease?.Invoke(id));
    }

    public void Dispose()
    {
        buttonIncrease.onClick.RemoveListener(() => OnDecrease?.Invoke(id));
        buttonDecrease.onClick.RemoveListener(() => OnDecrease?.Invoke(id));
    }

    public event Action<int> OnIncrease;
    public event Action<int> OnDecrease;
}
