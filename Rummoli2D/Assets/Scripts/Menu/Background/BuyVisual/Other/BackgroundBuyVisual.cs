using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BackgroundBuyVisual : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public int Id => id;

    [SerializeField] private int id;
    [SerializeField] private int price;

    [Header("Choose/Unchoose")]
    [SerializeField] private Image imageFrame;

    [Header("Select/Deselect")]
    [SerializeField] private Transform transformCheck;

    [Header("Open/Close")]
    [SerializeField] private Transform transformShadow;
    [SerializeField] private Transform transformPrice;

    public void Open(float timeOpenCloseShadow, float timeOpenClosePrice)
    {
        transformShadow.DOScaleY(0, timeOpenCloseShadow);
        transformPrice.DOScale(0, timeOpenClosePrice);
    }

    public void Close(float timeOpenCloseShadow, float timeOpenClosePrice)
    {
        transformShadow.DOScaleY(1, timeOpenCloseShadow);
        transformPrice.DOScale(1, timeOpenClosePrice);
    }

    public void Select(float timeSelectDeselectCheck)
    {
        transformCheck.DOScale(1, timeSelectDeselectCheck);
    }

    public void Deselect(float timeSelectDeselectCheck)
    {
        transformCheck.DOScale(0, timeSelectDeselectCheck);
    }

    public void Choose(Sprite spriteFrame)
    {
        imageFrame.sprite = spriteFrame;
    }

    public void Unchoose(Sprite spriteFrame)
    {
        imageFrame.sprite = spriteFrame;
    }



    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnChoose?.Invoke(id, price);
    }

    #region Output

    public event Action<int, int> OnChoose;

    #endregion
}
