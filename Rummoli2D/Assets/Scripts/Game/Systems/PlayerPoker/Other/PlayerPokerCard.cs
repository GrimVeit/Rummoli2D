using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class PlayerPokerCard : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Image image;
    public float flipDuration = 0.5f;  // Время одного этапа разворота
    public float moveDistance = 30f;   // Насколько карта поднимается
    public float moveDuration = 0.5f;

    private Vector3 originalPosition;
    private Sprite _spriteFace;

    public void SetData(Sprite spriteCover, Sprite spriteFace)
    {
        originalPosition = rectTransform.localPosition;

        _spriteFace = spriteFace;
        image.sprite = spriteCover;
    }

    public void Show()
    {
        transform.DOScale(1, 0.2f).SetEase(Ease.OutBack);
    }

    public void FlipCard()
    {
        Sequence seq = DOTween.Sequence();

        seq.Append(rectTransform.DOLocalMoveY(originalPosition.y + moveDistance, moveDuration)
            .SetEase(Ease.OutQuad));
        seq.Join(rectTransform.DOLocalRotate(new Vector3(0, 90, 0), flipDuration, RotateMode.LocalAxisAdd)
            .SetEase(Ease.InOutQuad));

        // Когда карта “исчезает” меняем спрайт
        seq.AppendCallback(() => {
            image.sprite = _spriteFace;
        });

        // Возврат поворота и опускание
        seq.Append(rectTransform.DOLocalRotate(new Vector3(0, 90, 0), flipDuration, RotateMode.LocalAxisAdd)
            .SetEase(Ease.InOutQuad));
        seq.Join(rectTransform.DOLocalMoveY(originalPosition.y, moveDuration)
            .SetEase(Ease.InQuad));
    }
}
