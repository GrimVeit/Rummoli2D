using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerPeopleCardVisual : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public ICard Card => _card;

    [SerializeField] private Image imageFace;
    private ICard _card;

    private Tween tweenSelect;

    public bool IsVisible;

    public void SetData(ICard card)
    {
        _card = card;

        imageFace.sprite = _card.SpriteFace;
    }

    public void Select()
    {
        tweenSelect?.Kill();

        tweenSelect = imageFace.transform.DOLocalMoveY(50, 0.2f).SetEase(Ease.OutBack);
    }

    public void Deselect()
    {
        tweenSelect?.Kill();

        tweenSelect = imageFace.transform.DOLocalMoveY(0, 0.2f).SetEase(Ease.OutBack);
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        OnChooseCard?.Invoke(_card);
    }

    #region Output

    public event Action<ICard> OnChooseCard;

    #endregion
}
