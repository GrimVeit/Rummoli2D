using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthView : View
{
    [SerializeField] private List<Image> heartImages;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    [SerializeField] private List<Image> shieldImages;
    [SerializeField] private Sprite fullShield;
    [SerializeField] private Sprite emptyShield;

    public void UpdateHealth(int current, int max)
    {
        for (int i = 0; i < heartImages.Count; i++)
        {
            if (i < max)
                heartImages[i].gameObject.SetActive(true);
            else
                heartImages[i].gameObject.SetActive(false);

            heartImages[i].sprite = i < current ? fullHeart : emptyHeart;
        }
    }

    public void UpdateShield(int current, int max)
    {
        for (int i = 0; i < shieldImages.Count; i++)
        {
            if (i < max)
                shieldImages[i].gameObject.SetActive(true);
            else
                shieldImages[i].gameObject.SetActive(false);

            shieldImages[i].sprite = i < current ? fullShield : emptyShield;
        }
    }
}
