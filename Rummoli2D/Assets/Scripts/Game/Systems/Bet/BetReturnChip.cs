using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BetReturnChip : MonoBehaviour
{
    private int _playerIndex;
    private int _score;

    public void SetData(int playerIndex, int score)
    {
        _playerIndex = playerIndex;
        _score = score;
    }

    public void MoveTo(Transform from, Transform to, float duration)
    {
        transform.localPosition = from.localPosition;

        transform.DOLocalMove(to.localPosition, duration).OnComplete(() => OnEndMove?.Invoke(_playerIndex, _score, this));
    }

    #region Output

    public event Action<int, int, BetReturnChip> OnEndMove;

    #endregion
}
