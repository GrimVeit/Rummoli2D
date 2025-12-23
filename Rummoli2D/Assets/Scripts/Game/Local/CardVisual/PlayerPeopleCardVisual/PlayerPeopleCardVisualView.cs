using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPeopleCardVisualView : View
{
    [Header("Links")]
    [SerializeField] private PlayerPeopleCardVisual cardPrefab;
    [SerializeField] private Transform cardsParent;
    [SerializeField] private Button buttonLeft;
    [SerializeField] private Button buttonRight;
    [SerializeField] private UIEffect effectButtonLeft;
    [SerializeField] private UIEffect effectButtonRight;

    [Header("Layout Settings")]
    [SerializeField] private float handWidth = 645f;
    [SerializeField] private float smallGap = 3f;
    [SerializeField] private float cardWidth = 150f;
    [SerializeField] private float maxOverlap = 60f;
    [SerializeField] private int noOverlapCards = 5;
    [SerializeField] private int maxVisibleCards = 8;

    [Header("Pos")]
    [SerializeField] private Vector3 vectorPosLeftActive;
    [SerializeField] private Vector3 vectorPosLeftDeactive;

    private readonly List<PlayerPeopleCardVisual> cards = new List<PlayerPeopleCardVisual>();
    private int scrollIndex = 0;

    private Tween tweenCards;

    public void Initialize()
    {
        buttonLeft.onClick.AddListener(ScrollLeft);
        buttonRight.onClick.AddListener(ScrollRight);

        effectButtonLeft.Initialize();
        effectButtonRight.Initialize();
    }

    public void Dispose()
    {
        buttonLeft.onClick.RemoveListener(ScrollLeft);
        buttonRight.onClick.RemoveListener(ScrollRight);

        effectButtonLeft.Dispose();
        effectButtonRight.Dispose();
    }

    public void AddCard(ICard card)
    {
        PlayerPeopleCardVisual cardVisual = Instantiate(cardPrefab, cardsParent);
        cardVisual.SetData(card);

        cards.Add(cardVisual);

        // Если карт стало больше максимального количества видимых
        if (cards.Count > maxVisibleCards)
        {
            // Сдвигаем scrollIndex вправо, чтобы показать новую карту
            scrollIndex = cards.Count - maxVisibleCards;
        }

        ClampScroll();   // на всякий случай
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
            // мало карт — подряд с небольшим gap
            return cardWidth + smallGap;
        }

        // больше noOverlapCards — фиксируем общую ширину руки = handWidth
        // spacing = расстояние между картами
        return (handWidth - cardWidth) / (visibleCount - 1);
    }

    private void UpdateHand()
    {
        int visibleCount = Mathf.Min(cards.Count, maxVisibleCards);
        float spacing = CalculateSpacing(visibleCount);

        // ЛЕВО-ЯКОРЬ: первая карта ровно на родителе
        float startX = 0f;

        // Скрываем все карты
        for (int i = 0; i < cards.Count; i++)
        {
            if (i < scrollIndex || i >= scrollIndex + visibleCount)
            {
                // Карты вне видимой области
                cards[i].gameObject.SetActive(false);
            }
        }

        // Показываем и анимируем нужное окно
        for (int i = 0; i < visibleCount; i++)
        {
            int index = scrollIndex + i;
            if (index >= cards.Count) break;

            var card = cards[index];
            float targetX = startX + i * spacing;
            float targetZ = -i * 0.01f;

            // Если карта ещё не активна, сразу ставим её в целевую позицию
            if (!card.gameObject.activeSelf)
            {
                card.gameObject.SetActive(true);
                card.transform.localPosition = new Vector3(targetX, 0f, targetZ);
            }

            // Отменяем предыдущие анимации, чтобы не было конфликта
            card.transform.DOKill();

            // Плавно двигаем карту в целевую позицию
            card.transform.DOLocalMoveX(targetX, 0.3f).SetEase(Ease.OutCubic);
        }

        UpdateButtons();
    }

    private void UpdateButtons()
    {
        bool needScroll = cards.Count > maxVisibleCards;

        if(needScroll && scrollIndex > 0)
        {
            tweenCards?.Kill();

            buttonLeft.enabled = true;
            buttonLeft.gameObject.SetActive(true);
            effectButtonLeft.ActivateEffect();

            tweenCards = cardsParent.DOLocalMoveX(vectorPosLeftActive.x, 0.2f).SetEase(Ease.OutBack);
        }
        else
        {
            tweenCards?.Kill();

            buttonLeft.enabled = false;
            effectButtonLeft.DeactivateEffect(() => buttonLeft.gameObject.SetActive(false));

            tweenCards = cardsParent.DOLocalMoveX(vectorPosLeftDeactive.x, 0.2f).SetEase(Ease.OutBack);
        }


        if(needScroll && scrollIndex < cards.Count - maxVisibleCards)
        {
            buttonRight.gameObject.SetActive(true);
            effectButtonRight.ActivateEffect();
        }
        else
        {
            effectButtonRight.DeactivateEffect(() => buttonRight.gameObject.SetActive(false));
        }
    }
}
