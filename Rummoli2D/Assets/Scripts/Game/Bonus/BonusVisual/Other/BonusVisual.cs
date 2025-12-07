using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BonusVisual : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public BonusType BonusType => bonusType;

    [SerializeField] private BonusType bonusType;
    [SerializeField] private TextMeshProUGUI textCount;

    private bool isActive = false;

    public void SetBonusCount(int count)
    {
        textCount.text = count.ToString();
    }

    public void ActivateInteraction()
    {
        isActive = true;
    }

    public void DeactivateInteraction()
    {
        isActive = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(!isActive) return;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(!isActive) return;

        OnChooseBonus?.Invoke(bonusType);
    }

    #region Output

    public event Action<BonusType> OnChooseBonus;

    #endregion
}
