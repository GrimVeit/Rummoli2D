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

    private bool isSelect = false;

    public bool IsVisible;

    public void SetData(ICard card)
    {
        _card = card;

        imageFace.sprite = _card.SpriteFace;
    }

    public void Select()
    {
        tweenSelect?.Kill();

        isSelect = true;

        tweenSelect = imageFace.transform.DOLocalMoveY(50, 0.2f).SetEase(Ease.OutBack);
    }

    public void Deselect()
    {
        tweenSelect?.Kill();

        isSelect = false;

        tweenSelect = imageFace.transform.DOLocalMoveY(0, 0.2f).SetEase(Ease.OutBack);
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isSelect)
            Deselect();
        else
            Select();
    }
}
