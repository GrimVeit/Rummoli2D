using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardSpawnerMove : MonoBehaviour
{
    private RectTransform rectTransform;
    private int _playerIndex;
    private ICard _card;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void SetData(int playerIndex, ICard card)
    {
        _playerIndex = playerIndex;
        _card = card;
    }

    public void MoveTo(
        RectTransform from,
        RectTransform to,
        RectTransform localParent,
        Canvas canvas,
        float duration
    )
    {
        // если есть стартовая точка — ставим туда
        if (from != null)
        {
            rectTransform.localPosition = ConvertToLocal(from, localParent, canvas);
        }

        Vector2 targetLocalPos = ConvertToLocal(to, localParent, canvas);

        rectTransform
            .DOLocalMove(targetLocalPos, duration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                OnEndMove?.Invoke(_playerIndex, _card, this);
            });
    }

    private Vector2 ConvertToLocal(
        RectTransform target,
        RectTransform localParent,
        Canvas canvas
    )
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            localParent,
            RectTransformUtility.WorldToScreenPoint(
                canvas.worldCamera,
                target.position
            ),
            canvas.worldCamera,
            out Vector2 localPoint
        );

        return localPoint;
    }

    #region Output

    public event Action<int, ICard, CardSpawnerMove> OnEndMove;

    #endregion
}
