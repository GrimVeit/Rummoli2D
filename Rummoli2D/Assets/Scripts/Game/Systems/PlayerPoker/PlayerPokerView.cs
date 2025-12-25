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

    private RectTransform[] currentPoints;
    private int currentIndex;
    private readonly List<PlayerPoker> spawnedCards = new();

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
        card.SetData(data);
        RectTransform rect = card.gameObject.GetComponent<RectTransform>();
        rect.anchoredPosition = point.anchoredPosition;
        rect.localScale = Vector3.zero;
        rect.localRotation = Quaternion.identity;
        rect.DOScale(1, 0.3f);

        spawnedCards.Add(card);
        currentIndex++;
    }

    public void ClearAll()
    {
        foreach (PlayerPoker obj in spawnedCards)
        {
            if (obj != null)
                Destroy(obj.gameObject);
        }

        spawnedCards.Clear();
        currentIndex = 0;
    }
}
