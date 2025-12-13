using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundVisualView : View
{
    [SerializeField] private Image image;
    [SerializeField] private float timeScale;
    [SerializeField] private BackgroundSprites backgroundSprites;

    private Tween tweenScale;

    public void SetBackground(int id, bool animate)
    {
        tweenScale?.Kill();

        var background = backgroundSprites.GetSprite(id);

        if(background == null)
        {
            Debug.LogWarning("Not found background sprite with id - " + id);
            return;
        }

        image.sprite = background;

        if (animate)
        {
            image.transform.localScale = Vector3.zero;

            tweenScale = image.transform.DOScale(1, timeScale);
        }
        else
        {
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
