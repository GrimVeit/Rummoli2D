using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerPokerView : View
{
    [Header("UI Prefab")]
    [SerializeField] private PlayerPoker prefabCard;
    [SerializeField] private RectTransform parent;

    [Header("Spawn Points")]
    [SerializeField] private RectTransform[] points2;
    [SerializeField] private RectTransform[] points3;
    [SerializeField] private RectTransform[] points4;
    [SerializeField] private RectTransform[] points5;

    [Header("Table")]
    [SerializeField] private Transform transformTable;
    private Tween tweenScale;

    private RectTransform[] currentPoints;
    private int currentIndex;
    private readonly List<PlayerPoker> _spawnedCards = new();


    private IEnumerator _timerShowAll;
    private IEnumerator _timerClearAll;

    public void SetCountPlayer(int count)
    {
        ClearAll(); // очищаем старые объекты

        currentPoints = count switch
        {
            2 => points2,
            3 => points3,
            4 => points4,
            5 => points5,
            _ => null
        };

        if (currentPoints == null)
        {
            Debug.LogError("Неверное количество: " + count);
            return;
        }

        currentIndex = 0;
    }

    public void SetPlayer(PlayerPokerData data)
    {
        if (currentPoints == null)
        {
            Debug.LogError("Сначала вызови SetCount()");
            return;
        }

        if (currentIndex >= currentPoints.Length)
        {
            Debug.LogWarning("Все точки уже использованы");
            return;
        }

        RectTransform point = currentPoints[currentIndex];

        PlayerPoker card = Instantiate(prefabCard, parent);
        card.Initialize();
        card.SetData(data);
        RectTransform rect = card.gameObject.GetComponent<RectTransform>();
        rect.anchoredPosition = point.anchoredPosition;
        rect.localScale = Vector3.zero;
        rect.localRotation = Quaternion.identity;
        rect.DOScale(1, 0.3f);

        _spawnedCards.Add(card);
        currentIndex++;
    }

    public void ShowWin(int playerId)
    {
        var player = GetPlayerPoker(playerId);

        if(player == null)
        {
            Debug.LogWarning("Not found PlayerPoker with PlayerId - " + playerId);
            return;
        }

        player.Select();
    }

    public void ShowAll()
    {
        if(_timerShowAll != null) Coroutines.Stop(_timerShowAll);

        _timerShowAll = TimerShowAll();
        Coroutines.Start(_timerShowAll);
    }

    public void ClearAll()
    {
        if (_timerClearAll != null) Coroutines.Stop(_timerClearAll);

        _timerClearAll = TimerClearAll();
        Coroutines.Start(_timerClearAll);
    }

    private IEnumerator TimerClearAll()
    {
        foreach (PlayerPoker card in _spawnedCards)
        {
            if (card != null)
            {
                card.Dispose();
                card.Hide();

                yield return new WaitForSeconds(0.2f);
            }
        }

        yield return new WaitForSeconds(0.2f);

        foreach (PlayerPoker card in _spawnedCards)
        {
            if (card != null)
            {
                card.Destroy();
            }
        }

        _spawnedCards.Clear();
        currentIndex = 0;
    }

    #region Table

    public void ShowTable()
    {
        tweenScale?.Kill();

        tweenScale = transformTable.DOScale(1, 0.3f).SetEase(Ease.OutBack);
    }

    public void HideTable()
    {
        tweenScale?.Kill();

        tweenScale = transformTable.DOScale(0.6f, 0.3f).SetEase(Ease.OutBack);
    }

    #endregion

    private IEnumerator TimerShowAll()
    {
        for (int i = 0; i < _spawnedCards.Count; i++)
        {
            _spawnedCards[i].Show();

            yield return new WaitForSeconds(0.4f);
        }

        yield return new WaitForSeconds(1);

        OnShowAll?.Invoke();
    }

    private PlayerPoker GetPlayerPoker(int playerId)
    {
        return _spawnedCards.Find(data => data.Data.PlayerId == playerId);
    }

    #region Output

    public event Action OnShowAll;

    #endregion
}
