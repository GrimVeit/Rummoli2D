using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BetAddChip : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;

    private int _playerIndex;
    private int _sectorIndex;

    public void SetData(int playerIndex, int sectorIndex)
    {
        _playerIndex = playerIndex;
        _sectorIndex = sectorIndex;
    }

    public void MoveTo(RectTransform from, RectTransform to, RectTransform localParent, Canvas canvas, float duration)
    {
        if (from != null)
        {
            rectTransform.localPosition = ConvertToLocal(from, localParent, canvas);
        }

        rectTransform.DOLocalMove(to.localPosition, duration).OnComplete(() => OnEndMove?.Invoke(_playerIndex, _sectorIndex, this));
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

    public event Action<int, int, BetAddChip> OnEndMove;

    #endregion
}
