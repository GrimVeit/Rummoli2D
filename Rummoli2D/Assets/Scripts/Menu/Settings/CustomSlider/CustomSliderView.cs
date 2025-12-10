using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEngine.Rendering.DebugUI;

public class CustomSliderView : View, IIdentify
{
    public string GetID() => id;

    [SerializeField] private string id;
    [SerializeField] private CustomSliderPanel sliderPanel;
    [SerializeField] private SliderBlock[] sliderBlocks;
    [SerializeField] private Transform[] transformsPoints;
    [SerializeField] private Transform handler;
    [SerializeField] private float tweenDuration = 0.2f;
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private TextMeshProUGUI volumeNumberText;

    private int activeBlockIndex = -1;
    private Tween handlerTween;
    private Tween valueTween;
    public float currentPercent = 0;

    public void Initialize()
    {
        sliderPanel.OnUpdatePercent += UpdatePercent;
        sliderPanel.OnUp += Up;
    }

    public void Dispose()
    {
        sliderPanel.OnUpdatePercent -= UpdatePercent;
        sliderPanel.OnUp -= Up;
    }

    #region Input

    public void SetValue(float value)
    {
        int closestIndex = FindClosestBlock(value * 100);
        activeBlockIndex = closestIndex;
        volumeNumberText.text = closestIndex.ToString();
        currentPercent = sliderBlocks[activeBlockIndex].percentValue;
        handler.transform.localPosition = new Vector3(transformsPoints[activeBlockIndex].localPosition.x, handler.transform.localPosition.y, handler.transform.localPosition.z);
        UpdateVisual();
    }

    #endregion

    private void UpdatePercent(PointerEventData eventData, bool isClick)
    {
        float percent = GetPercent(eventData);

        Debug.Log(percent);

        int closestIndex = FindClosestBlock(percent);

        volumeNumberText.text = closestIndex.ToString();

        if (closestIndex != activeBlockIndex)
        {
            activeBlockIndex = closestIndex;
            UpdateVisual();

            if (isClick)
                AnimateValue(sliderBlocks[activeBlockIndex].percentValue);
            else
                SetValueInstant(sliderBlocks[activeBlockIndex].percentValue);

        }
    }

    private void AnimateValue(float targetPercent)
    {
        valueTween?.Kill();
        handlerTween?.Kill();

        if (currentPercent == targetPercent) return;
        //OnChange?.Invoke();

        handlerTween = handler.transform.DOLocalMoveX(transformsPoints[activeBlockIndex].localPosition.x, tweenDuration);

        valueTween = DOTween.To(() => currentPercent, x => currentPercent = x, targetPercent, tweenDuration).OnUpdate(() =>
        {
            float value = currentPercent / 100f;
            OnChangeValue?.Invoke(value);
        });
    }

    private void Up()
    {
        OnChange?.Invoke();
    }

    private void SetValueInstant(float targetPercent)
    {
        valueTween?.Kill();
        handlerTween?.Kill();

        if (currentPercent == targetPercent) return;

        currentPercent = targetPercent;
        //OnChange?.Invoke();

        handler.transform.localPosition = new Vector3(transformsPoints[activeBlockIndex].localPosition.x, handler.transform.localPosition.y, handler.transform.localPosition.z);

        float value = currentPercent / 100f;
        OnChangeValue?.Invoke(value);
        if (currentPercent == targetPercent) return;
    }

    private float GetPercent(PointerEventData eventData)
    {

        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out Vector2 localPoint))
            return 0;

        float halfWidth = rectTransform.rect.width / 2f;
        return Mathf.Clamp01((localPoint.x + halfWidth) / rectTransform.rect.width) * 100f;
    }

    private int FindClosestBlock(float percent)
    {
        int closestIndex = 0;
        float minDiff = Mathf.Abs(percent - sliderBlocks[0].percentValue);

        for (int i = 0; i < sliderBlocks.Length; i++)
        {
            float diff = Mathf.Abs(percent - sliderBlocks[i].percentValue);

            if (diff < minDiff)
            {
                minDiff = diff;
                closestIndex = i;
            }
        }

        return closestIndex;
    }

    private void UpdateVisual()
    {
        for (int i = 0; i < sliderBlocks.Length; i++)
        {
            sliderBlocks[i].imageBlock.color = i <= activeBlockIndex ? sliderBlocks[i].ColorActive : sliderBlocks[i].colorInactive;
        }
    }

    #region Output

    public event Action<float> OnChangeValue;
    public event Action OnChange;

    #endregion
}
