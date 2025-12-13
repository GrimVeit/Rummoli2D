using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundVisualView : View
{
    [SerializeField] private Transform parentBackground;
    [SerializeField] private Image image;
    [SerializeField] private float timeScale;
    [SerializeField] private BackgroundSprites backgroundSprites;

    private Tween tweenScale;

    public void SetBackground(int id, bool animate)
    {
        var background = backgroundSprites.GetSprite(id);

        if(background == null)
        {
            Debug.LogWarning("Not found background sprite with id - " + id);
            return;
        }

        if (animate)
        {
            var tempGO = Instantiate(image.gameObject, parentBackground);
            tempGO.transform.SetAsLastSibling();
            tempGO.transform.localPosition = Vector3.zero;
            var tempImage = tempGO.GetComponent<Image>();
            tempImage.sprite = background;
            tempImage.color = new Color(1, 1, 1, 0); // изначально прозрачный

            // Анимация прозрачности
            tempImage.DOFade(1, timeScale).OnComplete(() =>
            {
                image.sprite = background; // обновляем основной Image
                Destroy(tempGO); // удаляем временный
            });
        }
        else
        {
            image.sprite = background;
            image.transform.localScale = Vector3.one;
        }
    }
}

[System.Serializable]
public class BackgroundSprites
{
    [SerializeField] private List<BackgroundSprite> backgroundSprites = new List<BackgroundSprite>();

    public Sprite GetSprite(int id)
    {
        return backgroundSprites.Find(bs => bs.Id == id).Sprite;
    }
}

[System.Serializable]
public class BackgroundSprite
{
    [SerializeField] private int id;
    [SerializeField] private Sprite sprite;

    public int Id => id;
    public Sprite Sprite => sprite;
}
