using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class HintVisual : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private TextMeshProUGUI _text;

    private Tween _currentTween;
    private const float ANIM_DURATION = 0.3f;
    private bool _isDeleting = false;

    private void OnDestroy()
    {
        _currentTween?.Kill();
        _currentTween = null;
    }

    public void SetText(string text)
    {
        _text.text = text;
    }

    public void SetSize(Vector2 size)
    {
        _rectTransform.sizeDelta = size;
    }

    public void SetFontSize(int size)
    {
        _text.fontSize = size;
    }

    public void Show(Action onComplete = null)
    {
        if (_isDeleting) return;

        _currentTween?.Kill();

        _currentTween = _rectTransform
            .DOScale(Vector3.one, ANIM_DURATION)
            .SetEase(Ease.OutBack)
            .OnComplete(() =>
            {
                onComplete?.Invoke();
            });
    }

    public void Hide(Action onComplete = null)
    {
        if(_isDeleting) return;

        _currentTween?.Kill();

        _currentTween = _rectTransform
            .DOScale(Vector3.zero, ANIM_DURATION)
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                onComplete?.Invoke();
            });
    }

    public void Delete()
    {
        _isDeleting = true;

        _currentTween?.Kill();

        _currentTween = _rectTransform
            .DOScale(Vector3.zero, ANIM_DURATION)
            .SetEase(Ease.InBack)
            .OnComplete(() =>
            {
                Destroy(gameObject);
            });
    }
}
