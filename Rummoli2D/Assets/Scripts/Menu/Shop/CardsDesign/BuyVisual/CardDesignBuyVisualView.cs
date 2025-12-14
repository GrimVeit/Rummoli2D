using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDesignBuyVisualView : View
{
    [SerializeField] private List<CardDesignBuyVisual> designVisuals = new();

    [SerializeField] private float timeOpenCloseShadow;
    [SerializeField] private float timeOpenClosePrice;
    [SerializeField] private float timeSelectDeselectCheck;
    [SerializeField] private Sprite spriteChoose;
    [SerializeField] private Sprite spriteUnchoose;

    [Header("Buy")]
    [SerializeField] private Button buttonBuy;
    [SerializeField] private UIEffect effectButtonBuy;

    public void Initialize()
    {
        for (int i = 0; i < designVisuals.Count; i++)
        {
            designVisuals[i].OnChoose += Choose;
        }

        effectButtonBuy.Initialize();

        buttonBuy.onClick.AddListener(Buy);
    }

    public void Dispose()
    {
        for (int i = 0; i < designVisuals.Count; i++)
        {
            designVisuals[i].OnChoose -= Choose;
        }

        effectButtonBuy.Dispose();

        buttonBuy.onClick.RemoveListener(Buy);
    }

    public void ActivateBuy()
    {
        buttonBuy.enabled = true;

        effectButtonBuy.ActivateEffect();
    }

    public void DeactivateBuy()
    {
        buttonBuy.enabled = false;

        effectButtonBuy.DeactivateEffect();
    }

    public void Open(int id)
    {
        var visual = GetDesignVisual(id);

        if (visual == null)
        {
            Debug.LogWarning("Not found CardDesignVisual by id - " + id);
            return;
        }

        visual.Open(timeOpenCloseShadow, timeOpenClosePrice);
    }

    public void Close(int id)
    {
        var visual = GetDesignVisual(id);

        if (visual == null)
        {
            Debug.LogWarning("Not found CardDesignVisual by id - " + id);
            return;
        }

        visual.Close(timeOpenCloseShadow, timeOpenClosePrice);
    }

    public void Select(int id)
    {
        var visual = GetDesignVisual(id);

        if (visual == null)
        {
            Debug.LogWarning("Not found CardDesignVisual by id - " + id);
            return;
        }

        visual.Select(timeSelectDeselectCheck);
    }

    public void Deselect(int id)
    {
        var visual = GetDesignVisual(id);

        if (visual == null)
        {
            Debug.LogWarning("Not found CardDesignVisual by id - " + id);
            return;
        }

        visual.Deselect(timeSelectDeselectCheck);
    }

    public void Choose(int id)
    {
        var visual = GetDesignVisual(id);

        if (visual == null)
        {
            Debug.LogWarning("Not found CardDesignVisual by id - " + id);
            return;
        }

        visual.Choose(spriteChoose);
    }

    public void Unchoose(int id)
    {
        var visual = GetDesignVisual(id);

        if (visual == null)
        {
            Debug.LogWarning("Not found CardDesignVisual by id - " + id);
            return;
        }

        visual.Unchoose(spriteUnchoose);
    }

    private CardDesignBuyVisual GetDesignVisual(int id)
    {
        return designVisuals.Find(v => v.Id == id);
    }

    #region Output

    public event Action<int, int> OnChoose;
    public event Action OnBuy;

    private void Choose(int id, int price)
    {
        OnChoose?.Invoke(id, price);
    }

    private void Buy()
    {
        OnBuy?.Invoke();
    }

    #endregion
}
