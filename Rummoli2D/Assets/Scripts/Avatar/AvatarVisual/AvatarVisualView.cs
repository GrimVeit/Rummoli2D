using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AvatarVisualView : View, IIdentify
{

    public string GetID() => id;

    [SerializeField] private string id;
    [SerializeField] private AvatarVisual avatarVisualPrefab;
    [SerializeField] private Transform transformSpawn;
    [SerializeField] private List<Sprite> spritesAvatar;

    [SerializeField] private List<Transform> slots;     // 6 точек для отображения
    [SerializeField] private float moveDuration = 0.5f;
    public float scaleDuration = 0.3f;

    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;

    private List<AvatarVisual> avatarVisuals = new();

    private int firstVisibleIndex = 0; // индекс первого видимого элемента

    public void Initialize()
    {
        leftButton.onClick.AddListener(OnClickLeft);
        rightButton.onClick.AddListener(OnClickRight);

        for (int i = 0; i < spritesAvatar.Count; i++)
        {
            var visual = Instantiate(avatarVisualPrefab, transformSpawn);
            visual.OnChooseAvatar += ChooseAvatar;
            visual.SetData(i, spritesAvatar[i]);
            visual.Initialize();

            avatarVisuals.Add(visual);
        }

        for (int i = 0; i < avatarVisuals.Count; i++)
        {
            if (i < slots.Count)
            {
                avatarVisuals[i].transform.localPosition = slots[i].localPosition;
                avatarVisuals[i].transform.localScale = Vector3.one;
            }
            else
            {
                avatarVisuals[i].transform.localScale = Vector3.zero;
            }
        }

        UpdateButtons();
    }

    public void Dispose()
    {
        leftButton.onClick.RemoveListener(OnClickLeft);
        rightButton.onClick.RemoveListener(OnClickRight);

        for (int i = 0; i < avatarVisuals.Count; i++)
        {
            avatarVisuals[i].OnChooseAvatar -= ChooseAvatar;
            avatarVisuals[i].Dispose();
        }

        avatarVisuals.Clear();
    }

    public void MoveRight()
    {
        // Анимация для первого слота: исчезает
        avatarVisuals[firstVisibleIndex].transform.DOScale(Vector3.zero, scaleDuration);

        // Сдвигаем все видимые элементы на одну точку влево
        for (int i = 1; i < slots.Count; i++)
        {
            int elementIndex = firstVisibleIndex + i;
            avatarVisuals[elementIndex].transform.DOLocalMove(slots[i - 1].localPosition, moveDuration);
        }

        // Появление нового элемента справа
        int newElementIndex = firstVisibleIndex + slots.Count;
        avatarVisuals[newElementIndex].transform.localPosition = slots[slots.Count - 1].localPosition;
        avatarVisuals[newElementIndex].transform.localScale = Vector3.zero;
        avatarVisuals[newElementIndex].transform.DOScale(Vector3.one, scaleDuration);

        firstVisibleIndex++;
        UpdateButtons();
    }

    public void MoveLeft()
    {
        if (firstVisibleIndex <= 0) return;

        firstVisibleIndex--;

        // Анимация для нового элемента слева
        avatarVisuals[firstVisibleIndex].transform.localScale = Vector3.zero;
        avatarVisuals[firstVisibleIndex].transform.localPosition = slots[0].localPosition;
        avatarVisuals[firstVisibleIndex].transform.DOScale(Vector3.one, scaleDuration);

        // Сдвигаем все видимые элементы на одну точку вправо
        for (int i = 0; i < slots.Count - 1; i++)
        {
            int elementIndex = firstVisibleIndex + i + 1;
            avatarVisuals[elementIndex].transform.DOLocalMove(slots[i + 1].localPosition, moveDuration);
        }

        // Крайний правый элемент исчезает
        int rightmostIndex = firstVisibleIndex + slots.Count;
        if (rightmostIndex < avatarVisuals.Count)
            avatarVisuals[rightmostIndex].transform.DOScale(Vector3.zero, scaleDuration);

        UpdateButtons();
    }

    public void Select(int avatar)
    {
        avatarVisuals.FirstOrDefault(data => data.Id == avatar).Select();
    }

    public void Deselect(int avatar)
    {
        avatarVisuals.FirstOrDefault(data => data.Id == avatar).Deselect();
    }

    void UpdateButtons()
    {
        // Левая кнопка
        if (firstVisibleIndex > 0)
        {
            leftButton.gameObject.SetActive(true);
            leftButton.transform.DOScale(1, 0.3f);
        }
        else
        {
            leftButton.transform.DOScale(Vector3.zero, 0.3f).OnComplete(() => leftButton.gameObject.SetActive(false));
        }

        // Правая кнопка
        if (firstVisibleIndex + slots.Count < avatarVisuals.Count)
        {
            rightButton.gameObject.SetActive(true);
            rightButton.transform.DOScale(1, 0.3f);
        }
        else
        {
            rightButton.transform.DOScale(Vector3.zero, 0.3f).OnComplete(() => rightButton.gameObject.SetActive(false));
        }
    }

    #region Output

    public event Action OnClickToLeft;
    public event Action OnClickToRight;

    public event Action<int> OnChooseAvatar;

    private void OnClickLeft()
    {
        if (firstVisibleIndex <= 0) return;

        OnClickToLeft?.Invoke();
    }

    private void OnClickRight()
    {
        if (firstVisibleIndex + slots.Count >= avatarVisuals.Count) return;

        OnClickToRight?.Invoke();
    }

    private void ChooseAvatar(int avatar)
    {
        OnChooseAvatar?.Invoke(avatar);
    }

    #endregion
}
