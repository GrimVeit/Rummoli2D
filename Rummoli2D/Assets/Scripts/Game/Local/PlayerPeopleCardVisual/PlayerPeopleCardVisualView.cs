using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPeopleCardVisualView : View
{
    [Header("Links")]
    [SerializeField] private PlayerPeopleCardVisual cardPrefab;
    [SerializeField] private Transform cardsParent;
    [SerializeField] private Button buttonLeft;
    [SerializeField] private Button buttonRight;

    [Header("Layout Settings")]
    [SerializeField] private float handWidth = 645f;
    [SerializeField] private float smallGap = 3f;
    [SerializeField] private float cardWidth = 150f;
    [SerializeField] private float maxOverlap = 60f;
    [SerializeField] private int noOverlapCards = 5;
    [SerializeField] private int maxVisibleCards = 8;

    private readonly List<PlayerPeopleCardVisual> cards = new List<PlayerPeopleCardVisual>();
    private int scrollIndex = 0;

    public void Initialize()
    {
        buttonLeft.onClick.AddListener(ScrollLeft);
        buttonRight.onClick.AddListener(ScrollRight);
    }

    public void Dispose()
    {
        buttonLeft.onClick.RemoveListener(ScrollLeft);
        buttonRight.onClick.RemoveListener(ScrollRight);
    }

    public void AddCard(ICard card)
    {
        PlayerPeopleCardVisual cardVisual = Instantiate(cardPrefab, cardsParent);
        cardVisual.SetData(card);

        cards.Add(cardVisual);

        ClampScroll();
        UpdateHand();
    }

    public void RemoveCard(ICard card)
    {
        PlayerPeopleCardVisual cardVisual = cards.Find(c => c.Card == card);
        if (cardVisual == null)
            return;

        int removedIndex = cards.IndexOf(cardVisual);

        cards.RemoveAt(removedIndex);
        Destroy(cardVisual.gameObject);

        if (scrollIndex > removedIndex)
            scrollIndex--;

        ClampScroll();
        UpdateHand();
    }

    private void ScrollLeft()
    {
        scrollIndex--;
        ClampScroll();
        UpdateHand();
    }

    private void ScrollRight()
    {
        scrollIndex++;
        ClampScroll();
        UpdateHand();
    }

    private void ClampScroll()
    {
        scrollIndex = Mathf.Clamp(
            scrollIndex,
            0,
            Mathf.Max(0, cards.Count - maxVisibleCards)
        );
    }

    private float CalculateSpacing(int visibleCount)
    {
        if (visibleCount <= noOverlapCards)
        {
            // мало карт Ч подр€д с небольшим gap
            return cardWidth + smallGap;
        }

        // больше noOverlapCards Ч фиксируем общую ширину руки = handWidth
        // spacing = рассто€ние между картами
        return (handWidth - cardWidth) / (visibleCount - 1);
    }

    private void UpdateHand()
    {
        int visibleCount = Mathf.Min(cards.Count, maxVisibleCards);
        float spacing = CalculateSpacing(visibleCount);

        // Ћ≈¬ќ-я ќ–№: перва€ карта ровно на родителе
        float startX = 0f;

        // —крываем все карты
        for (int i = 0; i < cards.Count; i++)
            cards[i].gameObject.SetActive(false);

        // ѕоказываем нужное окно
        for (int i = 0; i < visibleCount; i++)
        {
            int index = scrollIndex + i;
            if (index >= cards.Count)
                break;

            var card = cards[index];
            card.gameObject.SetActive(true);

            float x = startX + i * spacing;
            card.transform.localPosition = new Vector3(x, 0f, -i * 0.01f);
        }

        UpdateButtons();
    }

    private void UpdateButtons()
    {
        bool needScroll = cards.Count > maxVisibleCards;

        buttonLeft.gameObject.SetActive(needScroll && scrollIndex > 0);
        buttonRight.gameObject.SetActive(needScroll && scrollIndex < cards.Count - maxVisibleCards);
    }
}
