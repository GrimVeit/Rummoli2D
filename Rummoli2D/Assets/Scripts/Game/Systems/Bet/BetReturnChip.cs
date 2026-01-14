using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BetReturnChip : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;

    private int _playerIndex;
    private int _score;

    public void SetData(int playerIndex, int score)
    {
        _playerIndex = playerIndex;
        _score = score;
    }

    public void MoveTo(RectTransform from, RectTransform to, RectTransform localParent, Canvas canvas, float duration)
    {
        rectTransform.localPosition = from.localPosition;

        Vector2 targetLocalPos = ConvertToLocal(to, localParent, canvas);

        rectTransform.DOLocalMove(targetLocalPos, duration).OnComplete(() => OnEndMove?.Invoke(_playerIndex, _score, this));
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

    public event Action<int, int, BetReturnChip> OnEndMove;

    #endregion
}
