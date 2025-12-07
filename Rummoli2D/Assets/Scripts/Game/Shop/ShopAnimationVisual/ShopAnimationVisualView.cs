using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class ShopAnimationVisualView : View
{
    [SerializeField] private List<ShopAnimation> shopAnimations = new();

    public void SetLevelShopChanged(ShopGroup group, int levelId)
    {
        var shopAnimation = GetShopAnimation(group);

        if(shopAnimation == null)
        {
            Debug.LogError("Not found ShopAnimation with ShopGroup - " + group);
            return;
        }

        Debug.Log("Test");

        shopAnimation.Animate(levelId);
    }

    private ShopAnimation GetShopAnimation(ShopGroup shopGroup)
    {
        return shopAnimations.FirstOrDefault(data => data.ShopGroup == shopGroup);
    }
}

[System.Serializable]
public class ShopAnimation
{
    public ShopGroup ShopGroup => shopGroup;

    [SerializeField] private ShopGroup shopGroup;

    [SerializeField] private ShopAnimationVisual_LineProgress lineProgress;
    [SerializeField] private ShopAnimationVisual_LevelProgress levelProgress;

    public void Animate(int levelId)
    {
        Debug.Log($"Test {levelId} - LineProgress - {lineProgress != null}, LevelProgress - {levelProgress != null}");
        Debug.Log($"LineProgress type = {lineProgress.GetType()}");
        Debug.Log($"LevelProgress type = {levelProgress.GetType()}");

        lineProgress.Animate(levelId);
        levelProgress.Animate(levelId);
    }
}

[System.Serializable]
public class ShopAnimationVisual_LineProgress
{
    [SerializeField] private RectTransform lineTransform;
    [SerializeField] private List<int> widths = new();
    [SerializeField] private float duration;

    public void Animate(int levelId)
    {
        Debug.Log($"widths null: {widths == null}, count: {widths?.Count}, levelId: {levelId}");
        Debug.Log($"Can access index: {levelId < (widths?.Count ?? 0)}");

        float width = widths[levelId];

        lineTransform.DOSizeDelta(new Vector2(width, lineTransform.sizeDelta.y), duration).SetEase(Ease.OutBack);

        Debug.Log("Test");
    }
}

[System.Serializable]
public class ShopAnimationVisual_LevelProgress
{
    [SerializeField] private Transform groupTransform;
    [SerializeField] private List<Transform> transformPositions = new();
    [SerializeField] private float duration;

    public void Animate(int levelId)
    {
        Debug.Log("Test");

        var transformPosition = transformPositions[levelId];

        groupTransform.DOLocalMove(transformPosition.localPosition, duration).SetEase(Ease.OutBack);

        Debug.Log("Test");
    }

}
