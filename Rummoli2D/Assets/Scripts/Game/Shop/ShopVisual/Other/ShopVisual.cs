using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ShopVisual : MonoBehaviour
{
    [SerializeField] private int idLevel;
    [SerializeField] private int price;
    [SerializeField] private Button buttonBuy;

    [SerializeField] private List<Transform> elementsDescription = new();
    [SerializeField] private GameObject objectReceived;
    [SerializeField] private GameObject objectPrice;

    private Sequence _seq;

    public void Initialize()
    {
        buttonBuy.onClick.AddListener(Buy);
    }

    public void Dispose()
    {
        buttonBuy.onClick.RemoveListener(Buy);
    }

    private void Clear()
    {
        objectReceived.transform.localScale = Vector3.zero;
        objectPrice.transform.localScale = Vector3.zero;
        elementsDescription.ForEach(data => data.transform.localScale = Vector3.zero);
    }

    public void SetReceived()
    {
        buttonBuy.enabled = false;

        _seq?.Kill();
        _seq = DOTween.Sequence();
        
        elementsDescription.ForEach(data =>
        {
            if(data.localScale != Vector3.one)
               _seq.Append(data.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack));
        });

        if(objectReceived.transform.localScale != Vector3.one)
            _seq.Append(objectReceived.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack));

        if(objectPrice.transform.localScale != Vector3.zero)
            _seq.Join(objectPrice.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.OutBack));
    }

    public void SetAvailabled()
    {
        buttonBuy.enabled = true;

        _seq?.Kill();
        _seq = DOTween.Sequence();

        elementsDescription.ForEach(data =>
        {
            if(data.localScale != Vector3.one)
                _seq.Append(data.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack));
        });

        if(objectReceived.transform.localScale != Vector3.zero)
            _seq.Append(objectReceived.transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.OutBack));

        if(objectPrice.transform.localScale != Vector3.one)
            _seq.Join(objectPrice.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack));
    }

    public void SetLocked()
    {
        buttonBuy.enabled = false;

        Clear();
    }

    #region Output

    public event Action<int, int> OnBuy;

    private void Buy()
    {
        OnBuy?.Invoke(idLevel, price);
    }

    #endregion
}
