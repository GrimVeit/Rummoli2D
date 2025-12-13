using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopScrollView : View
{
    [SerializeField] private Button buttonLeft;
    [SerializeField] private Button buttonRight;
    [SerializeField] private ScaleEffect scaleEffect_Left;
    [SerializeField] private ScaleEffect scaleEffect_Right;
    //[SerializeField] private TextMeshProUGUI textPage;

    [SerializeField] private List<MovePanel> panelVisuals = new();
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

    public void ResetPage()
    {
        _currentPage = 0;
        UpdatePage();
    }

    public void CloseAllPages()
    {
        panelVisuals.ForEach(data =>
        {
            if(data.IsActive)
                data.DeactivatePanel();
        });
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
        if ((_currentPage + 1) < panelVisuals.Count)
        {
            _currentPage++;
            UpdatePage();

            OnClickLeftRight?.Invoke();
        }
    }

    private void UpdatePage()
    {
        if (panelVisuals.Count == 0) return;

        //textPage.text = $"{_currentPage + 1}/{panelVisuals.Count}";

        for (int i = 0; i < panelVisuals.Count; i++)
        {
            if (_currentPage == i)
            {
                if (!panelVisuals[i].IsActive)
                    panelVisuals[i].ActivatePanel();
            }
            else
            {
                if (panelVisuals[i].IsActive)
                    panelVisuals[i].DeactivatePanel();
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

        if ((_currentPage + 1) < panelVisuals.Count)
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
