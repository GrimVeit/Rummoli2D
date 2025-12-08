using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RulesVisualView : View
{
    [SerializeField] private Button buttonLeft;
    [SerializeField] private Button buttonRight;
    [SerializeField] private ScaleEffect scaleEffect_Left;
    [SerializeField] private ScaleEffect scaleEffect_Right;
    [SerializeField] private Material fontAsset;

    [SerializeField] private List<RulesVisual> rulesVisuals = new List<RulesVisual>();
    private int _currentPage = 0;

    public void Initialize()
    {
        buttonLeft.onClick.AddListener(() => Left());
        buttonRight.onClick.AddListener(() => Right());

        scaleEffect_Left.Initialize();
        scaleEffect_Right.Initialize();
    }

    public void Dispose()
    {
        buttonLeft.onClick.RemoveListener(() => Left());
        buttonRight.onClick.RemoveListener(() => Right());

        scaleEffect_Left.Dispose();
        scaleEffect_Right.Dispose();
    }

    private void Left()
    {
        if (_currentPage > 0)
        {
            _currentPage--;
            UpdatePage();

            OnClickLeftRight?.Invoke();
        }
    }

    private void Right()
    {
        if ((_currentPage + 1)  < rulesVisuals.Count)
        {
            _currentPage++;
            UpdatePage();

            OnClickLeftRight?.Invoke();
        }
    }

    private void UpdatePage()
    {
        if (rulesVisuals.Count == 0) return;

        int startIndex = _currentPage;
        int endIndex = Mathf.Min(startIndex, rulesVisuals.Count);

        for (int i = 0; i < rulesVisuals.Count; i++)
        {
            if (i >= startIndex && i < endIndex)
            {
                rulesVisuals[i].Show();
            }
            else
            {
                rulesVisuals[i].Hide();
            }
        }

        if (_currentPage > 0)
        {
            scaleEffect_Left.ActivateEffect();
        }
        else
        {
            scaleEffect_Left.DeactivateEffect();
        }

        if ((_currentPage + 1) < rulesVisuals.Count)
        {
            scaleEffect_Right.ActivateEffect();
        }
        else
        {
            scaleEffect_Right.DeactivateEffect();
        }
    }

    #region Output

    public event Action OnClickLeftRight;

    #endregion
}
