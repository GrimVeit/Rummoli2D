using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerBotCardVisualView : View, IIdentify
{
    public string GetID() => id;

    [SerializeField] private string id;

    [Header("Links")]
    [SerializeField] private PlayerBotCardVisual cardPrefab;
    [SerializeField] private Transform cardsParent;
    [SerializeField] private TMP_Text counterText;

    [Header("Layout Settings")]
    [SerializeField] private float handWidth = 645f;
    [SerializeField] private float smallGap = 3f;
    [SerializeField] private float cardWidth = 150f;
    [SerializeField] private float maxOverlap = 60f;
    [SerializeField] private int noOverlapCards = 5;
    [SerializeField] private int maxVisibleCards = 8;

    [Header("Layout Options")]
    [SerializeField] private bool isRightToLeft = true; // true = карты идут справа налево

    private readonly List<PlayerBotCardVisual> cards = new();
    private int scrollIndex = 0; // не используется для скролла, но нужен для логики Add/Remove

    // =======================
    // PUBLIC API
    // =======================

    public void AddCard(ICard card)
    {
        PlayerBotCardVisual cardVisual = Instantiate(cardPrefab, cardsParent);
        cardVisual.SetData(card);

        // Спавн карты в позиции (0,0,0)
        cardVisual.transform.localPosition = Vector3.zero;

        cards.Add(cardVisual);

        // Автоскролл при добавлении
        if (cards.Count > maxVisibleCards)
        {
            scrollIndex = cards.Count - maxVisibleCards;
        }

        ClampScroll();
        UpdateHand();
    }

    public void RemoveCard(ICard card)
    {
        PlayerBotCardVisual cardVisual = cards.Find(c => c.Card == card);
        if (cardVisual == null) return;

        int removedIndex = cards.IndexOf(cardVisual);
        cards.RemoveAt(removedIndex);
        Destroy(cardVisual.gameObject);

        if (scrollIndex > removedIndex) scrollIndex--;

        ClampScroll();
        UpdateHand();
    }

    public void DeleteCards()
    {
        foreach (var card in cards)
        {
            Destroy(card.gameObject);
        }

        cards.Clear();
        scrollIndex = 0;
        ClampScroll();
        UpdateHand();
    }

    // =======================
    // SCROLL LOGIC
    // =======================

    private void ClampScroll()
    {
        scrollIndex = Mathf.Clamp(scrollIndex, 0, Mathf.Max(0, cards.Count - maxVisibleCards));
    }

    private float CalculateSpacing(int visibleCount)
    {
        if (visibleCount <= noOverlapCards)
        {
            return cardWidth + smallGap;
        }

        // фиксируем общую ширину руки = handWidth
        return (handWidth - cardWidth) / (visibleCount - 1);
    }

    // =======================
    // HAND UPDATE
    // =======================

    private void UpdateHand()
    {
        int visibleCount = Mathf.Min(cards.Count, maxVisibleCards);
        float spacing = CalculateSpacing(visibleCount);

        // Скрываем все карты, которые вне видимой области
        for (int i = 0; i < cards.Count; i++)
        {
            if (i < scrollIndex || i >= scrollIndex + visibleCount)
                cards[i].gameObject.SetActive(false);
        }

        // Показываем и анимируем видимые карты
        for (int i = 0; i < visibleCount; i++)
        {
            int index = scrollIndex + i;
            if (index >= cards.Count) break;

            var card = cards[index];

            // Вычисляем target X в зависимости от направления
            float targetX = isRightToLeft ? i * spacing : -i * spacing;
            float targetZ = -i * 0.01f;

            if (!card.gameObject.activeSelf)
            {
                card.gameObject.SetActive(true);
                // Карта спавнится в 0 и затем DOTween анимирует в target
                card.transform.localPosition = Vector3.zero;
            }

            card.transform.DOKill();
            card.transform.DOLocalMove(new Vector3(targetX, 0f, targetZ), 0.3f).SetEase(Ease.OutCubic);
        }

        UpdateCounterText();
    }

    private void UpdateCounterText()
    {
        if (counterText == null)
            return;

        if (cards.Count <= maxVisibleCards)
        {
            counterText.gameObject.SetActive(false);
        }
        else
        {
            counterText.gameObject.SetActive(true);
            counterText.text = $"×{cards.Count}";
        }
    }
}
