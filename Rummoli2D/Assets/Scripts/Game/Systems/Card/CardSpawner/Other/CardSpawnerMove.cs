using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CardSpawnerMove : MonoBehaviour
{
    private int _playerIndex;
    private ICard _card;

    public void SetData(int playerIndex, ICard card)
    {
        _playerIndex = playerIndex;
        _card = card;
    }

    public void MoveTo(Transform from, Transform to, float duration)
    {
        transform.localPosition = from.localPosition;

        transform.DOLocalMove(to.localPosition, duration).OnComplete(() => OnEndMove?.Invoke(_playerIndex, _card, this));
    }

    #region Output

    public event Action<int, ICard, CardSpawnerMove> OnEndMove;

    #endregion
}
