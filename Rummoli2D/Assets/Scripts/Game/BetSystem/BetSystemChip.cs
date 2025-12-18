using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BetSystemChip : MonoBehaviour
{
    private int _playerIndex;
    private int _sectorIndex;

    public void SetData(int playerIndex, int sectorIndex)
    {
        _playerIndex = playerIndex;
        _sectorIndex = sectorIndex;
    }

    public void MoveTo(Transform from, Transform to, float duration)
    {
        transform.localPosition = from.localPosition;

        transform.DOLocalMove(to.localPosition, duration).OnComplete(() => OnEndMove?.Invoke(_playerIndex, _sectorIndex, this));
    }

    #region Output

    public event Action<int, int, BetSystemChip> OnEndMove;

    #endregion
}
