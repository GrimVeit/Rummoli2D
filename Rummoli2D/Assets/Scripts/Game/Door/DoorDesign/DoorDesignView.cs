using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DoorDesignView : View
{
    [SerializeField] private RawImage imageDoor_1;
    [SerializeField] private RawImage imageDoor_2;
    [SerializeField] private RawImage imageDoor_3;

    [SerializeField] private DoorDesigns doorDesigns;
    [SerializeField] private List<DoorVisualBonus> doorVisualBonuses = new();

    public void SetDesigns(List<DoorType> doors)
    {
        imageDoor_1.texture = doorDesigns.GetSpriteByDoorType(doors[0]).texture;
        imageDoor_2.texture = doorDesigns.GetSpriteByDoorType(doors[1]).texture;
        imageDoor_3.texture = doorDesigns.GetSpriteByDoorType(doors[2]).texture;
    }

    public void ShowOracle(int doorId)
    {
        var doorVisualBonus = GetDoorVisualBonus(doorId);

        if (doorVisualBonus == null)
        {
            Debug.LogWarning("Not found DoorVisualBonus with DoorId - " + doorId);
            return;
        }

        doorVisualBonus.ShowOracle();
    }

    public void ShowEvilTongue(int doorId)
    {
        var doorVisualBonus = GetDoorVisualBonus(doorId);

        if (doorVisualBonus == null)
        {
            Debug.LogWarning("Not found DoorVisualBonus with DoorId - " + doorId);
            return;
        }

        doorVisualBonus.ShowEvilTongue();
    }

    private DoorVisualBonus GetDoorVisualBonus(int doorId)
    {
        return doorVisualBonuses.Find(x => x.DoorId == doorId);
    }
}

[System.Serializable]
public class DoorDesigns
{
    [SerializeField] private List<DoorDesign> doorDesigns = new();

    public Sprite GetSpriteByDoorType(DoorType type)
    {
        var sprite = doorDesigns.FirstOrDefault(data => data.Type == type).Sprite;

        if(sprite == null)
        {
            Debug.LogWarning($"Not found DoorSprite by DoorType - {type}");
            return null;
        }

        return sprite;
    }
}

[System.Serializable]
public class DoorDesign
{
    [SerializeField] private DoorType type;
    [SerializeField] private Sprite sprite;

    public DoorType Type => type;
    public Sprite Sprite => sprite;
}



[System.Serializable]
public class DoorVisualBonus
{
    public int DoorId => doorId;

    [SerializeField] private int doorId;

    [SerializeField] private Image oracleImage;
    [SerializeField] private Sprite[] oracleSprites;
    [SerializeField] private Image evilTongueImage;
    [SerializeField] private Sprite[] evilTongueSprites;

    [SerializeField] private float timeShowHide = 0.2f;
    [SerializeField] private float timeVisible = 0.8f;
    [SerializeField] private float switchInterval = 0.5f;

    private Sequence sequenceBonus;

    public void ShowOracle()
    {
        sequenceBonus?.Kill();

        oracleImage.transform.localScale = Vector3.zero;
        evilTongueImage.transform.localScale = Vector3.zero;

        sequenceBonus = DOTween.Sequence();
        sequenceBonus.Append(oracleImage.transform.DOScale(1, timeShowHide));

        float totalTime = 0f;
        int spriteIndex = 0;

        // анимация смены спрайтов во время видимости
        while (totalTime < timeVisible)
        {
            sequenceBonus.AppendCallback(() =>
            {
                oracleImage.sprite = oracleSprites[spriteIndex % oracleSprites.Length];
                spriteIndex++;
            });
            sequenceBonus.AppendInterval(switchInterval);
            totalTime += switchInterval;
        }

        sequenceBonus.Append(oracleImage.transform.DOScale(0, timeShowHide));
    }

    public void ShowEvilTongue()
    {
        sequenceBonus?.Kill();

        oracleImage.transform.localScale = Vector3.zero;
        evilTongueImage.transform.localScale = Vector3.zero;

        sequenceBonus = DOTween.Sequence();
        sequenceBonus.Append(evilTongueImage.transform.DOScale(1, timeShowHide));

        float totalTime = 0f;
        int spriteIndex = 0;

        while (totalTime < timeVisible)
        {
            sequenceBonus.AppendCallback(() =>
            {
                evilTongueImage.sprite = evilTongueSprites[spriteIndex % evilTongueSprites.Length];
                spriteIndex++;
            });
            sequenceBonus.AppendInterval(switchInterval);
            totalTime += switchInterval;
        }

        sequenceBonus.Append(evilTongueImage.transform.DOScale(0, timeShowHide));
    }
}
