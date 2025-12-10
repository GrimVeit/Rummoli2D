using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
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

    public void ResetPage()
    {
        _currentPage = 0;
        UpdatePage();
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
        int endIndex = _currentPage;

        for (int i = 0; i < rulesVisuals.Count; i++)
        {
            if(_currentPage == i)
            {
                fontAsset.SetFloat("_FaceDilate", -0.4f);

                float currentDilate = fontAsset.GetFloat("_FaceDilate");

                DOTween.To(
                    () => currentDilate,
                    x => { currentDilate = x; fontAsset.SetFloat("_FaceDilate", x); },
                    0f,
                    0.5f);

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
