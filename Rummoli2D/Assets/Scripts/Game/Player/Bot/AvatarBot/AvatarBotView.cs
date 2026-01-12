using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvatarBotView : View, IIdentify
{
    public string GetID() => id;

    [SerializeField] private string id;
    [SerializeField] private List<Image> images = new();
    [SerializeField] private List<Sprite> avatars;

    public void SetAvatar(int avatar)
    {
        for (int i = 0; i < images.Count; i++)
        {
            images[i].sprite = avatars[avatar];
        }
    }
}
